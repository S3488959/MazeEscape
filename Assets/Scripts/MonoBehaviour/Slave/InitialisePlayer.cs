using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InitialisePlayer : NetworkBehaviour
{
    [SyncVar]
    public string playerName = "player";
    [SyncVar]
    public Color playerColor = Color.white;
    [SyncVar]
    public int playerChar = 0;

    public GameObject[] characters;
    // Use this for initialization
    void Start ()
    {
        if (isServer)
        {
            GameSettings.PLAYSTATE = GameSettings.PLAY_STATE.MASTER;
            Destroy(this);
        }
        else
        {
            SpawnPlayer();
        }
    }
	

    [Command]
    public void CmdSpawnPlayer(NetworkInstanceId netId)
    {
        GameObject newPlayer = NetworkServer.FindLocalObject(netId);
        NetworkConnection conn = newPlayer.GetComponent<NetworkIdentity>().connectionToClient;
        NetworkServer.ReplacePlayerForConnection(conn, newPlayer, 0);
    }

    public void SpawnPlayer()
    {
        GameObject newPlayer = Instantiate<GameObject>(characters[playerChar]);
        PlayerBehaviour player = newPlayer.GetComponent<PlayerBehaviour>();
        player.playerName = playerName;
        player.playerColor = playerColor;
        NetworkInstanceId netId = newPlayer.GetComponent<NetworkIdentity>().netId;
        CmdSpawnPlayer(netId);
        Destroy(this);
    }
}
