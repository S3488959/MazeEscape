using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour {

    GameNetworkEvents networkEvents;
    GameVariables gameVars;

    // Use this for initialization
    void Start ()
    {
        networkEvents = GetComponent<GameNetworkEvents>();
        gameVars = GetComponent<GameVariables>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameVars.GetTimeRatio() <= 0 && !(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("ResultsScreen")) && !(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu")))
        {
            networkEvents.CmdMasterWins();
        }
    }
}
