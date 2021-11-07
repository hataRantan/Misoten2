using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class ShutterEffects : MyUpdater
{
    private const int shutterNum = 6;

    [Header("シャッター用Image")]
    [SerializeField]
    Image[] m_shutters = new Image[shutterNum];

    const float minRadious = 0.0f;
    const float maxRadious = 80.0f;
    [Header("半径")]
    [Range(minRadious, maxRadious)]
    [SerializeField]
    float m_radious = 0.0f;

    [Header("シャッターが開閉する時間")]
    [SerializeField]
    private float m_shutterTime = 1.0f;

    [Header("シャッターが開くのを待つ時間")]
    [SerializeField] float m_waitShutterTime = 0.1f;

    //自身のキャンバスグループ
    private CanvasGroup m_group = null;

    //初期位置
    private Vector2[] m_firstPos =
    {
        new Vector2(0.0f, 33.0f),
        new Vector2(45.0f, 32.5f),
        new Vector2(45.0f, -32.5f),
        new Vector2(0.0f, -33.0f),
        new Vector2(-45.0f, -32.5f),
        new Vector2(-45.0f, 32.5f)
    };

    public override void MyFastestInit()
    {
        //初期位置に移動する
        MoveShutter(maxRadious);

        m_group = gameObject.GetComponent<CanvasGroup>();
        m_group.alpha = 0.0f;

        //アップデートが必要ないので更新しない
        m_isUpdate = false;
    }

    /// <summary>
    /// 処理なし
    /// </summary>
    public override void MyUpdate() { }

    /// <summary>
    /// シャッターエフェクトのコルーチン呼び出し
    /// </summary>
    public void CallShutterOn(UnityAction _call,UnityAction _end)
    {
        StartCoroutine(ShutterOn(_call, _end));
    }

    /// <summary>
    /// シャッターを開閉するコルーチン
    /// </summary>
    private IEnumerator ShutterOn(UnityAction _call,UnityAction _end)
    {
        //シャッターを初期位置に移動
        MoveShutter(maxRadious);
        m_group.alpha = 1.0f;

        //時間計測
        float time = 0.0f;
        float radious = 80.0f;

        //閉める移動
        while (time < m_shutterTime)
        {
            radious = -Easing.QuadIn(time, m_shutterTime, -maxRadious, -minRadious);
            MoveShutter(radious);

            time += Time.deltaTime;

            yield return null;
        }

        //調整
        MoveShutter(minRadious);

        //コールバック実施
        _call();

        //少々待つ
        time = 0.0f;
        while (time < m_waitShutterTime)
        {
            time += Time.deltaTime;
            yield return null;
        }


        //開ける移動
        time = 0.0f;
        while (time < m_shutterTime)
        {
            radious = Easing.QuadOut(time, m_shutterTime, minRadious, maxRadious);
            MoveShutter(radious);

            time += Time.deltaTime;

            yield return null;
        }

        //調整
        MoveShutter(maxRadious);
        m_group.alpha = 0.0f;

        //終了を告知
        _end();
    }


    /// <summary>
    /// インスペクターで確認するため
    /// </summary>
    private void OnValidate()
    {
        m_firstPos[0] = new Vector2(0.0f, 33.0f);
        m_firstPos[1] = new Vector2(45.0f, 32.5f);
        m_firstPos[2] = new Vector2(45.0f, -32.5f);
        m_firstPos[3] = new Vector2(0.0f, -33.0f);
        m_firstPos[4] = new Vector2(-45.0f, -32.5f);
        m_firstPos[5] = new Vector2(-45.0f, 32.5f);

        MoveShutter(m_radious);
    }

    /// <summary>
    ///  シャッターを移動させる
    /// </summary>
    private void MoveShutter(float _radious)
    {
        var pos = Vector2.zero;
        float euler = 90.0f;

        for (int idx = 0; idx < shutterNum; idx++)
        {
            //初期位置の取得
            pos = m_firstPos[idx];

            //ラジアンを求める
            float radian = (90.0f - euler) * Mathf.Deg2Rad;

            //移動座標を計算
            pos.x -= _radious * Mathf.Cos(radian);
            pos.y -= _radious * Mathf.Sin(radian);

            m_shutters[idx].rectTransform.anchoredPosition = pos;

            if (idx == 1 || idx == 4) euler += 45.0f;
            else euler += 67.5f;
        }
        
    }
}
