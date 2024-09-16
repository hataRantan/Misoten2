using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    //したいこと
  
    /// <summary>
    /// シーン読み込みに掛ける最低限の時間
    /// </summary>
    [Header("最低限のロード時間")]
    [Range(0.5f, 2.0f)]
    [SerializeField] private float m_minLoadTime = 1.0f;

    /// <summary>
    /// ローディングの読み込み具合
    /// </summary>
    public float LoadingPercent { get; private set; }

    /// <summary>
    /// シーン遷移処理が完了しているか
    /// </summary>
    public bool isDoneTransition { get; private set; }

    [Header("フェードを行うキャンバス一覧")]
    [SerializeField] List<Canvas> faderCanves = new List<Canvas>();

    /// <summary>
    /// 前回のシーン
    /// </summary>
    private string m_lastScene = "null";
    public string GetLastScene { get { return m_lastScene; } }
    
    public void Start()
    {
        //最初のシーン名取得
        m_lastScene = SceneManager.GetActiveScene().name;

        isDoneTransition = false;
    }

    /// <summary>
    /// デフォルトのフェード処理でシーン読み込み
    /// </summary>
    /// <param name="_scene"> 読みこむシーン名 </param>
    public void CallLoadSceneDefault(string _scene,Camera _target=null)
    {
        StartCoroutine(LoadScene(_scene, 0, 0, _target));
    }

    /// <summary>
    /// フェード処理を指定してシーン読み込み
    /// </summary>
    /// <param name="_scene"> 読みこむシーン名 </param>
    /// <param name="_fadeInIdx"> フェードインの番号 </param>
    /// <param name="_fadeOutIdx"> フェードアウトの番号 </param>
    public void CallLoadScene(string _scene, int _fadeInIdx, int _fadeOutIdx)
    {
        StartCoroutine(LoadScene(_scene, _fadeInIdx, _fadeOutIdx));
    }

    /// <summary>
    /// 前回のシーンをデフォルトのフェード処理で読み込む
    /// </summary>
    public void CallLastSceneDefault()
    {
        StartCoroutine(LoadLastScene(0, 0));
    }

    /// <summary>
    /// 前回のシーンをフェード処理を指定して読み込む
    /// </summary>
    /// <param name="_fadeInIdx"> フェードインの番号 </param>
    /// <param name="_fadeOutIdx"> フェードアウトの番号 </param>
    public void CallLastScene(int _fadeInIdx, int _fadeOutIdx)
    {
        StartCoroutine(LoadLastScene(_fadeInIdx, _fadeOutIdx));
    }

    /// <summary>
    /// シーンの非同期読み込み
    /// </summary>
    /// <param name="_scene"> 読みこむシーン名 </param>
    /// <param name="_fadeInIdx"> フェードインの番号 </param>
    /// <param name="_fadeOutIdx"> フェードアウトの番号 </param>
    private IEnumerator LoadScene(string _scene,int _fadeInIdx,int _fadeOutIdx,Camera _target=null)
    {
        if (!_target) _target = Camera.main;

        //現在シーンを前回シーンとして記憶
        m_lastScene = SceneManager.GetActiveScene().name;

        isDoneTransition = false;

        //キャンバスを生成
        FadeBase fader = Instantiate(faderCanves[_fadeInIdx]).GetComponent<FadeBase>();
        fader.gameObject.GetComponent<Canvas>().worldCamera = _target;
        //フェードイン処理
        if (fader == null)
        {
            Debug.LogError("フェードキャンバスにFadeBaseが存在しない");
        }

        yield return StartCoroutine(fader.FadeIn());

        //計測器生成
        ProcessTimer timer = new ProcessTimer();
        timer.Restart();

        //シーンを非同期で読み込み
        //現在のシーンをアンロードせずに、シーンを読み込みアクティブシーンを切り替える
        //AsyncOperation load = SceneManager.LoadSceneAsync(_scene, LoadSceneMode.Additive);
        AsyncOperation load = SceneManager.LoadSceneAsync(_scene);

        //シーンの自動切り替えを禁止する
        load.allowSceneActivation = false;


        while (load.progress < 0.9f || timer.TotalSeconds < m_minLoadTime)
        {
            //ローディング状況更新
            LoadingPercent = load.progress;

            yield return null;
        }//ロード完了

        LoadingPercent = 1.0f;

        //シーン遷移許可
        load.allowSceneActivation = true;
        //1フレーム待つ
        yield return null;

        //前回のシーンを非同期にアンロード
        //SceneManager.UnloadSceneAsync(m_lastScene);


        //フェードアウト処理
        fader = fader = Instantiate(faderCanves[_fadeOutIdx]).GetComponent<FadeBase>();
        fader.gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        if (fader == null)
        {
            Debug.LogError("フェードキャンバスにFadeBaseが存在しない");
            yield break;
        }

        yield return StartCoroutine(fader.FadeOut());

        //フェード完了
        isDoneTransition = true;
        
        //フェードキャンバスを破棄
        Destroy(fader.gameObject);

    }

    /// <summary>
    /// 前回のシーン読み込み
    /// </summary>
    /// <param name="_fadeInIdx"> フェードインの番号 </param>
    /// <param name="_fadeOutIdx"> フェードアウトの番号 </param>
    /// <returns></returns>
    private IEnumerator LoadLastScene(int _fadeInIdx, int _fadeOutIdx)
    {
        yield return StartCoroutine(LoadScene(m_lastScene, _fadeInIdx, _fadeOutIdx));
    }
}
