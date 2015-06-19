using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Player_SyncRotation : NetworkBehaviour {
    [SyncVar(hook = "OnPlayerRotSynced")]
    private float syncPlayerRotation;
    [SyncVar(hook = "OnCamRotSynced")]
    private float syncCamRotation;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform camTransform;
    [SerializeField]
    private float lerpRate = 20;

    private float lastPlayerRot;
    private float lastCamRot;
    private float threshhold = 1;

    private List<float> syncPlayerRotationList = new List<float>();
    private List<float> syncCamRotationList = new List<float>();
    private float closeEnough = 0.4f;
    [SerializeField]
    private bool useHistoricalInterpolations = false;
    void Update() {
        LerpRotations();
    }
    void FixedUpdate() {
        TrasmitRotations();
    }
    void LerpRotations() {
        if (!isLocalPlayer) {
            if (useHistoricalInterpolations) {
                HistoricalLerping();
            }
            else {
                OrdinaryLerping();
            }

        }
    }
    void OrdinaryLerping() {
        LerpPlayerRotation(syncPlayerRotation);
        LerpCamRotation(syncCamRotation);
    }
    void HistoricalLerping() {
        if (syncPlayerRotationList.Count > 0) {
            LerpPlayerRotation(syncPlayerRotationList[0]);
            if(Mathf.Abs(playerTransform.localEulerAngles.y - syncPlayerRotationList[0]) > closeEnough){
                syncPlayerRotationList.RemoveAt(0);
            }
            Debug.Log(syncPlayerRotationList.Count.ToString() + " syncPlayerRotList Count");
        }
        if (syncCamRotationList.Count > 0) {
            LerpCamRotation(syncCamRotationList[0]);
            if (Mathf.Abs(camTransform.localEulerAngles.x - syncPlayerRotationList[0]) > closeEnough) {
                syncCamRotationList.RemoveAt(0);
            }
            Debug.Log(syncCamRotationList.Count.ToString() + " syncCamRotList Count");
        }
    }
    void LerpPlayerRotation(float rotAngle) {
        Vector3 playerNewRot = new Vector3(0, rotAngle, 0);
        playerTransform.rotation = Quaternion.Lerp(playerTransform.rotation, Quaternion.Euler(playerNewRot), lerpRate * Time.deltaTime);
    }
    void LerpCamRotation(float rotAngle) {
        Vector3 camNewRot = new Vector3(rotAngle, 0, 0);
        camTransform.localRotation = Quaternion.Lerp(camTransform.localRotation, Quaternion.Euler(camNewRot), lerpRate * Time.deltaTime);
    }
    bool CheckIfBeyondThreshold(float rot1, float rot2) {
        if (Mathf.Abs(rot1 - rot2) > threshhold) {
            return true;
        }
        else {
            return false;
        }
    }
    [Command]
    void CmdProvideRotationsToServer(float playerRot, float camRot) {
        syncPlayerRotation = playerRot;
        syncCamRotation = camRot;
    }
    [ClientCallback]
    void TrasmitRotations() {
        if (isLocalPlayer) {
            if (CheckIfBeyondThreshold(playerTransform.localEulerAngles.y, lastPlayerRot) || CheckIfBeyondThreshold(camTransform.localEulerAngles.x, lastCamRot)) {
                lastPlayerRot = playerTransform.localEulerAngles.y;
                lastCamRot = camTransform.localEulerAngles.x;
                CmdProvideRotationsToServer(lastPlayerRot, lastCamRot);
            }
        }
    }

    [ClientCallback]
    void OnPlayerRotSynced(float latestPlayerRot) {
        syncPlayerRotation = latestPlayerRot;
        syncPlayerRotationList.Add(syncPlayerRotation);
    }

    [ClientCallback]
    void OnCamRotSynced(float latestCamRot) {
        syncCamRotation = latestCamRot;
        syncCamRotationList.Add(syncCamRotation);
    }
}
