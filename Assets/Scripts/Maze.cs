using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public GameObject wall;
    public float wallLength = 1.0f;
    public float xSize = 5.0f;
    public float ySize = 5.0f;
    private Vector3 initialPos;
    
    // Use this for initialization
    void Start ()
    {
        createWalls();
	}

    void createWalls()
    {
        initialPos = new Vector3((-xSize / 2) + wallLength/2, 0.0f, (-ySize/2)+ wallLength/2);
        Vector3 MyPos = initialPos;
        for (int i = 0; i < ySize; i++)
        {
            for(int j = 0; j<= xSize; j++)
            {
                MyPos = new Vector3(initialPos.x + (j * wallLength) - wallLength / 2, 0.0f, initialPos.z + (j * wallLength) - wallLength / 2);
                Instantiate(wall, MyPos, Quaternion.identity);
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
