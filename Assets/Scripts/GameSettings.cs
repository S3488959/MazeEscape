using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings {

    public enum PLAY_STATE {PLAYER, MASTER, SPECTATOR}
    public static PLAY_STATE PLAYSTATE = PLAY_STATE.MASTER;

    public enum CONTROL_MODE {KEYMOUSE, GAMEPAD }
    public static CONTROL_MODE CONTROLMODE = CONTROL_MODE.GAMEPAD;

    public static void GameStart() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject master = GameObject.FindGameObjectWithTag("Master");

        if (PLAYSTATE == PLAY_STATE.MASTER || PLAYSTATE == PLAY_STATE.SPECTATOR) {
            master.transform.GetChild(0).gameObject.SetActive(true);
            player.transform.GetChild(0).gameObject.SetActive(false);
        }
        else {
            master.transform.GetChild(0).gameObject.SetActive(false);
            player.transform.GetChild(0).gameObject.SetActive(true);
        }
        

    }

	
}
