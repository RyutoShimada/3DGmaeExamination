using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartLoad : MonoBehaviour
{
    public void Started()
    {
        Invoke("StartToLoad", 2f);
    }

    public void StartToLoad()
    {
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            SceneManager.LoadScene("StoryScene");
            //GameManager.m_audio.Stop();//音楽を止める
        }

        if (SceneManager.GetActiveScene().name == "StoryScene")
        {
            SceneManager.LoadScene("TutorialScene");
            //GameManager.m_audio.Play();
        }
    }
}
