using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour {


    public MazeCell cellPrefab;
    public MazeWall wallPrefab;
    private MazeCell[,] cells;

    private IntVector2 size;
    private float scale;
    private IntVector2[] midCells = new IntVector2[4];

    public MazeCell GetCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }

    public void Generate(IntVector2 mazeSize, float mazeScale, int emptySlot)
    {
        this.size = mazeSize;
        this.scale = mazeScale;
        cells = new MazeCell[size.x, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        InitMidCells();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            DoNextGenerationStep(activeCells);
        }

        List<MazeCell> emptyCells = new List<MazeCell>();
        CreateEmptyCells(emptyCells, emptySlot);
        while (emptyCells.Count > 0)
        {
            MazeCell removeCell = emptyCells[emptyCells.Count - 1];
            Destroy(removeCell.gameObject);
            emptyCells.Remove(removeCell);
        }
        CreateMidRoom();
        CreateExit();
    }

    private void CreateExit()
    {
        MazeDirection dir = MazeDirections.RandomDir;
        MazeCell exitCell;
        if (dir == MazeDirection.North)
        {
            exitCell = cells[Random.Range(0, size.x), size.z - 1];        
        }
        else if (dir == MazeDirection.East)
        {
            exitCell = cells[size.x - 1, Random.Range(0, size.z)];
        }
        else if (dir == MazeDirection.South)
        {
            exitCell = cells[Random.Range(0, size.x), 0];
        }
        else
        {
            exitCell = cells[0, Random.Range(0, size.z)];
        }
        exitCell.GetEdge(dir).gameObject.SetActive(false);
    }

    private void InitMidCells()
    {
        midCells[0] = new IntVector2(size.x / 2, size.z / 2);
        midCells[1] = new IntVector2(size.x / 2+1, size.z / 2);
        midCells[2] = new IntVector2(size.x / 2, size.z / 2+1);
        midCells[3] = new IntVector2(size.x / 2+1, size.z / 2+1);
    }

    private void CreateMidRoom()
    {
        MazeCell midCell;
        midCell = cells[midCells[0].x, midCells[0].z];
        Destroy(midCell.GetEdge(MazeDirection.North).gameObject);
        Destroy(midCell.GetEdge(MazeDirection.East).gameObject);

        midCell = cells[midCells[1].x, midCells[1].z];
        Destroy(midCell.GetEdge(MazeDirection.North).gameObject);
        Destroy(midCell.GetEdge(MazeDirection.West).gameObject);

        midCell = cells[midCells[2].x, midCells[2].z];
        Destroy(midCell.GetEdge(MazeDirection.South).gameObject);
        Destroy(midCell.GetEdge(MazeDirection.East).gameObject);

        midCell = cells[midCells[3].x, midCells[3].z];
        Destroy(midCell.GetEdge(MazeDirection.West).gameObject);
        Destroy(midCell.GetEdge(MazeDirection.South).gameObject);
    }

    private void CreateEmptyCells(List<MazeCell> emptyCells, int emptyslot)
    {
        int internalsize = ((size.x - 2) * (size.z - 2)) - 4;
        if (internalsize < emptyslot)
        {
            Debug.Log("Not enough space to put empty spaces reducing to " + internalsize);
            emptyslot = internalsize;
        }
        MazeCell newCell;
        while (emptyCells.Count < emptyslot)
        {
            bool isContaining = false;
            IntVector2 randcoord = InsideRandomCoord;
            newCell = cells[randcoord.x, randcoord.z];
            for(int i = 0; i < emptyCells.Count; i ++)
            {
                if (emptyCells[i] == newCell)
                    isContaining = true;
            }
            for(int i = 0; i < midCells.Length; i++)
            {
                if (cells[midCells[i].x, midCells[i].z] == newCell)
                    isContaining = true;
            }
            if(isContaining == false)
            {
                emptyCells.Add(newCell);
            }           
            
        }
        
    }

    private void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
        IntVector2 randcoord;
        do
        {
            randcoord = RandomCoordinates;
        }
        while (cells[randcoord.x, randcoord.z] != null);
        activeCells.Add(CreateCell(randcoord));
    }

    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        if (currentCell.IsFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        MazeDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if (ContainsCoordinates(coordinates))
        {
            MazeCell neighbor = GetCell(coordinates);
            if (neighbor == null)
            {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else
            {
                CreateWall(currentCell, neighbor, direction);
            }
        }
        else
        {
            CreateWall(currentCell, null, direction);
        }
    }

    private MazeCell CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.Find("Floor").localScale = Vector3.one * scale;
        float cellLength = 0.5f;
        //TODO decrease increase by mazesize
        newCell.transform.localPosition = new Vector3((coordinates.x - size.x * cellLength + cellLength) * scale, 0f, (coordinates.z - size.z * cellLength + cellLength) * scale);
        return newCell;
    }

    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall passage = Instantiate(wallPrefab) as MazeWall;
        passage.Initialize(cell, otherCell, direction, scale);
        passage.gameObject.SetActive(false);
        passage = Instantiate(wallPrefab) as MazeWall;
        passage.Initialize(otherCell, cell, direction.GetOpposite(), scale);
        passage.gameObject.SetActive(false);
    }

    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall wall = Instantiate(wallPrefab) as MazeWall;
        wall.Initialize(cell, otherCell, direction, scale);
        if (otherCell != null)
        {
            wall = Instantiate(wallPrefab) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite(), scale);
        }
    }

    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
        }
    }

    public IntVector2 InsideRandomCoord
    {
        get
        {
            return new IntVector2(Random.Range(1, size.x - 1), Random.Range(1, size.z - 1));
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }
}
