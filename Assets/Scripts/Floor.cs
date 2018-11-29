using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    public GameObject floor;
    private Vector3 floorSpawn;
    public float sizeX;
    public float sizeZ;

    // Use this for initialization
    void Start()
    {
        floorSpawn = new Vector3(0.0f, 0.0f, -0.5f);
        floor.transform.localScale = new Vector3(sizeX, 0.0F, sizeZ);
        
        Instantiate(floor, floorSpawn, Quaternion.identity);


    }

    // Update is called once per frame
    void Update()
    {

    }
}
