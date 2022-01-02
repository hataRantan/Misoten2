using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MyUpdater
{
    [Header("タイトルロゴの管理クラス")]
    [SerializeField]
    TitleRgoAnimetion m_animation = null;

    [Header("指示管理クラス")]
    [SerializeField] TitleInstructionMove m_instruction = null;

    [Header("次のシーン")]
    [SerializeField]
    SceneObject m_nextScene = null;

    public enum TitleState
    {
        tEvent,
        tWait,
        tEnd //特に処理なし
    }
    //現在の状態
    public TitleState m_tState { get; private set; }

    //プレイヤーの状態マシーン
    private IStateSpace.StateMachineBase<TitleState, TitleManager> m_tmachine;


    public override void MyFastestInit()
    {
        //状態生成
        m_tmachine = new IStateSpace.StateMachineBase<TitleState, TitleManager>();
        //状態の追加
        m_tmachine.AddState(TitleState.tEvent, new tEventState(), this);
        m_tmachine.AddState(TitleState.tWait, new tWaitState(), this);
        m_tmachine.AddState(TitleState.tEnd, new tEndState(), this);
        //初期状態に変更
        m_tmachine.InitState(TitleState.tEvent);

    }

    public override void MyUpdate()
    {
        m_tmachine.UpdateState();
    }

    /// <summary>
    /// アニメーション状態
    /// </summary>
    private class tEventState : IStateSpace.IState<TitleState, TitleManager>
    {
        //タイトルロゴのアニメーション呼び出しフラグ
        bool isCall = false;

        public override void Entry()
        {
           
        }

        public override void Exit() { }

        public override TitleState Update()
        {
            //ToDo：呼び出し変更
            if(!isCall)
            {
                //イベント呼び出し
                board.m_animation.CallEvent();
                isCall = true;
            }

            //イベントが終了すれば状態遷移
            if (board.m_animation.isEvent) return TitleState.tWait;

            return TitleState.tEvent;
        }
    }

    /// <summary>
    /// キー入力待ち状態
    /// </summary>
    private class tWaitState : IStateSpace.IState<TitleState, TitleManager>
    {
        public override void Entry()
        {
            //指示出現
            board.m_instruction.CallAppear();

        }

        public override void Exit()
        {
            
        }

        public override TitleState Update()
        {
            //指示文字移動
            //board.m_instruction.MoveText();

            //ToDo：入力関数変更予定(AnyKey及び、入力デバイスを認識した場合)
            if (MyRapperInput.Instance.AnyKey())
            {
                return TitleState.tEnd;
            }

            return TitleState.tWait;
        }
    }

    /// <summary>
    /// シーン遷移状態
    /// </summary>
    private class tEndState : IStateSpace.IState<TitleState, TitleManager>
    {
        public override void Entry()
        {
            //シーン遷移
            SceneLoader.Instance.CallLoadSceneDefault(board.m_nextScene);
        }

        public override void Exit()
        {
        }

        public override TitleState Update()
        {
            return TitleState.tEnd;
        }
    }

}
