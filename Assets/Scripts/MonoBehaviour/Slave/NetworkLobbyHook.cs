using Prototype.NetworkLobby;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook
{

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        PlayerBehaviour localPlayer = gamePlayer.GetComponent<PlayerBehaviour>();

        localPlayer.playerName = lobby.playerName;
        localPlayer.playerColor = lobby.playerColor;
    }
}
