using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNormalItem : MyItemInterface
{
    [Header("連打しない場合に減少する連打数の時間")]
    [Range(0.1f, 1.0f)]
    [SerializeField] float decreaseTime = 0.0f;
    float m_currentDecrease = 0.0f;

    [Header("アイテムの取得範囲")]
    [SerializeField] BoxCollider m_playerHitBox = null;

    [Header("通常状態に移動アニメーション")]
    [SerializeField] MyPlayerMoveAction m_move = null;

    //正規化した入力情報
    Vector3 nDirect = Vector3.zero;

    //取得候補の更新フラグ
    private bool isUpdateHitItem = true;

    //現在の連打数
    private int m_currentBlows = 0;
    //取得に必要な連打数
    private int m_maxBlows = 0;

    //ToDo：アイテム変更時に、プレイヤーのRigidや描画の再開を行うこと

    //取得しようとしているアイテム
    private MyItemInterface getPossibleItem = null;

    /// <summary>
    /// 初期化情報
    /// </summary>
    public override void Init(MyPlayerInfo _info)
    {
        //アイテムに必要な情報を取得する
        m_playerInfo = _info;
        //描画再開
        m_playerInfo.ReDraw();
        //自身の当たり判定再開
        m_playerHitBox.enabled = true;
    }


    public override void Exit()
    {
        //プレイヤーの描画停止
        m_playerInfo.StopDraw();
        //自身の当たり判定停止
        m_playerHitBox.enabled = false;
    }

    /// <summary>
    /// アイテムの取得開始
    /// </summary>
    public override void ActionInit()
    {
        //アイテムが無い場合は処理しない
        if (hitObj == null)
        if (hitObj == null)
        {
            isEndAntion = true;
            return;
        }

        //取得アイテムの更新終了
        isUpdateHitItem = false;

        //アイテムの取得連打数を取得する
        getPossibleItem = hitObj.transform.parent.gameObject.GetComponent<MyItemInterface>();
        m_maxBlows = getPossibleItem.ItemData.GetBlowsNum;
        m_playerInfo.Ui.SetUpBlowGauge(m_maxBlows);

        //パラメータ初期化
        m_currentDecrease = 0.0f;
        m_currentBlows = 0;
        isEndAntion = false;

        //プレイヤーの現在位置に連打ゲージを追従
        m_playerInfo.Ui.OnStageGauge(m_playerInfo.Trans.position);
    }
    /// <summary>
    ///  アイテムの取得終了
    /// </summary>
    public override void ActionExit()
    {
        getPossibleItem = null;
        m_playerInfo.Ui.GaugeOut();
        isUpdateHitItem = true;
    }

    public override void FiexdAction() { }

    public override void Action(Vector2 _input)
    {
        //他のプレイヤーが取得したならば,失敗
        if (getPossibleItem.isUser)
        {
            isEndAntion = true;
            return;
        }

        //アイテム取得のための連打
        if (MyRapperInput.Instance.GetItem(m_playerInfo.Number))
        //if (Input.GetMouseButton(0))
        {
            m_currentBlows++;

            m_currentDecrease = 0;
        }
        else
        {
            m_currentDecrease += Time.deltaTime;
        }

        //アイテムの取得
        if (m_currentBlows >= m_maxBlows)
        {
            //プレイヤーにアイテムの変更を通達
            MyItemInterface item = hitObj.transform.parent.gameObject.GetComponent<MyItemInterface>();

            if(item==null)
            {
                Debug.LogError("アイテム取得失敗：" + hitObj);
                isEndAntion = true;
                return;
            }

            m_playerInfo.NextItem = item;
            //アイテムの出現位置を空ける
            m_playerInfo.NextItem.GetComponent<MyItemInterface>().ClearAppearPos();

            //アイテムのアウトラインを変更
            Outline outline = m_playerInfo.NextItem.gameObject.GetComponent<Outline>();
            if (!outline) outline = m_playerInfo.NextItem.gameObject.AddComponent<Outline>();

            outline.OutlineColor = m_playerInfo.OutLineColor;

            m_playerInfo.Ui.BlowInGauge(m_maxBlows);

            isEndAntion = true;
            return;
        }

        //アイテム連打失敗
        if (m_currentDecrease > decreaseTime)
        {
            m_currentBlows--;

            m_currentDecrease = 0.0f;
        }
        //アイテム取得失敗
        if(m_currentBlows < 0)
        {
            isEndAntion = true;
            return;
        }

        //UIに連打数を反映
        m_currentBlows = Mathf.Clamp(m_currentBlows, 0, m_maxBlows + 1);
        Debug.Log("連打数：" + m_maxBlows + 1);
        m_playerInfo.Ui.BlowInGauge(m_currentBlows);

        //ToDo：変更必須
        //アイテム取得失敗 (移動)
        //Vector2 input = Vector2.zero;
        //input.x = (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.A)) ? 1 : 0;
        //input.y = (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) ? 1 : 0;
        //input = MyRapperInput.Instance.Move(m_playerInfo.Number);

        //閾値以上なら取得失敗
        if (_input.magnitude > 0.5f)
        {
            isEndAntion = true;
            return;
        }
    }

    /// <summary>
    /// 物理移動
    /// </summary>
    public override void FiexdMove()
    {
        m_playerInfo.Rigid.velocity = nDirect * GetSpeed();

        //移動先がステージ内か確認する
        m_playerInfo.Rigid.InsideStage(m_playerInfo.Trans.position);

    //    if (m_playerInfo.Number == 1)
    //        Debug.Log("kita");
    }

    /// <summary>
    /// 入力情報の整理
    /// </summary>
    public override void Move(Vector2 _direct)
    {
        //正規化
        _direct = _direct.normalized;
        //Vecotr3に変換
        nDirect = new Vector3(_direct.x, 0.0f, _direct.y);

        //移動アニメーション
        //if (nDirect.magnitude > 0.0f) m_move.MoveMode(3, nDirect);
        if (hitObj)
        {
            //プレイヤーに追従
            m_playerInfo.Ui.OnStageGauge(m_playerInfo.Trans.position);
            Debug.Log("いる");
        }
    }

    /// <summary>
    /// 取得可能なアイテムを更新
    /// </summary>
    public void OnTriggerStay(Collider _hitObj)
    {
        //衝突したオブジェクトを更新
        if (_hitObj.gameObject.layer == LayerMask.NameToLayer("ItemHitbox")
            && isUpdateHitItem)
        {
            hitObj = _hitObj.gameObject;
        }
    }

    /// <summary>
    /// 取得可能なアイテムを外す
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerExit(Collider _exitObj)
    {
        if (hitObj == null) return;

        if (hitObj.gameObject.layer == LayerMask.NameToLayer("ItemHitbox"))
        {
            Debug.Log("入る");
            hitObj = null;
            m_playerInfo.Ui.GaugeOut();

        }
    }
}
