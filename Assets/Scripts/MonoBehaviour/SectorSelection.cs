using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorSelection {

    List<SectorBehaviour> sectors = new List<SectorBehaviour>();
    int remaining = 0;

    //Creates a new sector selection using width and height to get the amount of remaining space.
    public SectorSelection(int width, int height) {
        remaining = width * height;
    }

    //Adds a given sector to the sectors list.
    public void AddSector(SectorBehaviour sector) {
        sectors.Add(sector);
        remaining -= sector.sectorSize * sector.sectorSize;
    }

    //Populates the rest of the sectors list with empty sectors.
    public void Done() {
        for (int i = 0; i < remaining; i++) {
            sectors.Add(null);
        }
        remaining = 0;
    }

    //Gets a random sector from the list.
    public SectorBehaviour Pop() {
        int rand = Random.Range(0, sectors.Count);
        SectorBehaviour toReturn = sectors[rand];
        sectors.RemoveAt(rand);
        return toReturn;
    }

    //Gets the sector that was added firstmost from the list.
    public SectorBehaviour PopFirst() {
        SectorBehaviour toReturn = sectors[0];
        sectors.RemoveAt(0);
        return toReturn;
    }



}
