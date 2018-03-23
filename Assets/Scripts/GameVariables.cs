using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameVariables : NetworkBehaviour{

    //How many seconds in a minute.
    private const float SEC_IN_MINUTE = 60;
    //How many minutes the game goes for
    private const float minutes = 1f;
    //How many minutes to players wait to begin playing. (Exclusive to game length, still shown in timer)
    private const float cooldownMinutes = 0.1f;


    float MaxTime = (minutes+cooldownMinutes) * SEC_IN_MINUTE;
    [SyncVar(hook = "UpdateRatio")]
    [SerializeField]
    private float TimeRemaining = (minutes + cooldownMinutes) * SEC_IN_MINUTE;

	// Use this for initialization
	void Start () {
        TimeRemaining = MaxTime;
    }
	
	// Update is called once per frame
	void Update () {
        if (isServer)
            TimeRemaining -= Time.deltaTime;
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

    public void UpdateRatio(float _timeRemaining) {
        TimeRemaining = _timeRemaining;
    }

    public float GetTimeRatio() {
        return TimeRemaining / MaxTime;
    }


}
