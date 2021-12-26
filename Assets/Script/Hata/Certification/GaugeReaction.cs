using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GaugeReaction : MonoBehaviour
{
    [Header("反応するためのメインゲージ")]
    [SerializeField] Image m_mainGauge = null;

    [Header("反応時間")]
    [SerializeField]
    [Range(0.0f, 1.0f)]
    float m_reactionTime = 0.1f;

    [Header("最大の大きさ")]
    [SerializeField, Range(1.0f, 2.0f)]
    float m_maxFactor = 0.0f;
    Vector2 m_maxSize = Vector2.zero;
    Vector2 m_beginSize = Vector2.zero;

    private RectTransform rect = null;
    CanvasGroup m_group = null;

    private void Start()
    {
        rect = m_mainGauge.rectTransform;
        m_beginSize = rect.localScale;
        m_maxSize = m_beginSize * m_maxFactor;
        m_group = m_mainGauge.GetComponent<CanvasGroup>();
        m_group.alpha = 0.0f;
    }

    /// <summary>
    /// ボタンを押した際の反応
    /// </summary>
    public IEnumerator StartReaction()
    {
        float timer = 0.0f;
        m_mainGauge.rectTransform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

        while (timer < m_reactionTime)
        {
            float alpha = -Easing.QuintOut(timer, m_reactionTime, -1.0f, 0.0f);
            Vector2 size = m_beginSize;
            size.x = Easing.QuintInOut(timer, m_reactionTime, m_beginSize.x, m_maxSize.x);
            size.y = Easing.QuintInOut(timer, m_reactionTime, m_beginSize.y, m_maxSize.y);

            m_group.alpha =alpha;
            rect.localScale = size;

            timer += Time.deltaTime;
            yield return null;
        }

        m_group.alpha = 0.0f;
    }
}
