using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CertificationManager : MyUpdater
{
    private enum State
    {
        ENTRY,
        END
    }

    IStateSpace.StateMachineBase<State, CertificationManager> m_machine = new IStateSpace.StateMachineBase<State, CertificationManager>();

    [Header("プレイヤー受付クラス")]
    [SerializeField] CertificationPlayer[] m_players;

    [Header("次のシーン")]
    [SerializeField]
    SceneObject m_nextScene = null;

    public override void MyFastestInit()
    {
        //受付クラスを生成
        for(int idx=0;idx<m_players.Length;idx++)
        {
            m_players[idx].Init(idx);
        }

        m_machine.AddState(State.ENTRY, new EntryState(), this);
        m_machine.AddState(State.END, new EndState(), this);
        m_machine.InitState(State.ENTRY);
    }

    public override void MyUpdate()
    {
        m_machine.UpdateState();
    }


    /// <summary>
    /// プレイヤー受付状態
    /// </summary>
    private class EntryState : IStateSpace.IState<State, CertificationManager>
    {
        int connectNum = 0;
        public override void Entry()
        {
            //BGMを変更
            MyAudioManeger.Instance.PlayBGM("Robby_BGM");
        }

        public override void Exit()
        {
            //プレイヤー人数を決定
            GameInPlayerNumber.Instance.SetPlayerNum(connectNum);

            //次のシーンへ移行する
            SceneLoader.Instance.CallLoadSceneDefault(board.m_nextScene);
        }

        public override State Update()
        {
            connectNum = 0;
            //受付クラスの更新
            for (int idx = 0; idx < board.m_players.Length; idx++)
            {
                board.m_players[idx].MyUpdate();

                if(board.m_players[idx].IsConnect())
                {
                    connectNum++;
                }
            }

            //コントローラが2人未満なら処理しない
            if (connectNum <= 1) return State.ENTRY;

            //接続完了人数を確認
            if (connectNum >= MyRapperInput.Instance.GetConnectNum())
            {
                return State.END;
            }

            return State.ENTRY;
        }
    }

    private class EndState : IStateSpace.IState<State, CertificationManager>
    {
        public override void Entry()
        {
        }

        public override void Exit()
        {
        }

        public override State Update()
        {
            return State.END;
        }
    }
}
