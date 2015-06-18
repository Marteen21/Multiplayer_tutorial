using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Zinko_NetworkManager : NetworkManager {
    [SerializeField]
    private GameObject[] Zinko_PrefabOptions;
    private int Zinko_Selected = 0;

    public void NextPlayerPrefab(){
        if (Zinko_PrefabOptions.Length == 0) {
            Debug.LogError("No prefab options available");
            return;
        }
        if (Zinko_Selected + 1 == Zinko_PrefabOptions.Length) {
            Zinko_Selected = 0;
        }
        else {
            Zinko_Selected++;
        }
        this.playerPrefab = Zinko_PrefabOptions[Zinko_Selected];
        return;
    }
}
