using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    public GameObject menu;
    public GameObject lobby;
	// Use this for initialization
	void Start ()
    {
        menu.SetActive(true);
        lobby.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnStartButtonClick()
    {
        menu.SetActive(false);
        lobby.SetActive(true);
    }
}
