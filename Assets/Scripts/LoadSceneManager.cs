using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    GameManager m_GM;
    /// <summary>現在のシーン（読み取り専用）</summary>
    public static Scene currentScene
    { get { return currentScene; } private set { } }


    private void Start()
    {
        m_GM = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// シーンをロードする時に呼ぶ関数
    /// </summary>
    public void StartLoadScene()
    {
        if (SceneManager.GetActiveScene().name == "Stage1")
        {
            SceneManager.sceneLoaded -= SceneLoaded;
            SceneManager.sceneLoaded += EndSceneLoaded;
        }
        else
        {
            SceneManager.sceneLoaded -= EndSceneLoaded;
            SceneManager.sceneLoaded += SceneLoaded;
        }

        currentScene = SceneManager.GetActiveScene();

        // シーンの読み込み
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            SceneManager.LoadScene("StoryScene");
        }
        else if (SceneManager.GetActiveScene().name == "StoryScene")
        {
            SceneManager.LoadScene("TutorialScene");
        }
        else if (SceneManager.GetActiveScene().name == "TutorialScene")
        {
            SceneManager.LoadScene("Stage1");
        }
        else if (SceneManager.GetActiveScene().name == "Stage1")
        {
            SceneManager.LoadScene("EndScene");
        }
        else if (SceneManager.GetActiveScene().name == "EndScene")
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    /// <summary>
    /// シーンがロードされたら必ずやる処理
    /// </summary>
    /// <param name="nextScene"></param>
    /// <param name="mode"></param>
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        m_GM.SceneLoaded();
    }

    /// <summary>
    /// EndSceneだったらやる処理
    /// </summary>
    /// <param name="nextScene"></param>
    /// <param name="mode"></param>
    void EndSceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        m_GM.EndSceneLoaded();
    }
}
