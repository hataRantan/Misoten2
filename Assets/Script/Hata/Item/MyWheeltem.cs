using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyWheeltem : MyItemInterface
{
    [Header("車輪の当たり判定")]
    [SerializeField] BoxCollider m_wheelCol = null;

    [Header("車輪の剛体")]
    [SerializeField] Rigidbody m_wheelRigid = null;

    public override void Init(MyPlayerInfo _info)
    {
        //プレイヤー情報の受け取り等
        base.Init(_info);

        //モアイの剛体等開始
        m_wheelCol.enabled = true;
        m_wheelRigid.isKinematic = true;

        //ToDo：他の初期化事項
    }

    public override void ActionInit()
    {
        //アクション完了フラグ初期化
        isEndAntion = false;

        //ToDo：アクション初期化
    }

    public override void FiexdAction()
    {
        //ToDo：アクション中
    }

    public override void FiexdMove()
    {
        //ToDo：移動
    }

    public override void Move(Vector2 _direct)
    {
        //ToDo：入力値の整理
    }
}
