﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SectorBehaviour : NetworkBehaviour {

    [SyncVar]
    public int wallSeed = 0;
    [SyncVar]
    public NetworkInstanceId parentNetId;

    //Whether or not the master can control these aspects.
    bool roomMove = true;
    bool roomRotate = true;

    //Variables used to store moving of rooms.
    private float timeToMove = 10f;
    private float timeLeftToMove = 0f;
    private Vector3 fullMove = Vector3.zero;


    //Is this sector moveable by the master.
    public bool isMoveable = true;
    public bool generateWalls = true;

    //The width and height of the sector.
    public int sectorSize = 1;

    public static float wallWidth = 0.01f;

    //This is the sector that we link NSEW transitions if there is no sector there. It has all its Exits set to true.
    //public static SectorBehaviour NoSector = new SectorBehaviour(true);

    public static GameObject wallDoor;
    public static GameObject fullWall;

    public GameObject wall;
    public GameObject door;

    public GameObject miniMapCounterpartPrefab;
    public GameObject miniMapCounterpart;

    private GameObject north, south, west, east;

    //The list of exits and transitions from this sector.
    //Used to determine if the maze is solvable.
    public struct Exits {
        public bool N, S, E, W; //You can go these direction from this sector.
        //These are the transtions NOT THE EXITS. MAKE THIS A SPERATE STRUCT ????
        public bool NS, NW, NE; //You can use these transitions; i.e SW is false if there is a wall blocking the south door.
        public bool SE, SW;
        public bool WE;
    }

    //SectorBehaviour northSector = NoSector;
    //SectorBehaviour westSector = NoSector;
    //SectorBehaviour eastSector = NoSector;
    //SectorBehaviour southSector = NoSector;
    private Exits exits = new Exits();

    private bool sidesGenerated = false;

    public SectorBehaviour(bool globalSector) {
        //Set all exits to true.
        exits.N = exits.S = exits.E = exits.W = exits.NS = exits.NW = exits.NE = exits.SE = exits.SW = exits.WE = true;
    }

    // Use this for initialization
    void Start() {
        parentNetId = gameObject.GetComponent<NetworkIdentity>().netId;
        if (isServer)
        {
            if (!sidesGenerated)
            {
                sidesGenerated = true;
                if (wallSeed >= -5000)
                    GenerateSides(wall, door);
            }
        }
        

        transform.parent = GameObject.Find("SectorHolder").transform;

        timeToMove = 10f;

        if (fullWall == null && wall != null)
            fullWall = wall;

        if (wallDoor == null && door != null)
            wallDoor = door;

    }

    // Update is called once per frame
    void Update() {
        if (timeLeftToMove > 0)
            UpdateMove();
    }

    void UpdateMove() {
        timeLeftToMove -= timeToMove / 10f;
        transform.position += fullMove / 10;
        north.transform.position += fullMove / 10;
        south.transform.position += fullMove / 10;
        west.transform.position += fullMove / 10;
        east.transform.position += fullMove / 10;
        //transform.position = Vector3.Lerp(transform.position, transform.position + fullMove, timeToMove);
    }


    public void GenerateSides(GameObject fullwall, GameObject doorwall) {

        Random.InitState(wallSeed);

        int doorsCount = Random.Range(0, 100);
        if (doorsCount < 35)
            doorsCount = 4;
        else if (doorsCount < 75)
            doorsCount = 3;
        else
            doorsCount = 2;

        if (doorsCount == 4) {
            GenerateNorth(doorwall);
            GenerateEast(doorwall);
            GenerateSouth(doorwall);
            GenerateWest(doorwall);
        }

        if (doorsCount == 3) {
            int side = Random.Range(0, 4);
            if (side == 0)
                GenerateNorth(fullwall);
            else
                GenerateNorth(doorwall);
            if (side == 1)
                GenerateEast(fullwall);
            else
                GenerateEast(doorwall);
            if (side == 2)
                GenerateSouth(fullwall);
            else
                GenerateSouth(doorwall);
            if (side == 3)
                GenerateWest(fullwall);
            else
                GenerateWest(doorwall);
        }

        if (doorsCount == 2) {
            int sides = Random.Range(0, 6);
            switch (sides) {
                case 0:
                    GenerateNorth(fullwall);
                    GenerateEast(fullwall);
                    GenerateSouth(doorwall);
                    GenerateWest(doorwall);
                    break;
                case 1:
                    GenerateNorth(fullwall);
                    GenerateEast(doorwall);
                    GenerateSouth(fullwall);
                    GenerateWest(doorwall);
                    break;
                case 2:
                    GenerateNorth(fullwall);
                    GenerateEast(doorwall);
                    GenerateSouth(doorwall);
                    GenerateWest(fullwall);
                    break;
                case 3:
                    GenerateNorth(doorwall);
                    GenerateEast(fullwall);
                    GenerateSouth(fullwall);
                    GenerateWest(doorwall);
                    break;
                case 4:
                    GenerateNorth(doorwall);
                    GenerateEast(fullwall);
                    GenerateSouth(doorwall);
                    GenerateWest(fullwall);
                    break;
                case 5:
                    GenerateNorth(doorwall);
                    GenerateEast(doorwall);
                    GenerateSouth(fullwall);
                    GenerateWest(fullwall);
                    break;
            }
        }


    }

    public void GenerateWest(GameObject go) {
        west = Instantiate(go, transform.position + new Vector3(-5 + wallWidth, 5, 0), Quaternion.Euler(0, -90, 0));
        west.name = wallSeed.ToString();
        west.transform.parent = transform;
        NetworkServer.Spawn(west);
        RpcSetParent(west);
    }

    public void GenerateEast(GameObject go) {
        east = Instantiate(go, transform.position + new Vector3(5 - wallWidth, 5, 0), Quaternion.Euler(0, 90, 0));
        east.name = wallSeed.ToString();
        east.transform.parent = transform;
        NetworkServer.Spawn(east);
        RpcSetParent(east);
    }

    public void GenerateNorth(GameObject go) {
        north = Instantiate(go, transform.position + new Vector3(0, 5, 5 - wallWidth), Quaternion.Euler(0, 0, 0));
        north.name = wallSeed.ToString();
        north.transform.parent = transform;
        NetworkServer.Spawn(north);
        RpcSetParent(north);
    }

    public void GenerateSouth(GameObject go) {
        south = Instantiate(go, transform.position + new Vector3(0, 5, -5 + wallWidth), Quaternion.Euler(0, 180, 0));
        south.name = wallSeed.ToString();
        south.transform.parent = transform;
        
        NetworkServer.Spawn(south);
        RpcSetParent(south);
    }

    [ClientRpc]
    public void RpcSetParent(GameObject obj)
    {
        if(parentNetId != null)
        {
            GameObject parentObj = NetworkServer.FindLocalObject(parentNetId);
            //Debug.Log(parentObj + " " + obj);
            obj.transform.parent = parentObj.transform;
        }
    }

    public void SpawnObj(GameObject newObj) {
        newObj.transform.SetParent(transform);
        NetworkServer.Spawn(newObj);
    }


    public void Move(Vector3 direction) {

        if (!isMoveable)
            return;

        if (timeLeftToMove > 0)
            return;

        transform.GetComponent<Collider>().enabled = false;

        if (!Physics.CheckBox(transform.position + direction * 10, new Vector3(sectorSize / 2, 0, sectorSize / 2))) {
            //transform.position += direction * 10;
            fullMove = direction * 10;
            timeLeftToMove = timeToMove;
        }
        transform.GetComponent<Collider>().enabled = true;
    }

    public void Rotate(float angle) {
        transform.Rotate(Vector3.up, angle);
    }
}

