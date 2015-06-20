using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Zombie_ID : NetworkBehaviour {

    [SyncVar]
    public string zombieID;
    private Transform myTransform;
	// Use this for initialization
	void Start () {
        myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        SetIdentity();
	
	}
    void SetIdentity(){
        if(myTransform.name == "" || myTransform.name == "Zombie(Clone)"){
            myTransform.name = zombieID;
        }
    }
}
