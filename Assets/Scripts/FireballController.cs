using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{

    public float lifePoints;
    public float rewardPoints;
    public GameObject player;
    public float playerFireballDistance;
    private new AudioSource audio;
    private bool isGonnaDie;


    // Start is called before the first frame update
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        isGonnaDie = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(lifePoints <= 0 && !isGonnaDie){
            Camera.main.GetComponent<PointsManager>().addPoints(rewardPoints);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.GetComponent<ExplosionController>().SetFireballToDestroy(gameObject);
            isGonnaDie = true;
        }
        if(player != null){
            if((gameObject.transform.position - player.transform.position).magnitude <= playerFireballDistance && !audio.isPlaying && !GameManager.gamePaused()){
                audio.Play();
            }else if((gameObject.transform.position - player.transform.position).magnitude > playerFireballDistance && audio.isPlaying && !GameManager.gamePaused()){
                audio.Pause();
            }
        }
    }

    public void setLifePoints(float points){
        lifePoints = points;
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "background"){
            Destroy(gameObject);
        }else if(collision.gameObject.tag == "Player"){
            audio.Pause();
        }
    }

    public void setDamage(float damage){
        lifePoints -= damage;
    }

    public void setRewardPoints(float points){
        rewardPoints = points;
    }
}
