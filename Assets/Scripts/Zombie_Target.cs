using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Zombie_Target : NetworkBehaviour {

    public NavMeshAgent agent;
    private Transform myTransform;
    public Transform targetTransform;
    private LayerMask raycastLayer;
    private float radius = 100;


    void Start() {
        agent = GetComponent<NavMeshAgent>();
        myTransform = transform;
        raycastLayer = 1 << LayerMask.NameToLayer("Player");
        if (isServer) {
            StartCoroutine(DoCheck());
        }
    }

    void FixedUpdate() {

    }
    void SearchForTarget() {
        if (!isServer) {
            return;
        }
        if (targetTransform == null) {
            Collider[] hitColliders = Physics.OverlapSphere(myTransform.position, radius, raycastLayer);
            if (hitColliders.Length > 0) {
                int randomint = Random.Range(0, hitColliders.Length);
                targetTransform = hitColliders[randomint].transform;
            }
        }
        else if (targetTransform.GetComponent<CharacterController>().enabled == false) {
            targetTransform = null;
        }
    }
    void MoveToTarget() {
        if (targetTransform != null && isServer) {
            SetNavDestination(targetTransform);
        }
    }
    void SetNavDestination(Transform tar) {
        agent.SetDestination(tar.position);
    }
    IEnumerator DoCheck() {
        while (true) {
            SearchForTarget();
            MoveToTarget();
            yield return new WaitForSeconds(0.2f);
        }
    }
}
