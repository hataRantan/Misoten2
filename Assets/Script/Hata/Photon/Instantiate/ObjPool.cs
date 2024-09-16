using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pun2
{
    /// <summary>
    /// WrapperInstantiate用に調整したオブジェクトプール
    /// </summary>
    public class ObjPool
    {
        //スタック
        private Stack<GameObject> pools = new Stack<GameObject>();

        //プールしているオブジェクトの名前
        public string GetPoolName { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="_poolName"> プール名 </param>
        public ObjPool(string _poolName) { GetPoolName = _poolName; }

        /// <summary>
        ///　オブジェクトを取り出す
        /// </summary>
        public GameObject Pop()
        {
            //プールから取り出す
            if (pools.Count > 0) return pools.Pop();

            //Count0なら呼び出さない用に処理する
            return null;
        }

        /// <summary>
        /// オブジェクトを追加
        /// </summary>
        public void Push(GameObject _obj)
        {
            pools.Push(_obj);
        }

        /// <summary>
        /// プールをクリア
        /// </summary>
        public void Clear()
        {
            pools.Clear();
        }

        /// <summary>
        /// スタック数を返す
        /// </summary>
        /// <returns></returns>
        public int GetCnt()
        {
            return pools.Count;
        }
    }
}

