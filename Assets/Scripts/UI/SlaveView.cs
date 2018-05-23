using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlaveView : MonoBehaviour
{

    public Text playerText;
    public MasterWorldNavigation navi;

	// Update is called once per frame
	void Update ()
    {
        playerText.text = navi.getCurrSlave().playerName;
        playerText.color = navi.getCurrSlave().playerColor;
	}
}
