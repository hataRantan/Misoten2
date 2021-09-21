using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;


//指定のイベントを保持
public abstract class StateBase<EVENT> 
    where EVENT : Enum
{
    //コンストラクタ,メンバ変数初期化
    public StateBase() 
    {
        m_nextState = new string(NonState.nonState.ToCharArray());
        m_actionDic = new Dictionary<EVENT, ActionBase>();
        m_transitionDic = new Dictionary<EVENT, TransitionBase>();
    }

    //デストラクタ
    ~StateBase() { }

    //状態開始時の処理
    public abstract void Entry(GameObject _obj);
    //状態遷移時の処理
    public abstract void Exit(GameObject _obj);

    //状態更新処理
    public bool Update(GameObject _obj, EVENT _event)
    {
        //指定したイベントの処理があるか確認
        bool haveAction = m_actionDic.ContainsKey(_event);
        bool haveTransition = m_transitionDic.ContainsKey(_event);

        //アクション、遷移どちらもなければ失敗
        if (!haveAction && !haveTransition)
            return false;

        //アクション処理
        if (haveAction)
            m_actionDic[_event].Action(_obj);

        //遷移処理
        if (haveTransition)
            m_nextState = m_transitionDic[_event].Transition(_obj);

        return true;
    }

    //指定したイベントでアクション処理を生成
    public void CreateAction<ACTION>(EVENT _event)
        where ACTION: ActionBase, new ()
    {
        //指定したイベントがなければ作成
        if(!m_actionDic.ContainsKey(_event))
        {
            ACTION action = new ACTION();
            m_actionDic.Add(_event, action);
        }
    }

    //指定したイベントで遷移処理を生成
    public void CreateTransition<TRANSITION>(EVENT _event)
        where TRANSITION :TransitionBase ,new()
    {
        //指定したイベントがなければ作成
        if(!m_transitionDic.ContainsKey(_event))
        {
            TRANSITION transition = new TRANSITION();
            m_transitionDic.Add(_event, transition);
        }
    }

    //次の状態
    protected string m_nextState;
    //次の状態を取得
    public string GetNextState
    {
        get { return m_nextState; }
    }

    //イベントとそれに対応するアクション処理
    protected Dictionary<EVENT, ActionBase> m_actionDic;
    //イベントとそれに対応する遷移処理
    protected Dictionary<EVENT, TransitionBase> m_transitionDic;
}

//遷移不必要の場合
public class NonState
{
    public static readonly string nonState = "nonState";
}