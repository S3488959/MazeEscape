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
public class GenerateMap : NetworkBehaviour
{

    public GameObject doorWall;
    public GameObject fullWall;

    //REPLACE THIS WITH A LIST OF ALL THE SECTORS.
    public SectorBehaviour defaultSector;
    public SectorBehaviour exitSector;
    public SectorBehaviour startSector;
    public SectorBehaviour mapSector;
    public SectorBehaviour sundialSector;

    SectorSelection selection = new SectorSelection(MAP_WIDTH, MAP_HEIGHT);

    //THE ATTACHED MasterSectorContol.
    private MasterSectorControl msc;

    //Dimensions of the map.
    private static readonly int MAP_WIDTH = 32;
    private static readonly int MAP_HEIGHT = 32;

    private static readonly int COUNT_DEFAULT_SECTORS = 600;
    private static readonly int COUNT_MAP_SECTORS = 4;
    private static readonly int COUNT_DIAL_SECTORS = 10;
    private static readonly int COUNT_EXIT_SECTORS = 4;

    // Use this for initialization
    void Start() {

        if (!isServer)
            return;

        //Get the refrence to the master sector control.
        msc = transform.GetComponent<MasterSectorControl>();

        selection.AddSector(startSector);
        
        for (int i = 0; i< COUNT_DEFAULT_SECTORS; i++) {
            selection.AddSector(defaultSector);
        }

        for (int i = 0; i < COUNT_MAP_SECTORS; i++) {
            selection.AddSector(mapSector);
        }

        for (int i = 0; i < COUNT_DIAL_SECTORS; i++) {
            selection.AddSector(sundialSector);
        }

        for (int i = 0; i < COUNT_EXIT_SECTORS; i++) {
            selection.AddSector(exitSector);
        }

        selection.Done();

        CreateSector(selection.PopFirst(), MAP_WIDTH / 2, MAP_HEIGHT / 2);

        //For every square in the map...
        for (int ix = 0; ix < MAP_WIDTH; ix++) {
            for (int iy = 0; iy < MAP_HEIGHT; iy++) {
                //Exclduing the middle 9 squares.
                if (ix >= 15 && ix <= 17 && iy >= 15 && iy <= 17) {
                    continue;
                }
                else {
                    CreateSector(selection.Pop(), ix, iy);
                }

            }
        }

    }

    private void CreateSector(SectorBehaviour sector, int x, int y) {
        if (sector == null)
            return;
        SectorBehaviour sect = Instantiate(sector,
             new Vector3((x - MAP_WIDTH / 2) * 10, 0, (y - MAP_HEIGHT / 2) * 10), sector.transform.rotation);
        if (sector.generateWalls)
            sect.wallSeed = Random.Range(-5000, 5000);
        else
            sect.wallSeed = -5001;

        //Add it to the master sector control.
        msc.AddSector(sect);
        NetworkServer.Spawn(sect.gameObject);

    }

}
