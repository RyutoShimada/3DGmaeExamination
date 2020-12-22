﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VcamController : MonoBehaviour
{
    [SerializeField] GameObject m_player = default;

    

    /// <summary>このクラスのインスタンスが既にあるかどうかを確認する</summary>
    public static bool m_isExists = false;

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

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
