using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMoaiItem : MyItemInterface
{
    [Header("モアイの当たり判定")]
    [SerializeField] BoxCollider m_moaiCol = null;

    [Header("モアイの剛体")]
    [SerializeField] Rigidbody m_moaiRigid = null;

    public override void Init(MyPlayerInfo _info)
    {
        //プレイヤー情報の受け取り等
        base.Init(_info);

        //モアイの剛体等開始
        m_moaiCol.enabled = true;
        m_moaiRigid.isKinematic = true;

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
