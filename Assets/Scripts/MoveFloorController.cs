using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloorController : MonoBehaviour
{
    [SerializeField] float m_speed = 1f;
    /// <summary>動く床を動かすボタンの判定をする</summary>
    [SerializeField] bool m_moveButton = false;
    /// <summary>動く床の折り返し地点A</summary>
    [SerializeField] Transform m_pointA = default;
    /// <summary>動く床の折り返し地点B</summary>
    [SerializeField] Transform m_pointB = default;
    Rigidbody m_rb;
    State state;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = this.gameObject.GetComponent<Rigidbody>();
        this.transform.position = m_pointA.position;
        state = State.GoToPointA;
    }

    private void FixedUpdate()
    {
        if (m_moveButton)
        {
            switch (state)
            {
                case State.GoToPointA:
                    Vector3 dirToA = m_pointA.position - this.transform.position;//行きたい方向が分かる
                    Vector3 veloToA = m_speed * dirToA.normalized;
                    //m_rb.AddForce(veloToA, ForceMode.Force);
                    m_rb.velocity = veloToA;
                    break;
                case State.GoToPointB:
                    Vector3 dirToB = m_pointB.position - this.transform.position;//行きたい方向が分かる
                    Vector3 veloToB = m_speed * dirToB.normalized;
                    //m_rb.AddForce(veloToB, ForceMode.Force);
                    m_rb.velocity = veloToB;
                    break;
                default:
                    break;
            }
        }
        else
        {
            m_rb.velocity = Vector3.zero;
        }
    }

    enum State
    {
        GoToPointA, GoToPointB
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PointB")
        {
            m_moveButton = false;
        }
        else if (other.tag == "PointA")
        {
            m_moveButton = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PointA" && m_rb.velocity.z < 0.1)
        {
            state = State.GoToPointB;
        }
        else if (other.tag == "PointB")
        {
            state = State.GoToPointA;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(this.gameObject.transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
