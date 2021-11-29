using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMoaiItem : MyItemInterface
{
    [Header("モアイの当たり判定")]
    [SerializeField] BoxCollider m_moaiCol = null;

    [Header("モアイの剛体")]
    [SerializeField] Rigidbody m_moaiRigid = null;

    [Header("移動速度")]
    [SerializeField] private float moveSpeed = 10.0f;
    [Header("Euler角修正用")]
    [SerializeField] Transform thisTrs = null;
    private float rot = 0.0f;                       // 座標移動による顔の振り向き角度
    private float lastRot = 0.0f;                   // 前回の角度
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

    public override void Init(MyPlayerInfo _info)
    {
        //プレイヤー情報の受け取り等
        base.Init(_info);

        //モアイの剛体等開始
        m_moaiCol.enabled = true;
        m_moaiRigid.isKinematic = false;

        //ToDo：他の初期化事項
        // 移動更新前の座標
        lastPos = m_moaiRigid.position;
        // 角度の固定
        m_moaiRigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

    public override void ActionInit()
    {
        //アクション完了フラグ初期化
        isEndAntion = false;

        //ToDo：アクション初期化
        // 重力を一時停止
        m_moaiRigid.useGravity = false;
        // モデルサイズによる高低差の修正。Y軸の取得
        defaultYPos = m_moaiRigid.position.y;
    }

    public override void FiexdAction()
    {
        //ToDo：アクション中
        if (!jumpRizeFlg)
        {
            if (timer < rizeTime)
            {// 上昇中
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
        {
            m_moaiRigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            if (timer < downTime)
            {
                float firstY = m_moaiRigid.position.y;
                float downY = Easing.QuartIn(timer, downTime, firstY, defaultYPos);
                Vector3 downPos = m_moaiRigid.position;
                downPos.y = downY;
                m_moaiRigid.position = downPos;
                timer += Time.fixedDeltaTime;
            }
            else
            {
                timer = 0;
                jumpRizeFlg = false;
                // 座標・角度の固定全解除
                m_moaiRigid.constraints = RigidbodyConstraints.None;
                //FixedMoveに遷移
                isEndAntion = true;
                // 重力を再開
                m_moaiRigid.useGravity = true;
                //角度の再固定
                m_moaiRigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            }
        }
    }

    public override void FiexdMove()
    {
        //ToDo：移動
        m_moaiRigid.velocity = moaiVelocity;

        m_moaiRigid.InsideStage(thisTrs.transform.position, 1.0f, 11.0f);

        //移動方向に顔を向ける
        diff = m_moaiRigid.position - lastPos;

        if (diff.magnitude > 0.01f)
        {//前回のPosと座標が違えば
            Vector2 vec = new Vector2(diff.z, diff.x).normalized;
            rot = Mathf.Atan2(vec.y, vec.x) * 180 / Mathf.PI;
            afterRot = rot - lastRot;
            if (rot > 180) rot -= 360;
            if (rot < -180) rot += 360;
            Quaternion euler = Quaternion.Euler(0, afterRot, 0);
            Quaternion q = thisTrs.transform.rotation;
            thisTrs.transform.rotation = euler * q;
        }
        //移動前の座標
        lastPos = m_moaiRigid.position;
        //移動前の顔の向き
        lastRot = rot;
    }

    public override void Move(Vector2 _direct)
    {
        //ToDo：入力値の整理
        normalInput = new Vector3(_direct.x, 0.0f, _direct.y).normalized;
        moaiVelocity.x = normalInput.x * GetSpeed();
        moaiVelocity.z = normalInput.z * GetSpeed();
    }
}