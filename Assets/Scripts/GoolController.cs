using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoolController : MonoBehaviour
{
    /// <summary>ゴールの判定</summary>
    public bool m_gool = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.other.tag == "Player")
        {
            m_gool = true;
           //Debug.Log("Gool:" + m_gool);
        }
    }
}
