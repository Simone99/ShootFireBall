using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballsHandler : MonoBehaviour
{

    public GameObject fireball;
    public GameObject player;
    public SpriteRenderer background;
    public float miniSpeed, mediumSpeed, gigaSpeed, miniLifePoints, mediumLifePoints, gigaLifePoints, miniRewardPoints, mediumRewardPoints, gigaRewardPoints, startTimeMini, startTimeMedium, startTimeGiga;
    private PointsManager pm;
    public float spawningDistance;
    

    // Start is called before the first frame update
    void Start()
    {
        fireball.SetActive(false);
        startTimeMini = Time.time;
        startTimeMedium = Time.time;
        startTimeGiga = Time.time;
        pm = Camera.main.GetComponent<PointsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTimeMini >= 5){
            startTimeMini = Time.time;
            miniSpawner();
        }
        if(Time.time - startTimeMedium >= 10 && pm.points >= 180){
            startTimeMedium = Time.time;
            mediumSpawner();
        }
        if(Time.time - startTimeGiga >= 60 && pm.points >= 500){
            startTimeGiga = Time.time;
            gigaSpawner();
        }
    }

    public void miniSpawner(){
        GameObject tmp = generateAndActive();
        tmp.GetComponent<Rigidbody2D>().linearVelocity *= miniSpeed;
        tmp.GetComponent<FireballController>().setLifePoints(miniLifePoints);
        tmp.GetComponent<FireballController>().setRewardPoints(miniRewardPoints);
    }

    public void mediumSpawner(){
        GameObject tmp = generateAndActive();
        tmp.GetComponent<Rigidbody2D>().linearVelocity *= mediumSpeed;
        tmp.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        tmp.GetComponent<FireballController>().setLifePoints(mediumLifePoints);
        tmp.GetComponent<FireballController>().setRewardPoints(mediumRewardPoints);
    }

    public void gigaSpawner(){
        GameObject tmp = generateAndActive();
        tmp.GetComponent<Rigidbody2D>().linearVelocity *= gigaSpeed;
        tmp.transform.localScale = new Vector3(0.9f, 0.9f, 1);
        tmp.GetComponent<FireballController>().setLifePoints(gigaLifePoints);
        tmp.GetComponent<FireballController>().setRewardPoints(gigaRewardPoints);
    }

    public Vector2 randomPosition(){
        Vector2 tmp;
        do{
            tmp = new Vector2(Random.Range(-background.bounds.extents.x, background.bounds.extents.x), Random.Range(-background.bounds.extents.y, background.bounds.extents.y));
        }while((tmp - (Vector2)player.transform.position).magnitude <= spawningDistance);
        return tmp;
    }

    private GameObject generateAndActive(){
        Vector2 fireballPosition = randomPosition();
        Vector2 newVelocity = (Vector2)player.transform.position - fireballPosition;
        GameObject tmp = Instantiate(fireball, fireballPosition, Quaternion.Euler(0, 0, Vector2.SignedAngle(new Vector2(-1,-1), newVelocity)));
        tmp.SetActive(true);
        tmp.transform.GetChild(0).gameObject.SetActive(false);
        tmp.GetComponent<Rigidbody2D>().linearVelocity = newVelocity.normalized;
        return tmp;
    }
}
