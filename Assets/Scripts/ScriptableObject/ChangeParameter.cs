using UnityEngine;

[CreateAssetMenu(menuName = "Skills/ChangeParameter")]
public class ChangeParameter : Skill
{
    public float parameter;
    public float skillTime;


    private ChangeParameterBehaviour cpBehaviour;

    public override void Init(GameObject obj)
    {
        cpBehaviour = obj.GetComponent<ChangeParameterBehaviour>();
        cpBehaviour.parameter = parameter;
        cpBehaviour.skillTime = skillTime;
    }

    public override void ActivateSkill()
    {
        cpBehaviour.Activate();
    }
}
