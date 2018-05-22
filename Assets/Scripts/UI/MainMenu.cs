using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public GameObject menu;
	// Use this for initialization
	void Start ()
    {
        menu.SetActive(true);
        NetworkLobbyManager.singleton.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnStartButtonClick()
    {
        NetworkLobbyManager.singleton.gameObject.SetActive(true);
        NetworkLobbyManager.singleton.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        menu.SetActive(false);
    }
}
