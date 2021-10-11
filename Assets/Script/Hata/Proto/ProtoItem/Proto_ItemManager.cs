using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//仕事一覧
//アイテムを生成、保持
//プレイヤーとアイテムの接触を取得
// └接触者が連打開始
// └受付時間開始
//　└先に連打が終了した人物が連打終了
//      └アイテムを渡す

/// <summary>
/// プロト用のアイテム管理クラス
/// </summary>
public class Proto_ItemManager : Singleton<Proto_ItemManager>
{
    [Header("地面")]
    [SerializeField] private GameObject flootObj = null;

    [Header("出現させるゲームオブジェクト一覧")]
    [SerializeField] List<GameObject> itemList = new List<GameObject>();
    //生成したゲームオブジェクト一覧
    List<GameObject> createdItems = new List<GameObject>();

    [Header("出現時間の最大間隔")]
    [SerializeField] float appearMaxIntervel = 20.0f;
    [Header("出現時間の最小間隔")]
    [SerializeField] float appearMinInterverl = 5.0f;

    [Header("アイテムテスト回数")]
    [Range(1, 50)] 
    [SerializeField]
    int itemTestNum = 1;

    private enum ItemType
    {
        MISSILE,
        MOAI,
        ALL
    }

    [Header("出現させるアイテム")]
    [SerializeField] ItemType itemType = ItemType.MISSILE;
    
    private int currentItemNum = 0;

    //現在の出現間隔時間
    float currentIntervel=0.0f;
    //現在の経過時間
    float appearTime = 0.0f;

    //床の大きさ
    float floorWidth = 0.0f;
    float floorDepth = 0.0f;


    public void MyStart()
    {
        //床の大きさを取得
        Renderer floorRender = flootObj.GetComponent<Renderer>();
        floorWidth = floorRender.bounds.size.x;
        floorDepth = floorRender.bounds.size.z;

        //最初の出現時間を設定
        currentIntervel = GetRandomIntervelTime();

        currentItemNum = 0;
    }

    public void MyUpdate()
    {
        if (currentItemNum < itemTestNum)
        {
            //アイテムの出現
            if (appearTime < currentIntervel) appearTime += Time.deltaTime;
            else
            {
                //次の出現の準備
                appearTime = 0.0f;
                currentIntervel = GetRandomIntervelTime();

                //出現場所を指定
                Vector2 pos = GetRandomAppearPos();

                //ToDo：アイテムの選択が必要になる
                GameObject item = new GameObject();
                switch(itemType)
                {
                    case ItemType.MISSILE:
                        item = Instantiate(itemList[0]);
                        break;

                    case ItemType.MOAI:
                        item = Instantiate(itemList[1]);
                        break;

                    case ItemType.ALL:
                        int itemIdx = Random.Range(0, 2);
                        Debug.Log(itemIdx);
                        item = Instantiate(itemList[itemIdx]);
                        break;
                }
                
                item.transform.position = new Vector3(pos.x, 0.0f, pos.y);
                item.GetComponent<Proto_ItemStateMachine>().MyInit();
                createdItems.Add(item);

                currentItemNum++;
            }
        }
        
        if (itemList.Count > 0)
        {
            foreach (var item in createdItems)
            {
                item.GetComponent<Proto_ItemStateMachine>().MyUpdate();
            }
        }
    }

    /// <summary>
    /// ランダムに出現の間隔時間を渡す
    /// </summary>
    private float GetRandomIntervelTime()
    {
        return Random.Range(appearMinInterverl, appearMaxIntervel);
    }

    /// <summary>
    /// ランダムに出現場所を渡す（プレイヤーの位置等は気にしないこととする）
    /// </summary>
    private Vector2 GetRandomAppearPos()
    {
        Vector2 pos = Vector2.zero;

        pos.x = Random.Range(-floorWidth / 2.0f, floorWidth / 2.0f);
        pos.y = Random.Range(-floorDepth / 2.0f, floorDepth / 2.0f);

        return pos;
    }

    /// <summary>
    /// 指定のアイテムをupdateから外す
    /// </summary>
    /// <param name="_target"></param>
    public void RemoveItem(GameObject _target)
    {
        createdItems.Remove(_target);
    }
}
