using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Player_NetworkSetup : NetworkBehaviour {

    [SerializeField]
    Camera FPSCharacterCAM;
    [SerializeField]
    AudioListener audioListener;
    // Use this for initialization
    public override void OnStartLocalPlayer() {
        GameObject.Find("SceneCamera").SetActive(false);
        GetComponent<CharacterController>().enabled = true;
        GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        FPSCharacterCAM.enabled = true;
        audioListener.enabled = true;
    }
}
