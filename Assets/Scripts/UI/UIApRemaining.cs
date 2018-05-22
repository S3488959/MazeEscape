using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIApRemaining : MonoBehaviour
{

    //The objects we require that are children
    public Text apText;
    public RectTransform imageRT;

    [SerializeField]
    private GameObject player;
    private PlayerAPController apControl;

    // Use this for initialization
    void Start()
    {
        apControl = player.GetComponent<PlayerAPController>();
    }

    // Update is called once per frame
    void Update()
    {
        imageRT.localScale = new Vector3(apControl.GetAPRatio(), 1, 1);
        apText.text = "AP: " + apControl.GetAP();
    }
}
