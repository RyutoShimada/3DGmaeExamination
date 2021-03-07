using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// カメラを制御する
/// </summary>
public class　CameraController : MonoBehaviour
{
    /// <summary>vcamを入れる変数</summary>
    [SerializeField] CinemachineVirtualCameraBase m_vcam = default;
    /// <summary>freelookを入れる変数</summary>
    [SerializeField] CinemachineVirtualCameraBase m_freelook = default;

    private void Update()
    {
        CameraControl();
    }

    void CameraControl()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            //先生に教えてもらったカメラ制御（エイムした時にvcamをFreeLockで見ていた方向にする処理）
            //===========================================================================================================
            Vector3 freelookDir = m_freelook.LookAt.position - m_freelook.transform.position;
            freelookDir.y = 0;
            float angle = Vector3.SignedAngle(Vector3.forward, freelookDir, Vector3.up);
            //Debug.Log($"angle: {angle}");
            ((CinemachineVirtualCamera)m_vcam).GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value = angle;
            ((CinemachineVirtualCamera)m_vcam).GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = 0f;
            m_vcam.MoveToTopOfPrioritySubqueue(); //vcamを優先
            //===========================================================================================================
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            m_freelook.MoveToTopOfPrioritySubqueue(); //freelookを優先
        }
    }
}
