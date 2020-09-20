using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoalScript : MonoBehaviour
{
    public TextMeshProUGUI goalText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            goalText.gameObject.SetActive(true);
        }

    }
}
