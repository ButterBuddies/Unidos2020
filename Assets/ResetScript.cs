using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScript : MonoBehaviour
{

    public void OnClick()
    {
        if(GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
        }
            SceneManager.LoadScene(0);
    }
}
