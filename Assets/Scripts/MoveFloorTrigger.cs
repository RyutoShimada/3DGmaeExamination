using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloorTrigger : MonoBehaviour
{
    [SerializeField] GameObject m_MoveFloor = default;
    //[SerializeField] GameObject m_FireEffect = default;
    [SerializeField] ParticleSystem m_FireEffect = default;
    MoveFloorController m_moveFloorController;

    private void Start()
    {
        m_FireEffect.Stop();//注意(発見した):このメソッドはParticleSystemのPlayerOnAwakeのチェックを外さないと使えない
        if (!m_MoveFloor) return;//MoveFloorがない時は以下の処理をしない
        m_moveFloorController = m_MoveFloor.gameObject.GetComponent<MoveFloorController>();
    }

    /// <summary>
    /// エフェクトを出す
    /// </summary>
    void OnEffect()
    {
        //m_FireEffect.SetActive(true);
        m_FireEffect.Play();
        //Debug.Log("START");
    }

    /// <summary>
    /// エフェクトを消す
    /// </summary>
    public void UnEffect()//外から呼ぶ
    {
        //m_FireEffect.SetActive(false);
        m_FireEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);//うっすらと消えていく
        //Debug.Log("STOP");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MagicBullet")
        {
            OnEffect();
            if (!m_MoveFloor) return;//MoveFloorがない時は以下の処理をしない
            m_moveFloorController.Move();
        }   
    }
}
