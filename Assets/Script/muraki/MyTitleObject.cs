using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyTitleStateClass;
using UnityEngine.UI;
/// <summary>
/// プレイヤーが操作する対象
/// </summary>
public class MyTitleObject : MyUpdater
{
    //----------------------------------------------------------------------------
    //操作対象
    //----------------------------------------------------------------------------
    [Header("黒背景")]
    [SerializeField]
    Image m_backImage = null;
  
    [Header("憑文字")]
    [SerializeField]
    Image m_hyoImage = null;

    [Header("火の玉")]
    [SerializeField]
    Image[] m_fireballs = null;

    [Header("INの文字")]
    [SerializeField]
    Image m_inImage = null;

    [Header("イベントで使用するキャンバスグループ")]
    [SerializeField] CanvasGroup m_beforeEvent = null;

    [Header("白背景")]
    [SerializeField]
    Image m_whiteImage = null;
    [Header("タイトルロゴ")]
    [SerializeField]
    Image m_titleImage = null;
    //----------------------------------------------------------------------------
    //Case0
    //----------------------------------------------------------------------------
    [Header("背景の初期色")]
    [SerializeField] Color mCase0_Color = new Color(0, 0, 0, 1);
    //----------------------------------------------------------------------------
    //Case1
    //----------------------------------------------------------------------------
    [Header("憑文字を大きくする時間")]
    [Range(0.0f, 5.0f)]
    [SerializeField]
    float mCase1_HyoBigTime = 0.0f;

    [Header("憑文字のα値の変化時間")]
    [Range(0.0f, 3.0f)]
    [SerializeField]
    float mCase1_HyoAlphaTime = 0.0f;

    [Header("憑の最大の大きさ")]
    [Range(1.0f,10.0f)]
    [SerializeField] 
    float mCase1_HyoMaxSize = 10.0f;

    [Header("憑文字のBackOutの幅")]
    [Range(0.0f, 3.0f)]
    [SerializeField]
    float mCase1_HyoSizeRange = 1.5f;
    //----------------------------------------------------------------------------
    //Case2
    //----------------------------------------------------------------------------
    [Header("Case1からCase2までの移行時間")]
    [Range(0.0f, 3.0f)]
    [SerializeField]
    float m_Case1ToCase2Time = 1.0f;

    [Header("火の玉の出現時間")]
    [Range(0.0f, 5.0f)]
    [SerializeField]
    float mCase2_FireAppearTime = 1.0f;
    //----------------------------------------------------------------------------
    //Case3
    //----------------------------------------------------------------------------
    [Header("Case2からCase3までの移行時間")]
    [Range(0.0f, 3.0f)]
    [SerializeField]
    float m_Case2ToCase3Time = 1.0f;

    [Header("火の玉の移動位置")]
    [SerializeField] RectTransform[] mCase3_Goal = null;

    [Header("火の玉の移動時間")]
    [Range(0.0f, 5.0f)]
    [SerializeField] float mCase3_FireMoveTime = 2.0f;

    [Header("火の玉移動の振れ幅")]
    [Range(0.0f, 3.0f)]
    [SerializeField] float mCase3_FireMoveRange = 1.5f;
    //----------------------------------------------------------------------------
    //Case4
    //----------------------------------------------------------------------------
    [Header("Case3からCase4までの移行時間")]
    [Range(0.0f, 3.0f)]
    [SerializeField]
    float m_Case3ToCase4Time = 1.0f;

    [Header("火の玉の拡大時間")]
    [Range(0.0f,5.0f)]
    [SerializeField]
    float mCase4_FireBigTime = 2.0f;

    [Header("火の玉の最大サイズ")]
    [Range(1.0f, 5.0f)]
    float mCase4_FireMaxSize = 2.0f;

    [Header("火の玉のサイズの振れ幅")]
    [Range(1.0f, 5.0f)]
    float mCase4_FireSizeRange = 1.0f;

    [Header("拡大してから透明化を開始する時間")]
    [Range(0.0f, 5.0f)]
    [SerializeField]
    float mCase4_ToClearTime = 1.0f;

    [Header("火の玉が透明になる時間")]
    [Range(0.0f, 5.0f)]
    [SerializeField]
    float mCase4_FireClearTime = 1.0f;
    //----------------------------------------------------------------------------
    //Case5
    //----------------------------------------------------------------------------
    [Header("Case4からCase5までの移行時間")]
    [Range(0.0f, 3.0f)]
    [SerializeField]
    float m_Case4ToCase5Time = 1.0f;

    [Header("憑の移動距離")]
    [Range(0.0f, 1000.0f)]
    [SerializeField]
    float mCase5_HyoMoveDistance = 25.0f;

    [Header("憑の移動時間")]
    [Range(0.0f, 5.0f)]
    [SerializeField]
    float mCase5_HyoMoveTime = 1.0f;

    [Header("INの最小サイズ")]
    [Range(1.0f, 1.0f)]
    [SerializeField]
    float mCase5_InMinSize = 1.0f;

    [Header("INの最大サイズ")]
    [Range(1.0f, 10.0f)]
    [SerializeField] 
    float mCase5_InMaxSize = 5.0f;

    [Header("INの拡大時間")]
    [Range(0.0f, 5.0f)]
    [SerializeField] 
    float mCase5_InBigTime = 2.0f;

    [Header("INの拡大開始時間")]
    [Range(0.0f, 5.0f)]
    [SerializeField]
    float mCase5_ToInBigTime = 1.0f;
    //----------------------------------------------------------------------------
    //Case6
    //----------------------------------------------------------------------------
    [Header("Case5からCase6までの移行時間")]
    [Range(0.0f, 3.0f)]
    [SerializeField]
    float m_Case5ToCase6Time = 1.0f;

    [Header("背景フェードインの時間")]
    [Range(0.0f, 3.0f)]
    [SerializeField]
    float mCase6_FadeInTime = 1.0f;

    [Header("白背景での待ち時間")]
    [Range(0.0f, 3.0f)]
    [SerializeField]
    float mCase6_WaitTime = 1.0f;

    [Header("背景フェードアウトの時間")]
    [Range(0.0f, 3.0f)]
    [SerializeField]
    float mCase6_FadeOutTime = 1.0f;


    /// <summary>
    /// プレイヤーの状態関係
    /// </summary>
    public enum MyTitleObjectState
    {
        tEvent,
        tWait,
    }
    //現在の状態
    public MyTitleObjectState m_tState { get; private set; }

    //プレイヤーの状態マシーン
    private IStateSpace.StateMachineBase<MyTitleObjectState, MyTitleObject> m_tmachine;
    
    // イベント終了通知フラグ
    public bool isEvent { get; private set; }

    public override void MyFastestInit()
    {
        isEvent = false;
        ///背景等初期化
        Case0();

        //状態生成
        m_tmachine = new IStateSpace.StateMachineBase<MyTitleObjectState, MyTitleObject>();
        //状態の追加
        m_tmachine.AddState(MyTitleObjectState.tEvent, new tEventState(), this);
        m_tmachine.AddState(MyTitleObjectState.tWait, new tWaitState(), this);
        //初期状態に変更
        m_tmachine.InitState(MyTitleObjectState.tEvent);
    }
    /// <summary>
    /// 初期化を行う段階
    /// </summary>
    private void Case0()
    {
        //背景カラー初期化
        m_backImage.color = mCase0_Color;

        //他の画像のα値を0にする
        Color clearness = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        ToClearImage(ref m_hyoImage);
        ToClearImage(ref m_fireballs[0]);
        ToClearImage(ref m_fireballs[1]);
        ToClearImage(ref m_inImage);
        ToClearImage(ref m_titleImage);
        ToClearImage(ref m_whiteImage);

        void ToClearImage(ref Image _image)
        {
            //色情報取得
            clearness = _image.color;
            //α値を0にして反映
            clearness.a = 0.0f;
            _image.color = clearness;
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    public override void MyUpdate()
    {
        m_tmachine.UpdateState();
    }

    //個々の呼び出しはこれ以上増やさない
    public void CallEvent()
    {
        StartCoroutine(TitleRgoEvents());
    }

    //1つめの処理
    private IEnumerator TitleRgoEvents()
    {
        //憑文字出現
        yield return StartCoroutine(Case1());

        //火の玉出現
        yield return StartCoroutine(Case2());

        //火の玉移動
        yield return StartCoroutine(Case3());

        //火の玉消失
        yield return StartCoroutine(Case4());

        //In出現
        yield return StartCoroutine(Case5());

        //終了処理
        yield return StartCoroutine(Case6());

        //イベント終了通知
        isEvent = true;
    }
 
    /// <summary>
    /// 「憑」の文字を出現させる
    /// </summary>
    private IEnumerator Case1()
    {
        float timer = 0.0f;
        RectTransform Hyou = m_hyoImage.rectTransform;
        float size = 0.0f;

        //時間オーバーを禁止する
        if (mCase1_HyoBigTime < mCase1_HyoAlphaTime) mCase1_HyoAlphaTime = mCase1_HyoBigTime;
        //憑のα値を徐々に1にする
        StartCoroutine(Case1_Alpha());

        while (timer < mCase1_HyoBigTime)
        {
            //大きさ変更
            size = Easing.BackOut(timer, mCase1_HyoBigTime, 0.0f, mCase1_HyoMaxSize, mCase1_HyoSizeRange);
            Hyou.localScale = new Vector3(size, size, 1.0f);

            timer += Time.deltaTime;

            yield return null;
        }
        Hyou.localScale = new Vector3(mCase1_HyoMaxSize, mCase1_HyoMaxSize, 1.0f);
    }
    /// <summary>
    /// 「憑」のα値を1に変更
    /// </summary>
    private IEnumerator Case1_Alpha()
    {
        float timer = 0.0f;
        Color hyoColor = m_hyoImage.color;

        while (timer < mCase1_HyoAlphaTime)
        {
            //α値変更
            hyoColor.a = Easing.SineIn(timer, mCase1_HyoAlphaTime, 0.0f, 1.0f);
            m_hyoImage.color = hyoColor;

            timer += Time.deltaTime;
            yield return null;
        }

        hyoColor.a = 1.0f;
        m_hyoImage.color = hyoColor;
    }
    /// <summary>
    /// 火の玉の出現(α値を1に変更)
    /// </summary>
    private IEnumerator Case2()
    {
        float timer = 0.0f;
        Color fireColor = m_fireballs[0].color;

        //一定時間待つ
        while(timer<m_Case1ToCase2Time)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        //α値の変更
        timer = 0.0f;
        while (timer < mCase2_FireAppearTime)
        {
            fireColor.a = Easing.QuintInOut(timer, mCase2_FireAppearTime, 0.0f, 1.0f);

            foreach(var fire in m_fireballs)
            {
                fire.color = fireColor;
            }
            
            timer += Time.deltaTime;
            yield return null;
        }
        //α値を1に変更
        fireColor.a = 1.0f;
        foreach (var fire in m_fireballs)
        {
            fire.color = fireColor;
        }

    }
    /// <summary>
    /// 火の玉移動
    /// </summary>
    private IEnumerator Case3()
    {
        float firemovetimer = 0.0f;

        //一定時間待つ
        while (firemovetimer < m_Case2ToCase3Time)
        {
            firemovetimer += Time.deltaTime;
            yield return null;
        }

        //キャストして保持
        RectTransform[] fireRect = new RectTransform[2];
        fireRect[0] = m_fireballs[0].rectTransform;
        fireRect[1] = m_fireballs[1].rectTransform;

        //初期位置の保存
        Vector2[] firstPoses = new Vector2[2];
        for(int idx = 0; idx < fireRect.Length; idx++)
        {
            firstPoses[idx] = fireRect[idx].anchoredPosition;
        }

        //移動後の位置
        Vector2 movePos = Vector2.zero;

        //移動
        firemovetimer = 0.0f;
        while (firemovetimer < mCase3_FireMoveTime)
        {
            for (int idx = 0; idx < fireRect.Length; idx++)
            {
                //X移動
                if (firstPoses[idx].x > mCase3_Goal[idx].anchoredPosition.x)
                    movePos.x = -Easing.BackIn(firemovetimer, mCase3_FireMoveTime, -firstPoses[idx].x, -mCase3_Goal[idx].anchoredPosition.x, mCase3_FireMoveRange);
                else
                    movePos.x = Easing.BackIn(firemovetimer, mCase3_FireMoveTime, firstPoses[idx].x, mCase3_Goal[idx].anchoredPosition.x, mCase3_FireMoveRange);

                //Y移動
                if (firstPoses[idx].y > mCase3_Goal[idx].anchoredPosition.y)
                    movePos.y = -Easing.BackIn(firemovetimer, mCase3_FireMoveTime, -firstPoses[idx].y, -mCase3_Goal[idx].anchoredPosition.y, mCase3_FireMoveRange);
                else
                    movePos.y = Easing.BackIn(firemovetimer, mCase3_FireMoveTime, firstPoses[idx].y, mCase3_Goal[idx].anchoredPosition.y, mCase3_FireMoveRange);

                fireRect[idx].anchoredPosition = movePos;
            }

            firemovetimer += Time.deltaTime;
            yield return null;
        }
        //調整
        for (int idx = 0; idx < fireRect.Length; idx++)
        {
            fireRect[idx].anchoredPosition = mCase3_Goal[idx].anchoredPosition;
        }
    }
    /// <summary>
    /// 火の玉消失
    /// </summary>
    private IEnumerator Case4()
    {
        float timer = 0.0f;
        //透明化コルーチンを読んだか
        bool isCall = false;
        //コルーチンの終了
        bool isEnd = false;

        //一定時間待つ
        while (timer < m_Case3ToCase4Time)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        float size = 0.0f;
        float currentSize = m_fireballs[0].rectTransform.localScale.x;

        //キャッシュして保持
        RectTransform[] fireRect = new RectTransform[2];
        fireRect[0] = m_fireballs[0].rectTransform;
        fireRect[1] = m_fireballs[1].rectTransform;

        //時間の制限
        if (mCase4_FireBigTime < mCase4_ToClearTime) mCase4_ToClearTime = mCase4_FireBigTime;

        //拡大及び透明化の呼び出し
        timer = 0.0f;
        while (timer < mCase4_FireBigTime)
        {
            //サイズ変更
            size = Easing.BackOut(timer, mCase4_FireBigTime, currentSize, mCase4_FireMaxSize, mCase4_FireSizeRange);

            for (int idx = 0; idx < fireRect.Length; idx++)
            {
                fireRect[idx].localScale = new Vector3(size, size, 1.0f);
            }

            timer += Time.deltaTime;

            //透明化呼び出し
            if (!isCall)
            {
                if (timer > mCase4_ToClearTime)
                {
                    StartCoroutine(Case4_FireToClear(EndAction));
                    isCall = true;
                }
            }

            yield return null;
        }

        //調整
        size = mCase4_FireMaxSize;
        for (int idx = 0; idx < fireRect.Length; idx++)
        {
            fireRect[idx].localScale = new Vector3(size, size, 1.0f);
        }

        //透明化終了待ち
        while (!isEnd)
        {
            yield return null;
        }

        //コルーチンの終了通知
        void EndAction() { isEnd = true; }
    }
    /// <summary>
    /// 火の玉を透明化
    /// </summary>
    private IEnumerator Case4_FireToClear(UnityEngine.Events.UnityAction _endAction)
    {
        float timer = 0.0f;
        Color clearCol = m_fireballs[0].color;
        //透明化
        while (timer < mCase4_FireClearTime)
        {
            //α値を1.0fから0.0fに変更
            clearCol.a = 1.0f - Easing.CircOut(timer, mCase4_FireClearTime, 0.0f, 1.0f);

            foreach(var fire in m_fireballs)
            {
                fire.color = clearCol;
            }

            timer += Time.deltaTime;
            yield return null;
        }
        //調整
        clearCol.a = 0.0f;
        foreach (var fire in m_fireballs)
        {
            fire.color = clearCol;
        }
        //終了処理
        _endAction();
    }
    /// <summary>
    /// In出現
    /// </summary>
    private IEnumerator Case5()
    {
        float timer = 0.0f;
        bool isCall = false;
        bool isEnd = false;

        //一定時間待つ
        while (timer < mCase5_HyoMoveTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        //移動後の位置
        RectTransform hyoRect = m_hyoImage.rectTransform;
        float startX = hyoRect.anchoredPosition.x;
        float endX = startX - mCase5_HyoMoveDistance;
        Vector2 pos = hyoRect.anchoredPosition;

        //時間制限
        if (mCase5_HyoMoveTime < mCase5_ToInBigTime) mCase5_HyoMoveTime = mCase5_ToInBigTime;

        //In拡大
        StartCoroutine(Case5_InBig(EndAction));

        //移動
        timer = 0.0f;
        while (timer < mCase5_HyoMoveTime)
        {
            pos.x = -Easing.QuintOut(timer, mCase5_HyoMoveTime, -startX, -endX);
            hyoRect.anchoredPosition = pos;

            timer += Time.deltaTime;

            yield return null;
        }

        //調整
        pos.x = endX;
        hyoRect.anchoredPosition = pos;

        //Inの拡大終了待ち
        while(!isEnd)
        {
            yield return null;
        }

        void EndAction() { isEnd = true; }
    }
    /// <summary>
    /// Inの移動と大きさ変更
    /// </summary>
    private IEnumerator Case5_InBig(UnityEngine.Events.UnityAction _endAction)
    {
        Vector2 hyoPos = m_hyoImage.rectTransform.anchoredPosition;
        //憑の右端を取得する
        hyoPos.x += (m_hyoImage.rectTransform.sizeDelta.x * m_hyoImage.rectTransform.localScale.x);
        //憑の下端を取得する
        hyoPos.y -= (m_hyoImage.rectTransform.sizeDelta.y * m_hyoImage.rectTransform.lossyScale.y);

        RectTransform inRect = m_inImage.rectTransform;
        //憑の端に移動する
        inRect.anchoredPosition = hyoPos;
        Vector2 inPos = inRect.anchoredPosition;

        float size = 0.0f;
        float timer = 0.0f;

        //Inのα値を1に変更
        Color inColor = m_inImage.color;
        inColor.a = 1.0f;
        m_inImage.color = inColor;

        //子オブジェクトの座標取得
        RectTransform childBottom = m_inImage.gameObject.GetComponentInChildren<RectTransform>();
        float diff = 0.0f;

        //拡大
        while (timer < mCase5_InBigTime)
        {
            //サイズ変更
            size = Easing.QuartOut(timer, mCase5_InBigTime, mCase5_InMinSize, mCase5_InMaxSize);
            inRect.localScale = new Vector3(size, size, 1.0f);

            Debug.Log("ボトム：" + childBottom.anchoredPosition + "  表:" + hyoPos.y);

            //下端を揃える
            diff = hyoPos.y - childBottom.anchoredPosition.y;
            inPos.y -= diff;
            //inRect.anchoredPosition = inPos;

            timer += Time.deltaTime;
            yield return null;
        }

        //調整
        inRect.localScale = new Vector3(mCase5_InMaxSize, mCase5_InMaxSize, 1.0f);
        inPos.y = m_hyoImage.rectTransform.anchoredPosition.y;
        inRect.anchoredPosition = inPos;

        _endAction();
    }
    /// <summary>
    /// 背景フェードインアウト
    /// </summary>
    private IEnumerator Case6()
    {
        float timer = 0.0f;

        //一定時間待つ
        while (timer < m_Case5ToCase6Time)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0.0f;
        Color backColor = m_whiteImage.color;

        //背景フェードイン
        while (timer < mCase6_FadeInTime)
        {
            //背景のα値を0.0fから1.0fに変更
            backColor.a = Easing.CircIn(timer, mCase6_FadeInTime, 0.0f, 1.0f);

            m_whiteImage.color = backColor;

            timer += Time.deltaTime;
            yield return null;
        }

        //調整
        backColor.a = 1.0f;
        m_whiteImage.color = backColor;

        //イベントで使用したイメージのα値を0に変換
        m_beforeEvent.alpha = 0.0f;
        //タイトルロゴを出現
        Color titleCol = m_titleImage.color;
        titleCol.a = 1.0f;
        m_titleImage.color = titleCol;

        //待ち時間
        timer = 0.0f;
        while (timer < mCase6_WaitTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0.0f;
        //白背景フェードアウト
        while (timer < mCase6_FadeOutTime)
        {
            //白背景のα値を1.0fから0.0fに変更
            backColor.a = 1.0f - Easing.CircIn(timer, mCase6_FadeOutTime, 0.0f, 1.0f);

            m_whiteImage.color = backColor;

            timer += Time.deltaTime;
            yield return null;
        }
        //調整
        backColor.a = 0.0f;
        m_whiteImage.color = backColor;
    }
}
