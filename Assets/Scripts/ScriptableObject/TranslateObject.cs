using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Skills/TranslateObject")]
public class TranslateObject : Skill {

    public float skillDelay = 2f;


    private TranslateObjectBehaviour toBehaviour;

    public override void Init(GameObject obj)
    {
        toBehaviour = obj.GetComponent<TranslateObjectBehaviour>();
        toBehaviour.skillDelay = skillDelay;
    }

    public override void ActivateSkill()
    {
        toBehaviour.Activate();
    }
}
