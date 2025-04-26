using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public GameObject fireballToDestroy;

    public void DestroyMe(){
        Destroy(gameObject);
    }

    public void DestroyFireball(){
        fireballToDestroy.transform.DetachChildren();
        Destroy(fireballToDestroy);
    }

    public void SetFireballToDestroy(GameObject fireball){
        fireballToDestroy = fireball;
    }
}
