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
    private int = 
    // Use this for initialization
    void Start ()
    {
        createWalls();
	}
    void createWalls()
    {
        initialPos = new Vector3((-xSize / 2) + wallLength/2, 0.0f, (-ySize/2)+ wallLength/2);
        Vector3 Mypos = initialPos;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
