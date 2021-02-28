using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    /// <summary>ダメージ時のボイス</summary>
    [SerializeField] AudioClip m_damageVoice = null;
    /// <summary>攻撃する時のボイス</summary>
    [SerializeField] AudioClip[] m_FireVoice = null;
    /// <summary>Playerのスポーン地点</summary>
    [SerializeField] GameObject m_spawnPoint = null;
    /// <summary>リスポーンした際のエフェクト</summary>
    [SerializeField] ParticleSystem m_fireParticle = null;
    /// <summary>動く速さ</summary>
    [SerializeField] float m_movingSpeed = 5f;
    /// <summary>ターンの速さ</summary>
    [SerializeField] float m_turnSpeed = 5f;
    /// <summary>魔法のオブジェクトプール</summary>
    [SerializeField] Transform m_magicBulletsPool = null;
    /// <summary>魔法を生成する場所</summary>
    [SerializeField] Transform m_muzzle = null;

    Rigidbody m_rb;
    float h;
    float v;
    Vector3 m_dir;
    Vector3 m_vel;
    Animator m_anim;
    AudioSource m_audio;
    Rigidbody m_moveFloorRb;
    int m_random = 0;
    bool m_onMoveFloor = false;
    public bool m_respawn = false;

    /// <summary>このクラスのインスタンスが既にあるかどうかを確認する</summary>
    public static bool m_isExists = false;

    bool isPlayerOperation = true;

    public bool IsPlayerOperation
    {
        get { return isPlayerOperation; }
        set { isPlayerOperation = value; }
    }

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
    }

    private void FixedUpdate()
    {
        if (m_respawn || GameManager.m_ending || !IsPlayerOperation) return;//リスポーン中とエンディング中は移動できないようにする
        PlayerMove();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_respawn || GameManager.m_ending || !IsPlayerOperation) return;//リスポーン中とエンディング中は攻撃できないようにする
        Attack();
    }

    /// <summary>
    /// 攻撃する
    /// </summary>
    void Attack()
    {
        if (Input.GetButton("Fire2"))
        {            
            this.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, this.transform.rotation, Time.deltaTime * m_turnSpeed); //Playerの向きをカメラの向いている方向にする
            this.transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);　//Playerが倒れないようにする
            if (Input.GetButtonDown("Fire1"))
            {
                // https://qiita.com/NekoCan/items/e3908b8e4e91b95d726a を参照

                //アクティブでないオブジェクトをm_magicBulletsの中から検索
                foreach (Transform t in m_magicBulletsPool)
                {
                    //アクティブでないなら
                    if (!t.gameObject.activeSelf)
                    {
                        t.SetPositionAndRotation(m_muzzle.position, m_muzzle.rotation); //非アクティブなオブジェクトの位置と回転を設定
                        t.GetComponent<MagicAttack>()?.OnFire(m_muzzle.transform.forward); //向きを設定 //三項演算子の一種でMagicAttackをGetComponent出来たら実行する
                        t.gameObject.SetActive(true);
                        m_audio.Play();

                        m_random = Random.Range(0, 3);
                        switch (m_random)//Playerが攻撃するたびにランダムでセリフが変わる
                        {
                            case 0:
                                AudioSource.PlayClipAtPoint(m_FireVoice[0], transform.position);
                                break;
                            case 1:
                                AudioSource.PlayClipAtPoint(m_FireVoice[1], transform.position);
                                break;
                            case 2:
                                AudioSource.PlayClipAtPoint(m_FireVoice[2], transform.position);
                                break;
                        }

                        return;
                    }
                }
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
        m_dir = Vector3.forward * v + Vector3.right * h;

        if (m_dir == Vector3.zero)
        {
            if (m_onMoveFloor)//動く床に乗っている時は、速度ベクトルを動く床と同じにする
            {
                m_rb.velocity = m_moveFloorRb.velocity;
            }
            else
            {
                //y軸方向の動きだけ反映し、xとzは0にすることで、移動を辞めた時にピタッと止まれる
                m_rb.velocity = new Vector3(0f, m_rb.velocity.y, 0f);
            }
        }
        else
        {
            //カメラが向いている方向を基準にキャラクターが動くように、入力のベクトルを変換する
            m_dir = Camera.main.transform.TransformDirection(m_dir);
            m_dir.y = 0;//y軸方向はゼロにして水平方向のベクトルにする

            //入力方向に滑らかに回転させる
            Quaternion targetRotation = Quaternion.LookRotation(m_dir);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * m_turnSpeed);

            m_vel = m_dir.normalized * m_movingSpeed;
            m_vel.y = m_rb.velocity.y;
            m_rb.velocity = m_vel;
        }
        
        m_anim.SetFloat("Run", m_rb.velocity.magnitude); //IdolとRunアニメーションを切り替える
    }

    /// <summary>
    /// SpawnPointへPlayerを移動させる
    /// </summary>
    void Respawn()
    {
        m_spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        this.transform.position = m_spawnPoint.transform.position;
        Invoke("UnEffect", 1.2f);
    }

    /// <summary>
    /// リスポーンエフェクトを再生する
    /// </summary>
    void OnEffect()
    {
        m_fireParticle.Play();
    }

    /// <summary>
    /// リスポーンエフェクトを停止する
    /// </summary>
    void UnEffect()
    {
        m_fireParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);//うっすらと消えていく
        m_respawn = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MoveFloor")
        {
            m_moveFloorRb = collision.gameObject.GetComponent<Rigidbody>();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "MoveFloor")
        {
            m_onMoveFloor = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "MoveFloor")
        {
            m_onMoveFloor = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Thorns")
        {
            m_respawn = true;
            Invoke("Respawn", 2);
            Invoke("OnEffect", 1.5f);
            m_anim.Play("Damage");
            AudioSource.PlayClipAtPoint(m_damageVoice, transform.position);
        }
    }
}
