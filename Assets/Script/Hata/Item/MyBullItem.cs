using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBullItem : MyItemInterface
{
    [Header("牛の当たり判定")]
    [SerializeField] BoxCollider m_bullCol = null;

    [Header("牛の剛体")]
    [SerializeField]
    Rigidbody m_bullRigid = null;

    public override void Init(MyPlayerInfo _info)
    {
        //プレイヤー情報の受け取り等
        base.Init(_info);

        //牛自体の剛体等開始
        m_bullCol.enabled = true;
        m_bullRigid.isKinematic = false;

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
