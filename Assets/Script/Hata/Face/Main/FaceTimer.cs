using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FaceTimer : MonoBehaviour
{
    [Header("10桁の文字")]
    [SerializeField] TextMeshProUGUI m_twoDigit = null;

    [Header("1桁の文字")]
    [SerializeField] TextMeshProUGUI m_oneDigit = null;

    [Header("1桁のみの場合の文字")]
    [SerializeField] TextMeshProUGUI m_onlyDigit = null;

    [Header("文字の大きさ変更のアニメーション時間")]
    [Range(0.0f, 0.9f)]
    [SerializeField] float characterAnimeTime = 0.5f;

    [Header("文字の最大サイズ")]
    [Range(120, 200)]
    [SerializeField] float charactermaxSize = 160;

    //二桁から一桁になった場合の切り替えフラグ
    private bool isChange = false;

    //前フレームの値
    private int lastTime = 0;

    //初期サイズ
    private float firstCharaSize;

    /// <summary>
    /// シーン開始時の初期化
    /// </summary>
    public void Awake()
    {
        //二桁から開始
        PrintTwoDigit();

        lastTime = 0;

        firstCharaSize = m_twoDigit.fontSize;
    }

    /// <summary>
    /// タイマー表示を行う
    /// </summary>
    /// <param name="_remainingTime"> 残り時間 </param>
    /// <param name="_maxTime"> 最大時間 </param>
    public void TimerSet(float _remainingTime, float _maxTime)
    {
        //制限して、小数点以下を切り捨てする
        int time = Mathf.FloorToInt(Mathf.Clamp(_remainingTime, 0.0f, _maxTime));

        //時間が同じなら更新不必要
        if (time == lastTime) return;

        //二桁の場合
        if (time >= 10)
        {
            //一桁目を変更
            int digit = (int)time % 10;
            m_oneDigit.text = digit.ToString();

            //文字のアニメーション開始
            StartCoroutine(Bigger());
        }
        else
        {
            //使用するテキストの変更
            if (!isChange)
            {
                isChange = true;
                PrintOnlyOnwDigit();
            }

            m_onlyDigit.text = time.ToString();

            //文字のアニメーション開始
            StartCoroutine(Bigger());
        }

        lastTime = time;
    }

    private IEnumerator Bigger()
    {
        //初期化
        float time = 0.0f;
        float size = firstCharaSize;

        while (time < characterAnimeTime)
        {
            //サイズ変更
            size = Easing.BackOut(time, characterAnimeTime, firstCharaSize, charactermaxSize, 1.5f);

            if (!isChange) m_oneDigit.fontSize = size;
            else m_onlyDigit.fontSize = size;

            time += Time.deltaTime;

            yield return null;
        }

        size = firstCharaSize;
        if (!isChange) m_oneDigit.fontSize = size;
        else m_onlyDigit.fontSize = size;
     
        //TODO：文字の大きさに合わせて、高さを上げる
    }


    /// <summary>
    /// 描画スタート
    /// </summary>
    public void TimerStart(float _firstTime)
    {
        //二桁から開始
        PrintTwoDigit();

        isChange = false;

        //最初の秒数をセット
        m_oneDigit.text = ((int)_firstTime % 10).ToString();
    }

    /// <summary>
    /// 二桁の場合の文字描画
    /// </summary>
    private void PrintTwoDigit()
    {
        m_twoDigit.gameObject.SetActive(true);
        m_oneDigit.gameObject.SetActive(true);
        m_onlyDigit.gameObject.SetActive(false);
    }

    /// <summary>
    /// 一桁の場合の描画
    /// </summary>
    private void PrintOnlyOnwDigit()
    {
        m_twoDigit.gameObject.SetActive(false);
        m_oneDigit.gameObject.SetActive(false);
        m_onlyDigit.gameObject.SetActive(true);
    }
}
