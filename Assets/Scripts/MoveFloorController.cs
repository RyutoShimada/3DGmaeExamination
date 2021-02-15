using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloorController : MonoBehaviour
{
    [SerializeField] float m_speed = 1f;
    [SerializeField] float m_width = 1f;
    Vector3 m_startPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_startPosition = this.transform.position;
    }

    private void FixedUpdate()
    {
        this.transform.position = m_startPosition + m_width * Mathf.Sin(m_speed * Time.time) * this.transform.forward;

        //yは動かさない
        Vector3 v3 = this.transform.position;
        v3.y = 0;
        this.transform.position = v3;
    }
}
