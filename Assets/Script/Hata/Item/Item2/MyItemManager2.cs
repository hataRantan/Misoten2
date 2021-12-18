﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItemManager2 : MyUpdater
{
    //アイテムの出現を管理するクラス

    //ToDo：出現させるアイテム一覧
    //ToDo：出現させる強力なアイテム一覧

    //ToDo：アイテムの出現間隔を管理するクラス
    //ToDo：アイテムの出現方法を決めるクラス
    //ToDo：どのアイテムを出現させるか決めるクラス

    //ToDo：プレイヤー人数に対する通常アイテムの出現個数

    [Header("次のフェーズに移行する秒数(60.0fなら60を超えることで次のフェーズに移行する)")]
    [Range(0.0f, 300.0f)]
    [SerializeField] List<float> m_phaseInterval = new List<float>();
    //現在のフェーズ
    private int m_phaze = 0;

    [Header("フェーズごとのアイテム出現間隔")]
    [Range(0.0f, 20.0f)]
    [SerializeField] List<float> m_itemIntervel;

    //アイテムの作成許可
    private bool m_license = false;
    //最後にアイテムを作成した時間
    private float m_lastCreatedTime = 0.0f;

    //アイテム管理のゲーム進行時間
    private float m_gameTimer = 0.0f;

    [Header("強力なアイテムを出現させる頻度(10なら10回ごとに出現)")]
    [Range(5, 25)]
    [SerializeField]
    int m_powerfulCreateNum = 10;
    //現在のアイテム生成回数
    int m_currentCreateNum = 0;

    //通常アイテムの出現個数
    private int m_createItemNum = 3;

    [Header("アイテム選択クラス")]
    [SerializeField]
    MyItemSelect m_itemSelecter = null;

    [SerializeField]
    ItemAppearUpdate m_itemAppear = null;

    [SerializeField]
    MyItemGeneratePos m_generate = null;

    int testNum = 0;

    public override void MySecondInit()
    {
        //メンバの初期化
        m_phaze = 0;
        m_gameTimer = 0.0f;
        m_license = false;
        m_lastCreatedTime = 0.0f;
        m_currentCreateNum = 0;

        //プレイヤー人数の取得して、アイテムの出現個数を取得する
    }

    public override void MyUpdate()
    {
        //アイテム出現許可を取得
        CreateLicensePublish();
        //アイテム生成
        CreateItem();
    }

    private void CreateItem()
    {
       //作成許可が無ければ、実行しない
        if (!m_license) return;

        Debug.Log("アイテム作成関数");

        //通常アイテムの生成
        if (m_currentCreateNum < m_powerfulCreateNum)
        {
            CreateNormalItem();

            //アイテム生成をカウント
            m_currentCreateNum++;
        }
        //強力なアイテムの生成
        else
        {
            Debug.Log("個数リセット");
            m_currentCreateNum = 0;
        }
    }

    //通常アイテムの生成
    private void CreateNormalItem()
    {
        testNum++;
        Debug.Log("回数：" + testNum);
        //ToDo：アイテムの出現方法の選択　ランダムかプレイヤーの近くか

        //生成可能場所があるか確認する
        if (!m_generate.IsOpenPlace()) return;

        //アイテムの生成位置を取得
        List<Vector3> floatPos = new List<Vector3>();

        //アイテムに渡す生成位置
        List<Vector2Int> IntPos = new List<Vector2Int>();

        floatPos.Add(m_generate.GetGeneratePos(ref IntPos));

        //アイテムの生成
        List<GameObject> created = m_itemSelecter.CreateNormalItems(floatPos);

        for (int idx = 0; idx < created.Count; idx++)
        {
            //アイテムに生成位置を渡す
            created[idx].GetComponent<MyItemInterface>().SetAppearPos(this, IntPos[idx]);

            //アイテム初期化
            ItemInit(created[idx]);
        }
    }

    /// <summary>
    /// 指定アイテムの生成
    /// </summary>
    /// <param name="_item"> 生成したいアイテム </param>
    /// <param name="_createPos"> 生成場所 </param>
    private void ItemInit(GameObject _item)
    {
        //アイテム出現時の初期化
        _item.transform.rotation = Quaternion.Euler(_item.GetComponent<MyItemInterface>().GeneretedDegree);
        Debug.Log("角度変更：");
        //アイテム出現エフェクトの呼び出し
        m_itemAppear.StartAppear(_item);
        //エフェクト呼び出し
        Debug.Log("エフェクト");
    }

    /// <summary>
    /// 作成許可の発行を行う
    /// </summary>
    private void CreateLicensePublish()
    {
        //作成許可
        m_license = false;

        //時間進行
        m_gameTimer += Time.deltaTime;

        if (m_phaze + 1 > m_phaseInterval.Count) return;

        //フェーズ進行具合を確認
        if (m_phaseInterval[m_phaze + 1] <= m_gameTimer)
        {
            ++m_phaze;
            m_lastCreatedTime = m_phaseInterval[m_phaze];
            Debug.Log("フェーズ進行");
        }

        //次のフェーズで無ければ、排出しない
        if (m_gameTimer < m_phaseInterval[m_phaze]) return;

        //作成許可の発行確認
        if (m_gameTimer - m_lastCreatedTime >= m_itemIntervel[m_phaze])
        {
            m_lastCreatedTime = m_gameTimer;
            m_license = true;
            Debug.Log("作成許可");
        }
    }

    public void ReSetCall(Vector2Int _generated)
    {
        m_generate.Reset(_generated);
    }
}
