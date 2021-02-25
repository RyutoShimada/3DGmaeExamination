using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    /// <summary>vcamを入れる変数</summary>
    [SerializeField] CinemachineVirtualCameraBase m_vcam = default;
    /// <summary>freelookを入れる変数</summary>
    [SerializeField] CinemachineVirtualCameraBase m_freelook = default;
    /// <summary>エイム時の魔法陣</summary>
    [SerializeField] GameObject m_magicCircle = default;
    /// <summary>playerのオブジェクト</summary>
    [SerializeField] GameObject m_player = default;
    /// <summary>Goolした時の判定をするためのオブジェクト</summary>
    [SerializeField] GameObject m_goolObject = default;
    /// <summary>フェードインアウトをコントロールする</summary>
    [SerializeField] FadeController m_FC = default;
    /// <summary>Playerのスポーン地点</summary>
    [SerializeField] GameObject m_spawnPoint = default;
    /// <summary>PlayerControllerを格納する変数</summary>
    PlayerController m_playerController;
    /// <summary>GoolControllerを格納する変数</summary>
    GoolController m_goolController;
    /// <summary>このクラスのインスタンスが既にあるかどうかを確認する</summary>
    public static bool m_isExists = false;

    private void Awake()
    {
        if (m_isExists)//2回目以降のロードは既に配置されているこのオブジェクトを破棄する
        {
            Destroy(this.gameObject);
        }
        else //初回のロードはこっちに入り、フラグを立てる
        {
            m_isExists = true;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void StartLoadScene()
    {
        SceneManager.sceneLoaded += SceneLoaded;//Sceneをロードした時に処理を実行
        // シーンの読み込み
        if (SceneManager.GetActiveScene().name == "TitleSceane")
            SceneManager.LoadScene("TutorialSceane");

        if (SceneManager.GetActiveScene().name == "TutorialSceane")
            SceneManager.LoadScene("Stage1");
    }

    /// <summary>
    /// Sceneのロードが行われたときに、フェードインして、プレイヤーの位置をスポーン地点に移動させる
    /// </summary>
    /// <param name="nextScene"></param>
    /// <param name="mode"></param>
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        m_FC.m_isFadeIn = true;
        m_spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        m_player.transform.position = m_spawnPoint.transform.position;
        m_goolObject = GameObject.FindGameObjectWithTag("Gool");
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;//カーソルを消す
        Cursor.lockState = CursorLockMode.Locked;//カメラの向きをマウスと連動させる
        m_freelook.MoveToTopOfPrioritySubqueue(); //freelookを優先
        m_magicCircle.SetActive(false);//魔法陣を非表示

        m_playerController = m_player.GetComponent<PlayerController>();
        m_goolController = m_goolObject.GetComponent<GoolController>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraControl();
        try
        {
            if (m_goolController.m_gool)//ゴールしたらシーンをロードする
            {
                m_goolController.m_gool = false;
                m_FC.m_isFadeOut = true;
                Invoke("StartLoadScene", 3);
            }
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("Goolオブジェクトがアサインされていません。");
        }

    }

    /// <summary>
    /// カメラを制御する
    /// </summary>
    void CameraControl()
    {
        //右クリックでエイム
        if (Input.GetButtonDown("Fire2"))
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //先生に教えてもらったカメラ制御（エイムした時にvcamをFreeLockで見ていた方向にする処理）
            Vector3 freelookDir = m_freelook.LookAt.position - m_freelook.transform.position;
            freelookDir.y = 0;
            float angle = Vector3.SignedAngle(Vector3.forward, freelookDir, Vector3.up);
            //Debug.Log($"angle: {angle}");
            ((CinemachineVirtualCamera)m_vcam).GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value = angle;
            ((CinemachineVirtualCamera)m_vcam).GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = 0f;
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////

            m_vcam.MoveToTopOfPrioritySubqueue(); //vcamを優先
            m_magicCircle.SetActive(true); //魔法陣の展開
            Camera.main.cullingMask = ~(1 << m_player.layer);//8レイヤー(Plyaer)以外は全部有効にする
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
