using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionController : MonoBehaviour
{
    public GameObject chosen;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion rotation = new Quaternion(0,180,0,0);
        transform.rotation = rotation;
	}
}
