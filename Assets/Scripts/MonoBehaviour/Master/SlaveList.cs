using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlaveList : MonoBehaviour
{
    private static SlaveList sharedInstance;

    public GameObject[] slaves = null;

    private void Awake()
    {
        sharedInstance = this;
    }

    public void FindSlaves()
    {
        slaves = GameObject.FindGameObjectsWithTag("MazeSlave");
    }

    public static SlaveList GetInstance()
    {
        return sharedInstance;
    }

    public GameObject GetSlave(int no)
    {
        return slaves[no];
    }

    public int GetCount()
    {
        return slaves.Length;
    }
}
