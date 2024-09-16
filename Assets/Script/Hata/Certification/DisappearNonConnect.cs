using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearNonConnect : MonoBehaviour
{
    [SerializeField] RectTransform m_nonImage = null;
    [SerializeField] TMPro.TextMeshProUGUI[] m_texts = null;

    [Header("小さくなる時間")]
    [SerializeField, Range(0.0f, 3.0f)]
    float m_smallTime = 1.5f;

    public IEnumerator Diappear(UnityEngine.Events.UnityAction _endAction)
    {
        //サイズ取得
        int cnt = m_texts.Length;
        float[] beginSize = new float[cnt];
        for (int idx = 0; idx < cnt; idx++)
        {
            beginSize[0] = 1;
            beginSize[idx] = m_texts[idx].fontSize;
        } 

        //テキストを大きくする
        float timer = 0.0f;
        //サイズ取得
        Vector2 begin = m_nonImage.sizeDelta;
        //テキスト及びイメージのサイズを減少
        timer = 0.0f;
        while (timer < m_smallTime)
        {
            //テキストサイズ変更
            float size = 0;
            for (int idx = 0; idx < cnt; idx++)
            {
                size = -Easing.QuintOut(timer, m_smallTime, -beginSize[idx], 0.0f);
                m_texts[idx].fontSize = size;
            }

            //バックサイズ変更
            Vector2 cSize;
            cSize.x = -Easing.QuintOut(timer, m_smallTime, -begin.x, 0.0f);
            cSize.y = -Easing.QuintOut(timer, m_smallTime, -begin.y, 0.0f);
            m_nonImage.sizeDelta = cSize;

            timer += Time.deltaTime;
            yield return null;
        }

        for (int idx = 0; idx < cnt; idx++)
        {
            m_texts[idx].fontSize = 0.0f;
        }
        m_nonImage.sizeDelta = Vector2.zero;

        _endAction();
    }

}
