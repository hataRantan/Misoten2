using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyPlayerManager : MyUpdater
{
    [Header("生成したいプレイヤーオブジェクト")]
    [SerializeField] GameObject createPlayer = null;

    [Header("プレイヤーの人数")]
    [Range(1, 4)]
    [SerializeField] private int m_maxPlayerNum = 1;

    [Header("プレイヤー名")]
    [SerializeField] string[] m_playerName = new string[4];

    [Header("プレイヤーのカラー")]
    [SerializeField] private Color[] m_playerColor = new Color[4];

    [Header("プレイヤーの情報を表示するCanvas")]
    [SerializeField] Canvas m_playerCanvas = null;

    [Header("プレイヤー情報を表示するUI")]
    [SerializeField] GameObject createUI = null;
    
    //生成したプレイヤー一覧
    private MyPlayerObject[] m_players;

    [Header("UIの初期位置")]
    [SerializeField] Vector2 uiFirstPos = new Vector2();

    //プレイヤーの最大Hp
    private int m_playerMaxHp = 0;
    //プレイヤーの最大Hpの合計
    private int m_playerTotalMaxHp = 0;

    public override void MySecondInit()
    {
        //ToDo：変更予定
        float uiWidth = 400.0f;

        //プレイヤーの生成
        m_players = new MyPlayerObject[m_maxPlayerNum];

        for (int idx = 0; idx < m_maxPlayerNum; idx++)
        {
            //UIの生成
            GameObject uiObj = Instantiate(createUI, Vector3.zero, Quaternion.identity);
            //UI位置変更
            Vector2 uiPos = new Vector2(uiFirstPos.x + uiWidth * idx, uiFirstPos.y);
            MyPlayerUI ui = uiObj.GetComponent<MyPlayerUI>();

            //プレイヤーの生成
            //ToDo：出現位置を変更させる
            GameObject playerObj = Instantiate(createPlayer, new Vector3(0.0f, 5.0f, 0.0f), Quaternion.identity);
            playerObj.name = m_playerName[idx];
            MyPlayerObject player = playerObj.GetComponent<MyPlayerObject>();


            player.SetUI(ui);
            //プレイヤーの初期化
            player.CreatedInit(idx);
            
            //UIのセットアップ
            ui.SetUp(m_playerCanvas, uiPos, m_playerName[idx], player.GetHP(), m_playerColor[idx]);
            
            //プレイヤーを保存
            m_players[idx] = player;
        }

        //最大Hpを保存する
        m_playerMaxHp = m_players[0].PlayerData.GetHp;
        //最大Hpの合計を保存する
        m_playerTotalMaxHp = m_playerMaxHp * m_maxPlayerNum;
    }


    public override void MyFixedUpdate()
    {
        //プレイヤーの物理更新
        foreach (var player in m_players)
        {
            if (player.GetHP() > 0 && player.IsUpdate) player.MyFixedUpdate();
        }
    }

    public override void MyUpdate()
    {
        //プレイヤーの更新
        foreach (var player in m_players)
        {
            if (player.GetHP() > 0 && player.IsUpdate) player.MyUpdate();
        }

    }

    public override void MyLateUpdate()
    {
        //プレイヤーの後更新
        foreach (var player in m_players)
        {
            if (player.GetHP() > 0 && player.IsUpdate) player.MyLateUpdate();
        }

    }


    /// <summary>
    /// 脱落したプレイヤーの人数を取得する
    /// </summary>
    public int GetDropPlayerNum()
    {
        int dropNum = 0;

        for (int playerIdx = 0; playerIdx < m_players.Length; playerIdx++)
        {
            if (m_players[playerIdx].GetHP() <= 0) dropNum++;
        }

        return dropNum;
    }

    /// <summary>
    /// プレイヤーの現在のHpに対する最大Hpの合計の割合
    /// </summary>
    public float GetPlayerTotalHpPercentage()
    {
        int currentTotalHp = 0;

        for (int playerIdx = 0; playerIdx < m_players.Length; playerIdx++)
        {
            currentTotalHp += m_players[playerIdx].GetHP();
        }

        return (float)currentTotalHp / m_playerTotalMaxHp;
    }
}
