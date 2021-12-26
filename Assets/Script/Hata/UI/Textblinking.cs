using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// TextMeshProを点滅させるだけ
/// </summary>
public class Textblinking : MonoBehaviour
{
    private TMPro.TextMeshProUGUI m_text = null;
    //現在のカラー情報
    private Color m_cColor = Color.white;

    [Header("一周に必要な秒数")]
    [SerializeField]
    [Range(0.0f, 10.0f)]
    float m_timerOfRound = 1.0f;

    //現在時間
    float m_cTimer = 0.0f;

    private void Awake()
    {
        //コンポーネント等取得
        m_text = GetComponent<TMPro.TextMeshProUGUI>();

        if (!m_text) return;

        m_cColor = m_text.color;
        m_cTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //コンポーネントが無ければ処理しない
        if (!m_text) return;

        //時間経過
        if (m_cTimer < m_timerOfRound) m_cTimer += Time.deltaTime;
        else m_cTimer = 0.0f;

        //-1~1の範囲でα値を求める
        float alpha = Mathf.Sin(2 * Mathf.PI * (1.0f / m_timerOfRound) * m_cTimer);
        //0~1の範囲にクランプする
        alpha = alpha / 2 + 0.5f;
        //α値変更
        m_cColor.a = alpha;
        m_text.color = m_cColor;
    }
}
