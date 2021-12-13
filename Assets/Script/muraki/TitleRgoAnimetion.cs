using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// プレイヤーが操作する対象
/// </summary>
public class TitleRgoAnimetion : MyUpdater
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

    [Header("火の玉エフェクト")]
    [SerializeField]
    Image m_fireEffect = null;

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
    //Case1 「憑」の文字を出現させる
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
    [SerializeField]
    Vector2 mCase1_HyoMaxSize;

    //----------------------------------------------------------------------------
    //Case2 火の玉の出現(α値を1に変更)
    //----------------------------------------------------------------------------
    [Header("Case1からCase2までの移行時間")]
    [Range(0.0f, 3.0f)]
    [SerializeField]
    float m_Case1ToCase2Time = 1.0f;

    [Header("火の玉の出現時間")]
    [Range(0.0f, 5.0f)]
    [SerializeField]
    float mCase2_FireAppearTime = 1.0f;

    [Header("火の玉のサイズ変更がα値変更より何分の1の速度になるか")]
    [Range(1.0f, 10.0f)]
    [SerializeField]
    float mCase2_FireSizeFactor = 2.5f;
    //----------------------------------------------------------------------------
    //Case3 火の玉移動
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

    [Header("火の玉の遅延時間")]
    [Range(0.0f, 4.9f)]
    [SerializeField]
    float mCase3_DelayStart = 0.15f;
    //----------------------------------------------------------------------------
    //Case4  火の玉消失
    //----------------------------------------------------------------------------
    [Header("Case3からCase4までの移行時間")]
    [Range(0.0f, 3.0f)]
    [SerializeField]
    float m_Case3ToCase4Time = 1.0f;

    [Header("火の玉を何週させるか")]
    [Range(0, 5)]
    [SerializeField]
    int mCase4_FireRotateNum = 3;

    [Header("火の玉一周の基本回転時間")]
    [Range(0.0f, 5.0f)]
    [SerializeField]
    float mCase4_FireRotateTime = 1.0f;

    [Header("一周ごとにどれだけ早くなるか")]
    [Range(0.0f, 1.0f)]
    [SerializeField]
    float mCase4_FireRotateTimeDecrease = 0.1f;

    [Header("火の玉が縮小開始する周回数")]
    [Range(0, 3)]
    [SerializeField]
    int mCase4_FireStartSamallRotateNum = 3;

    [Header("火の玉と中心点との距離の減少")]
    [Range(0.0f, 0.9f)]
    [SerializeField]
    float mCase4_FireDecreaseDis = 0.1f;

    [Header("火の玉の最小α値")]
    [Range(0.0f, 0.9f)]
    [SerializeField]
    float mCase4_FireMinAlpha = 0.3f;

    [Header("火の玉の最小サイズ")]
    [Range(0.0f, 0.9f)]
    [SerializeField]
    float mCase4_FireMinSize = 0.2f;

    [Header("エフェクト時間")]
    [Range(0.0f, 3.0f)]
    [SerializeField]
    float mCase4_FireEffectTime = 2.0f;

    [Header("エフェクトの最大サイズ")]
    [Range(0.0f, 8.0f)]
    [SerializeField]
    float mCasr4_EffectMaxSize = 6.0f;

    [Header("エフェクトのα値減少開始時間")]
    [Range(0.0f, 1.0f)]
    [SerializeField]
    float mCase4_EffectAlphaTime = 0.5f;

    [Header("火の玉縮小してからの停止時間")]
    [SerializeField]
    float mCase4_StopTime = 0.35f;

    [Header("エフェクトのα値減少時間")]
    [SerializeField]
    float mCase4_EffectAlphaDeTime = 0.5f;
    //----------------------------------------------------------------------------
    //Case5  In出現
    //----------------------------------------------------------------------------

    [Header("Case4からCase5までの移行時間")]
    [Range(0.0f, 3.0f)]
    [SerializeField]
    float m_Case4ToCase5Time = 1.0f;

    [Header("憑の振動回数")]
    [Range(10,50)]
    [SerializeField]
    int mCase5_ShakeNum = 20;

    [Header("憑依の最小振動時間")]
    [Range(0.0f,0.5f)]
    [SerializeField]
    float mCase5_ShakeMinTime = 0.1f;

    [Header("憑依の最大振動時間")]
    [Range(0.6f, 1.5f)]
    [SerializeField]
    float mCase5_ShakeMaxTime = 1.0f;

    [Header("憑依の最小振動距離")]
    [Range(0.0f, 20.0f)]
    [SerializeField]
    float mCase5_ShakeMinDis = 10.0f;

    [Header("憑依の最大振動距離")]
    [Range(10.0f, 100.0f)]
    [SerializeField]
    float mCase5_ShakeMaxDis = 50.0f;

    [Header("Inの最大サイズ")]
    [SerializeField]
    private Vector2 mCase5_InMaxSize = new Vector2(4, 6);
    [Header("Inの最小サイズ")]
    [SerializeField]
    private Vector2 mCase5_InMinSize = new Vector2(4 / 3, 6 / 3);
    [Header("Inの射出角度")]
    [SerializeField]
    public float mCase5_InDegree = 120.0f;
    [Header("Inの初速")]
    [SerializeField]
    public float mCase5_InV0 = 45.0f;
    [Header("Inの重力")]
    [SerializeField]
    public float mCase5_InGravity = 10.0f;
    [Header("InのX移動量")]
    [SerializeField]
    public float mCase5_InXMovePower = 0.1f;
    [Header("Inのバウンド回数")]
    [SerializeField]
    public int mCase5_InBoundNum = 3;
    [Header("Inのバウンド1フレーム進行時間")]
    [SerializeField]
    public float mCse5_InFrameTime = 0.1f;
    [Header("Inのサイズ変更時間")]
    [SerializeField]
    public float mCase5_InSizeTime = 1.0f;
    [Header("Inの憑依をずらす量")]
    [SerializeField]
    public float mCase5_HyoMoveX = 10.0f;

    //----------------------------------------------------------------------------
    //Case6 背景フェードインアウト
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

 
    // イベント終了通知フラグ
    public bool isEvent { get; private set; }

    public override void MyFastestInit()
    {
        isEvent = false;
        ///背景等初期化
        Case0();
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

        //テスト
        ToClearImage(ref m_fireEffect);

        void ToClearImage(ref Image _image)
        {
            //色情報取得
            clearness = _image.color;
            //α値を0にして反映
            clearness.a = 0.0f;
            _image.color = clearness;
        }
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

        ////終了処理
        yield return StartCoroutine(Case6());

        ////イベント終了通知
        //isEvent = true;
    }
    /// <summary>
    /// 「憑」の文字を出現させる
    /// </summary>
    private IEnumerator Case1()
    {
        float timer = 0.0f;
        RectTransform Hyou = m_hyoImage.rectTransform;
        Vector2 size = Vector2.zero;

        //時間オーバーを禁止する
        if (mCase1_HyoBigTime < mCase1_HyoAlphaTime) mCase1_HyoAlphaTime = mCase1_HyoBigTime;
        //憑のα値を徐々に1にする
        StartCoroutine(Case1_Alpha());

        while (timer < mCase1_HyoBigTime)
        {
            //大きさ変更
            size.x = Easing.SineOut(timer, mCase1_HyoBigTime, 0.0f, mCase1_HyoMaxSize.x);
            size.y = Easing.SineOut(timer, mCase1_HyoBigTime, 0.0f, mCase1_HyoMaxSize.y);
            Hyou.localScale = new Vector3(size.x, size.y, 1.0f);

            timer += Time.deltaTime;

            yield return null;
        }
        Hyou.localScale = new Vector3(mCase1_HyoMaxSize.x, mCase1_HyoMaxSize.y, 1.0f);
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
            hyoColor.a = Easing.SineOut(timer, mCase1_HyoAlphaTime, 0.0f, 1.0f);
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
        //大きさ変更必要だな、これ

        float timer = 0.0f;
        Color fireColor = m_fireballs[0].color;

        //左右それぞれの大きさ取得
        RectTransform[] fireRects = new RectTransform[] { m_fireballs[0].rectTransform, m_fireballs[1].rectTransform };
        Vector3[] endSize = new Vector3[] { fireRects[0].localScale, fireRects[1].localScale };

        //初期サイズ
        Vector2 firstSize = Vector2.zero;
        Vector2[] currentSize = new Vector2[] { Vector2.zero, Vector2.zero };

        //一定時間待つ
        while(timer<m_Case1ToCase2Time)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        float endFireSize = mCase2_FireAppearTime / mCase2_FireSizeFactor;

        timer = 0.0f;
        while (timer < mCase2_FireAppearTime)
        {
            //α値の変更
            fireColor.a = Easing.CircOut(timer, mCase2_FireAppearTime, 0.0f, 1.0f);
            //fireColor.a = Easing.CubicIn(timer, 1.0f, 0.0f, 0.5f);

            for (int idx = 0; idx < m_fireballs.Length; idx++)
            {
                //α値変更
                m_fireballs[idx].color = fireColor;

                if(timer< endFireSize)
                {
                    //大きさ変更
                    currentSize[idx].x = Easing.ExpOut(timer, endFireSize, firstSize.x, endSize[idx].x);
                    currentSize[idx].y = Easing.ExpOut(timer, endFireSize, firstSize.y, endSize[idx].y);

                    fireRects[idx].localScale = new Vector3(currentSize[idx].x, currentSize[idx].y, 1.0f);
                }
               
            }
            
            timer += Time.deltaTime;
            yield return null;
        }
        //α値を1に変更
        fireColor.a = 1.0f;
        //最終調整
        for (int idx = 0; idx < m_fireballs.Length; idx++)
        {
            m_fireballs[idx].color = fireColor;
            fireRects[idx].localScale = endSize[idx];
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
                //スタート開始をずらす
                if (idx == 0)
                {
                    if (firemovetimer < mCase3_DelayStart) continue;
                }

                float cTimer = (idx == 0) ? firemovetimer - mCase3_DelayStart : firemovetimer;
                float endTimer = (idx == 0) ? mCase3_FireMoveTime - mCase3_DelayStart : mCase3_FireMoveTime;

                //X移動
                if (firstPoses[idx].x > mCase3_Goal[idx].anchoredPosition.x)
                    movePos.x = -Easing.BackIn(cTimer,  endTimer, -firstPoses[idx].x, -mCase3_Goal[idx].anchoredPosition.x, mCase3_FireMoveRange);
                else
                    movePos.x =  Easing.BackIn(cTimer,  endTimer, firstPoses[idx].x, mCase3_Goal[idx].anchoredPosition.x, mCase3_FireMoveRange);

                //Y移動
                if (firstPoses[idx].y > mCase3_Goal[idx].anchoredPosition.y)
                    movePos.y = -Easing.BackIn(cTimer, endTimer, -firstPoses[idx].y, -mCase3_Goal[idx].anchoredPosition.y, mCase3_FireMoveRange);
                else
                    movePos.y =  Easing.BackIn(cTimer, endTimer, firstPoses[idx].y, mCase3_Goal[idx].anchoredPosition.y, mCase3_FireMoveRange);

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
    /// 
    private IEnumerator Case4()
    {
        //ToDo：リスト    2：少し大きくなって、サイズ減少＋透明化  3：エフェクト開始
        //1：通常回転
        //2：早くなる回転
        //3：止まって、大きくなる
        //4：中心に向かって、小さくなる
        //5：エフェクト開始

        //一定時間停止
        float timer = 0.0f;
        while (timer < m_Case3ToCase4Time)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        //キャッシュして保持
        // {} あなたは集大成初期化です（名前わかりづらいよ）でも便利だねありがとう
        RectTransform[] fireRects = new RectTransform[] { m_fireballs[0].rectTransform, m_fireballs[1].rectTransform };
        Vector2 firePos = Vector2.zero;

        //憑の中心点を取得
        GameObject hyoCenter = m_hyoImage.transform.GetChild(0).gameObject;
        Vector2 centerPos = hyoCenter.GetComponent<RectTransform>().anchoredPosition;

        //必要な度数を求める
        float[] beginDegs = new float[] { GetDegree(fireRects[0]), GetDegree(fireRects[1]) };
        float[] endDegs = new float[] { beginDegs[0] - mCase4_FireRotateNum * 360.0f, beginDegs[1] - mCase4_FireRotateNum * 360.0f };

        //中心点との距離を求める
        float[] beginRads = new float[] { (centerPos - fireRects[0].anchoredPosition).magnitude, (centerPos - fireRects[1].anchoredPosition).magnitude };
        float[] endRads = new float[] { beginRads[0] * mCase4_FireDecreaseDis, beginRads[1] * mCase4_FireDecreaseDis };

        //終了時間
        float endTime = 0.0f;
        for (int idx = 0; idx < mCase4_FireRotateNum; idx++)
        {
            endTime += mCase4_FireRotateTime - mCase4_FireRotateTimeDecrease * idx;
        }

        //縮小開始回数の上限を設定
        mCase4_FireStartSamallRotateNum = Mathf.Clamp(mCase4_FireStartSamallRotateNum, 0, mCase4_FireRotateNum);
        //縮小開始時間を求める
        float startSmallTime = 0.0f;
        for (int idx = 0; idx < mCase4_FireStartSamallRotateNum; idx++)
        {
            startSmallTime += mCase4_FireRotateTime - mCase4_FireRotateTimeDecrease * idx;
        }

        //初期サイズ
        Vector2[] beginSize = new Vector2[] { fireRects[0].localScale, fireRects[1].localScale };
        Vector2[] endSize = new Vector2[] { beginSize[0] * mCase4_FireMinSize, beginSize[1] * mCase4_FireMinSize };

        //回転及び縮小
        float deg = 0.0f;
        float radian = 0.0f;
        float radious = 0.0f;
        float alpha = 0.0f;
        Vector2 size = Vector2.zero;
        timer = 0.0f;
        while (timer < endTime)
        {
            for (int idx = 0; idx < m_fireballs.Length; idx++)
            {
                //角度を求める（180.0度ずらす）
                deg = -Easing.CubicIn(timer, endTime, -beginDegs[idx], -endDegs[idx]) + 180.0f;
                radian = deg * Mathf.Deg2Rad;

                //半径を求める
                radious = beginRads[idx];

                if (timer > startSmallTime)
                {
                    //半径減少
                    radious = -Easing.CircOut(timer - startSmallTime, endTime - startSmallTime, -beginRads[idx], -endRads[idx]);
                    //サイズ減少
                    size.x = -Easing.QuadIn(timer - startSmallTime, endTime - startSmallTime, -beginSize[idx].x, -endSize[idx].x);
                    size.y = -Easing.QuadIn(timer - startSmallTime, endTime - startSmallTime, -beginSize[idx].y, -endSize[idx].y);
                    fireRects[idx].localScale = new Vector3(size.x, size.y, 1.0f);

                    alpha = -Easing.CubicOut(timer - startSmallTime, endTime - startSmallTime, -1.0f, -mCase4_FireMinAlpha);
                    m_fireballs[idx].color = new Color(1, 1, 1, alpha);
                }

                //円周上の移動を計算
                firePos = new Vector2(centerPos.x +radious * Mathf.Cos(radian), centerPos.y + radious * Mathf.Sin(radian));
                fireRects[idx].anchoredPosition = firePos;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0.0f;
        while (timer < mCase4_StopTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        //完全に火の玉消す
        foreach (var fire in m_fireballs)
        {
            fire.color = new Color(1, 1, 1, 0.0f);
        }

        //エフェクトを中心に持ってくる
        RectTransform effectRect = m_fireEffect.rectTransform;
        effectRect.anchoredPosition = centerPos;
        //エフェクトの最大サイズ
        Vector2 effectSize = new Vector2(1.0f * mCasr4_EffectMaxSize, 1.2f * mCasr4_EffectMaxSize);
        //エフェクト出現
        Color effectCol = m_fireEffect.color;
        effectCol.a = 1.0f;
        m_fireEffect.color = effectCol;

        if (mCase4_EffectAlphaTime > mCase4_FireEffectTime) mCase4_EffectAlphaTime = mCase4_FireEffectTime;

        bool isCall = false;

        timer = 0.0f;
        while (timer < mCase4_FireEffectTime)
        {
            //サイズ変更
            size.x = Easing.QuartOut(timer, mCase4_FireEffectTime, 0.0f, effectSize.x);
            size.y = Easing.QuartOut(timer, mCase4_FireEffectTime, 0.0f, effectSize.y);
            effectRect.localScale = new Vector3(size.x, size.y, 1.0f);

            if (!isCall)
            {
                if (timer > mCase4_EffectAlphaTime)
                {
                    StartCoroutine(AlphaDecrease());
                    isCall = true;
                }
            }

            timer += Time.deltaTime;
            yield return null;
        }

        //Fireと憑の中心点との角度を求めるn
        float GetDegree(RectTransform _fire)
        {
            Vector2 d = centerPos - _fire.anchoredPosition;
            float rad = Mathf.Atan2(d.y, d.x);
            return rad * Mathf.Rad2Deg;
        }

        //エフェクトのα値減少
        IEnumerator AlphaDecrease()
        {
            float aTime = 0.0f;

            while (aTime < mCase4_EffectAlphaDeTime)
            {
                effectCol.a = -Easing.CircOut(aTime, mCase4_EffectAlphaDeTime, -1.0f, 0.0f);
                m_fireEffect.color = effectCol;

                aTime += Time.deltaTime;
                yield return null;
            }
        }

    }

 
    /// <summary>
    /// In出現
    /// </summary>
    private IEnumerator Case5()
    {
        float timer = 0.0f;

        //一定時間待つ
        while (timer < m_Case4ToCase5Time)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        //憑の文字を震わせる

        //位置情報を取得する
        RectTransform hyoRect = m_hyoImage.rectTransform;

        //憑位置
        Vector2 hyoPos = hyoRect.anchoredPosition;
        float hyoOriginX = hyoPos.x;

        m_hyoImage.color = Color.white;

        //振動の終了フラグ
        bool isShake = false;

        timer = 0.0f;
        float oneTime = mCase5_ShakeMaxTime;
        float shakeDis = mCase5_ShakeMaxDis;
        //揺らす
        for (int num = 0; num < mCase5_ShakeNum;
            num++,
            oneTime = -Easing.QuintOut(num, mCase5_ShakeNum - 1, -mCase5_ShakeMaxTime, -mCase5_ShakeMinTime),
            shakeDis = -Easing.QuintOut(num, mCase5_ShakeNum - 1, -mCase5_ShakeMaxDis, -mCase5_ShakeMinDis),
            isShake = false)
        {
            //振動開始
            StartCoroutine(Shaker());

            //振動終了まで待つ
            while (!isShake) yield return null;
        }

        //位置調整
        hyoPos.x = hyoOriginX;
        hyoRect.anchoredPosition = hyoPos;

        IEnumerator Shaker()
        {
            timer = 0.0f;
            while (timer < oneTime)
            {
                //-1~1の範囲を取得
                float move = GetSin(timer, oneTime);

                //移動後の位置を求める
                hyoPos.x = hyoOriginX + move * shakeDis;
                //移動
                hyoRect.anchoredPosition = hyoPos;

                timer += Time.deltaTime;
                yield return null;
            }

            isShake = true;
        }

        //-1~1が変える関数
        //_cTimeが現在時間
        //_oneTimeが一周に掛かる時間
        float GetSin(float _cTime,float _oneTime)
        {
            return Mathf.Sin((Mathf.PI * 2) * (1.0f / _oneTime) * _cTime);
        }

        //Inの位置
        RectTransform inRect = m_inImage.rectTransform;
        //Inの底
        RectTransform inBottom = m_inImage.transform.GetChild(0).GetComponent<RectTransform>();

        //Inを憑の位置に移動させる
        inRect.anchoredPosition = hyoRect.anchoredPosition;
        inRect.localScale = new Vector3(mCase5_InMinSize.x, mCase5_InMinSize.y, 1.0f);

        //憑の底
        RectTransform hyoBottom = m_hyoImage.transform.GetChild(1).GetComponent<RectTransform>();
        //Inの出現
        m_inImage.color = Color.white;

        //サイズ変更が終了したかのフラグ
        bool isEndSize = false;
        //サイズ変更
        StartCoroutine(SizeChangerIn());
        //憑の移動
        StartCoroutine(MoveHyo());
        
        float testTimer = 0.0f;

        for (int boundNum = 0; boundNum < mCase5_InBoundNum; boundNum++)
        {
            timer = 0.0f;

            //憑の底より高くするため実行
            inRect.anchoredPosition += Oblique(timer);
            //憑の底まで下がるまで実行
            while (hyoBottom.position.y < inBottom.position.y)
            {
                //放物線移動
                inRect.anchoredPosition += Oblique(timer);

                testTimer += Time.deltaTime;
                timer += mCse5_InFrameTime;
                yield return null;
            }

            //高さを揃える
            float offset = Mathf.Abs(hyoBottom.position.y - inBottom.position.y);
            inRect.anchoredPosition += new Vector2(0, offset + 0.1f);
        }

#if UNITY_EDITOR
        Debug.Log("テスト：" + testTimer);
#endif

        //サイズ変更終了まで待機
        while (!isEndSize)
        {
            yield return null;
        }

        //位置調整
        Vector2 inPos = inRect.anchoredPosition;
        inPos.y = hyoRect.anchoredPosition.y;
        inRect.anchoredPosition = inPos;

        //放物線の方程式
        Vector2 Oblique(float _cTime)
        { 
            Vector2 move = Vector2.zero;

            move.x = mCase5_InV0 * Mathf.Cos(mCase5_InDegree * Mathf.Deg2Rad) * _cTime * mCase5_InXMovePower;
            move.y = mCase5_InV0 * Mathf.Sin(mCase5_InDegree * Mathf.Deg2Rad) - 0.5f * mCase5_InGravity * _cTime * _cTime;

            return move;
        }

        //Inのサイズ変更
        IEnumerator SizeChangerIn()
        {
            float inTime = 0.0f;
            Vector2 size = Vector2.zero;

            while(inTime< mCase5_InSizeTime)
            {
                size.x = Easing.CubicOut(inTime, mCase5_InSizeTime, mCase5_InMinSize.x, mCase5_InMaxSize.x);
                size.y = Easing.CubicOut(inTime, mCase5_InSizeTime, mCase5_InMinSize.y, mCase5_InMaxSize.y);

                inRect.localScale = new Vector3(size.x, size.y, 1.0f);

                inTime += Time.deltaTime;
                yield return null;
            }

            inRect.localScale = new Vector3(mCase5_InMaxSize.x, mCase5_InMaxSize.y, 1.0f);
            isEndSize = true;
        }

        //憑の移動
        IEnumerator MoveHyo()
        {
            Vector2 hyoBegin = hyoRect.anchoredPosition;
            Vector2 hyoEnd = hyoBegin - new Vector2(mCase5_HyoMoveX, 0.0f);
            hyoPos = hyoBegin;

            float hyoTime = 0.0f;
            float hyoEndTime = mCase5_InSizeTime / 2.0f;
            
            while (hyoTime < hyoEndTime)
            {
                hyoPos.x = -Easing.CubicOut(hyoTime, hyoEndTime, -hyoBegin.x, -hyoEnd.x);
                hyoRect.anchoredPosition = hyoPos;

                hyoTime += Time.deltaTime;
                yield return null;
            }

            //調整
            hyoRect.anchoredPosition = hyoEnd;
        }
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

        Color backColor = m_whiteImage.color;
        //m_whiteImage.gameObject.SetActive(true);

        timer = 0.0f;
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
        m_titleImage.gameObject.GetComponent<CanvasGroup>().alpha = 1.0f;

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

    public override void MyUpdate()
    {
        
    }
}
