using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class MaterialChangeBehaviour : NetworkBehaviour {

	[HideInInspector]
    public float skillTime = 0;   
    [HideInInspector]
    public Material basicMat;
    [HideInInspector]
    public Material changedMat;

    public GameObject model;

    private SkinnedMeshRenderer meshRenderer;
    
    public void Init()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void Activate()
    {
        if (!isLocalPlayer)
            return;
        StartCoroutine("ChangeMatForSec", skillTime);
    }

    private IEnumerator ChangeMatForSec(float second)
    {
        meshRenderer.material = changedMat;
        CmdChange(1);
        yield return new WaitForSeconds(second);
        meshRenderer.material = basicMat;
        CmdChange(2);
    }

    [Command]
    public void CmdChange(int objNumber)
    {
        Change(objNumber);
        RpcChange(objNumber);
    }

    [ClientRpc]
    void RpcChange(int objNumber)
    {
        Change(objNumber);
    }

    public void Change(int objNumber)
    {
        if(!isLocalPlayer)
        {
            if (objNumber == 1)
            {
                model.SetActive(false);
            }
            if (objNumber == 2)
            {
                model.SetActive(true);
            }
        }
    }

}
