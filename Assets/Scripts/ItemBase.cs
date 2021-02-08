using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    Vector3 m_swapPoint = new Vector3(0, -10, 0);

    public void Get()
    {
        // 見えない所にアイテムを隠す
        this.transform.position = m_swapPoint;
    }

    void Use()
    {

    }
}
