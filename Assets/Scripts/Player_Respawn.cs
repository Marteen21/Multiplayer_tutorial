using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Player_Respawn : NetworkBehaviour {

    private Player_Health healthScript;
    private Image crosshairImage;
    private GameObject respawnButton;
	// Use this for initialization
	void Start () {
        healthScript = GetComponent<Player_Health>();
        healthScript.EventRespawn += EnablePlayer;
        crosshairImage = GameObject.Find("CrosshairImage").GetComponent<Image>();
        SetRespawnButton();
	}
    void EnablePlayer() {
        GetComponent<CharacterController>().enabled = true;
        GetComponent<Player_Shoot>().enabled = true;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer ren in renderers) {
            ren.enabled = true;
        }
        if (isLocalPlayer) {
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            crosshairImage.enabled = true;
            respawnButton.SetActive(false);
        }
    }
    void SetRespawnButton() {
        if (isLocalPlayer) {
            respawnButton = GameObject.Find("GameManager").GetComponent<GameManager_References>().respawnButton;
            respawnButton.GetComponent<Button>().onClick.AddListener(CommenceRespawn);
            respawnButton.SetActive(false);
        }
    }
    void CommenceRespawn(){
        CmdRespawnOnServer();
    }
    [Command]
    void CmdRespawnOnServer()
    {
        healthScript.ResetHealth();
    }
    void OnDisable() {
        healthScript.EventRespawn -= EnablePlayer;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
