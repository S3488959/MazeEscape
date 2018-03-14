using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinmapBehaviour : MonoBehaviour {

    private float miniMapRefresh = 10f;
    private int sectorCount;

    public GameObject sectorMini;
    public GameObject playerMapToken;

    GameObject player;

    SectorBehaviour[] sects;


    //The mastersectorcontrol, that has the list of all the sectors.
    public MasterSectorControl mcp;

    private void Start() {
        StartCoroutine(LateStart());
    }

    // Use this for initialization
    IEnumerator LateStart () {

        yield return new WaitForSeconds(0.001f);

        //mcp = GameObject.FindGameObjectWithTag("MainCamera").transform.parent.parent.GetComponent<MasterSectorControl>();

        player = GameObject.FindGameObjectWithTag("Player");

        sects = mcp.GetSectors();
        for (int i = 0; i < sects.Length; i++) {

            GameObject newMinimapNode = Instantiate(sectorMini, transform);
            sects[i].miniMapCounterpart = newMinimapNode;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        //Wait until the map has been made to continue.
        if (sects == null)
            return;

        miniMapRefresh += Time.deltaTime;
        if (miniMapRefresh > 0.0f) {

            playerMapToken.transform.SetParent(transform);
            playerMapToken.GetComponent<RectTransform>().localPosition = new Vector3(player.transform.position.x * 1.3f, player.transform.position.z * 1.3f, 0);
            playerMapToken.transform.SetParent(transform.parent);

            for (int i = 0; i < sects.Length; i++) {
                SectorBehaviour sect = sects[i];
                sect.miniMapCounterpart.GetComponent<RectTransform>().localPosition = new Vector3(sect.transform.position.x * 1.3f, sect.transform.position.z *1.3f, 0);
            }
            miniMapRefresh = 0;
        }
	}
}
