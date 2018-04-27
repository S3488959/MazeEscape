using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StartSectorBehaviour :  NetworkBehaviour{

    private float coolDown = GameVariables.cooldownMinutes * GameVariables.SEC_IN_MINUTE;

    private float currentTime;
    private GameObject[] walls;

	// Use this for initialization
	void Start () {
        walls = GameObject.FindGameObjectsWithTag("StartWall");
        currentTime = coolDown;
	}
	
	// Update is called once per frame
	void Update () {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            foreach(GameObject wall in walls)
            {
                //NetworkServer.Destroy(wall);
                Destroy(wall);
            }
        }
	}
}
