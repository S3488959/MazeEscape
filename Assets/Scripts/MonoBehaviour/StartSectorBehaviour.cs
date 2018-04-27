using UnityEngine;
using UnityEngine.Networking;

public class StartSectorBehaviour :  NetworkBehaviour
{
    private GameVariables gameVar;
    private GameObject[] walls;

	// Use this for initialization
	void Start () {
        walls = GameObject.FindGameObjectsWithTag("StartWall");
        gameVar = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameVariables>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameVar.isCoolDownDone())
        {
            foreach(GameObject wall in walls)
            {
                Destroy(wall);
            }
        }
	}
}
