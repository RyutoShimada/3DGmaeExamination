using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] GameObject m_model = null;
    [SerializeField] Transform m_target;
    [SerializeField] float m_time = 3;
    [SerializeField] float m_timing = 5f;
    Animator m_anim;
    GameObject m_textObj;
    Text m_text;

    private void Start()
    {
        m_anim = m_model.GetComponent<Animator>();
        m_textObj = GameObject.FindGameObjectWithTag("Text");
        m_text = m_textObj.GetComponent<Text>();
        m_text.enabled = false;
        Invoke("ModelMove", m_timing);
    }

    public void ModelMove()
    {
        m_model.transform.DOMove(m_target.position, m_time);
        m_anim.Play("Run");
    }

    public void StartLoad()
    {
        if (SceneManager.GetActiveScene().name == "EndScene")
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
