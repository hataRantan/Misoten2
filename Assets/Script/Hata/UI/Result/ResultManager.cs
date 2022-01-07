using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
public class ResultManager : MyUpdater
{
    [Header("操作対象の顔")]
    [SerializeField] RawImage[] m_players = null;

    [Header("ポストプロセス"), SerializeField]
    GameObject m_post = null;
    [Header("ポストプロファイル"), SerializeField]
    PostProcessProfile m_file = null;
    DepthOfField m_depth = null;
    PostProcessVolume m_volume = null;
    [SerializeField, Range(1, 300)] int m_maxLength = 300;
    const int m_minLength = 1;

    [SerializeField]
    TMPro.TextMeshProUGUI m_endText = null;

    [Header("終了告知のグループ"), SerializeField]
    CanvasGroup m_endGroup = null;

    [Header("プレイヤーのグループ"), SerializeField]
    CanvasGroup m_playerGroup = null;

    [Header("ゲームのUIグループ"), SerializeField]
    CanvasGroup m_uiGroup = null;
    public bool isEndRank { get; private set; }

    [SerializeField]
    private float m_to0ScaleTime = 1.0f;

    //テキスト関係-----------------------------------
    [SerializeField]
    Vector3 m_maxTextSize;
    [SerializeField]
    Vector3 m_minTextSize;
    [SerializeField]
    float m_textTime = 1.0f;
    //-----------------------------------

    //テキストから順位までの遷移関係----------------
    [SerializeField]
    float m_nextToRnakTime = 1.0f;

    [SerializeField]
    TMPro.TextMeshProUGUI[] m_rankTexts = null;
    //--------------------------------

    //順位発表の関係--------------------------------
    [SerializeField]
    float m_toAnnouncementTime = 1.0f;
    [SerializeField]
    float m_toRankIntervel = 0.5f;
    [SerializeField]
    float m_rankIntervel = 0.8f;
    [SerializeField]
    float m_intervelTime = 1.0f;
    [SerializeField]
    float m_appearTime = 0.3f;
    [SerializeField]
    float m_textY = -310.0f;

    [SerializeField]
    Vector2 m_rankTextMaxSize = new Vector2(12, 12);
    [SerializeField]
    Vector2 m_rankTextMinSize = new Vector2(8, 8);
    [SerializeField]
    float m_rankTextTime = 0.8f;
    [SerializeField]
    float m_rankTextSlope = 5.0f;
    [SerializeField]
    float m_rankTextSlopeTime = 0.35f;
    //---------------------------------

    //1位の表彰-----------------------
    [SerializeField]
    GameObject m_ConfettiObj = null;
    [SerializeField]
    Vector3 m_ConfettiSize = new Vector3(0.3f, 0.3f, 0.3f);
    [SerializeField]
    float m_ConfettiOffset = 10.0f;
    [SerializeField]
    Vector3 m_leftEuler = new Vector3(-90.0f, 45.0f, -180.0f);
    [SerializeField]
    Vector3 m_rightEuler = new Vector3(-90.0f, 135.0f, -180.0f);
    [SerializeField]
    float m_ConfettiCameraOffset = 0.5f;
    [SerializeField]
    Transform m_resultCameraPos = null;

    [SerializeField]
    float m_moveYAmount = 30.0f;
    [SerializeField]
    float m_upTime = 0.7f;
    //---------------------------------

    //キャンバス全体のα値等管理
    CanvasGroup m_group = null;
    //プレイヤーの人数
    int m_playerNum = 0;
    //画面の横幅
    const float m_canvasWidth = 1920.0f;
    float m_width = 0.0f;

    public override void MyFastestInit()
    {
        m_playerNum = GameInPlayerNumber.Instance.CurrentPlayerNum;
        m_group = GetComponent<CanvasGroup>();
        m_group.alpha = 0.0f;
        m_volume = null;
        //2人だと3分割
        //3人だと4分割
        //4人だと5分割
        m_width = m_canvasWidth / (m_playerNum + 1);

        float width = -m_canvasWidth / 2.0f;
        for (int idx = 0; idx < m_players.Length; ++idx)
        {
            width += m_width;
            if (idx<m_playerNum)
            {
                m_players[idx].gameObject.SetActive(true);
                m_players[idx].rectTransform.localPosition = new Vector3(width, 0.0f, 0.0f);
            }
            else
            {
                m_players[idx].gameObject.SetActive(false);
            }
        }

        isEndRank = false;
    }


    public void ResultStart(List<int> _rank, UnityEngine.Events.UnityAction _action)
    {
        //StopAllCoroutines();
        StartCoroutine(Result(_rank, _action));
    }

    private IEnumerator Result(List<int> _rank, UnityEngine.Events.UnityAction _action)
    {
        ProcessTimer timer = new ProcessTimer();
        timer.Restart();
        while (timer.TotalSeconds < m_to0ScaleTime)
        {
            float scale = -Easing.CubicOut(timer.TotalSeconds, m_to0ScaleTime, -1, 0);
            Time.timeScale = scale;
            yield return null;
        }
        Time.timeScale = 0.0f;
        //------------------------------------------------------------------
        // テキスト出現
        //------------------------------------------------------------------
        m_group.alpha = 1.0f;
        m_endGroup.alpha = 1.0f;
        m_playerGroup.alpha = 0.0f;

        MyAudioManeger.Instance.PlaySE("GameEnd");
        
        timer.Restart();
        Vector3 textSize = m_maxTextSize;
        //テキストサイズ変更
        while (timer.TotalSeconds < m_textTime)
        {
            textSize.x = -Easing.QuartOut(timer.TotalSeconds, m_textTime, -m_maxTextSize.x, -m_minTextSize.x);
            textSize.y = -Easing.QuartOut(timer.TotalSeconds, m_textTime, -m_maxTextSize.y, -m_minTextSize.y);
            m_endText.rectTransform.localScale = textSize;

            yield return null;
        }
        m_endText.rectTransform.localScale = m_minTextSize;

       
        //他のUpdaterを停止
        _action();

        //ポストプロセス取得
        m_depth = m_file.GetSetting<DepthOfField>();
        //m_depth = ScriptableObject.CreateInstance<DepthOfField>();
        m_depth.active = true;
        //書き換えOKに変更
        m_depth.enabled.Override(true);
        //値書き換え
        m_depth.focalLength.Override(m_minLength);
        //値変更を通達
        //PostProcessManager.instance.QuickVolume(m_post.layer, 1, m_depth);

        timer.Restart();
        while (timer.TotalSeconds < m_toRankIntervel)
        {
            yield return null;
        }
        MyAudioManeger.Instance.PlayBGMSpot("ResultBGM");
        //--------------------------------------------------------------------
        // 順位発表へと遷移
        //--------------------------------------------------------------------
        float alpha = 0.0f;
        float length = 0.0f;
        m_uiGroup.alpha = 0.0f;
        timer.Restart();
        while (timer.TotalSeconds < m_nextToRnakTime)
        {
            alpha = -Easing.QuartIn(timer.TotalSeconds, m_nextToRnakTime, -1.0f, 0.0f);
            m_endGroup.alpha = alpha;

            alpha = Easing.QuartOut(timer.TotalSeconds, m_nextToRnakTime, 0.0f, 1.0f);
            m_playerGroup.alpha = alpha;

            length = Easing.QuartOut(timer.TotalSeconds, m_nextToRnakTime, m_minLength, m_maxLength);
            m_depth.focalLength.Override(length);
            //PostProcessManager.instance.QuickVolume(m_post.layer, 1, m_depth);

            yield return null;
        }
        m_endGroup.alpha = 0.0f;
        m_playerGroup.alpha = 1.0f;
        m_depth.focalLength.Override(m_maxLength);
        //m_volume = PostProcessManager.instance.QuickVolume(m_post.layer, 1, m_depth);

        timer.Restart();
        while (timer.TotalSeconds < m_toAnnouncementTime)
        {
            yield return null;
        }
        //---------------------------------------------------------------------------
        //ランク発表
        //---------------------------------------------------------------------------
        //ランク開始位置
        int beginRank = m_rankTexts.Length - _rank.Count;
        textSize.z = 1.0f;
        Vector3 slope = Vector3.zero;
        for (int rank = 0; rank < _rank.Count; ++rank, ++beginRank)
        {
            timer.Restart();
            while (timer.TotalSeconds < m_rankIntervel)
            {
                yield return null;
            }

            //テキスト描画開始
            m_rankTexts[beginRank].gameObject.SetActive(true);
            //位置変更
            m_rankTexts[beginRank].rectTransform.localPosition = new Vector3(m_players[_rank[rank]].rectTransform.localPosition.x, m_textY, 0.0f);

            if (rank < _rank.Count - 1)
            {
                MyAudioManeger.Instance.PlaySE("ResultSecond");
            }
            else
            {
                MyAudioManeger.Instance.PlaySE("ResultFirst");
            }

            float endTime = (m_rankTextTime < m_rankTextSlopeTime) ? m_rankTextSlopeTime : m_rankTextTime;
            timer.Restart();
            while (timer.TotalSeconds < endTime)
            {
                if(timer.TotalSeconds<m_rankTextTime)
                {
                    //サイズ変更
                    textSize.x = Easing.QuintOut(timer.TotalSeconds, m_rankTextTime, m_rankTextMinSize.x, m_rankTextMaxSize.x);
                    textSize.y = Easing.QuintOut(timer.TotalSeconds, m_rankTextTime, m_rankTextMinSize.y, m_rankTextMaxSize.y);
                    m_rankTexts[beginRank].rectTransform.localScale = textSize;
                }

                if (timer.TotalSeconds < m_rankTextSlopeTime)
                {
                    //傾き
                    slope.z = Easing.CircOut(timer.TotalSeconds, m_rankTextSlopeTime, 0.0f, m_rankTextSlope);
                    m_rankTexts[beginRank].rectTransform.localRotation = Quaternion.Euler(slope);
                }

                yield return null;
            }
            m_rankTexts[beginRank].rectTransform.localScale = new Vector3(m_rankTextMaxSize.x, m_rankTextMaxSize.y, 1.0f);
        }

        //---------------------------------------------------------------------------
        // 一位の表彰
        //---------------------------------------------------------------------------

        //キャンバスの位置を取得する
        RectTransform canvasRect = GetComponent<RectTransform>();
        Vector3 canvasPos = canvasRect.localPosition;
        Vector3 canvasEuler = canvasRect.eulerAngles;

        //1stのテキストの位置をワールド座標に変換
        Vector3 pos = transform.TransformPoint(m_rankTexts[m_rankTexts.Length - 1].rectTransform.localPosition);
        pos += (m_resultCameraPos.position - pos) * m_ConfettiCameraOffset;
        //左紙吹雪オブジェクトを生成
        GameObject obj = Instantiate(m_ConfettiObj, pos + new Vector3(m_ConfettiOffset, 0, 0), Quaternion.Euler(-90, 0, 0));
        obj.transform.localScale = m_ConfettiSize;
        obj.transform.eulerAngles = m_rightEuler;
        //右紙吹雪オブジェクトを生成
        obj = Instantiate(m_ConfettiObj, pos + new Vector3(-m_ConfettiOffset, 0, 0), Quaternion.Euler(-90, 0, 0));
        obj.transform.localScale = m_ConfettiSize;
        obj.transform.eulerAngles = m_leftEuler;

        Time.timeScale = 1.0f;
        isEndRank = true;
    }

    public void ResetDepth()
    {
        m_depth.focalLength.Override(m_minLength);
    }

    public override void MyUpdate()
    {
    }
}
