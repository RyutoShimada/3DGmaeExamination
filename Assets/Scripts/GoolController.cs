using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoolController : MonoBehaviour
{
    /// <summary>ゴールの判定</summary>
    public static bool m_gool = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_gool = true;
           //Debug.Log("Gool:" + m_gool);
        }
    }
}
