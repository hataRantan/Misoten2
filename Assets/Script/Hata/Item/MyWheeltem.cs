using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyWheeltem : MyItemInterface
{
    [Header("車輪の当たり判定")]
    [SerializeField] BoxCollider m_wheelCol = null;

    [Header("車輪の剛体")]
    [SerializeField] Rigidbody m_wheelRigid = null;

    [Header("パーティクルの種類")]
    [SerializeField] GameObject particle;
    private ParticleSystem particleClone;
    [Header("移動速度")]
    [SerializeField] private float moveSpeed = 0.2f;

    [Header("ヒットボックスの拡大")]
    [SerializeField] public GameObject hitBox;
    private Vector3 speed = new Vector3(1, 1, 0);   // 拡大していく速度

    [Header("拡大を時間で調整")]
    [SerializeField] private float count = 0.3f;                     // 拡大時間
    private Vector3 normalInput = Vector3.zero;     // 入力値を正規化
    private Vector3 wheelVelocity = Vector3.zero;   // 移動
    private Vector3 nowPos = Vector3.zero;          // 移動前の座標
    private float deadTime = 0.0f;

    // 壁との衝突判定
    private bool isHitWall = false;
    //アクション中
    private bool isAction = false;

    public override void Init(MyPlayerInfo _info)
    {
        //プレイヤー情報の受け取り等
        base.Init(_info);

        //モアイの剛体等開始
        m_wheelCol.enabled = true;
        m_wheelRigid.isKinematic = false;

        //ToDo：他の初期化事項
        nowPos = m_wheelRigid.position;     // 角度の固定
        m_wheelRigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

    }

    public override void ActionInit()
    {
        //アクション完了フラグ初期化
        isEndAntion = false;
        isAction = true;
        //ToDo：アクション初期化
        //パーティクル開始
        GameObject obj = Instantiate(particle, m_wheelRigid.position, m_wheelRigid.rotation);
        particleClone = obj.GetComponent<ParticleSystem>();
    }

    public override void FiexdAction()
    {
        //ToDo：アクション中
        //当たり判定の拡大スタート
        if (count > 0.0f)
        {
            hitBox.transform.localScale += speed * Time.deltaTime;
        }
        count -= 1.0f;

        if(particleClone!=null)
        {
            if (deadTime < particleClone.duration)
            {
                deadTime += Time.fixedDeltaTime;
            }
            else
            {
                Debug.Log("kita:" + particleClone);
                Destroy(particleClone.gameObject);
                //プレイヤーを通常状態に変更
                m_playerInfo.ChangeNormal();
                //自身の消失
                Destroy(this.gameObject);

                particleClone = null;
            }
        }

      

        if (isHitWall)
        {
            //モアイの剛体等開始
            m_wheelCol.enabled = false;
            m_wheelRigid.isKinematic = true;
        }
    }

    public override void FiexdMove()
    {
        //ToDo：
        //ここが強制的に各方向の軸へと移動の処理を行ってしまっている
        m_wheelRigid.velocity = wheelVelocity * moveSpeed;
        //ジグザグに動かすようにする
        if (wheelVelocity.x > 0)
        {//進行方向:左
            m_wheelRigid.angularVelocity = new Vector3(0.0f, Random.Range(5.0f, 20.0f) * Time.fixedDeltaTime, 0.0f);
        }
        else if (wheelVelocity.x < 0)
        {//進行方向:右

        }


        //進行方向に顔を向ける
        Vector3 diff = m_wheelRigid.position - nowPos;
        if (diff.magnitude > 0.01f)
        {
            m_wheelRigid.rotation = Quaternion.LookRotation(diff);
        }
        nowPos = m_wheelRigid.position;
    }

    public override void Move(Vector2 _direct)
    {
        //ToDo：入力値の整理
        normalInput = new Vector3(_direct.x, 0.0f, _direct.y).normalized;
        wheelVelocity.x = normalInput.x * GetSpeed();
        wheelVelocity.z = normalInput.z * GetSpeed();
    }

    private void OnCollisionEnter(Collision _other)
    {
        if (!isAction) return;

        if (_other.gameObject.layer == LayerMask.NameToLayer("Player") && m_playerInfo.Player != _other.gameObject)
        {
            //ダメージ処理
            Damage(_other.gameObject.GetComponent<MyPlayerObject>().PlayerInfo, WheelDamage);
            //プレイヤーを通常状態に変更
            m_playerInfo.ChangeNormal();
            //自身の消失
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (!isAction) return;

        if (_other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            isHitWall = true;
            //プレイヤーを通常状態に変更
            m_playerInfo.ChangeNormal();
            //自身の消失
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// モアイのダメージ処理
    /// </summary>
    private void WheelDamage()
    {
        //ToDo：モアイの爆発エフェクトの出現など
    }
    private void OnParticleSystemStopped()
    {
        Debug.Log("パーティクル終了");
    }
}
