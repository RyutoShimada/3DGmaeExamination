using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //参考資料
    //http://tsubakit1.hateblo.jp/entry/2017/10/23/213606#%E3%82%B3%E3%83%B3%E3%83%88%E3%83%AD%E3%83%BC%E3%83%A9%E3%83%BC%E3%82%92%E5%8F%8D%E8%BB%A2%E3%81%95%E3%81%9B%E3%81%9F%E3%81%84

    public bool xInversion, yInversion;

    /// <summary>このクラスのインスタンスが既にあるかどうかを確認する</summary>
    static bool m_isExists = false;

    private void Awake()
    {
        if (m_isExists)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_isExists = true;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        Cinemachine.CinemachineCore.GetInputAxis = GetAxisCustom;
    }

    void Update()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public float GetAxisCustom(string axisName)
    {
        if (axisName == "Mouse X")
        {
            return Input.GetAxis(axisName) * (xInversion ? -1f : 1f);
        }
        else if (axisName == "Mouse Y")
        {
            return Input.GetAxis(axisName) * (yInversion ? -1 : 1);
        }

        return 0;
    }
}
