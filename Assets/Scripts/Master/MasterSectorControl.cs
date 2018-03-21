using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSectorControl : MonoBehaviour {
    List<SectorBehaviour> allSectors = new List<SectorBehaviour>();

    SectorBehaviour sectorSelected;

	// Use this for initialization
	void Start () {
        GameSettings.GameStart();
	}

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            GameSettings.PLAYSTATE = GameSettings.PLAY_STATE.MASTER;
            GameSettings.GameStart();
            transform.position = new Vector3(0, 40, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            GameSettings.PLAYSTATE = GameSettings.PLAY_STATE.PLAYER;
            GameSettings.GameStart();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            GameSettings.PLAYSTATE = GameSettings.PLAY_STATE.SPECTATOR;
            GameSettings.GameStart();
            transform.position = new Vector3(0, 40, 0);
        }


        if (GameSettings.PLAYSTATE == GameSettings.PLAY_STATE.MASTER) {

            if (sectorSelected != null) {
                sectorSelected.transform.GetChild(0).gameObject.SetActive(false);
                sectorSelected = null;
            }

            Ray mouseRay = new Ray();
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit)) {
                if (hit.collider.gameObject.tag == "Sector")
                    sectorSelected = hit.collider.gameObject.GetComponent<SectorBehaviour>();
            }
            if (sectorSelected != null) {
                sectorSelected.transform.GetChild(0).gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.J)) {
                    sectorSelected.Move(Vector3.left);

                }
                else if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.K)) {
                    sectorSelected.Move(Vector3.back);

                }
                else if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.L)) {
                    sectorSelected.Move(Vector3.right);

                }
                else if (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.I)) {
                    sectorSelected.Move(Vector3.forward);

                }
                if (Input.GetKeyDown(KeyCode.Joystick1Button4)) {
                    sectorSelected.Rotate(-90);
                    //Destroy(sectorSelected.gameObject);

                }
                if (Input.GetKeyDown(KeyCode.Joystick1Button5)) {
                    sectorSelected.Rotate(90);

                }
            }
        }
    }
       
    public void AddSector(SectorBehaviour newSector) {
        allSectors.Add(newSector);
    }

    public SectorBehaviour[] GetSectors() {
        return allSectors.ToArray();
    }

}
