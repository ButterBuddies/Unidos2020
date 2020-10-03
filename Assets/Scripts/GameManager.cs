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

    AudioSource source;

    public AudioClip arena, fast;
    public AudioClip[] catWin;
    public AudioClip[] catLose;
    public AudioClip[] quetzWin;
    public AudioClip[] quetzLose;
    public AudioClip crowdCheer;

    [SerializeField]
    int levelNumber = 1;
    bool suddenDeath = false;
    LevelManager levelManager;

    float timeRemaining = 90f;
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
        source = GetComponent<AudioSource>();
        source.clip = arena;
        source.Play();
        levelManager.timerText.text = (int)(timeRemaining / 60) + ":";
        if ((int)((timeRemaining % 60) - 1) < 10)
            levelManager.timerText.text += "0";
        levelManager.timerText.text += (int)((timeRemaining % 60) - 1);
        levelManager.splashAnim.SetBool("Action", true);
        levelManager.splashAnim.Play("Start");
        StartCoroutine(IntroDelay());
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
                    source.clip = fast;
                    source.Play();
                    suddenDeath = true;
                    levelManager.goalText.color = Color.white;
                    levelManager.goalText.text = "SUDDEN DEATH";
                    StartCoroutine(ResetDelay());
                }
            }
        }
    }

    void CheckWin()
    {
        Results();
        if (wins.x >= 2 || wins.y >= 2)
        {

            StartCoroutine(WinDelay());
        }
        else
        {

             StartCoroutine(NewLevelDelay());
        }


    }

    void Results()
    {
        startTimer = false;
        suddenDeath = false;
        if (score.x > score.y)
        {
            levelManager.splashAnim.SetBool("CatWin", false);
            levelManager.quip.volume = 0.9f;
            levelManager.quip.pitch = 1f;
            levelManager.quip.PlayOneShot(quetzWin[Random.Range(0, quetzWin.Length)]);
        }
        else
        {
            levelManager.splashAnim.SetBool("CatWin", true);
            levelManager.quip.volume = 0.9f;
            levelManager.quip.pitch = 1f;
            levelManager.quip.PlayOneShot(catWin[Random.Range(0, catWin.Length)]);
        }
        levelManager.splashAnim.SetBool("Action", false);
        levelManager.splashAnim.Play("Start");
    }
    public void AddScore(int whoScored)
    {
        if (suddenDeath)
        {
            suddenDeath = false;
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
        levelManager.quip.volume = 0.65f;
        levelManager.quip.pitch = Random.Range(0.8f, 1f);
        levelManager.quip.PlayOneShot(crowdCheer);
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

    IEnumerator IntroDelay()
    {
        yield return new WaitForSeconds(4f);
        StartCoroutine(StartRound());
    }

    public IEnumerator StartRound()
    {
        levelManager.goalText.gameObject.SetActive(true);
        levelManager.goalText.color = Color.white;
        levelManager.goalText.text = "READY";
        yield return new WaitForSeconds(1f);
        levelManager.goalText.text = "START";
        yield return new WaitForSeconds(0.75f);
        levelManager.goalText.gameObject.SetActive(false);
        startTimer = true;
        levelManager.quetz.start = true;
        levelManager.cat.start = true;
        levelManager.ball.start = true;

    }

    IEnumerator WinDelay()
    {
        yield return new WaitForSeconds(3f);
        if (wins.x < wins.y)
        {
            SceneManager.LoadScene("WinCat");
        }
        else
        {
            SceneManager.LoadScene("WinQuetz");
        }
    }
    IEnumerator NewLevelDelay()
    {
        
        yield return new WaitForSeconds(4f);
        levelNumber++;
        score = Vector2.zero;
        timeRemaining = 90f;
        SceneManager.LoadScene("Level" + levelNumber.ToString());
        yield return new WaitForSeconds(1f);
        if (levelNumber != 3)
        {
            if (source.clip != arena)
            {
                source.clip = arena;
                source.Play();
            }
        }
        else
        {
            source.clip = fast;
            source.Play();
        }
        levelManager = FindObjectOfType<LevelManager>();
        levelManager.catScore.text = score.y.ToString();
        levelManager.quetzScore.text = score.x.ToString();
        StartCoroutine(StartRound());
    }

}
