using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivatablePlayerWin : Activatable {

    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Activate() {

        //GameManagerBehaviour manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerBehaviour>();
        SceneManager.LoadScene("ResultsScreen");
        //manager.PlayerWins();   
        
    }
}
