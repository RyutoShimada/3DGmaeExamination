using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    [SerializeField] float m_speed = 10f;
    [SerializeField] float m_interval = 5f;
    [SerializeField] float m_impulsePower = 10f;
    [SerializeField] int m_impulseDirection = 5;
    [SerializeField] GameObject m_muzzle = default;
    [SerializeField] GameObject m_explosionEffect = default;
    
    Vector3 randomV3;
    float time;
    /// <summary>発射する方向</summary>
    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //muzzleの向きをカメラの向きにする
        m_muzzle.transform.rotation = Camera.main.transform.rotation;

        //muzzleの正面を取得
        dir = m_muzzle.transform.forward;

        //魔法の弾を発射
        this.transform.position += Attacking(dir, m_speed);

        unActive();

        time += Time.deltaTime;
    }

    /// <summary>
    /// 一定時間経過すると非アクティブになる
    /// </summary>
    private void unActive()
    {
        if (m_interval < time)
        {
            time = 0;
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 攻撃する
    /// </summary>
    /// <param name="dir">向き</param>
    /// <param name="speed">速さ</param>
    /// <returns>攻撃する方向と速度</returns>
    private Vector3 Attacking(Vector3 dir, float speed)
    {
        return dir.normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block")
        {
            if (m_explosionEffect)
            {
                Instantiate(m_explosionEffect, this.transform.position, this.transform.rotation);
            }
            gameObject.SetActive(false);
        }
    }
}
