using UnityEngine;
using System.Collections;

public class Player_PreviewCharacter : MonoBehaviour {
    private int selected;
    [SerializeField]
    private GameObject myNetworkManager;
    private Zinko_NetworkManager myZinko_NetworkManager;
	// Use this for initialization
	void Start () {
        myZinko_NetworkManager = myNetworkManager.GetComponent<Zinko_NetworkManager>();
	}
    public void OnClick() {
        myZinko_NetworkManager.NextPlayerPrefab();
    }
	// Update is called once per frame
	void Update () {
	
	}
}
