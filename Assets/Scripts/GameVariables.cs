using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameVariables : NetworkBehaviour{

    private const float SEC_IN_MINUTE = 60;
    private const float minutes = 11f;

    [SyncVar]
    float MaxTime = minutes * SEC_IN_MINUTE;
    float TimeRemaining = minutes * SEC_IN_MINUTE;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        TimeRemaining -= Time.deltaTime;

        if (TimeRemaining < 0) {
            //Game Over
        }

	}

    public string GetTimeAsString() {
        int minutesNow = Mathf.FloorToInt(TimeRemaining/SEC_IN_MINUTE);
        int secondsNow = Mathf.Abs(Mathf.FloorToInt(TimeRemaining%SEC_IN_MINUTE));

        string toReturn = minutesNow + ":";
        if (secondsNow < 10)
            toReturn += "0";
        toReturn += secondsNow;
        return toReturn;
    }

    public float GetTimeRatio() {
        return TimeRemaining / MaxTime;
    }


}
