using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public GameObject wall;
    public GameObject floor;
    public float wallLength = 1.0f;
    public float xSize;
    public float ySize;
    private Vector3 initialPos;
    private Vector3 floorSpawn;

    // Use this for initialization
    void Start()
    {
        createWalls();
        createFloor();
    }

    void createWalls()
    {
        initialPos = new Vector3((-xSize / 2) + wallLength / 2, 0.0f, (-ySize / 2) + wallLength / 2);
        Vector3 MyPos = initialPos;
        GameObject tempWall;

        //For vertical walls
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j <= xSize; j++)
            {
                MyPos = new Vector3(initialPos.x + (j * wallLength) - wallLength / 2, 1f, initialPos.z + (i * wallLength) - wallLength / 2);
                Instantiate(wall, MyPos, Quaternion.identity);
            }
        }
        //For horizontal walls
        for (int i = 0; i <= ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                MyPos = new Vector3(initialPos.x + (j * wallLength), 1f, initialPos.z + (i * wallLength) - wallLength);
                tempWall = Instantiate(wall, MyPos, Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;
            }
        }
    }
    void createFloor()
    {

        floorSpawn = new Vector3(0.0f,1.0f,0.0f);
        floor.transform.localScale = new Vector3(5.0f,1.0f,5.0f);

        Instantiate(floor, floorSpawn, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
