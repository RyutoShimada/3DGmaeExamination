using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttack : MonoBehaviour
{
    /// <summary>弾の速度</summary>
    [SerializeField] float m_speed = 10f;
    /// <summary>弾のインターバル</summary>
    [SerializeField] float m_interval = 5f;
    /// <summary>爆発のエフェクト</summary>
    [SerializeField] GameObject m_explosionEffect = default;
    float time;
    Vector3 dir;

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Attacking(dir, m_speed);//魔法の弾を発射
        if (m_interval < time)//一定時間経過すると非アクティブになる
        {
            unActive();
        }
        time += Time.deltaTime;//時間を計測
    }

    /// <summary>
    /// 非アクティブにする
    /// </summary>
    private void unActive()
    {
        gameObject.SetActive(false);
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

    /// <summary>
    /// 弾発射時の方向を保管し、時間計測をリセットする
    /// </summary>
    /// <param name="direction">方向</param>
    public void OnFire(Vector3 direction)
    {
        dir = direction;
        time = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!m_explosionEffect) return;//エフェクトがないなら実行しない
        Instantiate(m_explosionEffect, this.transform.position, this.transform.rotation);
        unActive();
    }
}
