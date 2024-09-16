using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_ItemStateMachine : MonoBehaviour
{
    //出現演出クラス
    private Proto_ItemAppear appear = null;

    [Header("プレイヤーの取得範囲")]
    [SerializeField] BoxCollider hitBox = null;

    [Header("アイテムの取得待機時間")]
    [Range(1.0f,5.0f)]
    [SerializeField] float standByTime = 1.0f;

    //取得をしようとしているプレイヤー
    public bool[] targetPlayer { get; set; }
    //取得可能かどうかのフラグ
    public bool isGain { get; private set; }

    //１人目が取得開始してからの時間
    private float processTimer = 0.0f;

    //誰かが取得済みのアイテムかどうか
    public bool isAcquired { get; private set; }

    private enum Proto_ItemState
    {
        BEFORE_APPEAR, //出現前
        STANDBY, //待機状態
        RECEPTION, //受付中
    }

    //状態マシーン
    private IStateSpace.StateMachineBase<Proto_ItemState, Proto_ItemStateMachine> machine = new IStateSpace.StateMachineBase<Proto_ItemState, Proto_ItemStateMachine>();

    public void MyInit()
    {
        targetPlayer = new bool[2];

        appear = gameObject.GetComponent<Proto_ItemAppear>();

        machine.AddState(Proto_ItemState.BEFORE_APPEAR, new Proto_BeforeAppearState(),this);
        machine.AddState(Proto_ItemState.STANDBY, new Proto_StandByState(),this);
        //初期状態をセット
        machine.InitState(Proto_ItemState.BEFORE_APPEAR);

        isAcquired = false;

        //Rigidbodyを停止する
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void MyUpdate()
    {
        if (isAcquired) return;

        machine.UpdateState();
    }

    /// <summary>
    /// 出現前の状態
    /// </summary>
    private class Proto_BeforeAppearState : IStateSpace.IState<Proto_ItemState, Proto_ItemStateMachine>
    {
        public override void Entry()
        {
            //衝突判定を一時停止
            board.hitBox.enabled = false;

            board.targetPlayer[0] = false;
            board.targetPlayer[1] = false;

            board.isGain = false;
        }

        public override void Exit()
        {
            //再開
            board.hitBox.enabled = true;

            //不必要なコンポーネントを削除
            Destroy(board.gameObject.GetComponent<Proto_ItemAppear>());
            board.appear = null;
        }

        public override Proto_ItemState Update()
        {
            //出現演出終了で遷移
            if (board.appear.isAppear) return Proto_ItemState.STANDBY;

            return Proto_ItemState.BEFORE_APPEAR;
        }
    }

    /// <summary>
    /// 受付開始前の状態
    /// </summary>
    private class Proto_StandByState : IStateSpace.IState<Proto_ItemState, Proto_ItemStateMachine>
    {
        public override void Entry()
        {
            board.targetPlayer[0] = false;
            board.targetPlayer[1] = false;
            board.isGain = true;
        }

        public override void Exit() { }

        public override Proto_ItemState Update()
        {
            //取得しようとしているか確認
            bool getFlg = false;
            foreach (var flg in board.targetPlayer)
            {
                getFlg = flg;
            }

            if (getFlg)
            {
                if (board.processTimer < board.standByTime) board.processTimer += Time.deltaTime;
                else board.isGain = false;
            }
            else
            {
                board.processTimer = 0.0f;
                board.isGain = true;
            }

            return Proto_ItemState.STANDBY;
        }
    }

    
    /// <summary>
    /// アイテム情報をプレイヤーにセットする
    /// </summary>
    public void GetItem(GameObject _player)
    {
        //取得されたフラグ
        isAcquired = true;
        //プレイヤーの判定を一時停止
        _player.GetComponent<Proto_PlayerControl>().SetHitBox(false);
        //顔を渡す
        int playerNum = _player.GetComponent<protoInput>().isController ? 0 : 1;
        gameObject.GetComponent<Proto_PlayerFace>().SetFace(SavingFace.Instance.GetFace(playerNum));
        //アイテムを渡す
        _player.GetComponent<Proto_PlayerControl>().ChangeItem(gameObject.GetComponent<Proto_ItemInterface>());
        //物理運動開始
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
