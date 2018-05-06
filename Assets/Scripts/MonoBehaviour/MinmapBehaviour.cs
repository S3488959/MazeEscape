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


    //The mastersectorcontrol, that has the list of all the sectors.
    public MasterSectorControl mcp;

    private Dictionary<PlayerBehaviour, GameObject> playerMapTokens = new Dictionary<PlayerBehaviour, GameObject>(); 

    private void Start() {

    }

    // Use this for initialization
    IEnumerator LateStart() {

        yield return new WaitForSeconds(0.001f);

    }

    // Update is called once per frame
    void Update() {

        miniMapRefresh += Time.deltaTime;
        if (miniMapRefresh > 0.0f) {

            GameObject[] sects = GameObject.FindGameObjectsWithTag("Sector");

            for (int i = 0; i < sects.Length; i++) {
                SectorBehaviour sect = sects[i].GetComponent<SectorBehaviour>();

                if (sect.miniMapCounterpart == null) {
                    GameObject newMinimapNode = Instantiate(sect.miniMapCounterpartPrefab, transform);
                    sect.miniMapCounterpart = newMinimapNode;
                }

                sect.miniMapCounterpart.GetComponent<RectTransform>().localPosition = new Vector3(sect.transform.position.x * 1.3f, sect.transform.position.z * 1.3f, 0) + Vector3.right * 6.5f + Vector3.up * 6.5f;
            }


            GameObject[] slaves = GameObject.FindGameObjectsWithTag("MazeSlave");

            for (int i = 0; i < slaves.Length; i++) {
                PlayerBehaviour slave = slaves[i].GetComponent<PlayerBehaviour>();
                if (!playerMapTokens.ContainsKey(slave)) {
                    GameObject newMinimapNode = Instantiate(playerMapToken, transform);
                    playerMapTokens.Add(slave, newMinimapNode);
                }

                GameObject miniMap;
                playerMapTokens.TryGetValue(slave, out miniMap);
                miniMap.GetComponent<RectTransform>().localPosition = new Vector3(slave.transform.position.x * 1.3f, slave.transform.position.z * 1.3f, 0) + Vector3.right * 6.5f + Vector3.up * 6.5f;
                miniMap.transform.SetParent(null, false);
                miniMap.transform.SetParent(transform, false);
            }

            miniMapRefresh = 0;
        }
    }
}
