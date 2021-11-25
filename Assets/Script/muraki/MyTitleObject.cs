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
    [Header("黒背景")]
    [SerializeField]
    Image case0_blackBack = null;

    [Header("黒背景サイズ")]
    [SerializeField] float case0_BackGroudSize = 50.0f;

    [Header("白背景")]
    [SerializeField] Color case7_whiteBackColor;

    [Header("白背景透明化")]
    [SerializeField] Color case8_whiteBackColor;

    [Header("憑文字")]
    [SerializeField]
    Image case1_m_Hyou = null;

    [Header("憑文字のα値")]
    [SerializeField] float case1_Hyou_a = 1.0f;

    [Header("憑文字zoomまでの時間")]
    [SerializeField] float case1_hyouzoomtime = 10.0f;

    [Header("憑の最大の大きさ")]
    [SerializeField] float case1_hyouiMaxSize = 10.0f;

    [Header("憑のIN出現後のポジション")]
    [SerializeField] float case1_hyou_after_position_change = -200.0f;

    [Header("憑の文字移動時間")]
    [SerializeField] float case5_hyoumovetime = 2.0f;

    [Header("火の玉")]
    [SerializeField]
    Image[] case2_m_fireball = null;

    [Header("火の玉サイズ")]
    [SerializeField] static float case2_m_firesize = 0.0f;

    [Header("火の玉の透明度")]
    [SerializeField] float case2_fireball_a = 0.0f;

    [Header("火の玉の移動時間")]
    [SerializeField] float case3_fireballmovetime = 0.0f;

    [Header("火の玉の拡大時間")]
    [SerializeField] float case4_firezoom_timer = 3.0f;

    [Header("火の玉のXポジション")]
    [SerializeField] float case4_fireball_position_x = -350.0f;

    [Header("火の玉の移動後Xポジション")]
    [SerializeField] float case4_fireball_change_position_x = -100.0f;

    [Header("火の玉のYポジション")]
    [SerializeField] float case4_fireball_position_y = -200.0f;

    [Header("火の玉の移動Yポジション")]
    [SerializeField] float case4_fireball_change_position_y = -100.0f;

    [Header("火の玉を透明にするまでの時間")]
    [SerializeField] float case5_fireball_a_color = 1.0f;

    [Header("憑の最大の大きさ")]
    [SerializeField] float case5_fireMaxSize = 10.0f;

    [Header("INの文字")]
    [SerializeField]
    Image case5_m_IN = null;

    [Header("INの透明度")]
    [SerializeField] float case5_IN_a = 1.0f;

    [Header("IN.xの大きさ")]
    [SerializeField] float case5_IN_x = 4.0f;

    [Header("IN.yの大きさ")]
    [SerializeField] float case5_IN_y = 3.0f;

    [Header("INの拡大時間")]
    [SerializeField] float case6_IN_zoom_timer = 3.0f;

    [Header("INの拡大スピード")]
    [SerializeField] float case6_IN_zoom_speed = 1.0f;

    [Header("INの拡大スピード")]
    [SerializeField] float case6_IN_after_position_x = 200.0f;

    [Header("INの拡大スピード")]
    [SerializeField] float case6_IN_after_position_y = -50.0f;

    [Header("白色フェイドアウト")]
    [SerializeField] float case7_whiteouttime = 3.0f;

    [Header("白色フェイドアウトα値のR")]
    [SerializeField] float case7_whiteout_a_r = 1.0f;

    [Header("白色フェイドアウトα値のG")]
    [SerializeField] float case7_whiteout_a_g = 1.0f;

    [Header("白色フェイドアウトα値のB")]
    [SerializeField] float case7_whiteout_a_b = 1.0f;

    [Header("白色フェイドアウトα値のa")]
    [SerializeField] float case7_whiteout_a_a = 1.0f;

    [Header("タイトルロゴ")]
    [SerializeField]
    Image Titleimage= null;

    [Header("タイトルロゴup時間")]
    [SerializeField] float case8_titleuptime = 4.0f;

    [Header("タイトルロゴupアルファ値0")]
    [SerializeField] float case8_titleup_a_0 = 0.0f;

    [Header("タイトルロゴupアルファ値1")]
    [SerializeField] float case8_titleup_a_1 = 1.0f;

    [SerializeField] Color case8_TitleimageColor;

    [Header("白色フェイドアウトα値のR")]
    [SerializeField] float case8_whiteout_a_r = 1.0f;

    [Header("白色フェイドアウトα値のG")]
    [SerializeField] float case8_whiteout_a_g = 1.0f;

    [Header("白色フェイドアウトα値のB")]
    [SerializeField] float case8_whiteout_a_b = 1.0f;

    [Header("白色フェイドアウトα値のa")]
    [SerializeField] float case8_whiteout_a_a = 0.0f;

    [Header("白背景を透過するまでの時間")]
    [SerializeField] float case8_white_down_time = 2.0f;

    [Header("白背景を透過するまでの遅延時間")]
    [SerializeField] float case8_white_down_wait_time = 1.0f;

    /// <summary>
    /// プレイヤーの状態関係
    /// </summary>
    public enum MyTitleObjectState
    {
        tMOVE,
        tACTION,
        tDEAD,
        tNONE    //通常は使わない
    }
    //現在の状態
    public MyTitleObjectState m_tState { get; private set; }

    //プレイヤーの状態マシーン
    private IStateSpace.StateMachineBase<MyTitleObjectState, MyTitleObject> m_tmachine;

    public override void MyFastestInit()
    {
        //状態生成
        m_tmachine = new IStateSpace.StateMachineBase<MyTitleObjectState, MyTitleObject>();
        //状態の追加
        m_tmachine.AddState(MyTitleObjectState.tMOVE, new tAnimationState(), this);

        //初期状態に変更
        m_tmachine.InitState(MyTitleObjectState.tMOVE);
    }
    /// <summary>
    /// 更新処理
    /// </summary>
    public override void MyUpdate()
    {
        m_tmachine.UpdateState();
    }
    //個々の呼び出しはこれ以上増やさない
    public void CallTest()
    {
        StartCoroutine(TitleScene());
    }
    //1つめの処理
    private IEnumerator TitleScene()
    {
        //憑依文字の初期化
        RectTransform Hyou = case1_m_Hyou.rectTransform;

        Vector3 zoom;

        zoom = Hyou.localScale;

        zoom.x = 0;
        zoom.y = 0;

        //火の玉の初期化
        RectTransform fireRect = case2_m_fireball[0].rectTransform;
        Vector3 firesize;
        firesize = fireRect.localScale;
        firesize.x = case2_m_firesize;
        firesize.y = case2_m_firesize;

        foreach (var fire in case2_m_fireball)
        {
            fire.color = new Color(1.0f, 1.0f, 1.0f, case2_fireball_a);
        }
        Titleimage.color = new Color(1.0f, 1.0f, 1.0f, case8_titleup_a_0);
        case5_m_IN.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        yield return StartCoroutine(BackGround_BL());

        float timer = 0.0f;
        //５秒待って処理を開始
        while (timer < 1.0f)
        {
            timer += Time.deltaTime;
            //1フレーム遅らせる
            yield return null;
        }

        yield return StartCoroutine(Hyou_up());

        yield return StartCoroutine(Fire_up());
        firesize.x = case2_m_firesize;
        firesize.y = case2_m_firesize;
        yield return StartCoroutine(Fire_Move());
        yield return StartCoroutine(Fire_ZomUp());

        while (timer < 1.0f)
        {
            timer += Time.deltaTime;
            //1フレーム遅らせる
            yield return null;
        }
        yield return StartCoroutine(Firecolor());
        yield return StartCoroutine(IN_up());
        yield return StartCoroutine(WhiteOut());
        yield return StartCoroutine(Title_up());

        yield return null;
    }
    private IEnumerator BackGround_BL()
    {
        Vector3 BlackBack;

        RectTransform backRect = case0_blackBack.rectTransform;

        BlackBack.x = case0_BackGroudSize;

        BlackBack.y = case0_BackGroudSize;

        case0_blackBack.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

        yield return null;
    }
    private IEnumerator Hyou_up()
    {
        float zoomtimer = 0.0f;

        RectTransform Hyou = case1_m_Hyou.rectTransform;

        Vector3 zoom = Vector2.zero;

        while (zoomtimer < case1_hyouzoomtime)
        {
            zoom = Hyou.localScale;

            zoom.x = zoom.x + 1;

            zoom.y = zoom.y + 1;

            zoom.x = Easing.QuartOut(zoomtimer, 5.0f, 0.0f, case1_hyouiMaxSize);
            zoom.y = Easing.QuartOut(zoomtimer, 5.0f, 0.0f, case1_hyouiMaxSize);

            Hyou.localScale = zoom;

            zoomtimer += Time.deltaTime;

            yield return null;
        }
        zoom = new Vector2(case1_hyouiMaxSize, case1_hyouiMaxSize);
        Hyou.localScale = zoom;
    }
    private IEnumerator Fire_up()
    {
        float firetimer = 0.0f;
        RectTransform fireRect = case2_m_fireball[0].rectTransform;

        while (firetimer < 2.0f)
        {
            case2_fireball_a = Easing.QuadOut(firetimer, 2.0f, 0.0f, 1.0f);

            foreach (var fire in case2_m_fireball)
            {
                fire.color = new Color(1.0f, 1.0f, 1.0f, case2_fireball_a);
            }
            firetimer += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator Fire_Move()
    {
        float firemovetimer = 0.0f;
        RectTransform[] fireRect = new RectTransform[2];
        fireRect[0] = case2_m_fireball[0].rectTransform;
        fireRect[1] = case2_m_fireball[1].rectTransform;

        Vector2 fireposition_a = Vector3.one;
        Vector2 fireposition_b = Vector3.one;
        Vector3 firesize_a = Vector2.one;
        Vector3 firesize_b = Vector2.one;

        while (firemovetimer < case3_fireballmovetime)
        {
            fireposition_a.x = -Easing.QuadOut(firemovetimer, case3_fireballmovetime, case4_fireball_position_x, case4_fireball_change_position_x);
            fireposition_a.y = -Easing.QuadOut(firemovetimer, case3_fireballmovetime, case4_fireball_position_y, case4_fireball_change_position_y);
            fireRect[0].anchoredPosition = fireposition_a;

            fireposition_b.x = Easing.QuadOut(firemovetimer, case3_fireballmovetime, case4_fireball_position_x, case4_fireball_change_position_x);
            fireposition_b.y = Easing.QuadOut(firemovetimer, case3_fireballmovetime, case4_fireball_position_y, case4_fireball_change_position_y);
            fireRect[1].anchoredPosition = fireposition_b;

            firemovetimer += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator Fire_ZomUp()
    {
        float firezoomtimer = 0.0f;
        RectTransform[] firezoomRect = new RectTransform[2];
        firezoomRect[0] = case2_m_fireball[0].rectTransform;
        firezoomRect[1] = case2_m_fireball[1].rectTransform;

        Vector3 firezoom_a;
        Vector3 firezoom_b;

        while (firezoomtimer < case4_firezoom_timer)
        {
            firezoom_a = firezoomRect[0].localScale;

            firezoom_a.x = Easing.QuartOut(firezoomtimer, case4_firezoom_timer, 0.0f, case5_fireMaxSize);
            firezoom_a.y = Easing.QuartOut(firezoomtimer, case4_firezoom_timer, 0.0f, case5_fireMaxSize);

            firezoomRect[0].localScale = firezoom_a;

            firezoom_b = firezoomRect[1].localScale;

            firezoom_b.x = Easing.QuartOut(firezoomtimer, case4_firezoom_timer, 0.0f, case5_fireMaxSize);
            firezoom_b.y = Easing.QuartOut(firezoomtimer, case4_firezoom_timer, 0.0f, case5_fireMaxSize);

            firezoomRect[1].localScale = firezoom_b;

            firezoomtimer += Time.deltaTime;

            yield return null;
        }
        yield return null;
    }
    private IEnumerator Firecolor()
    {
        float colortimer = 0.0f;
        while (colortimer < case5_fireball_a_color)
        {
            foreach (var fire in case2_m_fireball)
            {
                case2_fireball_a = Easing.QuadOut(colortimer, case5_fireball_a_color, 1.0f, 0.0f);
                fire.color = new Color(1.0f, 1.0f, 1.0f, case2_fireball_a);
                colortimer += Time.deltaTime;
            }
            yield return null;
        }
    }
    private IEnumerator IN_up()
    {
        float zoomtimer = 0.0f;
        RectTransform INRect = case5_m_IN.rectTransform;
        bool timeflag = true;
        bool flag = true;

        Vector2 INmove;
        Vector3 INzoom;

        while (zoomtimer < case6_IN_zoom_timer)
        {

            case5_IN_a = Easing.QuadOut(zoomtimer, 3.0f, 0.0f, 1.0f);
            case5_m_IN.color = new Color(1.0f, 1.0f, 1.0f, case5_IN_a);

            INzoom = INRect.localScale;

            INzoom.x = INzoom.x + case6_IN_zoom_speed;

            INzoom.y = INzoom.y + case6_IN_zoom_speed;

            INzoom.x = Easing.QuartOut(zoomtimer, case6_IN_zoom_timer, 2.0f, case5_IN_x);
            INzoom.y = Easing.QuartOut(zoomtimer, case6_IN_zoom_timer, 1.0f, case5_IN_y);

            INRect.localScale = INzoom;

            INmove.x = Easing.QuadOut(zoomtimer, case6_IN_zoom_timer, 0, case6_IN_after_position_x);
            INmove.y = Easing.QuadOut(zoomtimer, case6_IN_zoom_timer, 0, case6_IN_after_position_y);
            INRect.anchoredPosition = INmove;

            zoomtimer += Time.deltaTime;

            float m_timer = 0.0f;

            if (timeflag == true)
            {
                while (m_timer < 1.5f)
                {
                    m_timer += Time.deltaTime;
                    //1フレーム遅らせる
                    yield return null;
                }
                timeflag = false;
            }
            if (flag == true)
            {
                StartCoroutine(Hyou_Move());
                flag = false;
            }
            yield return null;
        }
    }
    private IEnumerator Hyou_Move()
    {
        float zoomtimer_a = 0.0f;

        RectTransform HyouMoveRect = case1_m_Hyou.rectTransform;

        Vector2 hyoumove;

        while (zoomtimer_a < case5_hyoumovetime)
        {
            hyoumove.x = Easing.QuadOut(zoomtimer_a, 2, 0, case1_hyou_after_position_change);
            hyoumove.y = Easing.QuadOut(zoomtimer_a, 2, 0, 0.0f);
            HyouMoveRect.anchoredPosition = hyoumove;

            zoomtimer_a += Time.deltaTime;

            yield return null;
        }
    }
    private IEnumerator WhiteOut()
    {
        float whiteouttimer = 0.0f;
        while (whiteouttimer < case7_whiteouttime)
        {
            //憑の文字を透過\
            case1_Hyou_a = Easing.QuadOut(whiteouttimer, case7_whiteouttime, 1.0f, 0.0f);
            case1_m_Hyou.color = new Color(1.0f, 1.0f, 1.0f, case1_Hyou_a);
            //INの文字を透過
            case5_IN_a = Easing.QuadOut(whiteouttimer, case7_whiteouttime, 1.0f, 0.0f);
            case5_m_IN.color = new Color(1.0f, 1.0f, 1.0f, case5_IN_a);
            //背景を白にフェードアウトする
            case7_whiteBackColor.r = Easing.QuadOut(whiteouttimer, case7_whiteouttime, 0.0f, case7_whiteout_a_r);
            case7_whiteBackColor.g = Easing.QuadOut(whiteouttimer, case7_whiteouttime, 0.0f, case7_whiteout_a_g);
            case7_whiteBackColor.b = Easing.QuadOut(whiteouttimer, case7_whiteouttime, 0.0f, case7_whiteout_a_b);
            case0_blackBack.color = new Color(case7_whiteBackColor.r, case7_whiteBackColor.g, case7_whiteBackColor.b, case7_whiteout_a_a);

            whiteouttimer += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator Title_up()
    {
        float title_up_time = 0.0f;
        float white_down_time = 0.0f;
        bool w_timeflag = true;
        bool w_flag = true;

        RectTransform TitleUpRect = Titleimage.rectTransform;
        while (title_up_time < case8_titleuptime)
        {
            case8_TitleimageColor.r = Easing.QuadOut(title_up_time, 1.0f, case8_titleup_a_0, case8_titleup_a_1);
            Titleimage.color = new Color(1.0f, 1.0f, 1.0f, case8_titleup_a_1);

            title_up_time += Time.deltaTime;

            if (w_timeflag == true)
            {
                while (white_down_time < case8_white_down_wait_time)
                {
                    white_down_time += Time.deltaTime;
                    //1フレーム遅らせる
                    yield return null;
                }
                w_timeflag = false;
            }
            if (w_flag == true)
            {
                StartCoroutine(whitedown());
                w_flag = false;
            }

            yield return null;
        }
    }
    private IEnumerator whitedown()
    {
        float twhite_down_time = 0.0f;
        float maxAlpha = 1.0f;
        while (twhite_down_time < case8_white_down_time)
        {
            case8_whiteBackColor.a = maxAlpha - Easing.QuadOut(twhite_down_time, case8_white_down_time, case8_whiteout_a_a, maxAlpha);
            case0_blackBack.color = case8_whiteBackColor;

            //Debug.Log("カラー:" + case8_whiteBackColor.a);
            twhite_down_time += Time.deltaTime;
            yield return null;

        }

    }
}
