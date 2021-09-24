using Photon.Pun;
using UnityEngine;

public class PrefubTestNet : MonoBehaviourPunCallbacks, IPunPrefabPool
{
    [Header("テストオブジェクト")]
    [SerializeField] GameObject obj;

    public void Destroy(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
    {
        GameObject reObj = Instantiate(obj, position, rotation);
        // 生成されたネットワークオブジェクトは非アクティブ状態で返す必要がある
        // （その後、PhotonNetworkの内部で正しく初期化されてから自動的にアクティブ状態に戻される）
        reObj.SetActive(false);

        return reObj;
    }

    private void Start()
    {
        // ネットワークオブジェクトの生成・破棄を行う処理を、このクラスの処理に差し替える
        PhotonNetwork.PrefabPool = this;
    }

}
