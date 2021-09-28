using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Pun2
{
    /// <summary>
    /// Pun2のインスタンスを上書きするクラス
    /// </summary>
    public class WrapperInstantiate : MonoBehaviourPunCallbacks, IPunPrefabPool
    {
        //生成対象のプール
        private Dictionary<string, ObjPool> m_generatedPool = new Dictionary<string, ObjPool>();
        //生成予定のオブジェクト一覧
        private Dictionary<string, GameObject> m_generateObjects = new Dictionary<string, GameObject>();

        private void Start()
        {
            // ネットワークオブジェクトの生成・破棄を行う処理を、このクラスの処理に差し替える
            PhotonNetwork.PrefabPool = this;
        }

        /// <summary>
        /// 生成したオブジェクトを設定 
        /// </summary>
        /// <param name="_obj"> 生成したいオブジェクト </param>
        public void SetObjectList(List<GameObject> _networkObjList)
        {
            foreach(var obj in _networkObjList)
            {
                m_generateObjects.Add(obj.name, obj);
            }
        }

        /// <summary>
        /// オブジェクトを生成または、プールから取り出す
        /// </summary>
        /// <param name="prefabId"> 生成したいオブジェクト名 </param>
        /// <param name="position"> 生成したい位置 </param>
        /// <param name="rotation"> 生成したい角度 </param>
        /// <returns> 生成または取り出したオブジェクト </returns>
        public GameObject Instantiate(string _prefabId, Vector3 _position, Quaternion _rotation)
        {
#if UNITY_EDITOR
            if (!m_generateObjects.ContainsKey(_prefabId))
            {
                Debug.LogError(_prefabId + "のプレハブは、PUN2Createに設定されていません");
            }
#endif

            //生成オブジェクトの入れ物
            GameObject generate = null;

            //プールの確認
            if(m_generatedPool.ContainsKey(_prefabId))
            {
                if(m_generatedPool[_prefabId].GetCnt()>0)
                {
                    //プールから取り出す
                    generate = m_generatedPool[_prefabId].Pop();
                    generate.transform.SetPositionAndRotation(_position, _rotation);
                }
                else
                {
                    //プールにスタックが無い為、生成
                    generate = Instantiate(m_generateObjects[_prefabId], _position, _rotation);
                }

            }
            else
            {
                //プールが無いため、生成
                generate = Instantiate(m_generateObjects[_prefabId], _position, _rotation);
            }

            //生成したオブジェクトの(Clone)を削除
            generate.name = _prefabId;

            //PhotonNetworkの内部で正しく初期化されてから自動的にアクティブ状態に戻される
            generate.SetActive(false);

            //ネットワークオブジェクトの初期化コンポーネントを起動
            Pun2Obj init = generate.GetComponent<Pun2Obj>();
            if(init!=null)
            {
                init.enabled = true;
            }
            else
            {
                Debug.LogError("ネットワークオブジェクトなのに、PhotonObjコンポーネントが存在しない");
            }

            return generate;
        }

        /// <summary>
        /// オブジェクトの削除 *実際にDestroyするのではなく、activeをfalseにするだけなので注意
        /// </summary>
        public void Destroy(GameObject _obj)
        {
            //生成したオブジェクトに付いている(Clone)を削除
            string poolName = _obj.name.Replace("(Clone)", "");

            //プールの有無の確認
            if (!m_generatedPool.ContainsKey(poolName))
            {
                //プール生成
                ObjPool pool = new ObjPool(poolName);
                m_generatedPool.Add(poolName, pool);
            }

            //プールに追加
            m_generatedPool[poolName].Push(_obj);

            // PhotonNetworkの内部で既に非アクティブ状態にされているので、以下の処理は不要
            // _obj.gameObject.SetActive(false);
        }

        /// <summary>
        /// メモリ解放
        /// </summary>
        public override void OnDisable()
        {
            m_generatedPool.Clear();
            //nullを入れることで、次のGCの対象になるらしい
            m_generatedPool.Add("none", null);

            m_generatedPool.Clear();
            m_generateObjects.Add("none", null);
        }
    }
}

