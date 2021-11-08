using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItemManager : MyUpdater
{
    //アイテムのランク
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
    //ランクごとにアイテム
    Dictionary<Rank, List<GameObject>> rankItems = new Dictionary<Rank, List<GameObject>>();

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
    private IStateSpace.StateMachineBase<MyGameProgress.GameProgress, MyItemManager> m_machine;

    //ToDo：後で消すことテスト用
    [SerializeField]
    MyItemAppearTest _testCanvas = null;

    public override void MySecondInit()
    {
        //それぞれのランクのアイテムの個数を求める
        List<GameObject> sRankItems = new List<GameObject>();
        List<GameObject> aRankItems = new List<GameObject>();
        List<GameObject> bRankItems = new List<GameObject>();
        List<GameObject> cRankItems = new List<GameObject>();
        List<GameObject> dRankItems = new List<GameObject>();
        
        foreach (var rankItem in createItem)
        {
            switch(rankItem.rank)
            {
                case Rank.S:
                    sRankItems.Add(rankItem.item);
                    break;

                case Rank.A:
                    aRankItems.Add(rankItem.item);
                    break;

                case Rank.B:
                    bRankItems.Add(rankItem.item);
                    break;

                case Rank.C:
                    cRankItems.Add(rankItem.item);
                    break;

                case Rank.D:
                    dRankItems.Add(rankItem.item);
                    break;
            }
        }

        //ランクごとのListをDictionaryにセットする
        rankItems.Add(Rank.S, sRankItems);
        rankItems.Add(Rank.A, aRankItems);
        rankItems.Add(Rank.B, bRankItems);
        rankItems.Add(Rank.C, cRankItems);
        rankItems.Add(Rank.D, dRankItems);


        //状態マシーン初期化
        m_machine = new IStateSpace.StateMachineBase<MyGameProgress.GameProgress, MyItemManager>();
        m_machine.AddState(MyGameProgress.GameProgress.BEGINNING, new MyItemBeggin(), this);
        m_machine.AddState(MyGameProgress.GameProgress.MIDDLE, new MyItemMiddle(), this);
        m_machine.AddState(MyGameProgress.GameProgress.FINAL, new MyItemFinal(), this);

        //初期状態
        m_machine.InitState(MyGameProgress.GameProgress.BEGINNING);
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
            //現在の進行状況を設定
            board.m_chance.SetProgress(MyGameProgress.GameProgress.BEGINNING);
        }

        public override void Exit() { }

        public override MyGameProgress.GameProgress Update()
        {
            if (board.gameProgress.Progress == MyGameProgress.GameProgress.MIDDLE)
                return MyGameProgress.GameProgress.MIDDLE;

            return MyGameProgress.GameProgress.BEGINNING;
        }
    }

    /// <summary>
    /// 中盤のアイテム出現
    /// </summary>
    public class MyItemMiddle : IStateSpace.IState<MyGameProgress.GameProgress, MyItemManager>
    {
        public override void Entry()
        {
            //現在の進行状況を設定
            board.m_chance.SetProgress(MyGameProgress.GameProgress.MIDDLE);
        }

        public override void Exit() { }

        public override MyGameProgress.GameProgress Update()
        {
            if (board.gameProgress.Progress == MyGameProgress.GameProgress.FINAL)
                return MyGameProgress.GameProgress.FINAL;

            return MyGameProgress.GameProgress.MIDDLE;
        }
    }

    /// <summary>
    /// 終盤のアイテム出現
    /// </summary>
    public class MyItemFinal : IStateSpace.IState<MyGameProgress.GameProgress, MyItemManager>
    {
        public override void Entry()
        {
            //現在の進行状況を設定
            board.m_chance.SetProgress(MyGameProgress.GameProgress.FINAL);
        }

        public override void Exit() { }

        public override MyGameProgress.GameProgress Update()
        {
            
            return MyGameProgress.GameProgress.FINAL;
        }
    }

    /// <summary>
    /// 進行状況クラスを取得する
    /// </summary>
    public void SetGameProgress(MyGameProgress _progress)
    {
        gameProgress = _progress;
    }


    /// <summary>
    /// ランクから出現させるアイテムを取得する
    /// </summary>
    private GameObject GetAppearItem(Rank _rank)
    {
        //指定のランクのアイテムの個数を取得
        int itemNum = rankItems[_rank].Count;

        if(itemNum <=0) //エラーチェック
        {
            Debug.LogError("MyItemManager：指定したランクのアイテムがありません");
            return null;
        }

        //ToDo：同じランクのアイテムの出現は、現状同じ
        return rankItems[_rank][Random.Range(0, itemNum)];
    }
}
