using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateObjectBehaviour : MonoBehaviour
{
    [HideInInspector]
    public float skillDelay;

    public void Activate()
    {
        StartCoroutine("ActivateAfterDelay");
    }

    IEnumerator ActivateAfterDelay()
    {
        Vector3 newPos = new Vector3(Random.Range(-16f, 16f) * 10f, 3f, Random.Range(-16f, 16f) * 10f);
        yield return new WaitForSeconds(skillDelay);
        transform.position = newPos;
    }
}
