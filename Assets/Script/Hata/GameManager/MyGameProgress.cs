using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの進捗具合を管理するクラス
// ToDo：適当に記述するので、修正必要
/// </summary>
public class MyGameProgress
{
    //ゲームの経過時間
    private float m_gameElapsedTime = 0.0f;
    //ゲームの最大経過時間
    private float m_gameMaxTime = 0.0f;
    //ゲームの3分の1の時間
    private float m_oneThirdGameTime = 0.0f;
    //ゲームの3分の2の時間
    private float m_twoThirdGameTime = 0.0f;

    //プレイヤーの合計Hp
    private int m_totalHp = 0;
    private int m_onwThirdHp = 0;
    private int m_twoThidHp = 0;

    //プレイヤーの参加人数
    private int m_playerMaxNum =0;

    //ゲームの進捗具合
    public enum GameProgress
    {
        BEGINNING,
        MIDDLE,
        FINAL,
        END
    }
    //ゲームの進捗具合を取得する
    public GameProgress Progress { get; private set; }

    //プレイヤー管理クラス
    MyPlayerManager playerManager = null;

    /// <summary>
    /// 初期化
    /// </summary>
    public MyGameProgress(float _maxTime)
    {
        Progress = GameProgress.BEGINNING;

        //分から秒へと変換
        m_gameMaxTime = _maxTime * 60.0f;
        //ゲーム進捗確認のための時間を求める
        m_oneThirdGameTime = m_gameMaxTime / 3.0f;
        m_twoThirdGameTime = m_oneThirdGameTime * 2;

        //管理クラスを取得
        playerManager = GameObject.FindWithTag("PlayerManager").GetComponent<MyPlayerManager>();

        //プレイヤーの合計Hp取得
        m_totalHp = playerManager.GetMaxTotalHp;
        m_onwThirdHp = m_totalHp / 3; //小数点以下切り捨て
        m_twoThidHp = m_onwThirdHp * 2;
        m_onwThirdHp = m_totalHp - m_onwThirdHp;
        m_twoThidHp = m_totalHp - m_twoThidHp;

        //プレイヤーの合計人数
        m_playerMaxNum = playerManager.GetMaxPlayerNum;
    }

    /// <summary>
    /// 進捗具合の判断をする
    /// </summary>
    public void ProgressJudement()
    {
        switch(Progress)
        {
            case GameProgress.BEGINNING:
                if (IsTransitionMIDDLE()) Progress = GameProgress.MIDDLE;
                break;

            case GameProgress.MIDDLE:
                if (IsTransitionFinl()) Progress = GameProgress.FINAL;
                break;

            case GameProgress.FINAL:
                if (IsTransitionEnd()) Progress = GameProgress.END;
                break;

            case GameProgress.END:
                break;
        }

        //時間経過
        m_gameElapsedTime += Time.deltaTime;

    }

    /// <summary>
    /// BegginからMiddleに変更するかの確認
    /// </summary>
    private bool IsTransitionMIDDLE()
    {
        //条件1：経過時間が3分の1を超えたら遷移
        if (m_gameElapsedTime >= m_oneThirdGameTime) return true;
        //条件2：プレイヤーの合計Hpが3分の1減ったら遷移
        if (playerManager.GetPlayerCurrentTotalHp() <= m_onwThirdHp) return true;
        //条件3：１人でも脱落していたら遷移
        if (playerManager.GetDropPlayerNum() > 0) return true;

        Debug.Log("Beggin状態");
        return false;
    }

    /// <summary>
    /// MiddleからFinalに変更するかの確認
    /// </summary>
    private bool IsTransitionFinl()
    {
        //条件1：経過時間が3分の2を超えたら遷移
        if (m_gameElapsedTime >= m_twoThirdGameTime) return true;
        //条件2：プレイヤーの合計Hpが3分の2減ったら遷移
        if (playerManager.GetPlayerCurrentTotalHp() <= m_twoThidHp) return true;
        //条件3：プレイヤーが最大人数の半数以上減ったら遷移
        if (playerManager.GetDropPlayerNum() >= m_playerMaxNum / 2) return true;

        Debug.Log("Middle状態");
        return false;
    }

    /// <summary>
    /// FinalからEndに変更するかの確認
    /// </summary>
    private bool IsTransitionEnd()
    {
        //条件1：生存しているプレイヤーが0or1人になれば終了
        if (m_playerMaxNum - playerManager.GetDropPlayerNum() <= 1) return true;

        //条件2：ゲーム終了時間を超えたら
        if (m_gameElapsedTime >= m_gameMaxTime) return true;

        Debug.Log("FINAL状態");

        return false;
    }
}
