using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandlerJump : MonoBehaviour
{
    public Rigidbody2D player;
    public float multiplier;

    public void Jump(){
        if(player.linearVelocity.y == 0 && !GameManager.gamePaused()){
            player.AddForce(Vector2.up * multiplier);
        }
    }
}
