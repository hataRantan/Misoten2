﻿using System.Collections;
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

    [Header("アイテム取得範囲")]
    [SerializeField]
    private BoxCollider m_getRange = null;

    //アイテム出現に利用
    [Header("モデルの最下地点")]
    [SerializeField] Transform m_bottomPoint = null;
    public Transform Bottom { get { return m_bottomPoint; } }

    [Header("生成時の角度")]
    [SerializeField] Vector3 m_generatedDegree = Vector3.zero;

    [Header("仮面"), SerializeField] GameObject m_mask = null;
    public Vector3 GeneretedDegree { get { return m_generatedDegree; } }

    // 衝突したオブジェクト（取得やダメージ衝突に使用）
    protected GameObject hitObj = null;

    //プレイヤー情報
    protected MyPlayerInfo m_playerInfo = null;
    public MyPlayerInfo GetInfo { get { return m_playerInfo; } }

    //出現位置
    private Vector2Int m_appearPos = Vector2Int.zero;
    //アイテム管理クラスへの参照
    private static MyItemManager2 itemManager = null;

    //自身を使用しているプレイヤーがいるかどうか
    public bool isUser = false;

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
    
    public virtual void Init(MyPlayerInfo _info)
    {
        m_getRange.enabled = false;
        m_playerInfo = _info;

        m_mask.SetActive(true);
        m_mask.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", SavingFace.Instance.GetFace(m_playerInfo.Number));
    }
    
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
    public virtual void Action(Vector2 _input) { }

    /// <summary>
    /// プレイヤーにダメージを与える
    /// </summary>
    /// <param name="_otherPlayer"> 対象となる敵プレイヤー </param>
    /// <param name="_damageAction"> ダメージ時に行うアイテムごとの処理</param>
    /// 使用例 Damage(player , MyMissileItem.MissileDamage)
    public void Damage(MyPlayerInfo _otherPlayer,UnityEngine.Events.UnityAction _damageAction)
    {
        //相手Hpの減少
        _otherPlayer.HpReduction(m_data.GetAttack);

        //ダメージ後のアクション実行
        _damageAction();
    }

    /// <summary>
    /// アイテムの取得範囲を切り替えを行う
    /// </summary>
    public void SwitchGetRangeEnabled(bool _enbled)
    {
        m_getRange.enabled = _enbled;
    }

    /// <summary>
    /// 出現位置を保存する
    /// </summary>
    public void SetAppearPos(MyItemManager2 _manager, Vector2Int _generated)
    {
        if (!itemManager) itemManager = _manager;

        m_appearPos = _generated;
    }

    public void ClearAppearPos()
    {
        if (itemManager != null)
        {
            itemManager.ReSetCall(m_appearPos);
        }
        else
        {
            Debug.Log("アイテムマネージャーが設定されていません");
        }
    }
}


