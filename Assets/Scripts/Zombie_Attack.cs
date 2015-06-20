using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Zombie_Attack : NetworkBehaviour {

    private float attackRate = 3;
    private float nextAttack;
    private int damage = 10;
    private float minDistance = 2;
    private float currentDistance;
    private Transform myTransform;
    private Zombie_Target targetScript;

    [SerializeField]
    private Material zombieGreen;
    [SerializeField]
    private Material zombieRed;
	// Use this for initialization
	void Start () {
        myTransform = transform;
        targetScript = GetComponent<Zombie_Target>();
        if (isServer) {
            StartCoroutine(Attack());
        }
	}
    IEnumerator Attack() {
        while (true) {
            yield return new WaitForSeconds(0.2f);
            if (ReadyToAttack()) {
                nextAttack = Time.time + attackRate;
                targetScript.targetTransform.GetComponent<Player_Health>().DeductHealth(damage);
                StartCoroutine(ChangeZombieMat());  //Host player
                RpcChangeZombieApperance();
            }
        }
    }
    bool ReadyToAttack() {
        if (targetScript.targetTransform == null) {
            return false;
        }
        else if (Vector3.Distance(targetScript.targetTransform.position, myTransform.position) > minDistance) {
            return false;
        }
        else if (Time.time < nextAttack) {
            return false;
        }
        else {
            return true;
        }
    }
    IEnumerator ChangeZombieMat() {
        GetComponent<Renderer>().material = zombieRed;
        yield return new WaitForSeconds(attackRate / 3);
        GetComponent<Renderer>().material = zombieGreen;
    }
    [ClientRpc]
    void RpcChangeZombieApperance() {
        StartCoroutine(ChangeZombieMat());
    }
	// Update is called once per frame
	void Update () {
	
	}
}
