using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_ID : NetworkBehaviour {
    [SyncVar]
    private string playerUniqueIdentity;
    private NetworkInstanceId playerNetID;
    private Transform myTransform;

    public override void OnStartLocalPlayer() {

        GetNetIdentity();
        SetIdentity();

    }
	void Awake () {
        myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        if (myTransform.name == "" || myTransform.name == "Player(Clone)") {
            SetIdentity();
        }
	}
    string MakeUniqueIdentity() {
        string uniqueName = "Player_" + playerNetID.ToString();
        return uniqueName;
    }
    void SetIdentity() {
        if (!isLocalPlayer) {
            myTransform.name = playerUniqueIdentity;
        }
        else {
            myTransform.name = MakeUniqueIdentity();
        }
    }
    [Client]
    void GetNetIdentity(){
        playerNetID = GetComponent<NetworkIdentity>().netId;
        CmdTellServerMyIdentity(MakeUniqueIdentity());
    }
    [Command]
    void CmdTellServerMyIdentity(string name) {
        playerUniqueIdentity = name;
    }
}
