using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyWheeltem : MyItemInterface
{
    [Header("車輪の当たり判定")]
    [SerializeField] BoxCollider m_wheelCol = null;

    [Header("車輪の剛体")]
    [SerializeField] Rigidbody m_wheelRigid = null;

    [Header("車輪のレンダラー")]
    [SerializeField] private Renderer m_wheelRenderer = null;

    [Header("パーティクルの種類")]
    [SerializeField] GameObject particle;
    private ParticleSystem particleClone;
    [Header("爆発エフェクトのサイズ")]
    [SerializeField] private float explosionSize = 2.0f;

    [Header("移動速度")]
    [SerializeField] private float moveSpeed = 0.2f;
    private Vector3 normalInput = Vector3.zero;     //入力値を正規化
    private Vector3 wheelVelocity = Vector3.zero;   //移動
    private Vector3 nowPos = Vector3.zero;          //移動前の座標

    [Header("車輪の回転速度")]
    [SerializeField] private float rotChange = 5.0f;
    private float rotChangeSum = 0.0f;              //車輪回転の加算後の変数

    [Header("当たり判定最大サイズ(初期0.04f)")]
    [SerializeField] private float hitMaxSize = 0.055f;
    private float hitboxExpansion = 0.0f;
    [Header("当たり判定拡大時間")]
    [SerializeField] private int hitboxExpansionTime = 1;

    [Header("offSetの値")]
    [SerializeField] private float offSet = 9.0f;

    //アクション中
    private bool isAction = false;

    public override void Init(MyPlayerInfo _info)
    {
        //プレイヤー情報の受け取り等
        base.Init(_info);

        //モアイの剛体等開始
        m_wheelCol.enabled = true;
        m_wheelRigid.isKinematic = false;
        //他オブジェクトの当たり判定を無効
        m_wheelCol.isTrigger = true;
        //ステージに沈むため無効にする
        m_wheelRigid.useGravity = false;

        //ToDo：他の初期化事項
        //現在の座標取得
        nowPos = m_wheelRigid.position;
        //角度の固定
        m_wheelRigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    public override void ActionInit()
    {
        //アクション完了フラグ初期化
        isEndAntion = false;
        isAction = true;
        //BoxCollider有効
        m_wheelCol.isTrigger = false;

        //ToDo：アクション初期化
        //爆発音
        MyAudioManeger.Instance.PlaySE("Explosion");
        //パーティクル生成、開始
        GameObject obj = Instantiate(particle, m_wheelRigid.position, Quaternion.identity);
        obj.transform.localScale = new Vector3(explosionSize, explosionSize, explosionSize);
        particleClone = obj.GetComponent<ParticleSystem>();
        //プレイヤーを戻した際、アイテムとの当たり判定をとらない
        m_wheelRigid.isKinematic = true;
        //パーティクルの再生
        StartCoroutine(DestroyCoroutine());
        //プレイヤーを通常状態に変更
        m_playerInfo.ChangeNormal();
        //本体の描画を削除
        m_wheelRenderer.enabled = false;
    }

    public override void FiexdAction()
    {
        //ToDo：アクション中
    }

    public override void FiexdMove()
    {
        //ToDo：
        //ここが強制的に各方向の軸へと移動の処理を行ってしまっている(酔っ払い歩行)
        m_wheelRigid.velocity = wheelVelocity * moveSpeed;

        //進行方向の取得
        Vector3 diff = m_wheelRigid.position - nowPos;
        if (diff.magnitude > 0.01f)
        {
            //顔を向ける 
            Quaternion lookRotatin = Quaternion.LookRotation(diff);
            //回転
            Quaternion objRotation = Quaternion.AngleAxis(rotChangeSum, Vector3.right);
            //合成
            m_wheelRigid.rotation = lookRotatin * objRotation;
            //回転用変数の整理
            rotChangeSum += rotChange;
            if (rotChangeSum > 360)
            {
                rotChangeSum = 0.0f;
            }
        }
        //座標の更新
        nowPos = m_wheelRigid.position;
        //操作アイテムがステージ内かどうか
        m_wheelRigid.InsideStage(m_wheelRigid.position, 0.1f, offSet);

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

    /// <summary>
    /// モアイのダメージ処理
    /// </summary>
    private void WheelDamage()
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
                hitboxExpansion = Easing.CubicOut(timer, hitboxExpansionTime, m_wheelCol.size.x, hitMaxSize);
                m_wheelCol.size = new Vector3(hitboxExpansion, hitboxExpansion, hitboxExpansion);

                timer += Time.deltaTime;
                // 1フレーム停止
                yield return null;
            }
            //エフェクトの削除
            Destroy(particleClone.gameObject);
            //自身の消失
            Destroy(this.gameObject);
        }
        // コルーチン内からコルーチンを停止させる
        yield break;
    }
}
