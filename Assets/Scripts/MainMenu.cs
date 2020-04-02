using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayBtn()
    {
        Debug.Log("Play Btn");
        SceneManager.LoadScene("Level_1", LoadSceneMode.Single);
    }

    public void ContinueBtn()
    {
        Debug.Log("Continue Btn");
    }
}