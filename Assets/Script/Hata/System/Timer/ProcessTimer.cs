//  ProcessTimer.cs
//  http://kan-kikuchi.hatenablog.com/entry/ProcessTimer
//
//  Created by kan.kikuchi on 2016.09.13.

using System;
using System.Diagnostics;

/// <summary>
/// 処理時間を計測するクラス
/// </summary>
public class ProcessTimer
{

    //時間を計るためのクラス
    private  Stopwatch _stopwatch = new Stopwatch();

    //現在の処理時間の合計をfloatで各単位に変換したもの(上からミリ秒、秒、分、時間、日)
    public float TotalMilliseconds { get { return (float)_stopwatch.Elapsed.TotalMilliseconds; } }
    public float TotalSeconds { get { return (float)_stopwatch.Elapsed.TotalSeconds; } }
    public float TotalMinutes { get { return (float)_stopwatch.Elapsed.TotalMinutes; } }
    public float TotalHours { get { return (float)_stopwatch.Elapsed.TotalHours; } }
    public float TotalDays { get { return (float)_stopwatch.Elapsed.TotalDays; } }

    //現在の処理時間の各単位だけをintで抜き出したもの(上からミリ秒、秒、分、時間、日)
    public int Milliseconds { get { return _stopwatch.Elapsed.Milliseconds; } }
    public int Seconds { get { return _stopwatch.Elapsed.Seconds; } }
    public int Minutes { get { return _stopwatch.Elapsed.Minutes; } }
    public int Hours { get { return _stopwatch.Elapsed.Hours; } }
    public int Days { get { return _stopwatch.Elapsed.Days; } }

    //計測中かどうかのフラグ
    public bool IsRunning { get { return _stopwatch.IsRunning; } }

    //=================================================================================
    //基本メソッド
    //=================================================================================

    /// <summary>
    /// 計測を開始する
    /// </summary>
    public void Start()
    {
        _stopwatch.Start();
    }

    /// <summary>
    /// 計測を停止し、現在の計測時間(秒)を返す
    /// </summary>
    public float Stop()
    {
        _stopwatch.Stop();
        return TotalSeconds;
    }

    /// <summary>
    /// 計測した時間をリセットする
    /// </summary>
    public void Reset()
    {
        _stopwatch.Reset();
    }

    /// <summary>
    /// 計測した時間をリセットしてから計測を開始する
    /// </summary>
    public void Restart()
    {
        Reset();
        Start();
    }

    //=================================================================================
    //メソッドの処理時間を測る
    //=================================================================================

    /// <summary>
    /// 入力したアクションを実行し、処理時間(秒)を返す。
    /// loopcountで実行回数指定し、isAverageを有効にすると平均を返す
    /// </summary>
    public float MeasureAction(Action action, int loopCount = 1, bool isAverage = false)
    {
        //アクションがない場合や、処理回数が0の時はエラーが出ないように0を返す
        if (action == null || loopCount == 0)
        {
            return 0;
        }

        //処理時間の合計
        float totalSeconds = 0;

        //指定回数処理を実行し、処理時間を加算
        for (int i = 0; i < loopCount; i++)
        {
            Restart();
            action();
            Stop();
            totalSeconds += TotalSeconds;
        }

        //平均を返す
        if (isAverage)
        {
            return totalSeconds / loopCount;
        }
        //そのまま返す
        return totalSeconds;
    }

}