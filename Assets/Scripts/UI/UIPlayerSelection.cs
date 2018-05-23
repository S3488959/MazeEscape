using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerSelection : MonoBehaviour
{
    public string className;
    public string description;

    public Skill[] skills = new Skill[3];
    [Space]
    [Header("UI Settings")]
    public Text main;
    public Image[] skillImages = new Image[3];
    public Text[] skillTexts = new Text[3];
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        main.text = string.Format("<size='20'><b>Class: {0}</b> </size>\n Description: {1}", className, description);
        for (int i = 0; i < skills.Length; i++)
        {
            skillImages[i].sprite = skills[i].skillSprite;
            skillTexts[i].text = string.Format("<size='16'><b>Skill Name: {0}</b> </size>\n Description: {1}", skills[i].skillName, skills[i].skillDescription);
        }
    }
}
