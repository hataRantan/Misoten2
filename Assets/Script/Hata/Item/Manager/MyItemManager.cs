using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemManagement;
/// <summary>
/// アイテム生成を管理する
/// </summary>

//ToDo：残りの仕様一覧
// モニターに対応させる
// インターバルをもう少し改良した方がいい、出てくるタイミングが知りたい
// はてなアイテムに対応させる
//全部一旦放置するしかない
//アイテムの生成時の処理エフェクト出したりとか

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

    [Header("ゲーム中一度だけの強力なアイテム")]
    [SerializeField] List<GameObject> powerfulItems = null;

    //[Header("プレイヤー管理クラス")]
    //[SerializeField] MyPlayerManager playerManager = null;

    private MyPlayerManager playerManager = null;

    [Header("通常のアイテム出現確率")]
    [SerializeField] ItemSelect m_chance = null;

    [Header("アイテム出現間隔管理クラス")]
    [SerializeField] ItemAppearanceInterval m_intervel = null;

    [Header("アイテム出現方法管理クラス")]
    [SerializeField] ItemSelectAppearanceMethods m_choiceAppear = null;

    //ゲームの進行状況を取得するためのクラス
    private MyGameProgress gameProgress = null;

    //出現位置を取得するクラス
    private ItemAppearsPosition m_itemAppear = null;

    //ToDo：はてなオブジェクトを出現させるか

    //アイテム管理の状態マシーン
    private IStateSpace.StateMachineBase<MyGameProgress.GameProgress, MyItemManager> m_machine;

    //ToDo：後で消すことテスト用
    //[SerializeField]
    //MyItemAppearTest _testCanvas = null;

    public override void MySecondInit()
    {
        //プレイヤー管理クラス取得
        playerManager = GameObject.FindWithTag("PlayerManager").GetComponent<MyPlayerManager>();

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

        //初期化
        m_itemAppear = new ItemAppearsPosition();
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
            board.m_intervel.SetProgress(MyGameProgress.GameProgress.BEGINNING);
        }

        public override void Exit() { }

        public override MyGameProgress.GameProgress Update()
        {
            //指定時間ごとにアイテム生成
            if(board.m_intervel.isCreate())
            {
                board.ChoiceItemInstantiate();
            }

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
            board.m_intervel.SetProgress(MyGameProgress.GameProgress.MIDDLE);

            //強力なアイテムを出現させる
            board.PowerfulItemInstantiate();
        }

        public override void Exit() { }

        public override MyGameProgress.GameProgress Update()
        {
            //指定時間ごとにアイテム生成
            if (board.m_intervel.isCreate())
            {
                board.ChoiceItemInstantiate();
            }


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
            board.m_intervel.SetProgress(MyGameProgress.GameProgress.FINAL);
        }

        public override void Exit() { }

        public override MyGameProgress.GameProgress Update()
        {
            //指定時間ごとにアイテム生成
            if (board.m_intervel.isCreate())
            {
                board.ChoiceItemInstantiate();
            }

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
    /// アイテムの出現方法を抽選して、出現させる
    /// </summary>
    private void ChoiceItemInstantiate()
    {
        switch (m_choiceAppear.SelectMethod(playerManager.GetPlayerTotalHp()))
        {
            case ItemSelectAppearanceMethods.Method.NORAML:
                NormalItemInstantiate();
                break;
            
            case ItemSelectAppearanceMethods.Method.BY_PLAYER:
                NormalItemInstantiateByPlayer();
                break;
        }
    }

    /// <summary>
    /// 通常時のアイテム出現
    /// </summary>
    private void NormalItemInstantiate()
    {
        InstantiateItem(GetAppearItem(m_chance.GetItemRank()), m_itemAppear.GetNormalItemPos());
    }

    /// <summary>
    /// プレイヤーのHpを参照して、残りHpが少ないプレイヤーの近くにアイテムを出現させる
    /// </summary>
    private void NormalItemInstantiateByPlayer()
    {
        InstantiateItem(GetAppearItem(m_chance.GetItemRank()), m_itemAppear.GetNormalItemPos_ByPlayer(playerManager.GetMinHpPlayerPos()));
    }

    /// <summary>
    /// 強力なアイテムの出現
    /// </summary>
    private void PowerfulItemInstantiate()
    {
        int idx = Random.Range(0, powerfulItems.Count);
        InstantiateItem(powerfulItems[idx], Vector3.zero);
    }

    /// <summary>
    /// 指定した場所にアイテムを出現させる
    /// </summary>
    /// <param name="_appearPos">出現位置 </param>
    private void InstantiateItem(GameObject _item, Vector3 _appearPos)
    {
        GameObject created = Instantiate(_item, _appearPos, Quaternion.identity);
        //ToDo：アイテム出現時の初期化を呼びだす事
        //ToDo：アイテム出現エフェクトの呼び出し
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
