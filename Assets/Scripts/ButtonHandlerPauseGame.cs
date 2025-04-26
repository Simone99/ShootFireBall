using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandlerPauseGame : MonoBehaviour
{
    public Text buttonText;

    public void Pause(){
        if(GameManager.gamePaused()){
            GameManager.ResumeGame();
            AudioListener.pause = false;
            buttonText.text = "Pause";
        }else{
            GameManager.PauseGame();
            AudioListener.pause = true;
            buttonText.text = "Resume";
        }
    }

}
