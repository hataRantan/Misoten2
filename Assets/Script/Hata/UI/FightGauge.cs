using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FightGauge : MonoBehaviour
{
    [Header("連打対象"), SerializeField]
    Image m_blow = null;

    [SerializeField]
    Vector3 size = new Vector3(1, 1.2f, 1);

    //自身の座標
    RectTransform m_thisRect = null;
    //親座標
    RectTransform m_parentRect = null;
    //キャンバス座標
   　RectTransform m_canvasRect = null;
    //メインカメラ
    Camera mainCamera = null;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Init(RectTransform _parent, RectTransform _canvas)
    {
        m_thisRect = this.gameObject.GetComponent<RectTransform>();
        m_parentRect = _parent;
        m_canvasRect = _canvas;
        mainCamera = Camera.main;

        m_thisRect.localScale = size;
    }

    /// <summary>
    /// 連打を反映
    /// </summary>
    public void Blow(float _percentage)
    {
        m_blow.fillAmount = _percentage;
    }

    public void OnStage(Vector3 _worldPos)
    {
        //ワールド座標をスクリーン座標に変換
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(mainCamera, _worldPos);
        Vector2 pos = Vector2.zero;
        //スクリーン空間をRectTransformのローカル空間の位置に変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_canvasRect, screenPos, Camera.main, out pos);
        //位置を反映
        m_thisRect.localPosition = pos;
        //親座標の位置を考慮
        m_thisRect.localPosition -= m_parentRect.localPosition;
    }
}
