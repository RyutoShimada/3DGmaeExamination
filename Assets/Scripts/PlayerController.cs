using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody m_rb;
    float h;
    float v;
    Vector3 dir;
    Vector3 vel;
    Animator m_anim;

    AudioSource m_audio;
    [SerializeField] AudioClip m_damageVoice = default;
    [SerializeField] AudioClip[] m_FireVoice = default;

    int m_random = 0;

    Rigidbody m_moveFloorRb;
    bool m_onMoveFloor = false;

    /// <summary>Playerのスポーン地点</summary>
    [SerializeField] GameObject m_spawnPoint = default;

    //-----Player情報-----
    /// <summary>Playerのライフ</summary>
    [SerializeField] int m_life = 3;
    /// <summary>動く速さ</summary>
    [SerializeField] float m_movingSpeed = 5f;
    /// <summary>ターンの速さ</summary>
    [SerializeField] float m_turnSpeed = 5f;
    /// <summary>魔法のオブジェクトプール</summary>
    [SerializeField] Transform m_magicBulletsPool = default;
    /// <summary>魔法を生成する場所</summary>
    [SerializeField] Transform m_muzzle = default;

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
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAttackAnimation();
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
            dir = Camera.main.transform.TransformDirection(dir);
            dir.y = 0;//y軸方向はゼロにして水平方向のベクトルにする

            //入力方向に滑らかに回転させる
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * m_turnSpeed);

            vel = dir.normalized * m_movingSpeed;
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

        //アクティブでないオブジェクトをm_magicBulletsの中から検索
        foreach (Transform t in m_magicBulletsPool)
        {
            //アクティブでないなら
            if (!t.gameObject.activeSelf)
            {
                //非アクティブなオブジェクトの位置と回転を設定
                t.SetPositionAndRotation(m_muzzle.position, m_muzzle.rotation);

                //向きを設定
                t.GetComponent<MagicAttack>()?.OnFire(m_muzzle.transform.forward);//三項演算子の一種でMagicAttackをGetComponent出来た実行する

                //アクティブにする
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

    /// <summary>
    /// SpawnPointへPlayerを移動させる
    /// </summary>
    void Respawn()
    {
        m_spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        this.transform.position = m_spawnPoint.transform.position;
        m_life--;
        Debug.Log($"Life:{m_life}");
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
            Invoke("Respawn", 2);
            m_anim.Play("Damage");
            AudioSource.PlayClipAtPoint(m_damageVoice, transform.position);
        }
    }
}
