using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [System.Serializable]
    public class Cell
    {
        public bool visited;
        public GameObject north;//1
        public GameObject east;//2
        public GameObject south;//3
        public GameObject west;//4
    }

    public GameObject wall;
    public GameObject floor;
    public float wallLength = 1.0f;
    public int xSize;
    public int ySize;
    private Vector3 initialPos;
    private Vector3 floorSpawn;
    private GameObject wallHolder;
    private Cell[] cells;
    public int currentCell = 0;
    private int TotalCells;
    private int VisitedCells = 0;
    private bool startedBuilding = false;
    private int currentNeighbour = 0;
    private List<int> lastCells;
    private int backingUp = 0;
    private int wallToBreak = 0;
    // Use this for initialization
    void Start()
    {
        CreateWalls();
        CreateFloor();
    }

    void CreateWalls()
    {

        wallHolder = new GameObject();
        wallHolder.name = "Maze";

        initialPos = new Vector3((-xSize / 2) + wallLength / 2, 0.0f, (-ySize / 2) + wallLength / 2);
        Vector3 MyPos = initialPos;
        GameObject tempWall;

        //For vertical walls
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j <= xSize; j++)
            {
                MyPos = new Vector3(initialPos.x + (j * wallLength) - wallLength / 2, 1f, initialPos.z + (i * wallLength) - wallLength / 2);
                tempWall = Instantiate(wall, MyPos, Quaternion.identity) as GameObject;
                tempWall.transform.parent = wallHolder.transform;
            }
            Debug.Log("Error");
        }
        
        //For horizontal walls
        for (int i = 0; i <= ySize; i++)
        {
            Debug.Log("Loop has started");
            for (int j = 0; j < xSize; j++)
            {
                MyPos = new Vector3(initialPos.x + (j * wallLength), 1f, initialPos.z + (i * wallLength) - wallLength);
                tempWall = Instantiate(wall, MyPos, Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;
                tempWall.transform.parent = wallHolder.transform;
            }
        }
        CreateCells();
    }
    void CreateCells()
    {
        lastCells = new List<int>();
        lastCells.Clear();
        TotalCells = xSize * ySize;
        GameObject[] allWalls;
        int children = wallHolder.transform.childCount;
        allWalls = new GameObject[children];
        cells = new Cell[xSize * ySize];
        int eastWestProcess = 0;
        int childProcess = 0;
        int termCount = 0;
        
        for (int i = 0; i < children; i++)
        {
            allWalls[i] = wallHolder.transform.GetChild(i).gameObject;
        }
        for (int cellprocess = 0; cellprocess < cells.Length; cellprocess++)
        {
            cells[cellprocess] = new Cell();
            cells[cellprocess].east = allWalls[eastWestProcess];
            cells[cellprocess].south = allWalls[childProcess + (xSize + 1) * ySize];
            if (termCount == xSize)
            {
                eastWestProcess += 2;
                termCount = 0;
            }
            else
            {
                eastWestProcess++;
            }
            termCount++;
            childProcess++;
            cells[cellprocess].west = allWalls[eastWestProcess];
            cells[cellprocess].north = allWalls[(childProcess + (xSize + 1) * ySize) + xSize - 1];
        }

        CreateMaze();
    }

    void CreateMaze()
    {
        
        if (VisitedCells < TotalCells)
        {            
            if (startedBuilding)
            {                
                GiveMeNeighbour();                
                if (cells[currentNeighbour].visited == false && cells[currentCell].visited == true)
                {                    
                    BreakWall();
                    cells[currentNeighbour].visited = true;
                    VisitedCells++;
                    lastCells.Add(currentCell);
                    currentCell = currentNeighbour;
                    if (lastCells.Count > 0)
                    {
                        backingUp = lastCells.Count - 1;
                    }                    
                }
            }
            else
            {                
                currentCell = Random.Range(0, TotalCells);
                cells[currentCell].visited = true;
                VisitedCells++;
                startedBuilding = true;
                
            }
            CreateMaze();
            // Invoke("CreateMaze", 0.0f);

        }
        //Debug.Log("Finished");
    }
    void BreakWall()
    {
        //Debug.Log("Destroy wall" + wallToBreak);
        switch (wallToBreak)
        {            
            case 1: Destroy(cells[currentCell].north); break;
            case 2: Destroy(cells[currentCell].east); break;
            case 3: Destroy(cells[currentCell].south); break;
            case 4: Destroy(cells[currentCell].west); break;
        }
        

    }
    void GiveMeNeighbour()
    {
        TotalCells = xSize * ySize;
        int length = 0;
        int[] neighbours = new int[4];
        int[] connectingWall = new int[4];
        int check = 0;

        check = ((currentCell + 1) / xSize);
        check -= 1;
        check *= xSize;
        check += ySize;

        //West wall
        if (currentCell + 1 < TotalCells && (currentCell + 1) != check)
        {
            if (cells[currentCell + 1].visited == false)
            {
                neighbours[length] = currentCell + 1;
                connectingWall[length] = 4;
                length++;
            }
        }
        //East wall
        if (currentCell - 1 >= TotalCells && currentCell != check)
        {
            if (cells[currentCell - 1].visited == false)
            {
                neighbours[length] = currentCell - 1;
                connectingWall[length] = 2;
                length++;
            }
        }
        //North wall
        if (currentCell + xSize < TotalCells)
        {
            if (cells[currentCell + xSize].visited == false)
            {
                neighbours[length] = currentCell + xSize;
                connectingWall[length] = 1;
                length++;
            }
        }
        //South wall
        if (currentCell - xSize >= 0)
        {
            if (cells[currentCell - xSize].visited == false)
            {
                neighbours[length] = currentCell - xSize;
                connectingWall[length] = 3;
                length++;
            }
        }
        
        if (length != 0)
        {
            int theChosenOne = Random.Range(0, length);
            currentNeighbour = neighbours[theChosenOne];
            wallToBreak = connectingWall[theChosenOne];
        }
        else
        {
            if (backingUp > 0)
            {
                currentCell = lastCells[backingUp];
                backingUp--;
            }
        }
    }

    void CreateFloor()
    {

        floorSpawn = new Vector3(0.0f, 1.0f, 0.0f);
        floor.transform.localScale = new Vector3(5.0f, 1.0f, 5.0f);

        Instantiate(floor, floorSpawn, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
