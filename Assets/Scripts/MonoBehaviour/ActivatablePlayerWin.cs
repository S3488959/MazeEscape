using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivatablePlayerWin : Activatable
{
    public override void Activate()
    {
        //GameManagerBehaviour manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerBehaviour>();
        SceneManager.LoadScene("ResultsScreen");
        //manager.PlayerWins();   
    }
}
