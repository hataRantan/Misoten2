﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 戦闘シーンの管理を行うクラス
/// </summary>
public class FightSceneManager : MyUpdater
{
    [Header("プレイヤー管理クラス")]
    [SerializeField] MyPlayerManager playerManager = null;

    [Header("アイテム管理クラス")]
    [SerializeField] MyItemManager2 itemManager = null;

    [Header("リザルトグループ")]
    [SerializeField]
    CanvasGroup m_resultGroup = null;

    [Header("待機時間（単位は秒）")]
    [Range(0.0f, 10.0f)]
    [SerializeField]
    float m_standByTime = 5;

    [Header("待機グループ")]
    [SerializeField]
    CanvasGroup m_standbyGroup = null;

    [Header("待機背景")]
    [SerializeField]
    Image m_standbyBack = null;

    [Header("待機文章")]
    [SerializeField]
    TMPro.TextMeshProUGUI m_standBytext = null;

    [Header("ゲームの最大時間(単位は分)")]
    [Range(1.0f, 5.0f)]
    [SerializeField]
    float m_maxGameTime = 5.0f;
    //static const float m_maxGameTime = 5.0f;

    //シーンの状態一覧
    enum FightSceneType
    {
        INIT,
        FIGHT,         //戦闘
        RESULT            //戦闘終了
    }
    //状態マシーンクラス
    IStateSpace.StateMachineBase<FightSceneType, FightSceneManager> m_machine = new IStateSpace.StateMachineBase<FightSceneType, FightSceneManager>();

    //ゲームの進捗具合を確認するクラス
    //private MyGameProgress m_progress;

  
    /// <summary>
    /// ゲーム初期化
    /// </summary>
    public override void MySecondInit()
    {
        //m_progress = new MyGameProgress(m_maxGameTime);

        //アイテム管理クラスにゲームの進行状況を渡す
        //itemManager.SetGameProgress(m_progress);

        //制限時間を秒に変換
        m_maxGameTime *= 60.0f;

        //状態の初期化
        m_machine.AddState(FightSceneType.INIT, new InitState(), this);
        m_machine.AddState(FightSceneType.FIGHT, new FightState(), this);
        m_machine.AddState(FightSceneType.RESULT, new ResultState(), this);
        m_machine.InitState(FightSceneType.INIT);
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
    //public MyGameProgress.GameProgress GetProgress() { return m_progress.Progress; }
    private class InitState : IStateSpace.IState<FightSceneType, FightSceneManager>
    {
        float timer = 0.0f;
        public override void Entry()
        {
            board.m_resultGroup.alpha = 0.0f;

            board.SetGameUpdate(false);
        }

        public override void Exit()
        {
            board.SetGameUpdate(true);
        }

        public override FightSceneType Update()
        {
            if(timer<board.m_standByTime)
            {
                timer += Time.deltaTime;

                board.m_standBytext.text = ((int)board.m_standByTime - (int)timer).ToString();
            }
            else
            {
                board.m_standBytext.text = "Start!!";

                return FightSceneType.FIGHT;
            }

            return FightSceneType.INIT;
        }
    }


    /// <summary>
    /// 戦闘中の状態
    /// </summary>
    private class FightState : IStateSpace.IState<FightSceneType, FightSceneManager>
    {
        float timer = 0.0f;
        public override void Entry()
        {
            timer = 0.0f;

            board.m_standbyGroup.alpha = 0.0f;
        }

        public override void Exit() { }

        public override FightSceneType Update()
        {
            if (timer < board.m_maxGameTime)
            {
                timer += Time.deltaTime;
            }
            else
            {
                return FightSceneType.RESULT;
            }

            if (board.playerManager.GetDropPlayerNum() >= GameInPlayerNumber.Instance.CurrentPlayerNum - 1)
            {
                return FightSceneType.RESULT;
            }

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
            board.m_resultGroup.alpha = 1.0f;
        }

        public override void Exit()
        {
        }

        public override FightSceneType Update()
        {
            return FightSceneType.RESULT;
        }
    }

    /// <summary>
    /// ゲームの更新を停止する
    /// </summary>
    private void SetGameUpdate(bool _doUpdate)
    {

        playerManager.gameObject.GetComponent<MyUpdaterList>().SetUpdate(_doUpdate);
        itemManager.gameObject.GetComponent<MyUpdaterList>().SetUpdate(_doUpdate);
    }
}
