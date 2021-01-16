using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //カメラのON/OFF制御
    [SerializeField] GameObject m_vcam = default;
    [SerializeField] GameObject m_freelook = default;
    //[SerializeField] GameObject m_player = default;
    [SerializeField] GameObject m_magicCircle = default;

    [SerializeField] GameObject m_player = default;

    PlayerController m_playerController;

    [SerializeField] GameObject m_goolObject = default;

    GoolController m_goolController;

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

        m_freelook.SetActive(true);

        //m_vcam.SetActive(false);

        m_magicCircle.SetActive(false);

        m_playerController = m_player.GetComponent<PlayerController>();

        m_goolController = m_goolObject.GetComponent<GoolController>();
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
        if (Input.GetButtonDown("Fire2"))
        {
            m_freelook.SetActive(false);
            //m_vcam.SetActive(true);
            m_magicCircle.SetActive(true);

            //8レイヤー(Plyaer)以外は全部有効にする
            Camera.main.cullingMask = ~(1 << 8);
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            m_freelook.SetActive(true);
            //m_vcam.SetActive(false);
            m_magicCircle.SetActive(false);

            //全てのレイヤーを有効にする
            Camera.main.cullingMask = -1;
        }
    }
}
