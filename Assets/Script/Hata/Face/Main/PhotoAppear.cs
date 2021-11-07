//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//public class PhotoAppear : MonoBehaviour
//{
//    [Header("操作対象のUIのRect")]
//    [SerializeField] 
//    RectTransform[] m_rects = new RectTransform[4];

//    [Header("角度変更に掛ける時間")]
//    [SerializeField]
//    [Range(0.0f, 1.0f)]
//    float m_changeAngleTime = 0.05f;

//    //操作対象の初期情報
//    private float[] m_height = new float[4];
//    private float[] m_zAngles = new float[4];

//    private void Awake()
//    {
//        //初期情報を取得する
//        for (int idx = 0; idx < m_rects.Length; idx++)
//        {
//            m_height[idx] = m_rects[idx].sizeDelta.y;
//            m_zAngles[idx] = m_rects[idx].eulerAngles.z;
//        }
//    }

//    /// <summary>
//    /// 出現の初期化
//    /// </summary>
//    public void InitAppear()
//    {
//        //高さと角度を0にする
//        Vector2 size;
//        Vector3 angle;

//        for (int idx = 0; idx < m_rects.Length; idx++)
//        {
//            //値を取得
//            size = m_rects[idx].sizeDelta;
//            angle = m_rects[idx].eulerAngles;

//            //必要な値のみ変更
//            size.y = 0.0f;
//            angle.z = 0.0f;

//            //代入
//            m_rects[idx].sizeDelta = size;
//            m_rects[idx].eulerAngles = angle;
//        }
//    }

//    /// <summary>
//    /// コルーチンの呼び出し
//    /// </summary>
//    /// <param name="_appearTime">  出現時間 </param>
//    /// <param name="_callBack"> 出現終了後の処理 </param>
//    public void CallAppear(float _appearTime,UnityEngine.Events.UnityAction _callBack)
//    {
//        StartCoroutine(AppearCoroutine(_appearTime, _callBack));
//    }

//    private IEnumerator AppearCoroutine(float _appearTime, UnityEngine.Events.UnityAction _callBack)
//    {
//        float time = 0.0f;
//        //回転時間から減少させる
//        float appearTime = _appearTime - m_changeAngleTime;

//        Vector2 size = Vector2.zero;
//        Vector3 angle = Vector3.zero;

//        //出現中
//        while (time < appearTime)
//        {
//            for (int idx = 0; idx < m_rects.Length; idx++)
//            {
//                //取得する
//                size = m_rects[idx].sizeDelta;

//                //変化
//                if (idx < 2)
//                {
//                    size.y = Easing.QuartInOut(time, appearTime, 0.0f, m_height[idx]);
//                }
//                else
//                {
//                    size.y = Easing.CircOut(time, appearTime, 0.0f, m_height[idx]);
//                }

//                //代入
//                m_rects[idx].sizeDelta = size;
//            }


//            time += Time.deltaTime;
//            yield return null;
//        }
//        time = 0.0f;

//        //位置調整
//        for (int idx = 0; idx < m_rects.Length; idx++)
//        {
//            //取得
//            size = m_rects[idx].sizeDelta;

//            //最終値
//            size.y = m_height[idx];

//            //代入
//            m_rects[idx].sizeDelta = size;
//        }

//        //回転
//        while (time < m_changeAngleTime)
//        {
//            for (int idx = 0; idx < m_rects.Length; idx++)
//            {
//                //取得
//                angle = m_rects[idx].eulerAngles;
//                //変化
//                angle.z = Easing.QuadOut(time, m_changeAngleTime, 0.0f, m_zAngles[idx]);
//                //代入
//                m_rects[idx].eulerAngles = angle;
//            }

//            time += Time.deltaTime;
//            yield return null;
//        }

//        //角度調整
//        for (int idx = 0; idx < m_rects.Length; idx++)
//        {
//            //取得
//            angle = m_rects[idx].eulerAngles;
//            //変化
//            angle.z = m_zAngles[idx];
//            //代入
//            m_rects[idx].eulerAngles = angle;
//        }

//        //後処理実行
//        _callBack();
//    }
//}
