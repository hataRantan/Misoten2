using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleInstructionMove : MyUpdater
{
    [Header("指示キャンバス")]
    [SerializeField] Canvas m_canvas = null;
    private CanvasGroup m_group = null;

    [Header("指示文字")]
    [SerializeField] TMPro.TextMeshProUGUI m_text = null;
    private RectTransform m_textPos = null;

    [Header("文字出現時間")]
    [Range(0.0f, 5.0f)]
    [SerializeField] float m_appearTime = 1.0f;

    //移動開始位置
    private Vector2 m_startPos = Vector2.zero;
    [Header("移動後の高さ")]
    [SerializeField] 
    private RectTransform m_endPos = null;

    [Header("指示文字の移動時間")]
    [Range(0.0f, 5.0f)]
    [SerializeField] private float m_textMoveTime = 2.0f;
    float m_moveTimer = 0.0f;

    public override void MyFastestInit()
    {
        //初期化し、α値を0に変更する
        m_group = m_canvas.GetComponent<CanvasGroup>();
        m_group.alpha = 0.0f;

        //現在地を取得する
        m_textPos = m_text.rectTransform;
        m_startPos = m_textPos.anchoredPosition;
    }
    /// <summary>
    /// 指示文字の移動処理
    /// </summary>
    public void MoveText()
    {
        if (m_group.alpha < 1.0f) return;

        if (m_moveTimer < m_textMoveTime)
        {
            //移動
            m_moveTimer += Time.deltaTime;
            float y = Easing.CubicOut(m_moveTimer, m_textMoveTime, m_startPos.y, m_endPos.anchoredPosition.y);
            m_textPos.anchoredPosition = new Vector2(m_startPos.x, y);
        }
        else
        {
            //リセット
            m_moveTimer = 0.0f;
            m_textPos.anchoredPosition = m_startPos;
        }
    }

    /// <summary>
    /// キャンバスの出現
    /// </summary>
    public void CallAppear()
    {
        StartCoroutine(AppearCanvas());
    }

    /// <summary>
    /// α値を0.0f~1.0fに徐々に変更
    /// </summary>
    private IEnumerator AppearCanvas()
    {
        float appearTimer = 0.0f;

        while(appearTimer<m_appearTime)
        {
           m_group.alpha = Easing.SineOut(appearTimer, m_appearTime, 0.0f, 1.0f);
            appearTimer += Time.deltaTime;
            yield return null;
        }

        m_group.alpha = 1.0f;

    }

    public override void MyUpdate() { }
}
