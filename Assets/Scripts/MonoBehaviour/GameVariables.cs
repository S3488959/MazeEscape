using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameVariables : NetworkBehaviour{

    //How many seconds in a minute.
    private const float SEC_IN_MINUTE = 60;
    //How many minutes the game goes for
    private const float minutes = 100f;
    //How many minutes to players wait to begin playing. (Exclusive to game length, still shown in timer)
    private const float cooldownMinutes = 0.1f;


    float MaxTime = (minutes+cooldownMinutes) * SEC_IN_MINUTE;
    [SyncVar]
    public float TimeRemaining = (minutes + cooldownMinutes) * SEC_IN_MINUTE;

	// Use this for initialization
	void Start () {
        TimeRemaining = MaxTime;
    }
	
	// Update is called once per frame
	void Update () {
        if (isServer && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Prototype"))
        {
            TimeRemaining -= Time.deltaTime;
            if (TimeRemaining < 0)
                TimeRemaining = 0;
        }
	}

    public string GetTimeRemainingAsString() {

        int minutesNow = Mathf.FloorToInt(TimeRemaining/SEC_IN_MINUTE);
        int secondsNow = Mathf.Abs(Mathf.FloorToInt(TimeRemaining%SEC_IN_MINUTE));

        string toReturn = minutesNow + ":";
        if (secondsNow < 10)
            toReturn += "0";
        toReturn += secondsNow;
        return toReturn;
    }


    public string GetTimeTakenAsString() {

        float time = MaxTime - TimeRemaining;

        int minutesNow = Mathf.FloorToInt(time / SEC_IN_MINUTE);
        int secondsNow = Mathf.Abs(Mathf.FloorToInt(time % SEC_IN_MINUTE));

        string toReturn = minutesNow + ":";
        if (secondsNow < 10)
            toReturn += "0";
        toReturn += secondsNow;
        return toReturn;
    }

    public bool isCoolDownDone()
    {
        if (TimeRemaining <= minutes * SEC_IN_MINUTE)
            return true;
        else
            return false;
    }

    public float GetTimeRatio() {
        return TimeRemaining / MaxTime;
    }


}
