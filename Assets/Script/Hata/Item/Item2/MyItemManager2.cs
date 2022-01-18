using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItemManager2 : MyUpdater
{
    //アイテムの出現を管理するクラス
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
    [Range(0, 25)]
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

    //最初の生成
    bool isFirstCreate = false;

    int testNum = 0;

    public override void MySecondInit()
    {
        //メンバの初期化
        m_phaze = 0;
        m_gameTimer = 0.0f;
        m_license = false;
        m_lastCreatedTime = 0.0f;
        m_currentCreateNum = 0;
        isFirstCreate = false;

        //通常アイテムの生成個数を決定する
        m_createItemNum = GameInPlayerNumber.Instance.CurrentPlayerNum - 1;
        if (m_createItemNum < 2) m_createItemNum = 2;
        //プレイヤー人数の取得して、アイテムの出現個数を取得する
    }

    public override void MyUpdate()
    {
        //DebugFile.Instance.WriteLog("ItemManager2-Updater開始");

        //アイテム出現許可を取得
        CreateLicensePublish();

        //DebugFile.Instance.WriteLog("ItemManager2-許可終了");

        //アイテム生成
        CreateItem();

        //DebugFile.Instance.WriteLog("ItemManager2-生成終了");
    }

    private void CreateItem()
    {
       //作成許可が無ければ、実行しない
        if (!m_license) return;

        //Debug.Log("アイテム作成関数");
        //DebugFile.Instance.WriteLog("ItemManager2-生成開始");

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
            //CreatePowerfulItem();

            //Debug.Log("個数リセット");
            m_currentCreateNum = 0;
        }
    }

    //通常アイテムの生成
    private void CreateNormalItem()
    {
        testNum++;

        //生成可能場所があるか確認する
        if (!m_generate.IsOpenPlace()) return;

        //DebugFile.Instance.WriteLog("生成位置はある");
      
        //アイテムの生成位置を取得
        List<Vector3> floatPos = new List<Vector3>();

        //アイテムに渡す生成位置
        List<Vector2Int> IntPos = new List<Vector2Int>();

        int createNum = m_createItemNum;
        int place = m_generate.IsAllOpenPlace();
        //生成可能位置を確認
        if (createNum > place)
        {
            createNum = place;
        }

        for(int idx=0;idx < createNum; idx++)
        {
            floatPos.Add(m_generate.GetGeneratePos(ref IntPos));
        }


        //アイテムの生成
        List<GameObject> created = m_itemSelecter.CreateNormalItems(floatPos);

        for (int idx = 0; idx < created.Count; idx++)
        {
            MyAudioManeger.Instance.PlaySE("ItemCreate_SE");

            //アイテムに生成位置を渡す
            created[idx].GetComponent<MyItemInterface>().SetAppearPos(this, IntPos[idx]);

            //アイテム初期化
            ItemInit(created[idx]);
        }

        //DebugFile.Instance.WriteLog("アイテムの初期化終了");
    }


    private void CreatePowerfulItem()
    {
        //MyAudioManeger.Instance.PlaySE("SuperItemAppear");
        //強力なアイテムを生成
        GameObject item = m_itemSelecter.CreatePowerfulItem();

        //アイテムの出現エフェクト開始
        //item.GetComponent<MyItemInterface>().SetAppearPos(this, Vector3.zero);

        ItemInit(item);
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
        //Debug.Log("角度変更：");
        //アイテム出現エフェクトの呼び出し
        m_itemAppear.StartAppear(_item);
        //エフェクト呼び出し
        //Debug.Log("エフェクト");
    }

    /// <summary>
    /// 作成許可の発行を行う
    /// </summary>
    private void CreateLicensePublish()
    {
        if(!isFirstCreate)
        {
            m_license = true;
            isFirstCreate = true;
            return;
        }

        //作成許可
        m_license = false;

        //時間進行
        m_gameTimer += Time.deltaTime;

        if (m_phaze + 1 < m_phaseInterval.Count)
        {
            //フェーズ進行具合を確認
            if (m_phaseInterval[m_phaze + 1] <= m_gameTimer)
            {
                ++m_phaze;
                m_lastCreatedTime = m_phaseInterval[m_phaze];
                //Debug.Log("フェーズ進行");
            }
        }

        //次のフェーズで無ければ、排出しない
        if (m_gameTimer < m_phaseInterval[m_phaze]) return;

        //作成許可の発行確認
        if (m_gameTimer - m_lastCreatedTime >= m_itemIntervel[m_phaze])
        {
            m_lastCreatedTime = m_gameTimer;
            m_license = true;
           // Debug.Log("作成許可");
        }
    }

    public void ReSetCall(Vector2Int _generated)
    {
        m_generate.Reset(_generated);
    }
}
