using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class ChooseCharacter : NetworkBehaviour
{
    public GameObject[] characters;
    public GameObject characterSelect;

    private int charIndex = 0;

    private void Start()
    {
        characterSelect = characters[charIndex];
        characterSelect.SetActive(true);
    }

    public void PickHero()
    {
        LobbyManager.s_Singleton.lobbyPanel.gameObject.SetActive(true);
        //LobbyManager.s_Singleton.chosenCharacter = charIndex;
        foreach (LobbyPlayer lp in FindObjectsOfType<LobbyPlayer>())
        {
            NetworkIdentity ni = lp.GetComponent<NetworkIdentity>();

            if(ni.isLocalPlayer)
            {
                lp.SetCharIndex(charIndex);
                break;
            }
        }
        gameObject.SetActive(false);
    }

    public void OnLeftButtonClick()
    {
        charIndex--;
        if (charIndex < 0)
            charIndex = characters.Length - 1;
        characterSelect.SetActive(false);
        characterSelect = characters[charIndex];
        characterSelect.SetActive(true);
    }

    public void OnRightButtonClick()
    {
        charIndex++;
        if (charIndex >= characters.Length)
            charIndex = 0;

        characterSelect.SetActive(false);
        characterSelect = characters[charIndex];
        characterSelect.SetActive(true);
    }
}
