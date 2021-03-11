using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ターゲットに向かって床を動かす
/// </summary>
public class MoveFloorDoTweenVer : MonoBehaviour
{
    /// <summary>移動する速さ</summary>
    [SerializeField] float m_speed = 0;
    [SerializeField] MoveFloorTrigger m_moveFloorTrigger = null;

    /// <summary>
    /// 行きたい位置まで移動する
    /// </summary>
    /// <param name="target">行先の位置</param>
    public void GoMove(Transform target)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(target.position, m_speed)).OnComplete(() =>
        {
            m_moveFloorTrigger?.UnEffect();
            Debug.Log("MoveCompleted");
        });
    }
}
