using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    //回転させるためだけのスクリプト
    [SerializeField] float m_speed = 1f;
    Transform m_transform;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        m_transform.rotation = Quaternion.Euler(0, m_speed * Time.time, 0);
        this.m_transform.rotation = m_transform.rotation;
    }
}
