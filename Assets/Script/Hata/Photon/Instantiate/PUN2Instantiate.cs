using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

/// <summary>
/// ネットワークオブジェクトを作成するシングルトンクラス
/// </summary>
public class PUN2Instantiate : Singleton<PUN2Instantiate>
{
    //ネットワークオブジェクト生成クラス
    private Pun2.WrapperInstantiate objCreator = null;

    // Start is called before the first frame update
    void Start()
    {
        //生成者を取得
        objCreator = this.gameObject.GetComponent<Pun2.WrapperInstantiate>();
    }

    /// <summary>
    /// ネットワークオブジェクトの生成
    /// </summary>
    /// <param name="_obj"> 生成したいオブジェクト </param>
    /// <param name="_position"> 生成後の位置 </param>
    /// <param name="_rotation">生成後の 角度 </param>
    /// <returns> 生成したオブジェクト </returns>
    public GameObject CreateNetworkObj(GameObject _obj, Vector3 _position, Quaternion _rotation)
    {
        //生成したいオブジェクトを設定
        objCreator.SetObj(_obj);

        //PhotonNetworlを通じて生成
        return PhotonNetwork.Instantiate(_obj.name, _position, _rotation);
    }

    /// <summary>
    /// オブジェクトの削除
    /// </summary>
    public void Destroy(GameObject _obj)
    {
        PhotonNetwork.Destroy(_obj);
    }
}



