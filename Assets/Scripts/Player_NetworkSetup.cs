using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class Player_NetworkSetup : NetworkBehaviour {

    [SerializeField]
    Camera FPSCharacterCAM;
    [SerializeField]
    AudioListener audioListener;
    // Use this for initialization
    void Start() {

        if (isLocalPlayer) {

            GameObject.Find("SceneCamera").SetActive(false);
            GetComponent<CharacterController>().enabled = true;
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            FPSCharacterCAM.enabled = true;
            audioListener.enabled = true;

        }
    }
}
