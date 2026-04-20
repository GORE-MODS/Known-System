<div align="center">
  <picture>
    <img alt="KnownSystem" width="700" src="https://github.com/AnomalyMenu/Known-System/blob/075dee735d9f387f9ff3b2b371f38cdd99a7a6a9/ks1.jpg" 
         style="border-radius:20px; border:4px solid #FF2B6C; box-shadow: 0 0 50px #FF2B6C, inset 0 0 30px rgba(255,43,108,0.4);">
  </picture>
  
**A lightweight "Known" tag system for Gorilla Tag mod menus.**
</div>

Displays **"Known"** above a player's head if their Player ID is in your list.
---

## Features

- Shows "Known" floating above matching players
- Super lightweight (minimal performance impact)
- Open source under GPL 3.0

---

## Installation

1. Copy/Download the latest script
2. Put the code into your mod menu's **Classes** or **Menu** folder
3. And then make a game object that does not destroy on load and add KnownSystem as a component.

```csharp
   GameObject knownsysobj = new GameObject("Known-System");
   DontDestroyOnLoad(knownsysobj);
   knownsysobj.AddComponent<Known-System>();
```

---
# [Discord](https://discord.gg/ka9rjxHUPj)
# [Wiki](https://anomalymenu.42web.io/wikis/knownsystemwiki)
