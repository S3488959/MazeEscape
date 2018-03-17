using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MazeGenerator
{
    public MazeController mazePrefab;
    public IntVector2 mazeSize;
    public float mazeScale;
    public int emptySlot;
}

public class GameManager : MonoBehaviour {

    public MazeGenerator mazeGenerator;
    private MazeController mazeInstance;

	// Use this for initialization
	void Start () {
        BeginGame();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.N))
            RestartGame();
	}

    private void BeginGame()
    {
        mazeInstance = Instantiate(mazeGenerator.mazePrefab) as MazeController;
        mazeInstance.Generate(mazeGenerator.mazeSize, mazeGenerator.mazeScale, mazeGenerator.emptySlot);
    }

    private void RestartGame()
    {
        Destroy(mazeInstance.gameObject);
        BeginGame();
    }
}
