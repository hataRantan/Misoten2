using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMyPlayerDead : MonoBehaviour
{
    [Range(1,4)]
    [SerializeField]
    int deadNum = 1;

    int startNum = 0;
    
    MyPlayerObject[] players;

    public void SetPlayers(MyPlayerObject[] _array)
    {
        players = new MyPlayerObject[_array.Length];
        players = _array;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            for (int idx = startNum; idx < deadNum; idx++)
            {
                players[idx].PlayerInfo.HpReduction(100);

                startNum += deadNum;
            }
        }


        if(Input.GetKeyDown(KeyCode.K))
        {
            for (int idx = startNum; idx < deadNum; idx++)
            {
                players[idx].PlayerInfo.HpReduction(2);

                //startNum += deadNum;
            }
        }
    }
}
