//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace ItemManagement
//{
//    /// <summary>
//    /// アイテムの出現時間のインターバルを操作する
//    /// </summary>
//    public class ItemAppearanceInterval : MyUpdater
//    {
//        [Header("序盤の出現時間の振れ幅")]
//        [Range(0.0f, 30.0f)]
//        [SerializeField]
//        float begginIntervel;

//        [Header("中盤の出現時間の振れ幅")]
//        [Range(0.0f, 30.0f)]
//        [SerializeField]
//        float middleIntervel;

//        [Header("終盤の出現時間の振れ幅")]
//        [Range(0.0f, 30.0f)]
//        [SerializeField]
//        float finalIntevel;

//        [Header("アイテム出現の基本の間隔時間")]
//        [Range(0.0f, 30.0f)]
//        [SerializeField]
//        float basicIntervel;

//        [Header("初期アイテム出現時間")]
//        [Range(0.0f, 10.0f)]
//        [SerializeField]
//        float firstIntervel;
//        //最初のインターバルを設定しているか
//        bool isFirstSet = false;

//        [Header("アイテム生成時間の最大時間")]
//        [SerializeField]
//        float maxIntervel;

//        //経過時間
//        float currentTimer = 0.0f;
//        //現在の振れ幅
//        float intervelRange = 0.0f;
//        //現在の出現時間
//        float currentIntervel = 0.0f;

//        //初期化
//        public override void MyFastestInit()
//        {
//            //値の初期化
//            currentTimer = 0.0f;
//        }

//        public override void MyUpdate() { }

//        /// <summary>
//        /// アイテムの出現時間を管理する
//        /// </summary>
//        /// <returns> trueなら出現させる </returns>
//        public bool isCreate()
//        {
//            //Debug.Log("出現間隔：" + currentIntervel + "現在時間:" + currentTimer);
//            if (currentTimer < currentIntervel)
//            {
//                currentTimer += Time.deltaTime;

//                return false;
//            }
//            else
//            {
//                //値をリセット
//                currentTimer = 0.0f;
//                SetIntervel();

//                return true;
//            }
//        }

//        /// <summary>
//        /// 進行状況に合わせて、アイテム出現の間隔を変更する
//        /// </summary>
//        /// <param name="_currentProgress"></param>
//        public void SetProgress(MyGameProgress.GameProgress _currentProgress)
//        {
//            switch (_currentProgress)
//            {
//                case MyGameProgress.GameProgress.BEGINNING:
//                    {
//                        intervelRange = begginIntervel;
//                        SetIntervel();
//                    }
//                    break;

//                case MyGameProgress.GameProgress.MIDDLE:
//                    {
//                        intervelRange = middleIntervel;
//                        SetIntervel();
//                    }
//                    break;

//                case MyGameProgress.GameProgress.FINAL:
//                    {
//                        intervelRange = finalIntevel;
//                        SetIntervel();
//                    }
//                    break;
//            }
//        }

//        /// <summary>
//        /// 出現時間の間隔を求める
//        /// </summary>
//        private void SetIntervel()
//        {
//            //最初の生成時間を設定
//            if (!isFirstSet)
//            {
//                currentIntervel = firstIntervel;
//                isFirstSet = true;
//                return;
//            }

//            //基本時間にランダムなインターバルを設定する
//            currentIntervel = basicIntervel + Random.Range(-intervelRange / 2, intervelRange / 2);

//            //インターバルを制限する
//            currentIntervel = Mathf.Clamp(currentIntervel, 0.0f, maxIntervel);
//        }
//    }

//}
