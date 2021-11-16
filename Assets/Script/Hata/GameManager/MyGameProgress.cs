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

    //ゲームの進捗具合
    public enum GameProgress
    {
        BEGINNING,
        MIDDLE,
        FINAL
    }
    //ゲームの進捗具合を取得する
    public GameProgress Progress { get; private set; }

    /// <summary>
    /// 初期化
    /// </summary>
    public MyGameProgress(float _maxTime)
    {
        Progress = GameProgress.BEGINNING;

        //分から秒へと変換
        m_gameMaxTime = _maxTime * 60.0f;
    }

    /// <summary>
    /// 進捗具合の判断をする
    /// 現在の進捗進行方法について
    /// 序盤 -> 中盤
    /// └経過時間が最大時間の3分の1を超えた場合
    ///  └プレイヤーの最大体力の3分の1が減少orプレイヤーが１人以上脱落した場合
    ///  中盤 ->　終盤
    ///   └経過時間が最大時間の3分の2を超えた場合
    ///   └プレイヤーの最大体力の2分の1が減少orプレイヤーが2人以上脱落した場合
    /// </summary>
    public void ProgressJudement(float _percentage, int _dropNum)
    {
        //Debug.Log("時間：" + m_gameElapsedTime + "  進行：" + Progress);
       
        //ToDo：if文から抽象関数へと変更必要
        if (Progress == GameProgress.BEGINNING)
        {
            //経過時間が最大時間の3分の1を超えた場合
            if (m_gameElapsedTime >= m_gameMaxTime / 3)
            {
                Progress = GameProgress.MIDDLE;
                return;
            }

            //プレイヤーの脱落人数を確認
            if (_dropNum >= 1) 
            {
                Progress = GameProgress.MIDDLE;
                return;
            }

            //プレイヤーの最大体力の3分の1が減少orプレイヤーが１人以上脱落した場合
            if (_percentage <= 0.7f)
            {
                Progress = GameProgress.MIDDLE;
                return;
            }

        }
        else if (Progress == GameProgress.MIDDLE)
        {
            //経過時間が最大時間の3分の2を超えた場合
            if (m_gameElapsedTime >= m_gameMaxTime / 3 * 2)
            {
                Progress = GameProgress.FINAL;
            }

            //プレイヤーの脱落人数を確認
            if (_dropNum >= 2)
            {

                Progress = GameProgress.FINAL;
                return;
            }

            //プレイヤーの最大体力の2分の1が減少orプレイヤーが１人以上脱落した場合
            if (_percentage <= 0.5f)
            {
                Progress = GameProgress.FINAL;
                return;
            }
        }
        else if (Progress == GameProgress.FINAL)
        {
            return;
        }

        //時間経過
        m_gameElapsedTime += Time.deltaTime;

    }


}
