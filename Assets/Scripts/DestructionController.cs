using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionController : MonoBehaviour
{
    /// <summary>破壊された時に出るオブジェクト</summary>
    [SerializeField] GameObject m_destroyObject = default;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MagicBullet")//魔法オブジェクトと接触した時
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(m_destroyObject, this.transform.position, this.transform.rotation);//破壊された時に出すオブジェクトをインスタンス化
            }
            Destroy(this.gameObject);//このオブジェクトを破棄
        }
    }
}
