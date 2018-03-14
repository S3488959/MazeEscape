using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  Name: GenerateMap.cs
 *  Author: Todd O'Donnell
 *  Purpose:
 *      - Generates the map at the start of each game.
 *      - Created sectors are add to the list on the attached MasterSectorControl.
 *  Data Required:
 *      - A list of all the sectors to be added to the map. UNFINISHED.
 */

[RequireComponent(typeof(MasterSectorControl))]
public class GenerateMap : MonoBehaviour {

    //REPLACE THIS WITH A LIST OF ALL THE SECTORS.
    public SectorBehaviour defaultSector;
    //THE ATTACHED MasterSectorContol.
    private MasterSectorControl msc; 

    //Dimensions of the map.
    private static int MAP_WIDTH = 16;
    private static int MAP_HEIGHT = 16;

    // Use this for initialization
    void Start () {

        //Get the refrence to the master sector control.
        msc = transform.GetComponent<MasterSectorControl>();

        //For every square in the map...
        for (int  ix = 0; ix < MAP_WIDTH; ix++) {
            for (int iy = 0; iy < MAP_HEIGHT; iy++) {

                int r = Random.Range(0, 10);
                if (r < 2)
                    continue;

                //Create a sector...
                SectorBehaviour sect = Instantiate(defaultSector, new Vector3((ix-MAP_WIDTH/2)*10, 0, (iy-MAP_HEIGHT/2)*10), defaultSector.transform.rotation);
                //Add it to the master sector control.
                msc.AddSector(sect);
            }
        }
		
	}

}
