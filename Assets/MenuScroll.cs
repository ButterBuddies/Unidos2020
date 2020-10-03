using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScroll : MonoBehaviour
{
    [SerializeField]
    float idleTime = 0;
    bool running = false;
    public GameObject scrollPanel;

    // Update is called once per frame
    void Update()
    {
        idleTime += Time.deltaTime;

        if (Input.anyKey)
        {
            idleTime = 0;
            if (running)
            {
                StopCoroutine(ScrollTimer());
                scrollPanel.SetActive(false);
            }

        }
        if (idleTime > 10 && !running)
        {
            StartCoroutine(ScrollTimer());
        }
    }

    IEnumerator ScrollTimer()
    {
        running = true;
        scrollPanel.SetActive(true);
        scrollPanel.GetComponent<Animator>().Play("MenuScroll");
        yield return new WaitForSeconds(28f);
        scrollPanel.SetActive(false);
        idleTime = 0;
        running = false;
    }
}
