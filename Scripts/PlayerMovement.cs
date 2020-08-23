using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static bool isLeftMove_Active = true;
    public static bool isRightMove_Active = true;

    public bool move_left, move_right;

    void Start()
    {
        Application.targetFrameRate = 100;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    
    void Update()
    {
        // FOR PC
        if (Input.GetKey(KeyCode.A) && isLeftMove_Active)
        {
            this.transform.position = new Vector3(this.transform.position.x - 0.1f, this.transform.position.y, this.transform.position.z);
        }
        if (Input.GetKey(KeyCode.D) && isRightMove_Active)
        {
            this.transform.position = new Vector3(this.transform.position.x + 0.1f, this.transform.position.y, this.transform.position.z);
        }

        // FOR MOBILE
        if (move_left && isLeftMove_Active)
        {
            this.transform.position = new Vector3(this.transform.position.x - 0.1f, this.transform.position.y, this.transform.position.z);
        }
        if (move_right && isRightMove_Active)
        {
            this.transform.position = new Vector3(this.transform.position.x + 0.1f, this.transform.position.y, this.transform.position.z);
        }
    }

    public void MoveLeft()
    {
        move_left = true;
    }

    public void MoveRight()
    {
        move_right = true;
    }

    public void Disable_MoveLeft()
    {
        move_left = false;
    }

    public void Disable_MoveRight()
    {
        move_right = false;
    }
}
