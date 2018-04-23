using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinmapBehaviour : MonoBehaviour {

    private float miniMapRefresh = 10f;
    private int sectorCount;

    public GameObject noramlSectorMini;
    public GameObject playerMapToken;
    public GameObject exitSectorMini;
    public GameObject startSectorMini;

    GameManagerBehaviour gameManager;

    SectorBehaviour[] sects;


    //The mastersectorcontrol, that has the list of all the sectors.
    public MasterSectorControl mcp;

    private void Start() {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerBehaviour>();

        StartCoroutine(LateStart());
    }

    // Use this for initialization
    IEnumerator LateStart() {

        yield return new WaitForSeconds(0.001f);

        sects = mcp.GetSectors();
        for (int i = 0; i < sects.Length; i++) {
            GameObject newMinimapNode;
            if (sects[i].name.Contains("Exit"))
                newMinimapNode = Instantiate(exitSectorMini, transform);
            else if (sects[i].name.Contains("Start"))
                newMinimapNode = Instantiate(startSectorMini, transform);
            else
                newMinimapNode = Instantiate(noramlSectorMini, transform);
            sects[i].miniMapCounterpart = newMinimapNode;
        }

    }

    // Update is called once per frame
    void Update() {
        //Wait until the map has been made to continue.
        if (sects == null)
            return;

        miniMapRefresh += Time.deltaTime;
        if (miniMapRefresh > 0.0f) {

            /*
                SlaveBehaviour[] slaves = gameManager.GetSlaves();
                for (int i = 0; i < slaves.Length; i++) {
                    GameObject pmt = slaves[i].minimapPosToken;
                    if (pmt == null) {
                        pmt = Instantiate(playerMapToken, transform);
                        slaves[i].minimapPosToken = pmt;
                    }
                    pmt.transform.SetParent(transform);
                    pmt.GetComponent<RectTransform>().localPosition = new Vector3(slaves[i].transform.position.x * 1.3f, slaves[i].transform.position.z * 1.3f, 0);
                    pmt.transform.SetParent(transform.parent);
                }*/

            GameObject[] sectsA = GameObject.FindGameObjectsWithTag("Sector");

            for (int i = 0; i < sectsA.Length; i++) {
                SectorBehaviour sect = sectsA[i].GetComponent<SectorBehaviour>();
                if (sect.miniMapCounterpart == null) {
                    GameObject newMinimapNode = Instantiate(noramlSectorMini, transform);
                    sect.miniMapCounterpart = newMinimapNode;
                }
                sect.miniMapCounterpart.GetComponent<RectTransform>().localPosition = new Vector3(sect.transform.position.x * 1.3f, sect.transform.position.z * 1.3f, 0);
            }
            miniMapRefresh = 0;
        }
    }
}
