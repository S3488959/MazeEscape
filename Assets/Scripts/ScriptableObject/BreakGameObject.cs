using UnityEngine;

[CreateAssetMenu(menuName = "Skills/BreakGameObject")]
public class BreakGameObject : Skill
{
    public LayerMask objectLayer;
    public float castDistance;
    public float hitDelay;

    private BreakGameObjectBehaviour bgBehaviour;

    public override void Init(GameObject obj)
    {
        bgBehaviour = obj.GetComponent<BreakGameObjectBehaviour>();
        bgBehaviour.objectLayer = objectLayer;
        bgBehaviour.castDistance = castDistance;
        bgBehaviour.hitDelay = hitDelay;
    }

    public override void ActivateSkill()
    {
        bgBehaviour.Activate();
    }
}
