using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMyInput : MonoBehaviour
{
    [Header("プレイヤー識別")]
    [SerializeField] private int playerIndex = 0;


    [SerializeField]
    MyPlayerMoveAction moveAction;



    public int GetPlayerIndex()
    {
        return playerIndex;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = MyRapperInput.Instance.Move(0);
       

        if (MyRapperInput.Instance.HoldDownButton(playerIndex))
        {
            moveAction.MoveMode(0,input);
            Debug.Log("kita"+playerIndex);

        }
        if (MyRapperInput.Instance.HoldLeftButton(playerIndex))
        {
            moveAction.MoveMode(1, input);
        }
        if (MyRapperInput.Instance.HoldUpButton(playerIndex))
        {
            moveAction.MoveMode(2, input);
        }
        if (MyRapperInput.Instance.HoldRightButton(playerIndex))
        {
            moveAction.MoveMode(3, input);
        }
    }
}
