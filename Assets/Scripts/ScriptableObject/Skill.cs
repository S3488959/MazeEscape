using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public string skillName = "skill name";
    public string skillDescription = "Description";
    public Sprite skillSprite;
    public float skillCoolTime = 1f;
    public int skillCost = 10;

    public abstract void Init(GameObject obj);
    public abstract void ActivateSkill();
}
