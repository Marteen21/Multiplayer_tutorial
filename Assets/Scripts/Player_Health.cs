using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Player_Health : NetworkBehaviour {

    [SyncVar (hook = "OnHealthChanged")]
    private int health = 100;
    private Text healthText;
    private bool shouldDie = false;
    public bool isDead = false;

    public delegate void DieDelegete();
    public event DieDelegete EventDie;

    public delegate void RespawnDelegete();
    public event RespawnDelegete EventRespawn;
	// Use this for initialization
    public override void OnStartLocalPlayer() {
        healthText = GameObject.Find("HpText").GetComponent<Text>();
        SetHealthText();
    }
    void SetHealthText() {
        if (isLocalPlayer) {
            healthText.text = "Health: " + health.ToString();
        }
    }
	// Update is called once per frame
	void Update () {
        CheckCondition();
	}
    void CheckCondition() {
        if (health <= 0 && !shouldDie && !isDead) {
            shouldDie = true;
        }
        if (health <= 0 && shouldDie) {
            if (EventDie != null) {
                EventDie();
            }
            shouldDie = false;
        }
        if (isDead && health > 0) {
            if (EventRespawn != null) {
                EventRespawn();
            }
            isDead = false;
        }
    }
    public void DeductHealth(int dmg) {
        health = health - dmg;
    }
    public void ResetHealth() {
        health = 100;
    }
    void OnHealthChanged(int hlth) {
        health = hlth;
        SetHealthText();
    }
    
}
