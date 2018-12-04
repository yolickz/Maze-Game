using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    Maze MAZE = new Maze();
    private Vector3 newCam;
    
    // Use this for initialization
    void Start ()
    {
        newCam = new Vector3(MAZE.xSize / 2, MAZE.xSize+ MAZE.ySize , MAZE.ySize / 2);
        Vector3 Cam = newCam;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
