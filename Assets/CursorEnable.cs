using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorEnable : MonoBehaviour
{
    public Text timeText;
    public Text scoreText;

    private GameVariables vars;
    // Use this for initialization
    void Start () {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        vars = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameVariables>();
    }
	
	// Update is called once per frame
	void Update () {
        
        timeText.text = vars.GetTimeTakenAsString();

        int score = Mathf.FloorToInt(vars.GetTimeRatio() * 1000.0f);
        scoreText.text = score.ToString();

    }
}
