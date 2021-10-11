using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ProtoFightScene : MonoBehaviour
{
    private enum FightSceneType
    {
        BEFORE_LOAD,
        AFTER_LOAD,
        FIGHT
    }

    [Header("プレイヤーのプレハブ")]
    [SerializeField] GameObject playerPrehub = null;

    [Header("ゲーム開始を教える時刻テキスト")]
    [SerializeField] TextMeshProUGUI timerText = null;

    [Header("ロードが完了してからの戦闘開始するまでの時間")]
    [SerializeField] float fightIntervelTime = 3.0f;

    [SerializeField]
    Proto_BlowUI blowUI = null;

    [Header("プレイヤー参加人数")]
    [Range(1, 2)]
    [SerializeField]
    int playerMaxNum = 2;

    [Header("Hpゲージ")]
    [SerializeField] Proto_PlayerHP[] playerHps = new Proto_PlayerHP[2];
    

    //生成したプレイヤー一覧
    private List<GameObject> players = new List<GameObject>();

    //プレイヤーの出現場所
    private Vector3[] appearPos = { new Vector3(30.0f, 5.0f, 0.0f), new Vector3(-30.0f, 5.0f, 0.0f) };

    //状態管理クラス
    private IStateSpace.StateMachineBase<FightSceneType, ProtoFightScene> machine = new IStateSpace.StateMachineBase<FightSceneType, ProtoFightScene>();

    // Start is called before the first frame update
    void Start()
    {
        //ステートマシンの生成
        machine.AddState(FightSceneType.BEFORE_LOAD, new Before_LoadState(), this);
        machine.AddState(FightSceneType.AFTER_LOAD, new After_LoadState(), this);
        machine.AddState(FightSceneType.FIGHT, new FightState(), this);

        //初期状態を追加
        machine.InitState(FightSceneType.BEFORE_LOAD);
    }

    // Update is called once per frame
    void Update()
    {
        machine.UpdateState();
    }

    /// <summary>
    /// ロード前の処理一覧
    /// </summary>
    private class Before_LoadState : IStateSpace.IState<FightSceneType, ProtoFightScene>
    {
        public override void Entry()
        {
            board.timerText.gameObject.SetActive(false);

            //プレイヤー生成し、顔を追加
            for (int idx = 0; idx < board.playerMaxNum; idx++)
            {
                board.players.Add(Instantiate(board.playerPrehub, board.appearPos[idx], Quaternion.identity));
                //顔を貼り付け
                board.players[idx].GetComponent<Proto_PlayerFace>().SetFace(SavingFace.Instance.GetFace(idx));
                //PL1ならキーボード、PL2ならコントローラ
                board.players[idx].GetComponent<protoInput>().isController = (idx == 0) ? false : true;
                //プレイヤー管理の初期化
                board.players[idx].GetComponent<Proto_PlayerControl>().MyStart();
                //HP作成
                board.playerHps[idx].SetUpHPImage(board.players[idx].GetComponent<Proto_PlayerControl>().GetPlayerData.GetMaxHp);
            }

            //アイテム管理クラス初期化
            Proto_ItemManager.Instance.MyStart();

            board.blowUI.MyStart();
        }

        public override void Exit()
        {

        }

        public override FightSceneType Update()
        {
            //ロード完了
            if (SceneLoader.Instance.GetLastScene == "ProtoFace")
            {
                if (SceneLoader.Instance.isDoneTransition) return FightSceneType.AFTER_LOAD;
            }

            else
                return FightSceneType.AFTER_LOAD;


            return FightSceneType.BEFORE_LOAD;
        }
    }


    /// <summary>
    /// ロード後の動き
    /// </summary>
    private class After_LoadState : IStateSpace.IState<FightSceneType, ProtoFightScene>
    {
        private ProcessTimer timer = new ProcessTimer();

        public override void Entry()
        {
            board.timerText.gameObject.SetActive(true);
            
            //計測開始
            timer.Restart();
        }

        public override void Exit()
        {
        }

        public override FightSceneType Update()
        {
            //経過時間を表示する
            board.timerText.text = TimeText();

            if (board.fightIntervelTime < timer.TotalSeconds) return FightSceneType.FIGHT;

            return FightSceneType.AFTER_LOAD;
        }

        /// <summary>
        /// 残り時間を表示する
        /// </summary>
        /// <returns></returns>
        private string TimeText()
        {
            //経過時間
            float progress = Mathf.Floor(timer.TotalSeconds);

            float time = board.fightIntervelTime - progress;

            if (time > 0) return time.ToString();
            else return "GameStart";
        }
    }

    /// <summary>
    /// 戦闘状態
    /// </summary>
    private class FightState : IStateSpace.IState<FightSceneType, ProtoFightScene>
    {
        public override void Entry()
        {
            //テキストを消去
            board.timerText.gameObject.SetActive(false);
        }

        public override void Exit()
        {
            
        }

        public override FightSceneType Update()
        {
            //アイテム管理クラスの更新
            Proto_ItemManager.Instance.MyUpdate();

            //プレイヤーの更新
            foreach(var player in board.players)
            {
                //毎フレームGetComponentするべきでない、プロトなので許して
                player.GetComponent<Proto_PlayerControl>().MyUpdate();
            }


            return FightSceneType.FIGHT;
        }
    }

    public Proto_PlayerHP GetHpUI(int _idx)
    {
        return playerHps[_idx];
    }

}
