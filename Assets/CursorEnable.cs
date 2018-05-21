using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorEnable : MonoBehaviour {
    public Text timeText;
    public Text scoreText;

    // Use this for initialization
    void Start () {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
	
	// Update is called once per frame
	void Update () {

        GameVariables vars = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameVariables>();
        timeText.text = vars.GetTimeTakenAsString();

        int score = Mathf.FloorToInt(vars.GetTimeRatio() * 1000.0f);
        scoreText.text = score.ToString();

    }
}
