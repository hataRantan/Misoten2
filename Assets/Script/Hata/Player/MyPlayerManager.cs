using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MyPlayerManager : MyUpdater
{
    [Header("生成したいプレイヤーオブジェクト")]
    [SerializeField] GameObject createPlayer = null;

    //[Header("プレイヤーの人数")]
    //[Range(1, 4)]
    //[SerializeField] private int m_maxPlayerNum = 1;
    private int m_maxPlayerNum = 2;
    public int GetMaxPlayerNum { get { return m_maxPlayerNum; } }

    [Header("プレイヤー名")]
    [SerializeField] string[] m_playerName = new string[4];

    [Header("プレイヤーのカラー")]
    [SerializeField] private Color[] m_playerColor = new Color[4];

    [Header("プレイヤーの情報を表示するCanvas")]
    [SerializeField] Canvas m_playerCanvas = null;

    [Header("プレイヤー情報を表示するUI")]
    [SerializeField] GameObject createUI = null;

    [Header("プレイヤーの顔UIを管理するクラス")]
    [SerializeField]
    MyPlayerFaceControl m_faceControl = null;

    //生成したプレイヤー一覧
    private MyPlayerObject[] m_players;

    [Header("UIの初期位置")]
    [SerializeField] Vector2 uiFirstPos = new Vector2();

    //プレイヤーの最大Hp
    private int m_playerMaxHp = 0;
    //プレイヤーの最大Hpの合計
    private int m_playerTotalMaxHp = 0;
    //プレイヤーの最大合計Hp
    public int GetMaxTotalHp { get { return m_playerMaxHp; } }

    //[Header("プレイヤーのゲームオーバー演出時間")]
    //[SerializeField]
    //[Range(0.0f, 10.0f)]
    //private float m_gameOberEffectTime = 5.0f;

    //終了を行うプレイヤーインデックスを格納する
    private Dictionary<int, bool> m_endEffectPlayers = new Dictionary<int, bool>();
    //プレイヤーがゲームオーバー時に衝突する位置
    private MyPlayerGameOverHits m_endHitPos = null;
    //ゲームオーバー演出中か確認
    public bool isProcessEnd { get; private set; }

    //プレイヤーの順位を入れる
    //低い順から代入している
    public List<int> PlayerRank { get; private set; }
    int m_rank = 0;

    const float m_firstPlayerYUP = 60.0f;

    //ゲームオーバーテスト
#if UNITY_EDITOR
    TestMyPlayerDead dead = null;
#endif

    public override void MyFastestInit()
    {
        isProcessEnd = false;
        //プレイヤーの出現位置を知るために、床管理クラスを取得
        FloorManager floor = GameObject.FindWithTag("FloorManager").GetComponent<FloorManager>();

        //ToDo：変更予定
        float uiWidth = 400.0f;
        //プレイヤー人数を取得する
        m_maxPlayerNum = GameInPlayerNumber.Instance.CurrentPlayerNum;
        //プレイヤーの生成
        m_players = new MyPlayerObject[m_maxPlayerNum];

        for (int idx = 0; idx < m_maxPlayerNum; idx++)
        {
            //UIの生成
            GameObject uiObj = Instantiate(createUI, Vector3.zero, Quaternion.identity);
            //UI位置変更
            Vector2 uiPos = new Vector2(uiFirstPos.x + uiWidth * idx, uiFirstPos.y);
            MyPlayerUI ui = uiObj.GetComponent<MyPlayerUI>();

            //プレイヤーの生成
            //ToDo：現在の出現位置を四隅のみ、プレイヤーの人数によって変更させる必要あり
            GameObject playerObj = Instantiate(createPlayer, floor.GetFourCornersPos(idx) + Vector3.up * m_firstPlayerYUP, Quaternion.identity);
            playerObj.name = m_playerName[idx];

            //アウトライン変更
            //playerObj.GetComponent<Outline>().OutlineColor = m_playerColor[idx];

            MyPlayerObject player = playerObj.GetComponent<MyPlayerObject>();


            player.SetUI(ui);
            //プレイヤーの初期化
            player.CreatedInit(idx, m_playerColor[idx]);

            //UIのセットアップ
            ui.SetUp(m_playerCanvas, uiPos, m_playerName[idx], idx, player.GetHP(), m_playerColor[idx], m_faceControl);
            
            //プレイヤーを保存
            m_players[idx] = player;
        }

        //最大Hpを保存する
        m_playerMaxHp = m_players[0].PlayerData.GetHp;
        //最大Hpの合計を保存する
        m_playerTotalMaxHp = m_playerMaxHp * m_maxPlayerNum;

        m_endHitPos =gameObject.GetComponent<MyPlayerGameOverHits>();

        PlayerRank = new List<int>();
        for (int idx = 0; idx < m_maxPlayerNum; ++idx)
        {
            PlayerRank.Add(idx);
        }
        m_rank = 0;

#if UNITY_EDITOR
        dead = GetComponent<TestMyPlayerDead>();
        dead.SetPlayers(m_players);
#endif
    }


    public override void MyFixedUpdate()
    {
        //プレイヤーの物理更新
        foreach (var player in m_players)
        {
            if (player.IsSurvival && player.IsUpdate) player.MyFixedUpdate();
        }
    }

    public override void MyUpdate()
    {
        //プレイヤーの更新
        foreach (var player in m_players)
        {
            if (player.IsSurvival && player.IsUpdate)
            {
                player.MyUpdate();
            }
        }

    }

    public override void MyLateUpdate()
    {
        //プレイヤーの後更新及び、状態確認
        foreach (var player in m_players)
        {
            if (player.IsSurvival && player.IsUpdate) player.MyLateUpdate();

            CheckPlayerState(player);
        }

        //ゲームオーバー処理呼び出し
        CallGameOver();
    }

    /// <summary>
    /// プレイヤーの状態を確認する
    /// </summary>
    private void CheckPlayerState(MyPlayerObject _player)
    {
        //ゲームオーバー状態なら処理する
        if (_player.GetCurrentState() != MyPlayerObject.MyPlayerState.DEAD) return;
        //キーがないなら処理する
        if (m_endEffectPlayers.ContainsKey(_player.PlayerNumber)) return;

        m_endEffectPlayers.Add(_player.PlayerNumber, false);

        //StartCoroutine(GameOverEffect(_player));
    }

    private void CallGameOver()
    {
        //処理しない
        if (m_endEffectPlayers.Count <= 0) return;
        //ゲームオーバー演出実行中なら処理しない
        if (isProcessEnd) return;

        //ゲームオーバーを行う対象
        //List<MyPlayerObject> endPlayers = new List<MyPlayerObject>();
        List<int> endPlayers = new List<int>();

        var keyList = new List<int>(m_endEffectPlayers.Keys);
        //対象追加
        foreach (var key in keyList)
        {
            if (!m_endEffectPlayers[key])
            {
                endPlayers.Add(key);
                m_endEffectPlayers[key] = true;
            }
        }

        //ゲームオーバー処理開始
        if (endPlayers.Count > 0) StartCoroutine(GameOverEffect(endPlayers));
    }

    private IEnumerator GameOverEffect(List<int> _endPlayers)
    {
        //エフェクト開始
        isProcessEnd = true;

        //順位を代入
        foreach (var num in _endPlayers)
        {
            //要素を一番最初に持ってくる
            PlayerRank.Remove(num);
            PlayerRank.Insert(m_rank, num);
            m_rank++;
            //PlayerRank.Add(num);
        }

        //徐々にスローにする
        const float timeToSlow = 0.5f;
        StartCoroutine(SlowScaleTime(timeToSlow));

        //ゲームオーバーになるプレイヤー数を確認
        int endNum = _endPlayers.Count;

        //目標位置を取得する
        List<Transform> targetPos = m_endHitPos.GetPlayerHItCamera(endNum);

        //初速
        const float firstSpeed = 50.0f;
        //加速度
        const float acceleration = 100.0f;
        //現在速度
        float currentSpeed = firstSpeed;
        //目的地と現在地の誤差範囲
        const float goalOffset = 0.1f;
        //1フレームにおける移動時間
        const float moveTime = 1.0f;
        //最終実行時間
        float lastTime = 0.0f;

        //進行方向
        Vector3 direct = Vector3.zero;
        //速度
        Vector3 speed = Vector3.zero;
        //移動量
        Vector3 moveAmount = Vector3.zero;
        //移動位置
        Vector3 movePos = Vector3.zero;

        //カメラ位置
        Vector3 cameraPos = Camera.main.transform.position;

        //計測器
        ProcessTimer timer = new ProcessTimer();
        timer.Restart();
        //カメラに向かって移動
        while(!isReached())
        {
            for (int playerIdx = 0; playerIdx < endNum; playerIdx++)
            {
                float frameTime = timer.TotalSeconds - lastTime;
                currentSpeed = currentSpeed + acceleration * frameTime;

                //目標位置を見る
                m_players[_endPlayers[playerIdx]].PlayerInfo.Trans.LookAt(cameraPos);

                //進行方向を求める
                direct = m_players[_endPlayers[playerIdx]].PlayerInfo.Trans.forward;

                //速度を求める
                speed = direct * currentSpeed / m_players[_endPlayers[playerIdx]].PlayerInfo.Rigid.mass * frameTime;

                //移動量を求める
                moveAmount = speed * moveTime;

                //移動制限
                movePos = m_players[_endPlayers[playerIdx]].PlayerInfo.Trans.position + moveAmount;

                if (direct.x > 0.0f)
                {
                    if (movePos.x > targetPos[playerIdx].position.x) movePos.x = targetPos[playerIdx].position.x;
                }
                else if (direct.x <= 0.0f)
                {
                    if (movePos.x < targetPos[playerIdx].position.x) movePos.x = targetPos[playerIdx].position.x;
                }

                if (direct.y > 0.0f)
                {
                    if (movePos.y > targetPos[playerIdx].position.y) movePos.y = targetPos[playerIdx].position.y;
                }
                else if (direct.y <= 0.0f)
                {
                    if (movePos.y < targetPos[playerIdx].position.y) movePos.y = targetPos[playerIdx].position.y;
                }

                if (direct.z > 0.0f)
                {
                    if (movePos.z > targetPos[playerIdx].position.z) movePos.z = targetPos[playerIdx].position.z;
                }
                else if (direct.z <= 0.0f)
                {
                    if (movePos.z < targetPos[playerIdx].position.z) movePos.z = targetPos[playerIdx].position.z;
                }

                m_players[_endPlayers[playerIdx]].PlayerInfo.Trans.position = movePos;
            }

            //最終実行時間を代入
            lastTime = timer.TotalSeconds;

            yield return null;
        }


        //移動終了、一定時間待つ
        const float waitTime = 1.0f;
        timer.Restart();
        while (timer.TotalSeconds < waitTime)
        {
            yield return null;
        }

        //スロー解除
        Time.timeScale = 1.0f;

        const float blowingPower = 100.0f;
        //プレイヤーを吹っ飛ばす
        for (int playerIdx = 0; playerIdx < endNum; playerIdx++)
        {
            //移動方向を求める
            direct = targetPos[playerIdx].position - cameraPos;

            //吹っ飛ばし力を求める
            direct = direct.normalized;
            direct = direct * blowingPower;

            //Rigidbodyの制限解除
            m_players[_endPlayers[playerIdx]].PlayerInfo.Rigid.constraints = RigidbodyConstraints.None;
            m_players[_endPlayers[playerIdx]].PlayerInfo.Rigid.isKinematic = false;
            //瞬間的に力を加える
            m_players[_endPlayers[playerIdx]].PlayerInfo.Rigid.AddForce(direct, ForceMode.Impulse);

            //ランダムな方向に回転を掛ける
            Vector3 randomDirect = new Vector3(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));
            m_players[_endPlayers[playerIdx]].PlayerInfo.Rigid.AddTorque(randomDirect * blowingPower);
        }

        //エフェクト終了
        isProcessEnd = false;


        //目的地に辿り着いたか求める
        bool isReached()
        {
            for (int playerIdx = 0; playerIdx < _endPlayers.Count; playerIdx++)
            {
                if ((m_players[_endPlayers[playerIdx]].PlayerInfo.Trans.position - targetPos[playerIdx].position).magnitude > goalOffset) return false;
            }

            return true;
        }
    }

    /// <summary>
    /// 徐々にスローにする
    /// </summary>
    /// <param name="_targetTime"> timeScale=0になるまでの時間 </param>
    private IEnumerator SlowScaleTime(float _targetTime)
    {
        ProcessTimer timer = new ProcessTimer();
        timer.Restart();
        //徐々にスローにする
        while (timer.TotalSeconds < _targetTime)
        {
            Time.timeScale = -Easing.ExpOut(timer.TotalSeconds, _targetTime, -1.0f, 0.0f);

            yield return null;
        }

        Time.timeScale = 0.0f;
    }

    /// <summary>
    /// 脱落したプレイヤーの人数を取得する
    /// </summary>
    public int GetDropPlayerNum()
    {
        int dropNum = 0;

        for (int playerIdx = 0; playerIdx < m_players.Length; playerIdx++)
        {
            if (m_players[playerIdx].GetHP() <= 0) dropNum++;
        }

        return dropNum;
    }

    public void ALLStopDamgeEffect()
    {
        for (int playerIdx = 0; playerIdx < m_players.Length; playerIdx++)
        {
            m_players[playerIdx].PlayerUI.StopDamage();
        }
    }

    /// <summary>
    /// プレイヤーの合計現在のHpを渡す
    /// </summary>
    public int GetPlayerCurrentTotalHp()
    {
        int hp = 0;

        for(int playerIdx=0;playerIdx<m_players.Length;playerIdx++)
        {
            hp += m_players[playerIdx].GetHP();
        }

        return hp;
    }


    /// <summary>
    /// Hpが最も低いプレイヤーの位置を取得する
    /// </summary>
    public Vector3 GetMinHpPlayerPos()
    {
        //プレイヤーのリストを残りHpを参考に昇順にする
        var list = m_players.OrderBy(p => p.GetHP());
        //最初の要素（残りHpが最も低いプレイヤー）の位置を渡す
        return list.First().gameObject.transform.position;
    }
}
