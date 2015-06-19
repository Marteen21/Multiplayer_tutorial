using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

[NetworkSettings (channel=0, sendInterval=0.033f)]
public class Player_SyncPosition : NetworkBehaviour {
    [SyncVar (hook="SyncPostionValues")]
    private Vector3 syncPos;

    [SerializeField]
    Transform myTransform;
    [SerializeField]
    private bool useHistoricalLerping = false;

    //Lerp Rates
    private float lerpRate;
    private float normalLerpRate = 16;
    private float fastLerpRate = 27;

    //Ordinary Lerping
    private Vector3 lastPos;
    private float threshhold = 0.5f;
    //Historical Lerping
    private List<Vector3> syncPosList = new List<Vector3>();
    private float closeEnough = 0.12f;




    void Start() {
        lerpRate = normalLerpRate;
    }
    void Update() {
        LerpPosition();
    }
    void FixedUpdate() {
        TransmitPosition();
    }
    void LerpPosition() {
        if (!isLocalPlayer) {
            if (useHistoricalLerping) {
                HistoricalLerping();
            }
            else {
                OrdinaryLerping();
            }
        }
    }

    void OrdinaryLerping() {
        myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);

    }
    void HistoricalLerping() {
        if (syncPosList.Count > 0) {
            Debug.Log(syncPosList.Count.ToString());
            myTransform.position = Vector3.Lerp(myTransform.position, syncPosList[0], Time.deltaTime * lerpRate);
            if (Vector3.Distance(myTransform.position, syncPosList[0]) < closeEnough) {
                syncPosList.RemoveAt(0);
            }
            if (syncPosList.Count > 10) {
                lerpRate = fastLerpRate;
            }
            else {
                lerpRate = normalLerpRate;
            }
        }
    }

    [Client]
    void SyncPostionValues(Vector3 latestPos) {
        syncPos = latestPos;
        syncPosList.Add(syncPos);
    }
    [Command]
    void CmdProvidePositionToServer(Vector3 pos) {
        syncPos = pos;
    }

    [ClientCallback]
    void TransmitPosition() {
        if (isLocalPlayer && Vector3.Distance(lastPos, myTransform.position) > threshhold) {
            CmdProvidePositionToServer(myTransform.position);
            lastPos = myTransform.position;
        }
    }
}
