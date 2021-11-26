using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTitleStateClass
{
    public class tEventState : IStateSpace.IState<MyTitleObject.MyTitleObjectState, MyTitleObject>
    {
        public override void Entry()
        {
            //イベント呼び出し
            board.CallEvent();
        }
        public override void Exit() { }
        
        public override MyTitleObject.MyTitleObjectState Update()
        {
            //イベントが終了すれば状態遷移
            if (board.isEvent) return MyTitleObject.MyTitleObjectState.tWait;

            return MyTitleObject.MyTitleObjectState.tEvent;
        }
    }


    public class tWaitState : IStateSpace.IState<MyTitleObject.MyTitleObjectState, MyTitleObject>
    {
        public override void Entry()
        {

        }

        public override void Exit()
        {
        }

        public override MyTitleObject.MyTitleObjectState Update()
        {
            return MyTitleObject.MyTitleObjectState.tWait;
        }
    }

}
