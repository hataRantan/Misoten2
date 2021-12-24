using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムなどが使用するプレイヤー情報
/// </summary>
public class MyPlayerInfo
{
    //アイテムに対応するプレイヤー
    private MyPlayerObject playerObj = null;
    //当たり判定に使用中
    public GameObject Player { get { return playerObj.gameObject; } }
    //プレイヤーの物理運動
    public Rigidbody Rigid { get; private set; }
    //プレイヤーの位置情報
    public Transform Trans { get; private set; }
    //プレイヤーの見た目
    public SkinnedMeshRenderer Render { get; private set; }
    //プレイヤー自身の当たり判定
    public BoxCollider Collider { get; private set; }

    //プレイヤーの初期データ（速度など）
    public PlayerBasicData Data { get; private set; }
    //現在のHp
    public int Hp { get; private set; }
    //プレイヤーナンバー
    public int Number { get { return playerObj.PlayerNumber; } }
    //プレイヤーに対応するUI
    public MyPlayerUI Ui { get { return playerObj.PlayerUI; } }

    //次のプレイヤーの状態を知る
    public MyPlayerObject.MyPlayerState NextState { get; private set; }
    //プレイヤーのアイテム変更をLateUpdateに通知する
    public MyItemInterface NextItem = null;

    //アイテムを最後に使用した位置
    private Vector3 m_itemPos = Vector3.zero;

    public Color OutLineColor { get; private set; }

    /// <summary>
    /// コンポーネントを取得する
    /// </summary>
    public void SetPlayer(MyPlayerObject _player, Color _outLine)
    {
        playerObj = _player;
        Data = _player.PlayerData;
        Hp = Data.GetHp;

        Rigid = _player.gameObject.GetComponent<Rigidbody>();
        Trans = _player.gameObject.transform;
        Render = _player.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        Collider = _player.gameObject.GetComponent<BoxCollider>();

        NextState = MyPlayerObject.MyPlayerState.NONE;

        OutLineColor = _outLine;
    }

    /// <summary>
    /// プレイヤーを通常状態に変更させる
    /// </summary>
    public void ChangeNormal() 
    {
        m_itemPos = playerObj.CurrentItem.gameObject.transform.position;
        NextItem = playerObj.NormalImte;
    }

    /// <summary>
    /// 強力なアイテムから通常アイテムに戻る場合
    /// </summary>
    public void ChangeNormalFromPowerful()
    {
        m_itemPos = Vector3.zero;
        NextItem = playerObj.NormalImte;
    }

    /// <summary>
    /// プレイヤーの次の状態を通知 次の状態にDamageまたはDeadが指定されていれば変更不可
    /// </summary>
    /// <param name="_nextState"></param>
    public void SetLateState(MyPlayerObject.MyPlayerState _nextState)
    {
        if (NextState == MyPlayerObject.MyPlayerState.DAMAGE || NextState == MyPlayerObject.MyPlayerState.DAMAGE) return;

        NextState = _nextState;
    }

    /// <summary>
    /// Hpの減少
    /// </summary>
    /// <param name="_attack"> 敵の攻撃力 </param>
    public void HpReduction(int _attack)
    {
        //減少前のHp
        int lastHp = Hp;
        //減少後のHp
        int currentHp = ((Hp - _attack) > 0) ? Hp - _attack : 0;

        //UIにHp減少を通達
        playerObj.PlayerUI.DamageEffect(lastHp, currentHp);

        //減少分を反映
        Hp = currentHp;

        //状態変更
        if (Hp > 0) NextState = MyPlayerObject.MyPlayerState.DAMAGE;
        else NextState = MyPlayerObject.MyPlayerState.DEAD;
    }

    /// <summary>
    /// プレイヤー生成時に使用
    /// </summary>
    /// <param name="_init"></param>
    public void InitPos(Vector3 _init)
    {
        m_itemPos = _init;
    }

    /// <summary>
    /// プレイヤーの見た目停止
    /// </summary>
    public void StopDraw()
    {
        //描画
        Render.enabled = false;
        //物理運動
        Rigid.isKinematic = true;
        //衝突判定
        Collider.isTrigger = true;
    }

    /// <summary>
    /// プレイヤーの見た目再開
    /// </summary>
    /// <param name="_itemPos"> 最後に使用したアイテムの位置 </param>
    public void ReDraw()
    {
        //描画
        Render.enabled = true;
        //物理運動
        Rigid.isKinematic = false;
        //衝突判定
        Collider.isTrigger = false;
        //アイテムの最後の位置に移動
        Trans.position = m_itemPos;
        //床の高さに合わせる
        playerObj.AdjustFloor();
    }
}
