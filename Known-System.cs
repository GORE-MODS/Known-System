using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KnownSystem : MonoBehaviourPunCallbacks
{
    public Dictionary<string, string> knownPlayers = new Dictionary<string, string>
    {
        { "C9027C8A184048B", "Deez" },
        { "B09EE5516007EFCB", "Gore" },
    };
    private Dictionary<int, GameObject> activeTags = new Dictionary<int, GameObject>();
    void Update()
    {
        if (!PhotonNetwork.InRoom || !VRRigCache.isInitialized) return;
        foreach (VRRig rig in VRRigCache.ActiveRigs)
        {
            if (rig.isOfflineVRRig || rig.isMyPlayer) continue;
            NetPlayer netPlayer = rig.Creator;
            if (netPlayer != null && knownPlayers.ContainsKey(netPlayer.UserId))
            {
                CreateOrUpdateTag(rig, netPlayer);
            }
        }
    }
    void CreateOrUpdateTag(VRRig rig, NetPlayer player)
    {
        int actorNumber = player.ActorNumber;
        string playerName = knownPlayers[player.UserId];
        if (!activeTags.ContainsKey(actorNumber))
        {
            GameObject tag = new GameObject("KnownTag_" + actorNumber);
            TextMeshPro textMesh = tag.AddComponent<TextMeshPro>();
            textMesh.text = playerName;
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
                activeTags[actorNumber].GetComponent<TextMeshPro>().text = playerName;
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
