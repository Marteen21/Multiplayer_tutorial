using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpawnManager_ZombiSpawner : NetworkBehaviour {
    [SerializeField]
    GameObject zombiePrefab;
    private GameObject[] zombieSpawns;
    private int counter = 0;
    private int numberOfZombies = 20;
    private int maxNumberOfZombies = 70;
    private float waveRate = 10;
    private bool isSpawnActivated = true;

    public override void OnStartServer() {
        zombieSpawns = GameObject.FindGameObjectsWithTag("ZombieSpawn");
        StartCoroutine(ZombieSpawner());
    }
    void SpawnZombie(Vector3 spawnPos) {
        counter++;
        GameObject go = GameObject.Instantiate(zombiePrefab, spawnPos, Quaternion.identity) as GameObject;
        go.GetComponent<Zombie_ID>().zombieID = "Zombie_" + counter.ToString();
        NetworkServer.Spawn(go);
    }
    IEnumerator ZombieSpawner() {
        while (true) {
            yield return new WaitForSeconds(waveRate);
            GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
            if (zombies.Length < maxNumberOfZombies) {
                CommenceSpawn();
            }
        }
    }
    void CommenceSpawn() {
        if (isSpawnActivated) {

            for (int i = 0; i < numberOfZombies; i++) {
                SpawnZombie(zombieSpawns[Random.Range(0,zombieSpawns.Length)].transform.position);
            }
        }
    }
}
