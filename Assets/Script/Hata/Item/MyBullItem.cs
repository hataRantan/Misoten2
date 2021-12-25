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
    private Vector3 nowPos = Vector3.zero;

    private float lastRotation = 0;
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
        nowPos = m_bullRigid.position;
        m_bullRigid.constraints= RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
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
        isAction = true;
        //ToDo：アクション初期化
        //m_playerInfo.ChangeNormal();

        bullRotate = m_bullRigid.transform.forward;
    }

    public override void FiexdAction()
    {
        //ToDo：アクション中
        if (isAction)
        {
            //isAction = true;
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
        //if (isInput && !isAction) 
        //{
        //    m_bullRigid.velocity = bullMove;
        //    transform.localRotation = Quaternion.Euler(bullRotation);
        //}

        Vector3 diff = m_bullRigid.position - nowPos;

        if (diff.magnitude > 0.01f)
        {
            m_bullRigid.MoveRotation(Quaternion.LookRotation(diff));
        }
       
        nowPos = m_bullRigid.position;


    }

    public override void Move(Vector2 _direct)
    {
        _direct = _direct.normalized;
        Vector3 power = new Vector3(_direct.x * 20,0 ,_direct.y * 20);
        m_bullRigid.velocity = power;


       

        //ToDo：入力値の整理
        //if (_direct.x > 0 && _direct.y < 0)
        //{
        //    rotateAcceleration = 90.0f + -_direct.y * 90.0f;
        //}
        //else if (_direct.x > 0 && _direct.y > 0 )
        //{
        //    rotateAcceleration = _direct.x * 90.0f;
        //}
        //else if (_direct.x < 0 && _direct.y >0)
        //{
        //    rotateAcceleration = _direct.x * 90.0f;
        //}
        //else if (_direct.x < 0 && _direct.y <0)
        //{
        //    rotateAcceleration = -90.0f + -_direct.y * 90.0f;
        //}
        //if (rotateAcceleration != 0)
        //{
        //    lastRotation = rotateAcceleration;
        //}
        //m_bullRigid.rotation = Quaternion.Euler(0, rotateAcceleration, 0);


        //WASD押されたらローテーションを動かす
        //a
        //if (_direct.x > 0 && _direct.y > -0.4 && _direct.y < 0.4)
        //{
        //    if (_direct.y > 20)
        //    {
        //        bullRotation = new Vector3(0, 135.0f, 0);
        //    }
        //    else if (_direct.y < -20)
        //    {
        //        bullRotation = new Vector3(0, 45.0f, 0);
        //    }
        //    else
        //    {
        //        bullRotation = new Vector3(0, 90.0f, 0);
        //    }
        //    isInput = true;
        //    bullMove = transform.forward * 25.0f;
        //}
        ////d
        //else if (_direct.x < 0 && _direct.y > -0.4 && _direct.y < 0.4)
        //{

        //    if (_direct.y > 20)
        //    {
        //        bullRotation = new Vector3(0, -135.0f, 0);
        //    }
        //    else if (_direct.y < -20)
        //    {
        //        bullRotation = new Vector3(0, -45.0f, 0);
        //    }
        //    else
        //    {
        //        bullRotation = new Vector3(0, -90.0f, 0);
        //    }
        //    isInput = true;
        //    bullMove = transform.forward * 25.0f;
        //}
        ////w
        //else if (_direct.y < 0 && _direct.x < 0.4 && _direct.x > -0.4)
        //{
        //    isInput = true;
        //    bullRotation = new Vector3(0, -180.0f, 0);
        //    bullMove = transform.forward * 25.0f;
        //}
        ////s
        //else if (_direct.y > 0 && _direct.x < 0.4 && _direct.x > -0.4) 
        //{
        //    isInput = true;
        //    bullRotation = new Vector3(0, 0, 0);
        //    bullMove = transform.forward * 25.0f;
        //}
        //WASDとアクションキーを押されてなかったら動きを止める
        if (_direct.magnitude==0) 
        {

            m_bullRigid.velocity = Vector3.zero;
            isInput = false;
        }


        Debug.Log(_direct);
        //Debug.Log(isInput);
        //Debug.Log("Action"+isAction);
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
