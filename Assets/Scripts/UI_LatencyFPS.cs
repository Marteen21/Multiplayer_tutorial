using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UI_LatencyFPS : NetworkBehaviour {
    private NetworkClient nClient;
    private int latency;
    private float deltaTime = 0.0f;
    private float fps;
    private Text latencyText;
    private Text fpsText;
	// Use this for initialization
    public override void OnStartLocalPlayer() {
        nClient = GameObject.Find("NetworkManager").GetComponent<Zinko_NetworkManager>().client;
        latencyText = GameObject.Find("LatencyText").GetComponent<Text>();
        fpsText = GameObject.Find("FPSText").GetComponent<Text>();
    }

	// Update is called once per frame
	void Update () {
        ShowLatency();
        ShowFps();
	}
    void ShowLatency() {
        if (isLocalPlayer) {
            latency = nClient.GetRTT();
            latencyText.text = "Latency: " + latency.ToString() + " ms";
        }
    }
    void ShowFps() {
        if (isLocalPlayer) {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            fps = 1.0f / deltaTime;
            fpsText.text = "FPS: " + Mathf.RoundToInt(fps).ToString();
        }
    }
}
