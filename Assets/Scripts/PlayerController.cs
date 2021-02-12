using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody m_rb;
    float h;
    float v;

    //入力方向のベクトル
    Vector3 dir;
    Vector3 vel;

    /// <summary>ステータスを表す</summary>
    //Status status = Status.Idol;

    Animator m_anim;
    AudioSource m_audio;

    Ray ray;
    public RaycastHit hit;

    //-----Player情報-----
    /// <summary>Playerのレベル</summary>
    [SerializeField] int m_playerLevel = 1;
    /// <summary>動く速さ</summary>
    [SerializeField] float m_movingSpeed = 5f;
    /// <summary>加速する力</summary>
    [SerializeField] float m_addSpeed = 5f;
    /// <summary>ターンの速さ</summary>
    [SerializeField] float m_turnSpeed = 5f;
    /// <summary>魔法のオブジェクトを入れておく</summary>
    [SerializeField] GameObject[] m_magicBullet = default;
    /// <summary>魔法のオブジェクトプール</summary>
    [SerializeField] Transform m_magicBulletsPool = default;
    /// <summary>魔法を生成する場所</summary>
    [SerializeField] Transform m_muzzle = default;
    /// <summary>攻撃のインターバル</summary>
    [SerializeField] float m_attackInterval = 3f;

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
        m_rb = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();
        m_audio = GetComponent<AudioSource>();
        foreach (var item in m_magicBullet)
        {
            Instantiate(item, transform.position, transform.rotation, m_magicBulletsPool);
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttackAnimation();
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartLoadScene()
    {
        // イベントにイベントハンドラーを追加
        SceneManager.sceneLoaded += SceneLoaded;
        // シーンの読み込み
        SceneManager.LoadScene("MapScene");
    }

    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        
    }

    /// <summary>
    /// 左クリックで攻撃のアニメーションをする
    /// </summary>
    void PlayerAttackAnimation()
    {
        if (Input.GetButton("Fire2"))
        {
            //Playerの向きをカメラの向いている方向にする
            this.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, this.transform.rotation, Time.deltaTime * m_turnSpeed);

            //Playerが倒れないようにする
            this.transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            int LayerMask = ~(1 <<11);

            if (Physics.Raycast(ray, out hit, 100f, LayerMask))
            {
                m_muzzle.gameObject.transform.LookAt(hit.point);    // muzzleの向きを変えている
                Debug.DrawLine(m_muzzle.position, hit.point, Color.red);
            }

            //クリックしたらAttackアニメーションを再生
            if (Input.GetButtonDown("Fire1"))
            {
                PlayerAttack();
            }
        }
    }

    /// <summary>
    /// Playerが実際にする動き
    /// </summary>
    void PlayerMove()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        dir = Vector3.forward * v + Vector3.right * h;

        if (dir == Vector3.zero)
        {
            //y軸方向の動きだけ反映し、xとzは0にすることで、移動を辞めた時にピタッと止まれる
            m_rb.velocity = new Vector3(0f, m_rb.velocity.y, 0f);
        }
        else
        {
            //カメラが向いている方向を基準にキャラクターが動くように、入力のベクトルを変換する
            dir = Camera.main.transform.TransformDirection(dir);
            dir.y = 0;//y軸方向はゼロにして水平方向のベクトルにする

            //入力方向に滑らかに回転させる
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * m_turnSpeed);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                vel = dir.normalized * (m_movingSpeed + m_addSpeed);
            }
            else
            {
                vel = dir.normalized * m_movingSpeed;
            }

            vel.y = m_rb.velocity.y;

            m_rb.velocity = vel;
        }

        //IdolとRunアニメーションを切り替える
        m_anim.SetFloat("Run", m_rb.velocity.magnitude);
    }

    /// <summary>
    /// 魔法弾生成
    /// </summary>
    /// <param name="pos">生成位置</param>
    /// <param name="rotation">生成時の回転</param>
    void PlayerAttack()
    {
        // https://qiita.com/NekoCan/items/e3908b8e4e91b95d726a を参照

        if (m_playerLevel < 5)
        {
            //アクティブでないオブジェクトをm_magicBulletsの中から検索
            foreach (Transform t in m_magicBulletsPool)
            {
                //アクティブでないなら
                if (!t.gameObject.activeSelf)
                {
                    //非アクティブなオブジェクトの位置と回転を設定
                    t.SetPositionAndRotation(m_muzzle.position, m_muzzle.rotation);

                    //アクティブにする
                    t.gameObject.SetActive(true);

                    m_audio.Play();

                    return;
                }
            }
        }
        else if (m_playerLevel >= 5)
        {
            StartCoroutine(ContinuousAttack(m_attackInterval));
        }
    }

    /// <summary>
    /// 一定間隔で魔法を連続生成
    /// </summary>
    /// <param name="rateTime">遅らせる間隔</param>
    /// <returns>連続魔法攻撃</returns>
    IEnumerator ContinuousAttack(float rateTime)
    {
        foreach (Transform t in m_magicBulletsPool)
        {
            //アクティブでないなら
            if (!t.gameObject.activeSelf)
            {
                //非アクティブなオブジェクトの位置と回転を設定
                t.SetPositionAndRotation(m_muzzle.position, transform.rotation);

                //アクティブにする
                t.gameObject.SetActive(true);

                yield return new WaitForSeconds(rateTime);
            }
        }
    }

    enum Status
    {
        Idol, Run, Attack
    }
}
