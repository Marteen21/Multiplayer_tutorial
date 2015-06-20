using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManager_ZombiSpawner : NetworkBehaviour {
    [SerializeField]
    GameObject zombiePrefab;
    [SerializeField]
    GameObject zombieSpawn;
    private int counter;
    private int numberOfZombies = 10;

    public override void OnStartServer() {
        for (int i = 0; i < numberOfZombies; i++) {
            SpawnZombie();
        }
    }
    void SpawnZombie() {
        counter++;
        GameObject go = GameObject.Instantiate(zombiePrefab, zombieSpawn.transform.position, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(go);
        go.GetComponent<Zombie_ID>().zombieID = "Zombie_" + counter.ToString();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
