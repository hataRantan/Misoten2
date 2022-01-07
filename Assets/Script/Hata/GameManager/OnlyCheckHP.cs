using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyCheckHP : MyUpdater
{
    [SerializeField] MyPlayerManager players = null;
    public bool isEndGame { get; private set; }

    public override void MyFastestInit()
    {
        isEndGame = false;
        m_isUpdate = false;
    }


    public override void MyUpdate()
    {

    }
    public override void MyLateUpdate()
    {
        if(players.GetDropPlayerNum() >= GameInPlayerNumber.Instance.CurrentPlayerNum - 1 &&
                !players.isProcessEnd)
        {
            isEndGame = true;
        }
    }
}
