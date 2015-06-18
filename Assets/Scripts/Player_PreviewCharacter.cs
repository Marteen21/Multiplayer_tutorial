using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Player_PreviewCharacter : MonoBehaviour {
    private int selected;
    private Zinko_NetworkManager myZinko_NetworkManager;
    private Text myText;
    private GameObject Model;
    // Use this for initialization
    void Start() {
        myZinko_NetworkManager = GameObject.Find("NetworkManager").GetComponent<Zinko_NetworkManager>();
        myText = GameObject.Find("Text").GetComponent<Text>();
        AddModel();
    }
    public void OnClick() {
        myZinko_NetworkManager.NextPlayerPrefab();
        myText.text = myZinko_NetworkManager.playerPrefab.name;
        AddModel();
    }
    private void AddModel() {
        if (Model != null) {
            GameObject.Destroy(Model);
        }
        Model = (GameObject)Instantiate(myZinko_NetworkManager.playerPrefab, new Vector3(3, 0, 3), Quaternion.Euler(0,200,0));
        Model.GetComponent<Player_NetworkSetup>().enabled = false;
        Model.GetComponent<Player_SyncPosition>().enabled = false;
        Model.GetComponent<Player_SyncRotation>().enabled = false;
    }
}
