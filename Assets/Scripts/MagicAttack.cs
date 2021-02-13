using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    [SerializeField] float m_speed = 10f;
    [SerializeField] float m_interval = 5f;
    [SerializeField] GameObject m_muzzle = default;
    [SerializeField] GameObject m_explosionEffect = default;

    [SerializeField] GameObject m_player = default;
    PlayerController m_playerController;

    float time;

    Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        m_playerController = m_player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //向きを決めたい
        //dir = m_playerController.hit.point + m_muzzle.transform.position;

        //魔法の弾を発射
        this.transform.position += Attacking(m_muzzle.transform.forward, m_speed);

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
        }
        gameObject.SetActive(false);
    }
}
