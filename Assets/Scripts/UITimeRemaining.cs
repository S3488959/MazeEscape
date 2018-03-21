using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeRemaining : MonoBehaviour {

    //The objects we require that are children
    private Text timeText;
    private RectTransform imageRT;

    private Vector2 baseSizeDelta;

    //The object that is currently holding the time.
    private GameVariables gameVar;

	// Use this for initialization
	void Start () {

        imageRT = transform.GetChild(0).GetComponent<RectTransform>();
        baseSizeDelta = Vector2.Scale(Vector3.one,imageRT.sizeDelta);
        timeText = transform.GetChild(1).GetComponent<Text>();
        gameVar = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameVariables>();
		
	}
	
	// Update is called once per frame
	void Update () {
        imageRT.localScale = new Vector3(gameVar.GetTimeRatio(),1,1);
        timeText.text = "Time Remaining: " + gameVar.GetTimeAsString();
	}
}
