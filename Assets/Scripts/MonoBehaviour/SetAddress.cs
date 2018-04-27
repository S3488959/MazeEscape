using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetAddress : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.GetComponent<Text>().text = "localhost";

    }
	
	// Update is called once per frame
	void Update () {

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerBehaviour>().SetNetworkAddress(
            transform.GetComponent<Text>().text
            );


    }
}
