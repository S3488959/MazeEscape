using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlaveList
{
    private static SlaveList sharedInstance;

    List<GameObject> slaves = new List<GameObject>();

    public static SlaveList GetInstance()
    {
        return sharedInstance;
    }

    public void AddSlave(GameObject slave)
    {
        slaves.Add(slave);
    }

    public GameObject GetSlave(int no)
    {
        return slaves[no];
    }

    public int GetCount()
    {
        return slaves.Count;
    }
}
