using UnityEngine;
using UnityEngine.Networking;

public class PlayerBehaviour : NetworkBehaviour
{
    public GameObject Canvas;
    SlaveController slaveCont;
    [SyncVar]
    public string playerName = "player";
    [SyncVar]
    public Color playerColor = Color.white;
    
    [SyncVar]
    public GameObject minimapPosToken = null;

    TextMesh textMesh;

    public override void OnStartLocalPlayer()
    {
        if (!isServer)
        {
            GameSettings.PLAYSTATE = GameSettings.PLAY_STATE.PLAYER;
            slaveCont = gameObject.GetComponent<SlaveController>();
            slaveCont.ChangeView();
            Canvas.SetActive(true);
        }
    }

    // Use this for initialization
    void Start () {
        if(isLocalPlayer)
        {
            if(isServer)
            {
                GameSettings.PLAYSTATE = GameSettings.PLAY_STATE.MASTER;
                Destroy(gameObject);
            }
            else
            {
                textMesh = this.GetComponentInChildren<TextMesh>();
                //ChangeLabel(playerName);
                CmdSendNameToServer(playerName, playerColor);
            }
        }
        if(!isLocalPlayer)
        {
            transform.GetComponent<AudioListener>().enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        
        if (!isLocalPlayer)
        {
            return;          
        }
        SlaveControls();
		
	}

    void SlaveControls()
    {
        slaveCont.GetInput();
        
    }

    public void ChangeLabel(string name)
    {
        textMesh.text = name;
    }

    [Command]
    void CmdSendNameToServer(string nameToSend, Color newColor)
    {
        RpcSetPlayerName(nameToSend, newColor);
    }

    [ClientRpc]
    void RpcSetPlayerName(string name, Color newColor)
    {
        this.GetComponentInChildren<TextMesh>().text = name;
        this.GetComponentInChildren<TextMesh>().color = newColor;
    }
}
