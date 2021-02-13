using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VcamController : MonoBehaviour
{
    [SerializeField] GameObject m_player = default;
    [SerializeField] GameObject m_freeLookCamera = default;

    /// <summary>このクラスのインスタンスが既にあるかどうかを確認する</summary>
    public static bool m_isExists = false;

    [SerializeField] GameObject m_muzzle = default;

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

    private void Start()
    {
        m_muzzle.transform.position = this.transform.position;
    }

    private void Update()
    {
        
    }
}
