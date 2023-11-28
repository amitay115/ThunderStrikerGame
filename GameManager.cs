using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float timeValue = 90;
    public TextMeshProUGUI timerText;
    public float ScoreByTime;

    int score = 0;
    int lives = 3;
    int Shield = 0;
    int Thunder = 0;

    bool gameOver = false, sheild = false, hit = false, fastStrike = false;

    public AudioClip[] clips;
    AudioSource _source;

    public static GameManager instance;

    //public GameObject livesHolder;

    //public GameObject gameOverPanel;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI shieldText;
    public TextMeshProUGUI thunderText;
    public GameObject gameOverLogo;
    public GameObject gameOverPanel;
    public GameObject shieldForce;
    public GameObject hitForce;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        _source = GetComponent<AudioSource>();
        livesText.text = lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
        }
        DisplayTime(timeValue);
        if(sheild==false)
        {
            shieldForce.SetActive(false);
        }

    }

    public void DecreaseLives()
    {
        hitForce.SetActive(true);
        StartCoroutine("HitTime");
        if (sheild == true)
        {
            sheild = false;
            shieldForce.SetActive(false);
        }

        else
        {
            if (lives > 0)
            {
                lives--;
                //print(lives);
                _source.PlayOneShot(clips[0]);
                livesText.text = lives.ToString();


            }

            if (lives <= 0)
            {

                GameOver();
            }
        }
    }
    public void IncrementLives()
    {
        lives++;
        livesText.text = lives.ToString();

    }
    public void IncrementShield()
    {
        Shield++;
        shieldText.text = Shield.ToString();

    }
    public void IncrementThunder()
    {
        Thunder++;
        thunderText.text = Thunder.ToString();

    }
    public void GameOver()
    {
        PlayerController.main.Dead();
        //CandySpawner.instance.StopSpawningCandies();
        //GameObject.Find("Player").GetComponent<PlayerController>().canMove = false;
        gameOverPanel.SetActive(true);
        gameOverLogo.SetActive(true);

        print("Game Over");
    }

    public void IncrementScore()
    {
        if (!gameOver)
        {
            score++;
            scoreText.text = ((int)score).ToString("0000");

            print(score);
        }


    }
    public void AddShield()
    {
        if (Shield > 0)
        {
            shieldForce.SetActive(true);
            sheild = true;
            Shield--;
            shieldText.text = Shield.ToString();
            StartCoroutine("ShieldTime");

        }
    }
    public void AddFastStrike()
    {
        if (Thunder > 0)
        {
            //shieldForce.SetActive(true);
            fastStrike = true;
            Thunder--;
            thunderText.text = Thunder.ToString();
            StartCoroutine("ThunderTime");
            //Bullet.instance.AddFastStrikes();
            //Particles.part.AddFastStrikesParticles();
            //Beam.beam.AddFastStrikesBeam();
        }

        
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }


    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay <0)
        {
            timeToDisplay = 0;
        }

        float minute = Mathf.FloorToInt(timeToDisplay / 60);
        float second = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minute, second);
    }
    IEnumerator ShieldTime()
    {
        
        yield return new WaitForSeconds(18f);
        sheild = false;

    }
    IEnumerator HitTime()
    {

        yield return new WaitForSeconds(2f);
        hitForce.SetActive(false);

    }
    IEnumerator ThunderTime()
    {

        yield return new WaitForSeconds(18f);
        fastStrike = false;

    }
}
