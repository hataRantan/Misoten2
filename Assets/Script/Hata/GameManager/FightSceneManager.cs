using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 戦闘シーンの管理を行うクラス
/// </summary>
public class FightSceneManager : MyUpdater
{
    //[Header("プレイヤー管理クラス")]
    //[SerializeField] MyPlayerManager playerManager = null;

    [Header("アイテム管理クラス")]
    [SerializeField] MyItemManager itemManager = null;

    [Header("ゲームの最大時間(単位は分)")]
    [Range(1.0f, 5.0f)]
    [SerializeField] float m_maxGameTime = 3.0f;

    //シーンの状態一覧
    enum FightSceneType
    {
        FIGHT,         //戦闘
        RESULT            //戦闘終了
    }
    //状態マシーンクラス
    IStateSpace.StateMachineBase<FightSceneType, FightSceneManager> m_machine = new IStateSpace.StateMachineBase<FightSceneType, FightSceneManager>();

    //ゲームの進捗具合を確認するクラス
    private MyGameProgress m_progress;

  
    /// <summary>
    /// ゲーム初期化
    /// </summary>
    public override void MySecondInit()
    {
        m_progress = new MyGameProgress(m_maxGameTime);

        //アイテム管理クラスにゲームの進行状況を渡す
        itemManager.SetGameProgress(m_progress);

        //状態の初期化
        m_machine.AddState(FightSceneType.FIGHT, new FightState(), this);
        m_machine.AddState(FightSceneType.RESULT, new ResultState(), this);
        m_machine.InitState(FightSceneType.FIGHT);
    }


    public override void MyUpdate()
    {
       
    }

    public override void MyLateUpdate()
    {
        //状態更新
        m_machine.UpdateState();
        //ゲームの進捗具合を確認
    }

    /// <summary>
    /// ゲームの進捗具合を渡す
    /// </summary>
    public MyGameProgress.GameProgress GetProgress() { return m_progress.Progress; }


    /// <summary>
    /// 戦闘中の状態
    /// </summary>
    private class FightState : IStateSpace.IState<FightSceneType, FightSceneManager>
    {
        public override void Entry() { }

        public override void Exit() { }

        public override FightSceneType Update()
        {
            //ゲームの進捗状態を更新
            board.m_progress.ProgressJudement();

            //ゲーム終了状態へ遷移する
            if (board.m_progress.Progress == MyGameProgress.GameProgress.END) return FightSceneType.RESULT;

            return FightSceneType.FIGHT;
        }
    }

    /// <summary>
    /// リザルト処理
    /// </summary>
    private class ResultState : IStateSpace.IState<FightSceneType, FightSceneManager>
    {
        public override void Entry()
        {
            //リザルト処理開始
        }

        public override void Exit()
        {
        }

        public override FightSceneType Update()
        {
            return FightSceneType.RESULT;
        }
    }
}
