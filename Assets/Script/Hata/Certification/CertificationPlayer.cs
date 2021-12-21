using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CertificationPlayer : MyUpdater
{
    //自身のプレイヤー番号
    private int m_playerIdx = 0;

    [Header("プレイヤーアイコン")]
    [SerializeField] Image m_icon = null;
    [Header("アイコンの移動速度")]
    [SerializeField] float m_iconSpeed = 10.0f;
    //アイコンの移動情報
    RectTransform m_iconRect = null;

    [Header("移動領域のオフセット")]
    [SerializeField]
    float m_moveOffset = 10.0f;
    //自身の移動領域
    private Vector2 m_minMove = Vector2.zero;
    private Vector2 m_maxMove = Vector2.zero;

    [Header("テキストとアイコンの取得に移行する際の距離")]
    [SerializeField]
    float m_distance = 10.0f;

    [Header("取得のための連打回数")]
    [SerializeField]
    int m_blowNum = 15;

    [Header("連打終了の秒数")]
    [SerializeField]
    float m_nonBlowTime = 0.5f;

    [Header("ターゲットテキスト")]
    [SerializeField]
    TMPro.TextMeshProUGUI m_textMesh = null;
    RectTransform m_textRect = null;

    [Header("最初のテキスト")]
    [SerializeField]
    string m_beginText = "Ready";

    [Header("取得後のテキスト")]
    [SerializeField]
    string m_endText = "Playing!!";

    [Header("表示するコントローラUI")]
    [SerializeField] OperationUI m_operator = null;

    [Header("連打画像")]
    [SerializeField]
    Image m_blowContent = null;

    [Header("接続していない時のグループ")]
    [SerializeField]
    CanvasGroup m_nonConnectGroup = null;
    CanvasGroup m_connectGroup = null;


    //プレイヤーの状態
    public enum State
    {
        NONE,
        MOVE,
        GET,
        ACTION,
        ACCEPTED
    }
    //状態マシーン
    IStateSpace.StateMachineBase<State, CertificationPlayer> m_machine = new IStateSpace.StateMachineBase<State, CertificationPlayer>();
    //自身の位置情報
    private Vector2 m_pos = Vector2.zero;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Init(int _number)
    {
        //自身の位置情報を取得する
        m_pos = this.gameObject.GetComponent<RectTransform>().anchoredPosition;

        //プレイヤー番号を取得する
        m_playerIdx = _number;

        Vector2 minEdge = new Vector2(-960.0f, -540.0f);
        Vector2 maxEdge = new Vector2(960.0f, 540.0f);
        Vector2 center = new Vector2(0.0f, 0.0f);
        //移動領域の設定
        switch (m_playerIdx)
        {
            case 0:
                {
                    m_minMove = new Vector2(minEdge.x + m_moveOffset, center.y + m_moveOffset);
                    m_maxMove = new Vector2(center.x - m_moveOffset, maxEdge.y - m_moveOffset);
                }
                break;

            case 1:
                {
                    m_minMove = new Vector2(center.x + m_moveOffset, center.y + m_moveOffset);
                    m_maxMove = new Vector2(maxEdge.x - m_moveOffset, maxEdge.y - m_moveOffset);
                }
                break;

            case 2:
                {
                    m_minMove = new Vector2(minEdge.x + m_moveOffset, minEdge.y + m_moveOffset);
                    m_maxMove = new Vector2(center.x - m_moveOffset, center.y - m_moveOffset);
                }
                break;

            case 3:
                {
                    m_minMove = new Vector2(center.x + m_moveOffset, center.y + m_moveOffset);
                    m_maxMove = new Vector2(center.x - m_moveOffset, center.y - m_moveOffset);
                }
                break;

            default:
                {
                    Debug.LogError("CertifionPlayerエラー：Initのプレイヤー番号がエラーです");
                }
                break;
        }

        //テキストを変換させる
        m_textMesh.text = m_beginText;

        //必要なコンポーネントを取得する
        m_iconRect = m_icon.rectTransform;
        m_textRect = m_textMesh.rectTransform;
        m_connectGroup = this.gameObject.GetComponent<CanvasGroup>();

        //状態マシーンの生成
        m_machine.AddState(State.NONE, new NonState(), this);
        m_machine.AddState(State.MOVE, new MoveState(), this);
        m_machine.AddState(State.GET, new GetState(), this);
        m_machine.AddState(State.ACTION, new ActionState(), this);
        m_machine.AddState(State.ACCEPTED, new AcceptedState(), this);
        m_machine.InitState(State.NONE);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    public override void MyUpdate()
    {
        m_machine.UpdateState();
    }

    /// <summary>
    /// 接続処理が完了したか確認する
    /// </summary>
    /// <returns> trueなら接続済み </returns>
    public bool IsConnect()
    {
        if (m_machine.currenrState == State.ACCEPTED) return true;
        return false;
    }

    class NonState : IStateSpace.IState<State, CertificationPlayer>
    {
        public override void Entry()
        {
            board.m_nonConnectGroup.alpha = 1.0f;
            board.m_connectGroup.alpha = 0.0f;
        }

        public override void Exit()
        {
            //コントローラUIの初期化
            board.m_operator.Init(board.m_playerIdx);

            board.m_nonConnectGroup.alpha = 0.0f;
            board.m_connectGroup.alpha = 1.0f;
        }

        public override State Update()
        {
            //コントローラの接続人数を確認する
            if (MyRapperInput.Instance.GetConnectNum() > board.m_playerIdx)
            {
                return State.MOVE;
            }

            return State.NONE;
        }
    }

    class MoveState : IStateSpace.IState<State, CertificationPlayer>
    {
        Vector2 direct = Vector2.zero;
        Vector2 power = Vector2.zero;
        Vector2 movedPos = Vector2.zero;
        Vector2 iconPos = Vector2.zero;
        Vector2 textPos = Vector2.zero;

        public override void Entry()
        {
            //移動アイコンを出現させる
            board.m_operator.ChangeUI(State.MOVE);

            //連打画像の不可視化
            board.m_blowContent.fillAmount = 0.0f;
            board.m_blowContent.gameObject.GetComponent<CanvasGroup>().alpha = 0.0f;

        }

        public override void Exit()
        {
        }

        public override State Update()
        {
            //移動方向を取得する
            direct = MyRapperInput.Instance.Move(board.m_playerIdx);
            direct = direct.normalized;
            //移動力を計算
            power = direct * board.m_iconSpeed * Time.deltaTime;
            //移動制限を行う
            movedPos = board.m_iconRect.anchoredPosition + board.m_pos + power;
            if (movedPos.x < board.m_minMove.x || movedPos.x > board.m_maxMove.x) power.x = 0.0f;
            if (movedPos.y < board.m_minMove.y || movedPos.y > board.m_maxMove.y) power.y = 0.0f;
            //移動する
            if (power.magnitude > 0.0f)
                board.m_iconRect.anchoredPosition += power;

            //テキストとアイコンの距離を求める
            iconPos = board.m_iconRect.anchoredPosition + board.m_pos;
            textPos = board.m_textRect.anchoredPosition + board.m_pos;
            if ((iconPos - textPos).magnitude <= board.m_distance)
            {
                return State.GET;
            }

            return State.MOVE;
        }
    }

    class GetState : IStateSpace.IState<State, CertificationPlayer>
    {
        Vector2 direct = Vector2.zero;
        Vector2 power = Vector2.zero;
        Vector2 movedPos = Vector2.zero;
        Vector2 iconPos = Vector2.zero;
        Vector2 textPos = Vector2.zero;
        Vector2 opposite = Vector2.zero;
        float distance = 0.0f;
        float cTime = 0.0f;
        int cBlowNum = 0;
        public override void Entry()
        {
            board.m_operator.ChangeUI(State.GET);
            cTime = 0.0f;
            cBlowNum = 0;
            board.m_textMesh.text = board.m_beginText;

            //連打画像の可視化
            board.m_blowContent.gameObject.GetComponent<CanvasGroup>().alpha = 1.0f;
        }

        public override void Exit()
        {
        }

        public override State Update()
        {
            //移動方向を取得する
            direct = MyRapperInput.Instance.Move(board.m_playerIdx);
            direct = direct.normalized;
            //移動力を計算
            power = direct * board.m_iconSpeed * Time.deltaTime;
            //移動制限を行う
            movedPos = board.m_iconRect.anchoredPosition + board.m_pos + power;
            if (movedPos.x < board.m_minMove.x || movedPos.x > board.m_maxMove.x) power.x = 0.0f;
            if (movedPos.y < board.m_minMove.y || movedPos.y > board.m_maxMove.y) power.y = 0.0f;
            //移動する
            if (power.magnitude > 0.0f)
                board.m_iconRect.anchoredPosition += power;

            //テキストとアイコンの距離を求める
            iconPos = board.m_iconRect.anchoredPosition + board.m_pos;
            textPos = board.m_textRect.anchoredPosition + board.m_pos;
            distance = (iconPos - textPos).magnitude;
            if (distance > board.m_distance)
            {
                return State.MOVE;
            }

            //連打取得
            if (MyRapperInput.Instance.GetItem(board.m_playerIdx))
            {
                cBlowNum++;
                cTime = 0.0f;

                if (cBlowNum > board.m_blowNum)
                {
                    return State.ACTION;
                }
            }
            //連打がない場合、一定時間経過で連打減少
            else
            {
                cTime += Time.deltaTime;

                if (cTime > board.m_nonBlowTime)
                {
                    cBlowNum--;
                    //最小制限
                    if (cBlowNum < 0) cBlowNum = 0;

                    cTime = 0.0f;
                }
            }

            board.m_blowContent.fillAmount = (float)cBlowNum / board.m_blowNum;

            //テキストとアイコンの位置関係を取得する
            opposite = textPos - iconPos;
            float deg = Mathf.Atan2(opposite.y, opposite.x) * Mathf.Rad2Deg;
            Vector2 blowPos = new Vector2(distance * Mathf.Cos(deg * Mathf.Deg2Rad), distance * Mathf.Sin(deg * Mathf.Deg2Rad));
            board.m_blowContent.rectTransform.anchoredPosition = blowPos;

            return State.GET;
        }

    }

    class ActionState : IStateSpace.IState<State, CertificationPlayer>
    {
        public override void Entry()
        {
            board.m_operator.ChangeUI(State.ACTION);

            board.m_blowContent.fillAmount = 1.0f;
        }

        public override void Exit()
        {
        }
        public override State Update()
        {
            if(MyRapperInput.Instance.ActionItem(board.m_playerIdx))
            {
                return State.ACCEPTED;
            }

            return State.ACTION;
        }
    }


    class AcceptedState : IStateSpace.IState<State, CertificationPlayer>
    {
        public override void Entry()
        {
            board.m_textMesh.text = board.m_endText;

            //ToDo：後々変更
            board.m_icon.color = new Color(1, 1, 1, 0);
            board.m_operator.NonDisply();
            board.m_blowContent.color = new Color(1, 1, 1, 0);
            board.m_blowContent.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }

        public override void Exit()
        {
        }

        public override State Update()
        {
            return State.ACCEPTED;
        }
    }

}
