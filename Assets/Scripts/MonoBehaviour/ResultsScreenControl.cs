using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Prototype.NetworkLobby;
using UnityEngine.UI;

public class ResultsScreenControl : NetworkBehaviour {

    //The attached GamManagerBehaviour
    LobbyManager lm;

    public GameObject canvas;

    public Text scoreText;
    public Text timeText;

    // Use this for initialization
    void Start () {

        //Get the GameManagaer Behaviour
        lm = GameObject.FindGameObjectWithTag("LobbyManager").GetComponent<LobbyManager>();



    }
	
	// Update is called once per frame
	void Update () {

        canvas.SetActive(true);



    }

    public void ReturnToMenu() {
        SceneManager.LoadScene("MainMenu");

        if (isServer)
            lm.DisconnectClient();
        else
            lm.DisconnectHost();
    }
}
