using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class HFSMBase<EVENT>
    where EVENT : Enum
{
    //メンバ変数初期化
    public HFSMBase()
    {
        m_childrenHFSM = new List<HFSMBase<EVENT>>();
        m_stateDic = new Dictionary<string, StateBase<EVENT>>();
        m_currentState = new string(NonState.nonState.ToCharArray());
    }

    //親HFSMを設定
    public void SetParent(HFSMBase<EVENT> _parent)
    {
        m_parentHFSM = _parent;
    }
    //子HFSMを設定
    public void SetChild(HFSMBase<EVENT> _child)
    {
        m_childrenHFSM.Add(_child);
    }

    //状態の更新処理
    public bool Update(GameObject _obj,EVENT _event)
    {
        //更新したか
        bool doUpdate = false;

        //空でなければ
        if (m_childrenHFSM.Any())
        {
            foreach(var child in m_childrenHFSM)
            {

                doUpdate |= child.Update(_obj,_event);
            }
        }

        //子ステートマシンが実行してなければ
        if(!doUpdate)
        {
            if (!m_stateDic.ContainsKey(m_currentState))
                Debug.LogError("HFSMBase.Update Error");
            else
                return m_stateDic[m_currentState].Update(_obj, _event);
        }

        //状態更新なし
        return false;
    }

    //状態の変更確認
    public void CheckState(GameObject _obj)
    {
        ChangeState(_obj, m_stateDic[m_currentState].GetNextState);

        if(m_childrenHFSM.Any())
        {
            foreach(var child in m_childrenHFSM)
            {
                child.CheckState(_obj);
            }
        }
    }

    //状態作成
    public void CreateState<STATE>()
        where STATE : StateBase<EVENT>,new()
    {
        string stateName = typeof(STATE).Name;
        
        //作成済みでなければ
        if (!m_stateDic.ContainsKey(stateName))
        {
            STATE state = new STATE();
            m_stateDic.Add(stateName, state);
        }
    }

    //最初の状態をセットする
    public void SetFirstState<STATE>(GameObject _obj)
        where STATE :StateBase<EVENT>,new()
    {
        string stateName = typeof(STATE).Name;
        //現在の状態にセット
        m_currentState = stateName;

        //作成済みでなければ
        if (!m_stateDic.ContainsKey(stateName))
        {
            STATE state = new STATE();
            m_stateDic.Add(stateName, state);
        }

        //入場処理
        m_stateDic[m_currentState].Entry(_obj);
    }

    //状態変更
    protected void ChangeState(GameObject _obj,string _state)
    {
        //状態遷移なし
        if (_state == NonState.nonState)
            return;

        //現在の状態の終了処理
        if (m_stateDic.ContainsKey(m_currentState))
            m_stateDic[m_currentState].Exit(_obj);

        //状態変更
        m_currentState = _state;

        //次の状態の開始処理
        if (m_stateDic.ContainsKey(m_currentState))
            m_stateDic[m_currentState].Entry(_obj);
    }

    //親ステートマシン
    protected HFSMBase<EVENT> m_parentHFSM;

    //子ステートマシン
    protected List<HFSMBase<EVENT>> m_childrenHFSM;

    //このステートマシンが取りうる状態
    //protected List<StateBase<EVENT>> m_stateDic;
    protected Dictionary<string, StateBase<EVENT>> m_stateDic;

    //現在の状態名
    protected string m_currentState;
}
