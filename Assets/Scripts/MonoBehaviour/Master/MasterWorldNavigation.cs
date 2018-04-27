using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterWorldNavigation : MonoBehaviour {

    private const float CAMMOVESPEED = 40;

    private int slaveIndex = 0;
    private int slaveCount;
    private GameObject currentSlave;
    private SlaveList slaveList;
    bool controller = true;

	// Use this for initialization
	void Start () {
		if (controller) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        StartCoroutine("Init");
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = transform.position;
        pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * CAMMOVESPEED;
        pos.z += Input.GetAxis("Vertical") * Time.deltaTime * CAMMOVESPEED;
        

        if(Input.GetKeyDown(KeyCode.Q))
        {
            slaveIndex--;
            if (slaveIndex < 0)
                slaveIndex = slaveCount - 1;
            currentSlave = SlaveList.GetInstance().GetSlave(slaveIndex);  
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            slaveIndex++;
            if (slaveIndex >= slaveCount)
                slaveIndex = 0;
            currentSlave = SlaveList.GetInstance().GetSlave(slaveIndex);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            pos.x = currentSlave.transform.position.x;
            pos.z = currentSlave.transform.position.z;
        }

        transform.position = pos;
    }

    IEnumerator Init()
    {
        yield return new WaitForSeconds(1f);

        slaveList = SlaveList.GetInstance();
        slaveCount = slaveList.GetCount();
        currentSlave = slaveList.GetSlave(slaveIndex);
    }
}
