using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class BreakGameObjectBehaviour : NetworkBehaviour
{
    [HideInInspector]
    public LayerMask objectLayer;
    [HideInInspector]
    public float castDistance;
    [HideInInspector]
    public float hitDelay;

    private GameObject breakable;

    public void Activate()
    {
        if(breakable != null)
        {
            StartCoroutine("HitDelay", breakable);
        }
            
    }

    public bool CheckIfBreakable()
    {
        Vector3 rayPos = gameObject.transform.position + Vector3.up;
        Ray ray = new Ray(rayPos, gameObject.transform.forward);
        Debug.DrawLine(rayPos, rayPos + (gameObject.transform.forward * castDistance), Color.blue);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.2f, out hit, castDistance, objectLayer))
        {
            breakable = hit.transform.gameObject;
            return true;
        }
        return false;
    }

    private IEnumerator HitDelay(GameObject obj)
    {
        yield return new WaitForSeconds(hitDelay);
        Destroy(obj);
    }

    [Command]
    public void CmdDestroy(GameObject obj)
    {
        Destroy(obj);
        RpcDestroy(obj);
    }

    [ClientRpc]
    void RpcDestroy(GameObject obj)
    {
        Destroy(obj);
    }

    public void Destroy(GameObject obj)
    {
        GameObject.Destroy(obj);
    }
}
