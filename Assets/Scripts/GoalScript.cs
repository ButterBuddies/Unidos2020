using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GoalScript : MonoBehaviour
{
    public TextMeshProUGUI goalText;
    public int lastHit;
    GameManager manager;
    bool canScore = true;

    private void Start()
    {
        manager = GameManager.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball")&&canScore)
        {
            manager.AddScore(lastHit);
            StartCoroutine(ScoreCooldown());
        }

    }

    IEnumerator ScoreCooldown()
    {
        canScore = false;
        yield return new WaitForSeconds(2f);
        canScore = true;
    }
}
