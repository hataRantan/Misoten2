using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMoaiItem : MyItemInterface
{
    [Header("モアイの当たり判定")]
    [SerializeField] BoxCollider m_moaiCol = null;

    [Header("モアイの剛体")]
    [SerializeField] Rigidbody m_moaiRigid = null;

    [Header("パーティクルの種類")]
    [SerializeField] GameObject particle;
    private ParticleSystem particleClone;
    [Header("着地エフェクトのサイズ")]
    [SerializeField] private float effectSize = 7.0f;

    [Header("移動速度")]
    [SerializeField] private float moveSpeed = 1.0f;
    [Header("Euler角修正用")]
    [SerializeField] Transform thisTrs = null;
    private float rot = 0.0f;                       // 座標移動による顔の振り向き角度
    private float lastRotValue = 0.0f;                   // 前回の角度
    private float afterRot = 0.0f;                  // 顔が振り向いた角度
    private Vector3 normalInput = Vector3.zero;     // 入力値を正規化
    private Vector3 moaiVelocity = Vector3.zero;    // 移動
    private Vector3 lastPos = Vector3.zero;          // プレイヤーの位置情報
    private Vector3 diff = Vector3.zero;            // 差分を求める

    [Header("指定の高さまで上昇")]
    [SerializeField] private float targetY = 40.0f;
    [Header("上昇時間")]
    [SerializeField] private float rizeTime = 1.0f;
    [Header("下降時間")]
    [SerializeField] private float downTime = 0.2f;
    private float defaultYPos = 0.0f;               // モデルのY軸の初期位置
    private float timer = 0.0f;                     // カウント用変数
    private bool jumpRizeFlg = false;               // 上昇中かどうか

    private int loopCount = 0;                      // パーティクル生成処理の制御用変数(フレーム更新で多数生成されるため)

    [Header("当たり判定最大サイズ(初期0.04f)")]
    [SerializeField] private float hitMaxSize = 0.06f;
    private float hitboxExpansion = 0.0f;
    [Header("当たり判定拡大時間")]
    [SerializeField] private int hitboxExpansionTime = 1;

    //アクション中
    private bool isAction = false;

    public override void Init(MyPlayerInfo _info)
    {
        //プレイヤー情報の受け取り等
        base.Init(_info);

        //モアイの剛体等開始
        m_moaiCol.enabled = true;
        m_moaiRigid.isKinematic = false;
        //他オブジェクトの当たり判定を無効
        m_moaiCol.isTrigger = true;

        //ToDo：他の初期化事項
        // 移動更新前の座標
        lastPos = m_moaiRigid.position;
        // 角度の固定
        m_moaiRigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        //こいつのレイヤーを変更
        gameObject.layer = LayerMask.NameToLayer("Possess");
    }

    public override void ActionInit()
    {
        //アクション完了フラグ初期化
        isEndAntion = false;
        isAction = true;

        //ToDo：アクション初期化
        // 重力を一時停止
        m_moaiRigid.useGravity = false;
        // モデルサイズによる高低差の修正。Y軸の取得
        defaultYPos = m_moaiRigid.position.y;
        //ジャンプ音
        MyAudioManeger.Instance.PlaySE("Moai_Jump");

    }
    public override void Action(Vector2 _input)
    {
        //ToDo：入力値の整理
        normalInput = new Vector3(_input.x, 0.0f, _input.y).normalized;
        moaiVelocity.x = normalInput.x * GetSpeed();
        moaiVelocity.z = normalInput.z * GetSpeed();
        //ToDo：移動
        m_moaiRigid.velocity = moaiVelocity * moveSpeed;
    }
    public override void FiexdAction()
    {
        //ToDo：アクション中
        if (!jumpRizeFlg)
        {// 上昇中
            if (timer < rizeTime)
            {
                float y = Easing.QuartOut(timer, rizeTime, defaultYPos, targetY);
                Vector3 pos = m_moaiRigid.position;
                pos.y = y;
                m_moaiRigid.position = pos;
                timer += Time.fixedDeltaTime;
            }
            else
            {
                timer = 0;
                jumpRizeFlg = true;
            }
        }
        if (jumpRizeFlg)
        {//下降中
            //BoxCollider有効
            m_moaiCol.isTrigger = false;
            //軸の固定
            m_moaiRigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            if (timer < downTime)
            {
                float firstY = m_moaiRigid.position.y;
                float downY = -Easing.QuartIn(timer, downTime, -firstY, -defaultYPos);
                Vector3 downPos = m_moaiRigid.position;
                downPos.y = downY;
                m_moaiRigid.position = downPos;
                timer += Time.fixedDeltaTime;
            }
            else
            {
                if (loopCount < 1)
                {
                    //着地音
                    MyAudioManeger.Instance.PlaySE("MoaiAttack");
                    //パーティクル開始
                    GameObject obj = Instantiate(particle, thisTrs.transform.position, Quaternion.identity);
                    obj.transform.localScale = new Vector3(effectSize, effectSize, effectSize);
                    particleClone = obj.GetComponent<ParticleSystem>();
                    //パーティクルの角度と高さの修正
                    Quaternion eulerEffect = Quaternion.Euler(0, 90, 0);
                    Quaternion q = thisTrs.transform.rotation;
                    obj.transform.rotation = eulerEffect * q;
                    loopCount++;
                    StartCoroutine(DestroyCoroutine());
                }
            }
        }



    }

    public override void FiexdMove()
    {
        //ToDo：移動
        m_moaiRigid.velocity = moaiVelocity * moveSpeed;

        //ステージの移動制限
        m_moaiRigid.InsideStage(thisTrs.transform.position, 1.0f, 11.0f);

        //移動方向に顔を向ける
        diff = m_moaiRigid.position - lastPos;

        if (diff.magnitude > 0.01f)
        {//前回のPosと座標が違えば
            Vector2 vec = new Vector2(diff.z, diff.x).normalized;
            rot = Mathf.Atan2(vec.y, vec.x) * 180 / Mathf.PI;
            afterRot = rot - lastRotValue;
            if (rot > 180) rot -= 360;
            if (rot < -180) rot += 360;
            Quaternion euler = Quaternion.Euler(0, afterRot, 0);
            Quaternion q = thisTrs.transform.rotation;
            m_moaiRigid.rotation = euler * q;
        }
        //移動前の座標
        lastPos = m_moaiRigid.position;
        //移動前の顔の向きの値
        lastRotValue = rot;
    }

    public override void Move(Vector2 _direct)
    {
        //ToDo：入力値の整理
        normalInput = new Vector3(_direct.x, 0.0f, _direct.y).normalized;
        moaiVelocity.x = normalInput.x * GetSpeed();
        moaiVelocity.z = normalInput.z * GetSpeed();
    }

    private void OnCollisionEnter(Collision _other)
    {
        if (!isAction) return;
        if (_other.gameObject.layer == LayerMask.NameToLayer("Player") && m_playerInfo.Player != _other.gameObject)
        {
            //ダメージ処理
            Damage(_other.gameObject.GetComponent<MyPlayerObject>().PlayerInfo, MoaiDamage);
        }
        else if (_other.gameObject.layer == LayerMask.NameToLayer("Possess") && m_playerInfo.Player != _other.gameObject)
        {
            //ダメージ処理
            Damage(_other.gameObject.GetComponent<MyItemInterface>().GetInfo, MoaiDamage);
        }
    }
    private void OnTriggerEnter(Collider _other)
    {
        if (!isAction) return;
        if (_other.gameObject.layer == LayerMask.NameToLayer("Possess") && m_playerInfo.Player != _other.gameObject)
        {
            //ダメージ処理
            Damage(_other.gameObject.GetComponent<MyItemInterface>().GetInfo, MoaiDamage);
        }
    }

    /// <summary>
    /// モアイのダメージ処理
    /// </summary>
    private void MoaiDamage()
    {
        //ToDo：モアイの爆発エフェクトの出現など
    }

    /// <summary>
    /// パーティクルの再生
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestroyCoroutine()
    {
        float timer = 0.0f;
        if (particleClone != null)
        {
            while (timer < hitboxExpansionTime)
            {
                //当たり判定の拡大スタート
                hitboxExpansion = Easing.CubicOut(timer, hitboxExpansionTime, m_moaiCol.size.x, hitMaxSize);
                m_moaiCol.size = new Vector3(hitboxExpansion, hitboxExpansion, 0.0f);
                timer += Time.deltaTime;
                // 1秒停止
                yield return null;
            }
            //エフェクトの削除
            Destroy(particleClone.gameObject);
            //プレイヤーを通常状態に変更
            m_playerInfo.ChangeNormal();
            //自身の消失
            Destroy(this.gameObject);
        }
        // コルーチン内からコルーチンを停止させる
        yield break;
    }
}