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
    public Cell[] cells;
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
            cells[cellprocess].south = allWalls[children + (xSize + 1) * ySize];
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
            cells[cellprocess].south = allWalls[(childProcess + (xSize + 1) * ySize)+xSize+1];
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
