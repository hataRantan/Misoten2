using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMissileItem : MyItemInterface
{
    [Header("ミサイルの当たり判定")]
    [SerializeField] BoxCollider m_missileCol = null;

    [Header("ミサイルの剛体")]
    [SerializeField]
    Rigidbody m_missileRigid = null;

    [Header("パーティクルの種類")]
    [SerializeField] GameObject particle;
    private ParticleSystem particleClone;
    [Header("爆発エフェクトのサイズ")] private float explosionSize = 1.0f;
    private float deadTime = 0.0f;                  // オブジェクトの消滅時間調整

    // ミサイルの回転制御
    private Vector3 missileRotate = Vector3.zero;

    // 回転の加速度
    private float rotateAcceleration = 0.0f;

    [Header("回転速度")]
    [SerializeField] private float rotateSpeed = 70.0f;

    // 壁との衝突判定
    private bool isHitWall = false;
    //アクション中
    private bool isAction = false;

    public override void Init(MyPlayerInfo _info)
    {
        //プレイヤー情報の受け取り等
        base.Init(_info);

        //牛自体の剛体等開始
        m_missileCol.enabled = true;
        m_missileRigid.isKinematic = false;


    }

    public override void ActionInit()
    {
        //アクション完了フラグ初期化
        isEndAntion = false;
        isAction = true;

        //アクション初期化
        // 回転停止
        m_missileRigid.freezeRotation = true;
        // オブジェクトのZ軸の向き取得
        missileRotate = m_missileRigid.transform.forward;
    }


    public override void FiexdAction()
    {
        //ToDo：アクション中
        // 壁にぶつかったらendactonをtrueにする
        // 移動処理
        m_missileRigid.velocity = missileRotate * GetSpeed();

        if (isHitWall)
        {
            //牛自体の剛体等停止
            m_missileCol.enabled = false;
            m_missileRigid.isKinematic = true;
        }
    }

    public override void FiexdMove()
    {
        //ToDo：移動
        // Y座標の回転処理
        m_missileRigid.angularVelocity = new Vector3(0.0f, rotateAcceleration * Time.fixedDeltaTime, 0.0f);
    }

    public override void Move(Vector2 _direct)
    {
        //ToDo：入力値の整理
        // 回転速度
        rotateAcceleration = -rotateSpeed * _direct.x;
    }
    private void OnCollisionEnter(Collision _other)
    {
        if (!isAction) return;

        if (_other.gameObject.layer == LayerMask.NameToLayer("Player") && m_playerInfo.Player != _other.gameObject)
        {
            //パーティクル開始
            GameObject obj = Instantiate(particle, m_missileRigid.position, Quaternion.identity);
            obj.transform.localScale = new Vector3(explosionSize, explosionSize, explosionSize);
            particleClone = obj.GetComponent<ParticleSystem>();
            //本体の描画を削除
            this.gameObject.SetActive(false);
            //ダメージ処理
            Damage(_other.gameObject.GetComponent<MyPlayerObject>().PlayerInfo, MissileDamage);
            //プレイヤーを通常状態に変更
            m_playerInfo.ChangeNormal();

            if (particleClone != null)
            {
                if (deadTime < 2.0f)
                {
                    deadTime += Time.fixedDeltaTime;
                }
                else
                { 
                    //エフェクトの削除
                    Destroy(particleClone.gameObject);
                    //自身の消失
                    Destroy(this.gameObject);
                }
            }
        }

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
    /// ミサイルのダメージ処理
    /// </summary>
    private void MissileDamage()
    {
        //ToDo：ミサイルの爆発エフェクトの出現など

    }

}
