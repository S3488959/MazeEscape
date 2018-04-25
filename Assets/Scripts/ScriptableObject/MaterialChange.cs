using UnityEngine;

[CreateAssetMenu (menuName = "Skills/MaterialChange")]
public class MaterialChange : Skill {

    public float skillTime = 3f;
    public Material basicMat;
    public Material changedMat;

    private MaterialChangeBehaviour mcBehaviour;

    public override void Init(GameObject obj)
    {
        mcBehaviour = obj.GetComponent<MaterialChangeBehaviour>();
        mcBehaviour.Init();
        mcBehaviour.skillTime = skillTime;
        mcBehaviour.basicMat = basicMat;
        mcBehaviour.changedMat = changedMat;
    }

    public override void ActivateSkill()
    {
        mcBehaviour.Init();
        mcBehaviour.Activate();
    }
}
