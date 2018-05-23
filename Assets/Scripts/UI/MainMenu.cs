using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    
	// Use this for initialization
	void Start ()
    {
        NetworkLobbyManager.singleton.gameObject.SetActive(false);
    }

    public void OnStartButtonClick()
    {
        NetworkLobbyManager.singleton.gameObject.SetActive(true);
        
    }
}
