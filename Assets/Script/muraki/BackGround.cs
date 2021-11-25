using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTitleStateClass
{
    public class tAnimationState : IStateSpace.IState<MyTitleObject.MyTitleObjectState, MyTitleObject>
    {
        //正規化した移動方向
        Vector3 m_moveDirect = Vector3.zero;

        public override void Entry()
        {
            board.CallTest();
        }
        public override void Exit() { }
        

        public override MyTitleObject.MyTitleObjectState Update()
        {
            return MyTitleObject.MyTitleObjectState.tMOVE;
        }
    }




    public class tDeadState : IStateSpace.IState<MyTitleObject.MyTitleObjectState, MyTitleObject>
    {
        public override void Entry()
        {
            //ToDo：dead処理の呼び出し
        }

        public override void Exit()
        {
        }

        public override MyTitleObject.MyTitleObjectState Update()
        {
            return MyTitleObject.MyTitleObjectState.tDEAD;
        }
    }

}
