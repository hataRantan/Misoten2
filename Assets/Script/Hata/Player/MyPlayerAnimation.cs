using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerAnimation : MyUpdater
{
    private Animator m_animator = null;
    //プレイヤーのアニメーション一覧
    const string m_dependenceStop = "dependencestop";
    const string m_dependencing = "dependenceing";
    const string m_startStop = "StartStop";
    const string m_finish = "finish";

    int m_stopId, m_ingId, m_startStopId,m_finishId;

    const string m_stopAnime = "stoping";
    const string m_startAnime = "dependence start";
    const string m_ingAnime = "dependenceing";
    const string m_finishAnime = "dependence finish";

    public void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_stopId = Animator.StringToHash(m_dependenceStop);
        m_ingId = Animator.StringToHash(m_dependencing);
        m_startStopId = Animator.StringToHash(m_startStop);
        m_finishId = Animator.StringToHash(m_finish);
        //stopingから遷移しない
        SetFlg(false, false, false, false);
    }

    /// <summary>
    /// 通常アニメーション
    /// </summary>
    public void WalkAnimatioin()
    {
        switch (GetCurrentAnime())
        {
            case m_stopAnime:
                {
                    SetFlg(false, false, false, false);
                }
                break;
            case m_startAnime:
                {
                    SetFlg(false, false, true, false);
                }
                break;
            case m_ingAnime:
                {
                    SetFlg(false, true, false, false);
                }
                break;
            case m_finishAnime:
                {
                    SetFlg(false, false, false, true);
                }
                break;
        }

        //string test = GetCurrentAnime();
        //m_animator.SetBool(m_stopId, false);
    }

    /// <summary>
    /// 憑依開始のアニメーション
    /// </summary>
    public void StartDependence()
    {
        switch (GetCurrentAnime())
        {
            case m_stopAnime:
                {
                    SetFlg(true, false, false, false);
                }
                break;
            case m_startAnime:
                {
                    SetFlg(true, false, false, false);
                }
                break;
            case m_ingAnime:
                {
                    SetFlg(true, false, false, false);
                }
                break;
            case m_finishAnime:
                {
                    SetFlg(false, false, false, true);
                }
                break;
        }

        //m_animator.SetBool(m_ingId, true);
        //m_animator.SetBool(m_stopId, false);
    }

    /// <summary>
    /// 憑依終了のアニメーション
    /// </summary>
    public void EndDependence()
    {
        switch (GetCurrentAnime())
        {
            case m_stopAnime:
                {
                    SetFlg(false, false, false, false);
                }
                break;
            case m_startAnime:
                {
                    SetFlg(false, false, true, false);
                }
                break;
            case m_ingAnime:
                {
                    SetFlg(false, false, false, true);
                }
                break;
            case m_finishAnime:
                {
                    SetFlg(false, false, false, true);
                }
                break;
        }

        //m_animator.SetBool(m_ingId, false);
        //m_animator.SetBool(m_stopId,true);
    }

    /// <summary>
    /// 憑依キャンセルのアニメーション
    /// </summary>
    public void StopDependence()
    {
        switch (GetCurrentAnime())
        {
            case m_stopAnime:
                {
                    SetFlg(false, false, false, false);
                }
                break;
            case m_startAnime:
                {
                    SetFlg(false, false, true, false);
                }
                break;
            case m_ingAnime:
                {
                    SetFlg(false, true, false, false);
                }
                break;
            case m_finishAnime:
                {
                    SetFlg(false, false, false, true);
                }
                break;
        }

        //m_animator.SetBool(m_stopId, true);
        //m_animator.SetBool(m_ingId, false);
    }

    private void SetFlg(bool _ing,bool _stop,bool _startStoop,bool _finish)
    {
        m_animator.SetBool(m_ingId, _ing);
        m_animator.SetBool(m_stopId, _stop);
        m_animator.SetBool(m_startStopId, _startStoop);
        m_animator.SetBool(m_finishId, _finish);
    }

    /// <summary>
    /// 現在のアニメーションを渡す
    /// </summary>
    public string GetCurrentAnime()
    {
        return m_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
    }

    /// <summary>
    /// 憑依アニメーションの終了（EndDependence()を呼んだ後に使用すること）
    /// </summary>
    public bool IsEndDependence()
    {
        if (GetCurrentAnime() != m_finishAnime) return true;
        else return false;
    }

    /// <summary>
    /// 使用しない
    /// </summary>
    public override void MyUpdate() { }
}
