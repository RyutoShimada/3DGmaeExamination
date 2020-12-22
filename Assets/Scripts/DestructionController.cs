using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionController : MonoBehaviour
{
    /// <summary>破壊したオブジェクト</summary>
    [SerializeField] GameObject m_destroyObject = default;

    /// <summary>破壊したいオブジェクトを生成する数</summary>
    //[SerializeField] int m_instanceNum = 0;

    /// <summary>分割する数</summary>
    //[SerializeField] int m_divisionNum = 0;

    /// <summary>破壊した時のパワー</summary>
    [SerializeField] float m_power = 5f;

    /// <summary>ランダム最小値</summary>
    [SerializeField] float m_randomMin = 0f;

    /// <summary>ランダム最大値</summary>
    [SerializeField] float m_randomMax = 10f;

    /// <summary>ランダム生成</summary>
    float m_randomRange;

    Rigidbody m_rb;

    Vector3 m_randomV3;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MagicBullet")
        {
            //for (int i = 0; i < m_instanceNum; i++)
            //{
            Instantiate(m_destroyObject, this.transform.position, this.transform.rotation);
            Instantiate(m_destroyObject, this.transform.position, this.transform.rotation);
            Instantiate(m_destroyObject, this.transform.position, this.transform.rotation);
            Instantiate(m_destroyObject, this.transform.position, this.transform.rotation);
            //}

            this.gameObject.SetActive(false);
            m_randomRange = Random.Range(m_randomMin, m_randomMax);
            m_randomV3 = new Vector3(m_randomRange, m_randomRange, m_randomRange);
            m_rb = this.gameObject.GetComponent<Rigidbody>();
            m_rb.AddForce(m_randomV3 * m_power, ForceMode.Impulse);
        }
    }
}
