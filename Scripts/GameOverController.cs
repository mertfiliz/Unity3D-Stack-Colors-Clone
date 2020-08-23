using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void Restart_Game()
    {
        SceneManager.LoadScene("Game");
        Road_Movement.moving = true;
        PlayerMovement.isLeftMove_Active = true;
        PlayerMovement.isRightMove_Active = true;
    }
}
