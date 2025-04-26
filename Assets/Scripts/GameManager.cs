using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager : object
{
    public static bool gameOver = false;

    public static void PauseGame(){
        Time.timeScale = 0;
    }

    public static void SetGameOver(bool g){
        gameOver = g;
    }

    public static void ResumeGame(){
        Time.timeScale = 1;
    }

    public static bool gamePaused(){
        return Time.timeScale == 0;
    }
}
