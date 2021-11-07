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
        INIT,          //準備段階
        FIGHT,         //戦闘
        END            //戦闘終了
    }
    //ToDo：状態マシーンクラス

    //ゲームの進捗具合を確認するクラス
    private MyGameProgress m_progress;


    public override void MyFastestInit()
    {
        m_progress = new MyGameProgress(m_maxGameTime);

        //アイテム管理クラスにゲームの進行状況を渡す
        itemManager.SetGameProgress(m_progress);
    }

    public override void MySecondInit()
    {
       
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
}
