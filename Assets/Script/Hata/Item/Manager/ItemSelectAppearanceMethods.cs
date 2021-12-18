//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace ItemManagement
//{
//    /// <summary>
//    /// アイテムの排出方法を選択する
//    /// </summary>
//    public class ItemSelectAppearanceMethods : MyUpdater
//    {
//        const int minWeight = 0;
//        const int maxWeight = 100;
//        //現在の出現方法
//        public enum Method
//        {
//            NORAML,
//            BY_PLAYER
//        }

//        [System.Serializable]
//        private struct MethodWeight
//        {
//            public Method methodType;
//            [Range(minWeight, maxWeight)]
//            public int weight;
//        }

//        [Header("アイテム出現方法の重み")]
//        [SerializeField]
//        MethodWeight[] methodDic = new MethodWeight[2];
//        //基本の重み
//        Dictionary<Method, int> m_basicWeight = new Dictionary<Method, int>();
//        //変動する重み
//        Dictionary<Method, int> m_methodWeight = new Dictionary<Method, int>();

//        [Header("プレイヤーの1Hpに対する重み")]
//        [Range(0, 50)]
//        [SerializeField] int weightForHp;

//        //前回のプレイヤーの残Hpの合計
//        private int lastTotalHp = -1;

//        /// <summary>
//        /// 初期化
//        /// </summary>
//        public override void MyFastestInit()
//        {
//            //配列をdictionaryに変換
//            for (int idx = 0; idx < methodDic.Length; idx++)
//            {
//                if (!m_basicWeight.ContainsKey(methodDic[idx].methodType))
//                {
//                    m_basicWeight.Add(methodDic[idx].methodType, methodDic[idx].weight);
//                }
//            }
//        }

//        /// <summary>
//        /// アイテムの抽選方法を決める
//        /// </summary>
//        /// <param name="_currentTotalHp"> 現在のプレイヤーの残Hpの合計 </param>
//        public Method SelectMethod(int _currentTotalHp)
//        {
//            //プレイヤーのHpを参照する場合の重みを取得
//            int hpWeight = m_basicWeight[Method.BY_PLAYER];

//            //プレイヤーのHp参照方法の重みを変動させる ---------------------
//            //前回のHpと今回のHpを見比べる
//            int diff = lastTotalHp - _currentTotalHp;
//            //Hpの差があれば
//            if (diff > 0)
//            {
//                //Hpの減り具合に応じて、重みを追加
//                hpWeight += diff * weightForHp;
//                //上限設定
//                if (hpWeight > maxWeight) hpWeight = maxWeight;
//            }

//            //-----------------------------------------------------------------

//            m_methodWeight[Method.BY_PLAYER] = hpWeight;
//            return WeightProbability.GetWeightRandm(ref m_methodWeight);
//        }

//        public override void MyUpdate() { }
//    }

//}
