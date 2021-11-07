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
    //プレイヤーの物理運動
    public Rigidbody Rigid { get; private set; }
    //プレイヤーの位置情報
    public Transform Trans { get; private set; }

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

    /// <summary>
    /// コンポーネントを取得する
    /// </summary>
    public void SetPlayer(MyPlayerObject _player)
    {
        playerObj = _player;
        Data = _player.PlayerData;
        Hp = Data.GetHp;

        Rigid = _player.gameObject.GetComponent<Rigidbody>();
        Trans = _player.gameObject.transform;

        NextState = MyPlayerObject.MyPlayerState.NONE;
    }

    /// <summary>
    /// プレイヤーを通常状態に変更させる
    /// </summary>
    public void ChangeNormal() { NextItem = playerObj.NormalImte; }

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
}
