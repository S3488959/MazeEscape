using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWall : MonoBehaviour
{
    public MazeCell cell, otherCell;

    public MazeDirection direction;

    public void Initialize(MazeCell cell, MazeCell otherCell, MazeDirection direction, float scale)
    {
        this.cell = cell;
        this.otherCell = otherCell;
        this.direction = direction;
        cell.SetEdge(direction, this);
        transform.parent = cell.transform;
        transform.Find("Wall").localScale *= scale;
        transform.Find("Wall").localPosition *= scale;
        transform.localPosition = Vector3.zero;
        transform.localRotation = direction.ToRotation();
    }
}
