using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuStart : MonoBehaviour
{
    int readyUp = 0;
    List<Collider2D> cols = new List<Collider2D>();
    Image image;
    public TextMeshProUGUI instructions;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (readyUp >= 2)
        {
            image.color = Color.yellow;
            instructions.text = "Press Select (Space)";
            if (Input.GetButton("Select"))
            {
                SceneManager.LoadScene("Level1");
            }
        }
        else
        {
            image.color = Color.white;
            instructions.text = "Enter the Circle";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball")&& !cols.Contains(collision))
        {
            readyUp++;
            cols.Add(collision);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (cols.Contains(collision))
        {
            cols.Remove(collision);
            readyUp--;
        }
    }
}
