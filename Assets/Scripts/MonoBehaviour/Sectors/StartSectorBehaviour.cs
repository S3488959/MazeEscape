using UnityEngine;
using UnityEngine.Networking;

public class StartSectorBehaviour : NetworkBehaviour
{
    public GameObject startPoint;
    private GameVariables gameVar;
    private GameObject[] walls;

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start() {
        walls = GameObject.FindGameObjectsWithTag("StartWall");
        gameVar = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameVariables>();
    }

    // Update is called once per frame
    void Update() {
        if(isServer)
        {
            if (gameVar.isCoolDownDone())
            {
                foreach (GameObject wall in walls)
                {
                    Destroy(wall);
                    RpcDestroyWall(wall);
                }
            }
        }
        
    }

    [ClientRpc]
    public void RpcDestroyWall(GameObject wall)
    {
        GameObject.Destroy(wall);
    }
}
