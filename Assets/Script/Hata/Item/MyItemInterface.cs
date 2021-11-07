using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムのインターフェイス
/// </summary>
public abstract class MyItemInterface : MonoBehaviour
{
    //アイテムの攻撃力など
    [Header("アイテムのデータ")]
    [SerializeField]
    protected ItemData m_data = null;
    public ItemData ItemData { get { return m_data; } }

    // 衝突したオブジェクト（取得やダメージ衝突に使用）
    protected GameObject hitObj = null;

    //プレイヤー情報
    protected MyPlayerInfo m_playerInfo = null;

    //自身を使用しているプレイヤーがいるかどうか
    public bool isUser
    {
        get
        {
            if (m_playerInfo != null) return true;
            else return false;
        }
    }

    private void Awake()
    {
        //更新が必要ないので、止める
        enabled = false;
        isEndAntion = false;
    }

    /// <summary>
    /// アイテムの速度を渡す
    /// </summary>
    protected float GetSpeed()
    {
        return m_data.GetSppedFactor * m_playerInfo.Data.GetSpped;
    }

    //アクション完了フラグ
    public bool isEndAntion { get; protected set; }

    /// <summary>
    /// アイテム初期化 baze.Init()で呼び出し必須
    /// </summary>
    
    public virtual void Init(MyPlayerInfo _info) { m_playerInfo = _info; }
    
    /// <summary>
    /// アイテムの後処理
    /// </summary>
    public virtual void Exit() { }

    /// <summary>
    /// 物理運動処理
    /// </summary>
    public abstract void FiexdMove();

    /// <summary>
    /// 移動方向を処理する
    /// </summary>
    public abstract void Move(Vector2 _direct);

    /// <summary>
    /// アクション処理の前の初期化処理
    /// </summary>
    public abstract void ActionInit();

    /// <summary>
    /// アクション終了処理
    /// </summary>
    public virtual void ActionExit() { }

    /// <summary>
    /// 物理アクション処理
    /// </summary>
    public abstract void FiexdAction();

    /// <summary>
    /// アクション処理
    /// </summary>
    public virtual void Action() { }

    /// <summary>
    /// プレイヤーにダメージを与える
    /// </summary>
    /// <param name="_otherPlayer"> 対象となる敵プレイヤー </param>
    /// <param name="_damageAction"> ダメージ時に行うアイテムごとの処理</param>
    /// 使用例 Damage(player , MyMissileItem.MissileDamage)
    public void Damge(MyPlayerInfo _otherPlayer,UnityEngine.Events.UnityAction _damageAction)
    {
        //相手Hpの減少
        _otherPlayer.HpReduction(m_data.GetAttack);

        //ダメージ後のアクション実行
        _damageAction();
    }
}
