using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PointsManager : MonoBehaviour
{

    public Text pointsText;
    public float points;
    public Text highScoreText;
    private float highScore;

    // Start is called before the first frame update
    void Start()
    {
        float tmp = PlayerPrefs.GetFloat("HighScore", -1);
        if(tmp != -1){
            highScore = tmp;
        }else{
            highScore = 0;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }
        highScoreText.text = "High Score: " + highScore.ToString();
        pointsText.text = "0";
        points = 0;
    }

    void Update(){
        if(GameManager.gameOver){
            if(highScore < points){
                PlayerPrefs.SetFloat("HighScore", points);
            }
            if(Input.touchCount > 0){
                SceneManager.LoadScene("SampleScene");
                GameManager.ResumeGame();
                GameManager.SetGameOver(false);
                AudioListener.pause = false;
            }
        }
    }

    public void addPoints(float p){
        points += p;
        pointsText.text = points.ToString();
    }
}
