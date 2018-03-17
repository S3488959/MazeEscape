using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour {

    public IntVector2 coordinates;
    private int initializedWallCount;

    private MazeWall[] walls = new MazeWall[MazeDirections.Count];

    public bool IsFullyInitialized
    {
        get
        {
            return initializedWallCount == MazeDirections.Count;
        }
    }


    public MazeWall GetEdge(MazeDirection direction)
    {
        return walls[(int)direction];
    }

    public void SetEdge(MazeDirection direction, MazeWall wall)
    {
        walls[(int)direction] = wall;
        initializedWallCount += 1;
    }

    public MazeDirection RandomUninitializedDirection
    {
        get
        {
            int skips = Random.Range(0, MazeDirections.Count - initializedWallCount);
            for (int i = 0; i < MazeDirections.Count; i++)
            {
                if (walls[i] == null)
                {
                    if (skips == 0)
                    {
                        return (MazeDirection)i;
                    }
                    skips -= 1;
                }
            }
            throw new System.InvalidOperationException("MazeCell has no uninitialized directions left.");
        }
    }
}
