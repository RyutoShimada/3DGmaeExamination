using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoolController : MonoBehaviour
{
    public bool m_gool = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Obsolete]
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.other.tag == "Player")
        {
            m_gool = true;
           //Debug.Log("Gool:" + m_gool);
        }
    }
}
