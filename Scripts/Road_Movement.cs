using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road_Movement : MonoBehaviour
{
    public GameObject Road;
    public static bool moving = true;

    void Update()
    {
        if(moving)
        {
            Road.transform.position = new Vector3(Road.transform.position.x, Road.transform.position.y, Road.transform.position.z - 0.12f);
        }
        
    }
}
