using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class ChangeParameterBehaviour : NetworkBehaviour
{
    [HideInInspector]
    public float parameter;
    [HideInInspector]
    public float skillTime;

    private float targetParam;


    public void Init()
    {
        
    }

    public void Activate()
    {

    }
}
