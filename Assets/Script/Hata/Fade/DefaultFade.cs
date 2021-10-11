using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultFade : FadeBase
{
    [Header("Imageそれぞれの移動時間")]
    [Range(0.1f, 1.0f)]
    [SerializeField] float moveTime = 1.0f;

    [Header("Imageそれぞれの移動の開始間隔")]
    [Range(0.0f, 1.0f)]
    [SerializeField] float intervelTime = 0.1f;

    [Header("フェード後のカラー")]
    [SerializeField] Color fadeColor = new Color(0.15f, 0.15f, 0.15f, 1.0f);

    [Header("右のフェードイメージ群")]
    [SerializeField] List<RectTransform> rightImages = new List<RectTransform>();

    [Header("左のフェードイメージ群")]
    [SerializeField] List<RectTransform> leftImages = new List<RectTransform>();

    //Imageが見えない位置
    private float nonLookPos = 1400.0f;

    //初期位置の記憶
    List<float> firstX = new List<float>();

    public new void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// フェードイン処理
    /// </summary>
    public override IEnumerator FadeIn()
    {
        //初期位置の記憶
        for (int idx = 0; idx < rightImages.Count; idx++)
        {
            firstX.Add(rightImages[idx].anchoredPosition.x);
        }

        Vector2 outSide = new Vector2(nonLookPos, 0.0f);
        //全ての画像をCanvas外に設定
        for (int idx = 0; idx < rightImages.Count; idx++)
        {
            rightImages[idx].anchoredPosition = outSide;
            leftImages[idx].anchoredPosition = -outSide;
        }

        //計測開始
        ProcessTimer timer = new ProcessTimer();
        timer.Restart();

        int startIdx = 0;
        while (timer.TotalSeconds < rightImages.Count * intervelTime + moveTime)
        {

            if (startIdx < rightImages.Count)
            {
                //間隔を超えたらコルーチン開始
                if (timer.TotalSeconds > startIdx * intervelTime)
                {
                    //移動開始
                    StartCoroutine(RightMoveImage(nonLookPos, firstX[startIdx], startIdx, false));
                    
                    StartCoroutine(LeftMoveImage(-nonLookPos, -firstX[startIdx], startIdx, true));
                    startIdx++;
                }
            }

            yield return null;
        }

        //Debug.Log("終了時間：" + timer.TotalSeconds);

        yield return null;
    }

    public override IEnumerator FadeOut()
    {
        //初期位置の記憶
        for (int idx = 0; idx < rightImages.Count; idx++)
        {
            firstX.Add(rightImages[idx].anchoredPosition.x);
        }

        //計測開始
        ProcessTimer timer = new ProcessTimer();
        timer.Restart();

        //最後尾から開始
        int startIdx = rightImages.Count - 1;

        while (timer.TotalSeconds < rightImages.Count * intervelTime + moveTime)
        {
            if ((rightImages.Count - 1 - startIdx) * intervelTime < timer.TotalSeconds)
            {
                if (startIdx >= 0)
                {
                    //移動開始
                    StartCoroutine(RightMoveImage(firstX[startIdx], nonLookPos, startIdx, false));
                    StartCoroutine(LeftMoveImage(-firstX[startIdx], -nonLookPos, startIdx, false));
                }
                startIdx--;

                //Debug.Log("呼び出し自機：" + timer.TotalSeconds + " 、カウント:" + (rightImages.Count - 1 - startIdx));
            }

            yield return null;
        }

        yield return null;
    }

    /// <summary>
    /// 右側のイメージ移動
    /// </summary>
    /// <param name="_startX"> スタート位置 </param>
    /// <param name="_endX"> ゴール位置 </param>
    /// <param name="_idx"> 移動させたい画像番号 </param>
    /// <param name="_isBig"> _startXが_endXより大きいならtrue </param>
    private IEnumerator RightMoveImage(float _startX,float _endX,int _idx,bool _isBig)
    {
        RectTransform target = rightImages[_idx];

        ProcessTimer timer = new ProcessTimer();
        timer.Restart();

        Vector2 pos = new Vector2(_startX, 0.0f);
        while (timer.TotalSeconds<moveTime)
        {
            if (_isBig)
                pos.x = Easing.QuartInOut(timer.TotalSeconds, moveTime, _startX, _endX);
            else
                pos.x = -Easing.QuartInOut(timer.TotalSeconds, moveTime, -_startX, -_endX);

            target.anchoredPosition = pos;

            yield return null;
        }

        //位置調整
        pos.x = _endX;
        target.anchoredPosition = pos;
    }
    /// <summary>
    /// 左側のイメージ移動
    /// </summary>
    /// <param name="_startX"> スタート位置 </param>
    /// <param name="_endX"> ゴール位置 </param>
    /// <param name="_idx"> 移動させたい画像番号 </param>
    /// <param name="_isBig"> _startXが_endXより大きいならtrue </param>
    private IEnumerator LeftMoveImage(float _startX,float _endX,int _idx,bool _isBig)
    {
        RectTransform target = leftImages[_idx];

        ProcessTimer timer = new ProcessTimer();
        timer.Restart();

        Vector2 pos = new Vector2(_startX, 0.0f);
        while (timer.TotalSeconds < moveTime)
        {
            if (_isBig)
                pos.x = Easing.QuartInOut(timer.TotalSeconds, moveTime, _startX, _endX);
            else
                pos.x = -Easing.QuartInOut(timer.TotalSeconds, moveTime, -_startX, -_endX);

            target.anchoredPosition = pos;

            yield return null;
        }

        //位置調整
        pos.x = _endX;
        target.anchoredPosition = pos;
    }
    
}
