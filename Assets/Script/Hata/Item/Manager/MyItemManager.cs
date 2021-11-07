using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItemManager : MyUpdater
{
    //アイテムのランク
    //Numは通常使わない
    public enum Rank
    {
        S,A,B,C,D
    }
    [System.Serializable]
    //出現させるアイテム
    public struct RankItem
    {
        public Rank rank;
        public GameObject item;
    }

    [Header("出現させるアイテム一覧")]
    [SerializeField] List<RankItem> createItem = new List<RankItem>();

    [Header("床管理クラス")]
    [SerializeField] FloorManager floorManager = null;

    [Header("プレイヤー管理クラス")]
    [SerializeField] MyPlayerManager playerManager = null;

    [Header("通常のアイテム出現確率")]
    [SerializeField] MyItemChance m_chance = null;

    //ゲームの進行状況を取得するためのクラス
    private MyGameProgress gameProgress = null;

    //ゲーム中に一度、大きなアイテムを出現
    //ランダム出現
    //体力の低いプレイヤーの近くに出現
    //ランダムアイテムの出現

    //しないといけないこと
    //アイテムの出現位置を床管理から取得
    //プレイヤーの体力監視

    //通常は序盤、中盤、終盤でアイテムの出現具合をばらけさせる
    
    //アイテム管理の状態マシーン
    private IStateSpace.StateMachineBase<MyGameProgress.GameProgress, MyPlayerObject> m_machine;


    public override void MySecondInit()
    {
        //状態マシーン初期化

    }

    public override void MyUpdate()
    {
        //状態更新
        m_machine.UpdateState();
    }


    public override void MyLateUpdate()
    {

    }


    /// <summary>
    /// 序盤のアイテム出現
    /// </summary>
    public class MyItemBeggin : IStateSpace.IState<MyGameProgress.GameProgress, MyItemManager>
    {
        public override void Entry()
        {

        }

        public override void Exit()
        {
        }

        public override MyGameProgress.GameProgress Update()
        {
            return MyGameProgress.GameProgress.BEGINNING;
        }
    }


    /// <summary>
    /// 進行状況クラスを取得する
    /// </summary>
    public void SetGameProgress(MyGameProgress _progress)
    {
        gameProgress = _progress;
    }
}
