using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Color32 quetzColor, catColor;
    public Vector2 score = new Vector2(0,0);
    public bool startTimer = false;

    Vector2 wins = new Vector2(0, 0);

    int levelNumber = 1;
    bool suddenDeath = false;
    LevelManager levelManager;

    float timeRemaining = 120f;
    //Awake is called when the object is activated, before when the scene is loaded
    private void Awake()
    {
        //Checks to see if there is already a GameManager in Scene
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        levelManager.timerText.text = (int)(timeRemaining / 60) + ":";
        if ((int)((timeRemaining % 60) - 1) < 10)
            levelManager.timerText.text += "0";
        levelManager.timerText.text += (int)((timeRemaining % 60) - 1);
        StartCoroutine(StartRound());
    }

    private void Update()
    {
        if (startTimer)
        {
            timeRemaining -= Time.deltaTime;
            levelManager.timerText.text = (int)(timeRemaining / 60) + ":";
            if ((int)((timeRemaining % 60) - 1) < 10)
                levelManager.timerText.text += "0";
            levelManager.timerText.text += (int)((timeRemaining % 60) - 1);
            if (timeRemaining <= 0 && !suddenDeath)
            {
                levelManager.goalText.gameObject.SetActive(true);
                if (score.x > score.y)
                {
                    levelManager.goalText.color = quetzColor;
                    levelManager.goalText.text = "ROUND WON";
                    wins.x++;
                    CheckWin();
                }
                else if(score.x< score.y)
                {
                    levelManager.goalText.color = catColor;
                    levelManager.goalText.text = "ROUND WON";
                    wins.y++;
                    CheckWin();
                }
                else
                {
                    levelManager.goalText.color = Color.white;
                    levelManager.goalText.text = "SUDDEN DEATH";
                    StartCoroutine(ResetDelay());
                }
            }
        }
    }

    void CheckWin()
    {
        if (wins.x >= 2 || wins.y >= 2)
        {
            Debug.Log("Won");
        }
        else
        {
             StartCoroutine(NewLevelDelay());
        }


    }
    public void AddScore(int whoScored)
    {
        if (suddenDeath)
        {
            
            return;
        }
        if (whoScored == 1)
        {
            levelManager.goalText.color = quetzColor;
            levelManager.goalText.text = "GOAL";
            levelManager.goalText.gameObject.SetActive(true);
            score.x++;
            levelManager.quetzScore.text = score.x.ToString();
        }
        else
        {
            levelManager.goalText.color = catColor;
            levelManager.goalText.text = "GOAL";
            levelManager.goalText.gameObject.SetActive(true);
            score.y++;
            levelManager.catScore.text = score.y.ToString();
        }
     
            //reset
            StartCoroutine(ResetDelay());
    }

    IEnumerator ResetDelay()
    {
        
        yield return new WaitForSeconds(4f);
        //levelManager.NewRound();
        //StartCoroutine(StartRound());
        levelManager.goalText.gameObject.SetActive(false);
    }
    public IEnumerator StartRound()
    {
        levelManager.goalText.gameObject.SetActive(true);
        levelManager.goalText.color = Color.white;
        levelManager.goalText.text = "READY";
        yield return new WaitForSeconds(2.5f);
        levelManager.goalText.text = "START";
        yield return new WaitForSeconds(0.75f);
        levelManager.goalText.gameObject.SetActive(false);
        startTimer = true;
        levelManager.quetz.start = true;
        levelManager.cat.start = true;
        levelManager.ball.start = true;

    }
    IEnumerator NewLevelDelay()
    {
        startTimer = false;
        suddenDeath = false;
        yield return new WaitForSeconds(4f);
        levelNumber++;
        score = Vector2.zero;
        timeRemaining = 120f;
        SceneManager.LoadScene("Level" + levelNumber.ToString());
        yield return new WaitForSeconds(1f);
        levelManager = FindObjectOfType<LevelManager>();
        levelManager.catScore.text = score.y.ToString();
        levelManager.quetzScore.text = score.x.ToString();
    }

}
