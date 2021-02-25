using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartLoad : MonoBehaviour
{
    public void StartToLoad()
    {
        SceneManager.LoadScene("TutorialSceane");
    }
}
