using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInPlayerNumber : Singleton<GameInPlayerNumber>
{
    //プレイヤーの最大人数
    const int m_maxPlayerNumber = 4;
    //プレイヤーの最小人数
    const int m_minPlayerNumber = 2;

    //現ゲームのプレイ人数
    public int CurrentPlayerNum { get; private set; }

    [Header("デバックに使用")]
    [SerializeField]
    int testNum = 4;

    private new void Awake()
    {
        //最小人数を設定
        CurrentPlayerNum = m_minPlayerNumber;

#if UNITY_EDITOR
        CurrentPlayerNum = testNum;
#endif
    }

    //人数をセットする
    public void SetPlayerNum(int _playerNum)
    {
        //人数を制限する
        if (_playerNum < m_minPlayerNumber)
        {
            _playerNum = m_minPlayerNumber;
            Debug.Log("GameInPlayerNumber：人数最小エラー");
        }
        else if (_playerNum > m_maxPlayerNumber)
        {
            _playerNum = m_maxPlayerNumber;
            Debug.Log("GameInPlayerNumber：人数最大エラー");
        }

        CurrentPlayerNum = _playerNum;
    }
}
