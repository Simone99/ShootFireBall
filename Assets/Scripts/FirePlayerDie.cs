using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlayerDie : MonoBehaviour
{
    public GameObject player;

    public void DestroyPlayer(){
        Destroy(player);
    }

    public void DestroyMe(){
        AudioListener.pause = true;
        GameManager.SetGameOver(true);
        Destroy(gameObject);
    }
}
