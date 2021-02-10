using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    //カメラのON/OFF制御
    [SerializeField] CinemachineVirtualCameraBase m_vcam = default;
    [SerializeField] CinemachineVirtualCameraBase m_freelook = default;
    //[SerializeField] GameObject m_player = default;
    [SerializeField] GameObject m_magicCircle = default;

    [SerializeField] GameObject m_player = default;

    PlayerController m_playerController;

    [SerializeField] GameObject m_goolObject = default;

    GoolController m_goolController;

    LayerMask m_playerLayer;

    /// <summary>このクラスのインスタンスが既にあるかどうかを確認する</summary>
    public static bool m_isExists = false;

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

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;

        m_freelook.MoveToTopOfPrioritySubqueue(); //freelookを優先

        //m_vcam.SetActive(false);

        m_magicCircle.SetActive(false);

        m_playerController = m_player.GetComponent<PlayerController>();

        m_goolController = m_goolObject.GetComponent<GoolController>();

        if (m_player)
        {
            m_playerLayer = m_player.layer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CameraControl();

        DontDestroyOnLoad(this.gameObject);

        if (m_goolController)
        {
            if (m_goolController.m_gool)
            {
                m_playerController.StartLoadScene();
                m_goolController.m_gool = false;
            }
        }
    }

    void CameraControl()
    {
        //右クリックでエイム
        if (Input.GetButtonDown("Fire2"))
        {
            Vector3 freelookDir = m_freelook.LookAt.position - m_freelook.transform.position;
            freelookDir.y = 0;
            float angle = Vector3.SignedAngle(Vector3.forward, freelookDir, Vector3.up);
            //Debug.Log($"angle: {angle}");
            ((CinemachineVirtualCamera)m_vcam).GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value = angle;
            ((CinemachineVirtualCamera)m_vcam).GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = 0f;

            m_vcam.MoveToTopOfPrioritySubqueue(); //vcamを優先
            //m_freelook.SetActive(false);

            m_magicCircle.SetActive(true); //魔法陣の展開

            //8レイヤー(Plyaer)以外は全部有効にする
            Camera.main.cullingMask = ~(1 << m_playerLayer);
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            m_freelook.MoveToTopOfPrioritySubqueue(); //freelookを優先
            //m_vcam.SetActive(false);

            m_magicCircle.SetActive(false); //魔法陣を収束

            //全てのレイヤーを有効にする
            Camera.main.cullingMask = -1;
        }
    }
}
