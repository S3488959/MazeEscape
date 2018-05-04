using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/*  Name: GenerateMap.cs
 *  Author: Todd O'Donnell
 *  Purpose:
 *      - Generates the map at the start of each game.
 *      - Created sectors are add to the list on the attached MasterSectorControl.
 *  Data Required:
 *      - A list of all the sectors to be added to the map. UNFINISHED.
 */

[RequireComponent(typeof(MasterSectorControl))]
public class GenerateMap : NetworkBehaviour {

    public GameObject doorWall;
    public GameObject fullWall;

    //REPLACE THIS WITH A LIST OF ALL THE SECTORS.
    public SectorBehaviour defaultSector;
    public SectorBehaviour exitSector;
    public SectorBehaviour startSector;

    //THE ATTACHED MasterSectorContol.
    private MasterSectorControl msc;



    //Dimensions of the map.
    private static int MAP_WIDTH = 32;
    private static int MAP_HEIGHT = 32;

    // Use this for initialization
    void Start() {

        Vector2 positionOfExit = new Vector2(Random.Range(0, MAP_WIDTH + 1), Random.Range(0, MAP_HEIGHT + 1));
        positionOfExit.x = 8;
        positionOfExit.y = 12;
        if (!isServer)
            return;

        //Get the refrence to the master sector control.
        msc = transform.GetComponent<MasterSectorControl>();

        //For every square in the map...
        for (int ix = 0; ix < MAP_WIDTH; ix++) {
            for (int iy = 0; iy < MAP_HEIGHT; iy++) {

                SectorBehaviour sect;

                if (ix >= 7 && ix <= 9 && iy >= 7 && iy <= 9) {
                    if (ix == 8 && iy == 8) {
                        sect = Instantiate(startSector, new Vector3((ix - MAP_WIDTH / 2) * 10, 0, (iy - MAP_HEIGHT / 2) * 10), startSector.transform.rotation);
                        sect.wallSeed = -5001;
                    }
                    else {
                        continue;
                    }
                }
                else if (ix == positionOfExit.x && iy == positionOfExit.y) {
                    sect = Instantiate(exitSector, new Vector3((ix - MAP_WIDTH / 2) * 10, 0, (iy - MAP_HEIGHT / 2) * 10), exitSector.transform.rotation);
                    sect.wallSeed = -5001;
                }
                else {

                    int r = Random.Range(0, 10);
                    if (r < 2)
                        continue;

                    //Create a sector...
                    sect = Instantiate(defaultSector, new Vector3((ix - MAP_WIDTH / 2) * 10, 0, (iy - MAP_HEIGHT / 2) * 10), defaultSector.transform.rotation);
                    sect.wallSeed = Random.Range(-5000, 5000);
                }
                //Add it to the master sector control.
                msc.AddSector(sect);
                NetworkServer.Spawn(sect.gameObject);

            }
        }

    }

}
