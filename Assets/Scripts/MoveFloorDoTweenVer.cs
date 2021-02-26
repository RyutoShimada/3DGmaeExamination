using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveFloorDoTweenVer : MonoBehaviour
{
    /// <summary>移動先</summary>
    [SerializeField] Transform m_traget = null;
    Vector3 m_initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GoMove()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(m_traget.position, 3f));
    }

    void BackMove()
    {

    }
}
