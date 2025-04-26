using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage;
    public GameObject fireCollision;

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "background"){
            Destroy(gameObject);
        }else if(collision.gameObject.tag == "fireball"){
            collision.gameObject.GetComponent<FireballController>().setDamage(damage);
            GameObject tmp = Instantiate(fireCollision, gameObject.transform.position, Quaternion.Euler(0, 0, -Vector2.SignedAngle(collision.GetContact(0).normal, Vector2.right)));
            tmp.SetActive(true);
            tmp.GetComponentInChildren<Rigidbody2D>().linearVelocity = collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity;
            Destroy(gameObject);
        }
    }
}
