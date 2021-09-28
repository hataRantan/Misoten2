using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

/// <summary>
/// ネットワークオブジェクトを作成するシングルトンクラス
/// </summary>
public class PUN2Creater : Singleton<PUN2Creater>
{
    //ネットワークオブジェクト生成クラス
    private Pun2.WrapperInstantiate objCreator = null;

    [Header("生成予定のネットワークオブジェクト一覧")]
    [SerializeField] List<GameObject> networkObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //生成者を取得
        objCreator = this.gameObject.GetComponent<Pun2.WrapperInstantiate>();
        //生成予定のオブジェクト一覧を渡す
        objCreator.SetObjectList(networkObjects);
    }
}



