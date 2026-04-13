using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KnownSystem : MonoBehaviourPunCallbacks
{
    public List<string> knownPlayerIds = new List<string>
    {
        "30CABA3A8C73DCBB",
        "B09EE5516007EFCB",
        "52795AAE00D486A9",
        "23D5B41B538633C8",
        "A4E06D8CDF522CDA",
        "235AB88A298B4943",
        "4605B2A0900028E1",
        "E44DB47304BA9B9A",
        "BFB4D91D17B44FDD",
        "A4AA347B576A5AAD",
        "E4C1C960B316D71",
        "A0007A5B13627673",
        "FD6CE5F599154993"
    };

    private Dictionary<int, GameObject> activeTags = new Dictionary<int, GameObject>();

    void Update()
    {
        if (!PhotonNetwork.InRoom || !VRRigCache.isInitialized) return;

        foreach (VRRig rig in VRRigCache.ActiveRigs)
        {
            if (rig.isOfflineVRRig || rig.isMyPlayer) continue;

            NetPlayer netPlayer = rig.Creator;

            if (netPlayer != null && knownPlayerIds.Contains(netPlayer.UserId))
            {
                CreateOrUpdateTag(rig, netPlayer);
            }
        }
    }

    void CreateOrUpdateTag(VRRig rig, NetPlayer player)
    {
        int actorNumber = player.ActorNumber;

        if (!activeTags.ContainsKey(actorNumber))
        {
            GameObject tag = new GameObject("KnownTag_" + actorNumber);
            TextMeshPro textMesh = tag.AddComponent<TextMeshPro>();

            textMesh.text = "KNOWN"
            textMesh.color = Color.white;

            textMesh.fontSize = 2;
            textMesh.alignment = TextAlignmentOptions.Center;

            textMesh.font = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/motdBodyText").GetComponent<TextMeshPro>().font;

            tag.transform.SetParent(rig.transform);
            tag.transform.localPosition = new Vector3(0, 0.5f, 0);

            activeTags.Add(actorNumber, tag);
        }
        else
        {
            if (activeTags[actorNumber] != null)
            {
                activeTags[actorNumber].transform.LookAt(Camera.main.transform);
                activeTags[actorNumber].transform.Rotate(0, 180, 0);

                activeTags[actorNumber].GetComponent<TextMeshPro>().text = "KNOWN"
            }
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (activeTags.ContainsKey(otherPlayer.ActorNumber))
        {
            Destroy(activeTags[otherPlayer.ActorNumber]);
            activeTags.Remove(otherPlayer.ActorNumber);
        }
    }

    public override void OnLeftRoom()
    {
        foreach (var tag in activeTags.Values)
        {
            Destroy(tag);
        }
        activeTags.Clear();
    }
}
