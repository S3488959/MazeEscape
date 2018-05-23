using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAPController : MonoBehaviour
{
    private int ap = 200;
    private int currentAP;

    private void Start()
    {
        currentAP = ap;
        StartCoroutine("IncreaseAP");
    }

    private void Update()
    {
        
    }

    IEnumerator IncreaseAP()
    {
        while(true)
        {
            yield return new WaitForSeconds(2f);
            if (currentAP < ap)
                currentAP++;
        }
    }

    public int GetAP()
    {
        return currentAP;
    }

    public bool CanUseSkill(int amountToUse)
    {
        if(currentAP >= amountToUse)
        {
            return true;
        }
        return false;
    }

    public void UseSkill(int amountToUse)
    {
        currentAP -= amountToUse;
    }

    public float GetAPRatio()
    {
        float ratio = currentAP / (float)ap;
        return ratio;
    }
}
