using UnityEngine;

public class MyBullItem : MyItemInterface
{
    [Header("牛の当たり判定")]
    [SerializeField] BoxCollider m_bullCol = null;

    [Header("牛の剛体")]
    [SerializeField]
    Rigidbody m_bullRigid = null;

    private Vector3 bullRotate = Vector3.zero;
    private Vector3 bullMove = Vector3.zero;
    private Vector3 bullRotation = Vector3.zero;

    private float rotateAcceleration = 0.0f;

    [SerializeField] private float rotateSpeed = 80.0f;
    [SerializeField] private float bullSpeed = 100.0f;

    //壁と当たり判定
    public bool isHitWall = false;
    //WASD押されてるか
    private bool isInput = false;
    //アクションキーを押されてるか
    private bool isAction = false;

    public ParticleSystem bullEffectL;
    public ParticleSystem bullEffectR;
    
    public override void Init(MyPlayerInfo _info)
    {
        //プレイヤー情報の受け取り等
        base.Init(_info);

        //牛自体の剛体等開始
        m_bullCol.enabled = true;
        m_bullRigid.isKinematic = false;

        //ToDo：他の初期化事項
        //パーティクルの情報を獲得
        bullEffectL = transform.GetChild(4).GetComponent<ParticleSystem>();
        bullEffectR = transform.GetChild(5).GetComponent<ParticleSystem>();
        //アニメーションのスクリプトを有効化する
        GetComponent<bullanimation>().enabled = true;


       
    }

    public override void ActionInit()
    {
        //アクション完了フラグ初期化
        isEndAntion = false;

        //ToDo：アクション初期化
        //m_playerInfo.ChangeNormal();

        bullRotate = m_bullRigid.transform.forward;
    }

    public override void FiexdAction()
    {
        //ToDo：アクション中
        if (Input.GetKey(KeyCode.Mouse1)) 
        {
            isAction = true;
            Invoke("Delay", 0.5f);//時間差を作る
        }

        if (isHitWall)
        {
            m_bullCol.enabled = false;
            m_bullRigid.isKinematic = true;
        }

        isEndAntion = true;
    }

    public override void Action(Vector2 _input)
    {
        base.Action(_input);
        
    }

    public override void FiexdMove()
    {
        //ToDo：移動
        //m_bullRigid.angularVelocity = new Vector3(0.0f, rotateAcceleration * Time.fixedDeltaTime, 0.0f);

        //WASDが押されたときだけ
        if (isInput && !isAction) 
        {
            m_bullRigid.velocity = bullMove;
            transform.localRotation = Quaternion.Euler(bullRotation);
        }
    }

    public override void Move(Vector2 _direct)
    {
        //ToDo：入力値の整理
        rotateAcceleration = -rotateSpeed * _direct.x;

        //WASD押されたらローテーションを動かす
        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.W))
            {
                bullRotation = new Vector3(0, 135.0f, 0);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                bullRotation = new Vector3(0, 45.0f, 0);
            }
            else
            {
                bullRotation = new Vector3(0, 90.0f, 0);
            }
            isInput = true;
            bullMove = transform.forward * 25.0f;
        }

        else if (Input.GetKey(KeyCode.D))
        {

            if (Input.GetKey(KeyCode.W))
            {
                bullRotation = new Vector3(0, -135.0f, 0);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                bullRotation = new Vector3(0, -45.0f, 0);
            }
            else
            {
                bullRotation = new Vector3(0, -90.0f, 0);
            }
            isInput = true;
            bullMove = transform.forward * 25.0f;
        }

        else if (Input.GetKey(KeyCode.W))
        {
            isInput = true;
            bullRotation = new Vector3(0, -180.0f, 0);
            bullMove = transform.forward * 25.0f;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            isInput = true;
            bullRotation = new Vector3(0, 0, 0);
            bullMove = transform.forward * 25.0f;
        }
        //WASDとアクションキーを押されてなかったら動きを止める
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !isAction) 
        {

            m_bullRigid.velocity = Vector3.zero;
            isInput = false;
        }
            
        

        Debug.Log(isInput);
        Debug.Log("Action"+isAction);
    }

    public override void Exit()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            isHitWall = true;

            //Debug.Log("hit wall");
            m_playerInfo.ChangeNormal();

            Destroy(this.gameObject);
        }
    }

    void Delay()
    {
        m_bullRigid.velocity = bullRotate * bullSpeed;

        //パーティクル実行
        bullEffectL.Play();
        bullEffectR.Play();
    }

    
}
