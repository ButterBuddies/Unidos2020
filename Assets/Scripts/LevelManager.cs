using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI goalText;
    public TextMeshProUGUI quetzScore, catScore;
    public PlayerMovement quetz, cat;
    public Transform startPos1, startPos2, ballStart;
    public SunBall ball;
    public Animator splashAnim;
    public AudioSource quip;

    public void NewLevel()
    {
        quetz.transform.position = startPos1.position;
        cat.transform.position = startPos2.position;
        ball.transform.position = ballStart.position;
        quetz.start = false;
        cat.start = false;
        ball.start = false;
        goalText.gameObject.SetActive(false);
    }

    public void NewRound()
    {
        ball.transform.position = ballStart.position;
        goalText.gameObject.SetActive(false);
    }
}
