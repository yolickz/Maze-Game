using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [System.Serializable]
    public class Cell
    {
        public bool visited;
        public GameObject north;
        public GameObject east;
        public GameObject south;
        public GameObject west;
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
        }
        //For horizontal walls
        for (int i = 0; i <= ySize; i++)
        {
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
            if(termCount == xSize)
            {
                eastWestProcess += 2;
                termCount = 0;
            }
            else
            {
                eastWestProcess ++;
            }
            termCount++;
            childProcess++;
            cells[cellprocess].west = allWalls[eastWestProcess];
            cells[cellprocess].north = allWalls[(childProcess + (xSize + 1) * ySize)+xSize-1];
        }
        CreateMaze();
    }

    void CreateMaze()
    {
        GiveMeNeighbour();
    }
    void GiveMeNeighbour()
    {
        TotalCells = xSize * ySize;
        int length = 0;
        int[] neighbours = new int[4];
        int check = 0;
        
        check = ((currentCell+1)/xSize);
        check -= 1;
        check *= xSize;
        check += ySize;
        //West wall
        if (currentCell + 1 < TotalCells && (currentCell + 1) != check)
        {
            if (cells[currentCell + 1].visited == false)
            {
                neighbours[length] = currentCell + 1;
                length++;
            }                    
        }
        //East wall
        if (currentCell - 1 >= TotalCells && (currentCell + 1) != check)
        {
            if (cells[currentCell - 1].visited == false)
            {
                neighbours[length] = currentCell - 1;
                length++;
            }
        }
        //North wall
        if (currentCell + xSize< TotalCells)
        {
            if (cells[currentCell + xSize].visited == false)
            {
                neighbours[length] = currentCell + xSize;
                length++;
            }
        }
        //South wall
        if (currentCell - xSize >= 0)
        {
            if (cells[currentCell - xSize].visited == false)
            {
                neighbours[length] = currentCell - xSize;
                length++;
            }
        }
        for (int i = 0; i < length; i++)
        {
            Debug.Log(neighbours[i]);
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
