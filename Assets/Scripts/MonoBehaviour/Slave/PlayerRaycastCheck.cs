using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerRaycastCheck : NetworkBehaviour {

    public float castDistance = 3f;
    public float sphereRadius = 1f;
    public LayerMask activatable;

    public Image activateUI;

    public Sprite defaultSprite;

    private float hitDistance = 0f;

	// Use this for initialization
	void Start () {
        activateUI.sprite = defaultSprite;
        hitDistance = castDistance;

    }
	
	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer)
            return;

        activateUI.sprite = defaultSprite;
        Vector3 rayPos = gameObject.transform.position;
        //Ray ray = GameObject.FindGameObjectWithTag("SlaveCamera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        Ray ray = new Ray(rayPos, gameObject.transform.forward);
        //RaycastHit[] hits = Physics.RaycastAll(ray);
        RaycastHit hit;
        if (Physics.SphereCast(ray, sphereRadius, out hit, castDistance, activatable))
        {
            hitDistance = hit.distance;
            Activatable act = hit.transform.GetComponent<Activatable>();
            if (act != null) {
                activateUI.sprite = act.image;
                if (Input.GetKeyDown(KeyCode.E))
                    act.Activate();
            }
        }
        else
        {
            hitDistance = castDistance;
        }
		
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * hitDistance);
        Gizmos.DrawWireSphere(transform.position + transform.forward * hitDistance, sphereRadius);
    }
}
