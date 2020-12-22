using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleController : MonoBehaviour
{
    Animator m_anim;

    bool m_circleOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        this.transform.localScale = new Vector3(7, 7, 0);
    }

    private void OnDisable()
    {
        //this.transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_circleOpen) return;
        m_anim.Play("MagicCircleRotation");
    }

    /// <summary>
    /// アニメーションイベントから呼ばれる
    /// </summary>
    void MagicCircleOpenStart()
    {
        m_circleOpen = true;
    }

    /// <summary>
    /// アニメーションイベントから呼ばれる
    /// </summary>
    void MagicCircleOpenEnd()
    {
        m_circleOpen = false;
    }
}
