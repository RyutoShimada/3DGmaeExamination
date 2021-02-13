using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>このクラスのインスタンスが既にあるかどうかを確認する</summary>
    static bool m_isExists = false;

    private void Awake()
    {
        if (m_isExists)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_isExists = true;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Update()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
