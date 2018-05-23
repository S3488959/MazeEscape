using Prototype.NetworkLobby;
using UnityEngine;
using UnityEngine.Networking;

public class StartSectorBehaviour : NetworkBehaviour
{
    public GameObject startPoint;
    private GameVariables gameVar;
    private GameObject[] walls;

    private void Awake()
    {
        int playerSize = LobbyManager.s_Singleton.getPlayerCount();
        for(int i= 0; i < playerSize; i++)
        {
            Vector3 spawnPos = Vector3.zero;
            switch (i)
            {
                case (1):
                    spawnPos += Vector3.right * 9;
                    break;
                case (2):
                    spawnPos += Vector3.left * 9;
                    break;
                case (3):
                    spawnPos += Vector3.forward * 9;
                    break;
                case (4):
                    spawnPos += Vector3.back * 9;
                    break;
                case (5):
                    spawnPos += Vector3.right * 9 + Vector3.forward * 9;
                    break;
                case (6):
                    spawnPos += Vector3.right * 9 + Vector3.back * 9;
                    break;
                case (7):
                    spawnPos += Vector3.left * 9 + Vector3.forward * 9;
                    break;
                case (8):
                    spawnPos += Vector3.left * 9 + Vector3.back * 9;
                    break;
                default:
                    break;
            }
            GameObject start = Instantiate(startPoint, spawnPos, Quaternion.identity);
            NetworkServer.Spawn(start);
        }
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
