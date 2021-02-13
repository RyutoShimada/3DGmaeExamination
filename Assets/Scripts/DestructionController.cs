using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionController : MonoBehaviour
{
    /// <summary>破壊したオブジェクト</summary>
    [SerializeField] GameObject m_destroyObject = default;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MagicBullet")
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(m_destroyObject, this.transform.position, this.transform.rotation);
            }
            this.gameObject.SetActive(false);
        }
    }
}
