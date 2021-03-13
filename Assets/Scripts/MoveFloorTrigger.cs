using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoveFloorTrigger : MonoBehaviour
{
    [SerializeField] ParticleSystem m_fireEffect = null;
    [SerializeField] GameObject m_moveFloor = null;
    [SerializeField] MoveFloorDoTweenVer m_moveFloorDoTween = null;
    [SerializeField] Transform[] m_targets = null;

    Transform m_initialPosition = null;
    Transform m_nextPosition = null;
    int m_index = 0;
    bool m_reverse = false;

    private void Start()
    {
        m_fireEffect.Stop();//注意(発見した):このメソッドはParticleSystemのPlayerOnAwakeのチェックを外さないと使えない
    }

    /// <summary>
    /// エフェクトを出す
    /// </summary>
    private void OnEffect()
    {
        m_fireEffect.Play();
    }

    /// <summary>
    /// エフェクトを消す
    /// </summary>
    public void UnEffect()
    {
        m_fireEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);//うっすらと消えていく
    }

    /// <summary>
    /// 最も近い位置へ移動する
    /// </summary>
    void MoveToMastNear()
    {
        var pos = m_moveFloor.gameObject.transform.position;

        //2番目に近い位置を次のターゲットにする
        m_nextPosition = m_targets
            .OrderBy(t => Vector3.Distance(pos, t.position))
            .Skip(1)
            .FirstOrDefault();

        m_moveFloorDoTween?.GoMove(m_nextPosition);
    }

    /// <summary>
    /// 配列の順番通りに移動し、最後まで行ったら逆順で移動。
    /// </summary>
    void MoveInOrder()
    {
        //インデックスのnullチェック
        if (m_targets[m_index + 1] == null)
        {
            m_reverse = true;
        }
        else if (m_index - 1 <= 0)
        {
            m_reverse = false;
        }

        //次の位置を保存
        if (m_reverse)
        {
            m_nextPosition.position = m_targets[m_index - 1].position;
            m_index--;
        }
        else
        {
            m_nextPosition.position = m_targets[m_index + 1].position;
            m_index++;
        }
        m_moveFloorDoTween?.GoMove(m_nextPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MagicBullet")
        {
            OnEffect();
            //m_moveFloorController?.Move();  //旧方式
            //MoveToMastNear();               //新方式(1)動作確認済み
            MoveInOrder();                //新方式(2)
        }
    }
}
