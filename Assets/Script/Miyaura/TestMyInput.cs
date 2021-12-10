using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMyInput : MyUpdater
{
    [Header("プレイヤー識別")]
    [SerializeField] private int playerIndex = 0;

    [SerializeField]
    MyPlayerMoveAction moveAction;

    public int GetPlayerIndex()
    {
        return playerIndex;
    }
   
    public override void MyUpdate()
    {
       // string d_Name = MyRapperInput.Instance.SetDevice();
        if (MyRapperInput.Instance.AnyKey(playerIndex))
        {
            //Debug.Log(d_Name);
        }
        if (MyRapperInput.Instance.Submit(playerIndex))
        {
            Debug.Log("Submit");
        }
        if (MyRapperInput.Instance.GetItem(playerIndex))
        {
            Debug.Log("GetItem");
        }
        if (MyRapperInput.Instance.ActionItem(playerIndex))
        {
            Debug.Log("ActionItem");
        }
        if (MyRapperInput.Instance.PressNorthButton(playerIndex))
        {
            Debug.Log("PressNorthButton");
        }
        if (MyRapperInput.Instance.PressSouthButton(playerIndex))
        {
            Debug.Log("PressSouthButton");
        }
        if (MyRapperInput.Instance.PressWestButton(playerIndex))
        {
            Debug.Log("PressWestButton");
        }
        if (MyRapperInput.Instance.PressEastButton(playerIndex))
        {
            Debug.Log("PressEastButton");
        }
        if (MyRapperInput.Instance.LeftTrigger(playerIndex))
        {
            Debug.Log("LeftTrigger");
        }
        if (MyRapperInput.Instance.LeftShoulder(playerIndex))
        {
            Debug.Log("LeftShoulder");
        }
        if (MyRapperInput.Instance.RightTrigger(playerIndex))
        {
            Debug.Log("RightTrigger");
        }
        if (MyRapperInput.Instance.RightShoulder(playerIndex))
        {
            Debug.Log("RightShoulder");
        }
    }


}
