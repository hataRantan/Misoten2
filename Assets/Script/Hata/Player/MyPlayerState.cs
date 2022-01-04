using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyPlayerStateClass
{
    public static class InputFlg
    {
        public static bool RapperOn = true;
    }

    //移動状態
    public class MoveState : IStateSpace.IState<MyPlayerObject.MyPlayerState, MyPlayerObject>
    {
        //正規化した移動方向
        Vector3 m_moveDirect = Vector3.zero;

        public override void Entry() { }
        public override void Exit() { }
        
        public override void FiexdUpdate()
        {
            //if (board.PlayerNumber != 0) return;

            //アイテムの物理運動を実行
            board.CurrentItem.FiexdMove();
        }

        public override MyPlayerObject.MyPlayerState Update()
        {
            //if (board.PlayerNumber != 0) return MyPlayerObject.MyPlayerState.MOVE;

            //アイテムの移動処理を実行する
            //board.CurrentItem.Move(MyRapperInput.Instance.Move(board.PlayerNumber));


            if (InputFlg.RapperOn)
            {
                board.CurrentItem.Move(-MyRapperInput.Instance.Move(board.PlayerNumber));

                //if (board.PlayerNumber == 1)
                //    Debug.Log("入力値：" + -MyRapperInput.Instance.Move(board.PlayerNumber));
            }
            else
            {
                Vector2 move = Vector2.zero;

                if (Input.GetKey(KeyCode.A)) move.x = 1.0f;
                else if (Input.GetKey(KeyCode.D)) move.x = -1.0f;

                if (Input.GetKey(KeyCode.W)) move.y = -1.0f;
                else if (Input.GetKey(KeyCode.S)) move.y = 1.0f;

                board.CurrentItem.Move(move);
            }

            //通常状態とそれ以外で、操作方法を変更
            if (board.CurrentItem == board.NormalImte)
            {
                if (InputFlg.RapperOn)
                {
                    if (MyRapperInput.Instance.GetItem(board.PlayerNumber))
                    {
                        return MyPlayerObject.MyPlayerState.ACTION;
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        return MyPlayerObject.MyPlayerState.ACTION;
                    }
                }
            }
            else
            {
                if (InputFlg.RapperOn)
                {
                    if (MyRapperInput.Instance.ActionItem(board.PlayerNumber))
                    {
                        return MyPlayerObject.MyPlayerState.ACTION;
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        return MyPlayerObject.MyPlayerState.ACTION;
                    }
                }
            } 
            //移動状態を維持する
            return MyPlayerObject.MyPlayerState.MOVE;
        }
    }

    //アクション状態
    public class ActionState : IStateSpace.IState<MyPlayerObject.MyPlayerState, MyPlayerObject>
    {
        public override void Entry()
        {
            board.CurrentItem.ActionInit();
        }

        public override void Exit()
        {
            board.CurrentItem.ActionExit();
        }

        public override void FiexdUpdate()
        {
            board.CurrentItem.FiexdAction();
        }

        public override MyPlayerObject.MyPlayerState Update()
        {
            //アクションが終了すれば、移動状態に変更
            if(board.CurrentItem.isEndAntion)
                return MyPlayerObject.MyPlayerState.MOVE;

            //アクション実行
            //Debug.Log("Move：" + MyRapperInput.Instance.Move(board.PlayerNumber));
            if (InputFlg.RapperOn)
            {
                board.CurrentItem.Action(-MyRapperInput.Instance.Move(board.PlayerNumber));
            }
            else
            {
                Vector2 move = Vector2.zero;

                if (Input.GetKey(KeyCode.A)) move.x = 1.0f;
                else if (Input.GetKey(KeyCode.D)) move.x = -1.0f;

                if (Input.GetKey(KeyCode.W)) move.y = -1.0f;
                else if (Input.GetKey(KeyCode.S)) move.y = 1.0f;

                board.CurrentItem.Action(move);
            }

            //状態維持
            return MyPlayerObject.MyPlayerState.ACTION;
        }
    }

    //ダメージ状態
    public class DamageState : IStateSpace.IState<MyPlayerObject.MyPlayerState, MyPlayerObject>
    {
        public override void Entry()
        {
            //ToDo：ダメージ処理の呼び出し
        }

        public override void Exit()
        {

        }

        public override MyPlayerObject.MyPlayerState Update()
        {
            //ダメージ処理終了
            return MyPlayerObject.MyPlayerState.MOVE;

            //return MyPlayerObject.MyPlayerState.DAMAGE;
        }
    }

    //ゲームオーバー演出状態
    public class DeadState : IStateSpace.IState<MyPlayerObject.MyPlayerState, MyPlayerObject>
    {
        public override void Entry()
        {
            //プレイヤーの操作を不可能にする
            board.NonItem();
        }

        public override void Exit()
        {
           
        }

        public override MyPlayerObject.MyPlayerState Update()
        {
            //カメラ描画範囲外ならEnd状態へ移行
            if(!board.PlayerInfo.Render.isVisible)
            {
                return MyPlayerObject.MyPlayerState.END;
            }

            return MyPlayerObject.MyPlayerState.DEAD;
        }
    }

    public class EndState : IStateSpace.IState<MyPlayerObject.MyPlayerState, MyPlayerObject>
    {
        public override void Entry()
        {
            //プレイヤーの生存終了
            board.Abort();
            //ToDo：プレイヤーがゲームオーバー後も何かするなら、ここを変更する
        }

        public override void Exit()
        {
        }

        public override MyPlayerObject.MyPlayerState Update()
        {
            return MyPlayerObject.MyPlayerState.END;
        }
    }
}


