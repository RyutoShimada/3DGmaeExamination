using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartLoad : MonoBehaviour
{
    public static void StartToLoad()
    {
        if (SceneManager.GetActiveScene().name == "TitleSceane")
            SceneManager.LoadScene("StoryScene");

        if (SceneManager.GetActiveScene().name == "StoryScene")
            SceneManager.LoadScene("TutorialSceane");
    }
}
