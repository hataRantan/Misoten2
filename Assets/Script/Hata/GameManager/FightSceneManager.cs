using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 戦闘シーンの管理を行うクラス
/// </summary>
public class FightSceneManager : MyUpdater
{
    [Header("プレイヤー管理クラス")]
    [SerializeField] MyPlayerManager playerManager = null;

    [Header("アイテム管理クラス")]
    [SerializeField] MyItemManager itemManager = null;

    [Header("ゲームの最大時間(単位は分)")]
    [Range(1.0f, 5.0f)]
    [SerializeField] float m_maxGameTime = 3.0f;

    //シーンの状態一覧
    enum FightSceneType
    {
        FIGHT,         //戦闘
        END            //戦闘終了
    }
    //状態マシーンクラス
    IStateSpace.StateMachineBase<FightSceneType, FightSceneManager> m_machine = new IStateSpace.StateMachineBase<FightSceneType, FightSceneManager>();

    //ゲームの進捗具合を確認するクラス
    private MyGameProgress m_progress;

    /// <summary>
    /// ゲーム初期化
    /// </summary>
    public override void MyFastestInit()
    {
        m_progress = new MyGameProgress(m_maxGameTime);

        //アイテム管理クラスにゲームの進行状況を渡す
        itemManager.SetGameProgress(m_progress);
    }

    public override void MySecondInit()
    {
        m_machine.AddState(FightSceneType.FIGHT, new FightState(), this);
    }


    public override void MyUpdate()
    {
       
    }

    public override void MyLateUpdate()
    {
        //ゲームの進捗具合を確認
        m_progress.ProgressJudement(playerManager.GetPlayerTotalHpPercentage(), playerManager.GetDropPlayerNum());
    }

    /// <summary>
    /// ゲームの進捗具合を渡す
    /// </summary>
    public MyGameProgress.GameProgress GetProgress() { return m_progress.Progress; }


    private class FightState : IStateSpace.IState<FightSceneType, FightSceneManager>
    {
        public override void Entry()
        {
        }

        public override void Exit()
        {
        }

        public override FightSceneType Update()
        {
            return FightSceneType.FIGHT;
        }
    }
}
