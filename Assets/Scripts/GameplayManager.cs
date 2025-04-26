using UnityEngine;
using UnityEngine.InputSystem;

public class GameplayManager : MonoBehaviour
{
    public void PauseResume(InputAction.CallbackContext context){
        if (context.phase == InputActionPhase.Started){
            if(GameManager.gamePaused()){
                GameManager.ResumeGame();
                AudioListener.pause = false;
            }else{
                GameManager.PauseGame();
                AudioListener.pause = true;
            }
        }
    }
}
