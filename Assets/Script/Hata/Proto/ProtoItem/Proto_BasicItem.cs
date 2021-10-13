using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// プレイヤーの通常状態
/// </summary>
public class Proto_BasicItem : Proto_ItemInterface
{
    [Header("アイテム取得の限界時間")]
    [SerializeField] private float limitGetTime = 3.0f;
    //連打数
    uint blowsNum = 0;
    //取得に必要な連打数
    uint getBlowsNum = 0;
    //経過時間
    float progressTimer = 0.0f;
    //アクション終了フラグ
    private bool isEndAction = false;
    //連打数の表示
    private Proto_BlowUI ui = null;
    //PL1,PL2のどちらか
    int plNum = 0;

    private enum BasicAction
    {
        INIT,
        BLOW,
        NONGET,
        GET
    }

    //状態マシーン
    IStateSpace.StateMachineBase<BasicAction, Proto_BasicItem> machine = new IStateSpace.StateMachineBase<BasicAction, Proto_BasicItem>();


    public override bool Action()
    {
        //アクション状態を更新
        machine.UpdateState();

        return isEndAction;
    }

    public override void Init()
    {
        blowsNum = 0;
        progressTimer = 0.0f;

        machine.AddState(BasicAction.INIT, new Init_ActionState(), this);
        machine.AddState(BasicAction.BLOW, new Blow_AcitonState(), this);
        machine.AddState(BasicAction.NONGET, new NonGet_ActionState(), this);
        machine.AddState(BasicAction.GET, new Get_ACtionState(), this);

        machine.InitState(BasicAction.INIT);

    }

    public override void ActionInit()
    {
        machine.InitState(BasicAction.INIT);
    }

    public override void Move(Vector2 _input)
    {
        //入力値を正規化
        Vector3 normalInput = new Vector3(_input.x, 0.0f, _input.y).normalized;

        //速度
        Vector3 velocity = Vector3.zero;

        velocity.x = -normalInput.x * GetSpped();
        velocity.z = -normalInput.z * GetSpped();

        //速度を追加
        playerRigid.velocity = velocity;
    }

    /// <summary>
    /// アイテム取得に必要な変数を初期化
    /// </summary>
    private class Init_ActionState : IStateSpace.IState<BasicAction, Proto_BasicItem>
    {
        public override void Entry()
        { 
            board.blowsNum = 0;
            board.progressTimer = 0.0f;
            board.isEndAction = false;

            //UIを取得
            board.ui = GameObject.Find("Canvas").GetComponent<Proto_BlowUI>();
            board.ui.SetFill(board.plNum, 0.0f);

            //プレイヤー番号取得
            board.plNum = (board.playerInput.isController) ? 1 : 0;
        }

        public override void Exit() { }

        public override BasicAction Update()
        {
            if (board.playerControl.hitItem != null)
            {
                //取得可能か確認
                if(board.playerControl.hitItem.GetComponentInParent<Proto_ItemStateMachine>().isGain)
                {
                    //アイテムに通知
                    board.playerControl.hitItem.GetComponentInParent<Proto_ItemStateMachine>().targetPlayer[board.plNum] = true;

                    //取得に必要な連打数取得
                    board.getBlowsNum = board.playerControl.hitItem.GetComponentInParent<Proto_ItemInterface>().GetData.GetBlowsNum;
                    return BasicAction.BLOW;
                }
                else
                {
                    return BasicAction.NONGET;
                }

            }

            return BasicAction.INIT;
        }
    }

    /// <summary>
    /// 連打状態
    /// </summary>
    private class Blow_AcitonState : IStateSpace.IState<BasicAction, Proto_BasicItem>
    {
        public override void Entry()
        {
            
           

        }

        public override void Exit()
        {
        }

        public override BasicAction Update()
        {
            if(board.playerControl.hitItem==null)
            {
                return BasicAction.NONGET;
            }

            if(board.playerControl.hitItem.GetComponentInParent<Proto_ItemStateMachine>().isAcquired)
            {
                Debug.Log("他プレイヤーに取得された");
                return BasicAction.NONGET;
            }

            //時間経過
            if (board.progressTimer < board.limitGetTime) board.progressTimer += Time.deltaTime;
            else 
            {
                Debug.Log("取得失敗");
                return BasicAction.NONGET;
            }

            //連打
            if (board.blowsNum < board.getBlowsNum)
            {
                if (board.playerInput.Get_AButtonDown())
                {
                    board.blowsNum++;

                    board.ui.SetFill(board.plNum, (float)board.blowsNum / board.getBlowsNum);
                }
            }
            else
            {
                Debug.Log("取得");
                //プレイヤーにアイテムをセット
                board.playerControl.hitItem.GetComponentInParent<Proto_ItemStateMachine>().GetItem(board.playerControl.gameObject);

                return BasicAction.GET;
            }

            return BasicAction.BLOW;
        }
    }

    /// <summary>
    /// 取得失敗、通常状態に移行
    /// </summary>
    private class NonGet_ActionState : IStateSpace.IState<BasicAction, Proto_BasicItem>
    {
        public override void Entry()
        {
            Debug.Log("変更");
            //アイテムを通常に変更
            board.playerControl.SetFirstItem();
            //アクション終了
            board.isEndAction = true;

            //アイテムに通知
            board.playerControl.hitItem.GetComponentInParent<Proto_ItemStateMachine>().targetPlayer[board.plNum] = false;
        }

        public override void Exit() {  }

        public override BasicAction Update()
        {
            return BasicAction.INIT;
        }
    }

    private class Get_ACtionState : IStateSpace.IState<BasicAction, Proto_BasicItem>
    {
        public override void Entry()
        {
            //アクション終了
            board.isEndAction = true;
        }

        public override void Exit()
        {
        }

        public override BasicAction Update()
        {
            return BasicAction.INIT;
        }
    }
}