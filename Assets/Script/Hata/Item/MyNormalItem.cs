using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNormalItem : MyItemInterface
{
    [Header("連打しない場合に減少する連打数の時間")]
    [Range(0.1f, 1.0f)]
    [SerializeField] float decreaseTime = 0.0f;
    float m_currentDecrease = 0.0f;

    //正規化した入力情報
    Vector3 nDirect = Vector3.zero;

    //取得候補の更新フラグ
    private bool isUpdateHitItem = true;

    //現在の連打数
    private int m_currentBlows = 0;
    //取得に必要な連打数
    private int m_maxBlows = 0;

    //ToDo：通常状態の時のプレイヤーの見た目への参照
    //ToDo：アイテム取得時に、プレイヤーとプレイヤーの見た目のRigidBodyや描画を停止する
    //ToDo：アイテム変更時に、プレイヤーのRigidや描画の再開を行うこと

    //取得しようとしているアイテム
    private MyItemInterface getPossibleItem = null;

    /// <summary>
    /// 初期化情報
    /// </summary>
    /// <param name="_info"></param>
    public override void Init(MyPlayerInfo _info)
    {
        //アイテムに必要な情報を取得する
        m_playerInfo = _info;

        //ToDo：変更必須
        BoxCollider collider = gameObject.GetComponent<BoxCollider>();
        collider.enabled = true;

        Rigidbody rigid = gameObject.GetComponent<Rigidbody>();
        rigid.isKinematic = false;

        collider = gameObject.transform.parent.GetComponent<BoxCollider>();
        collider.enabled = true;

        rigid = gameObject.transform.parent.GetComponent<Rigidbody>();
        rigid.isKinematic = false;
    }

    public override void Exit()
    {
        //ToDo：変更必須
        BoxCollider collider = gameObject.GetComponent<BoxCollider>();
        collider.enabled = false;

        Rigidbody rigid = gameObject.GetComponent<Rigidbody>();
        rigid.isKinematic = true;

        collider = gameObject.transform.parent.GetComponent<BoxCollider>();
        collider.enabled = false;

        rigid = gameObject.transform.parent.GetComponent<Rigidbody>();
        rigid.isKinematic = true;
    }

    /// <summary>
    /// アイテムの取得開始
    /// </summary>
    public override void ActionInit()
    {
        //アイテムが無い場合は処理しない
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
    }
    /// <summary>
    ///  アイテムの取得終了
    /// </summary>
    public override void ActionExit()
    {
        getPossibleItem = null;
        m_playerInfo.Ui.BlowInGauge(0);
        isUpdateHitItem = true;
    }

    public override void FiexdAction() { }

    public override void Action()
    {
        //他のプレイヤーが取得したならば,失敗
        if (getPossibleItem.isUser)
        {
            isEndAntion = true;
            return;
        }

        //アイテム取得のための連打
        if(Input.GetMouseButtonDown(0))
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
            m_playerInfo.NextItem = hitObj.transform.parent.gameObject.GetComponent<MyItemInterface>();

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
        m_currentBlows = Mathf.Clamp(m_currentBlows, 0, m_maxBlows);
        m_playerInfo.Ui.BlowInGauge(m_currentBlows);

        //ToDo：変更必須
        //アイテム取得失敗 (移動)
        Vector2 input = Vector2.zero;
        input.x = (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.A)) ? 1 : 0;
        input.y = (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)) ? 1 : 0;

        //閾値以上なら取得失敗
        if (input.magnitude > 0.5f)
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
    }

    /// <summary>
    /// 取得可能なアイテムを更新
    /// </summary>
    public void OnTriggerEnter(Collider _hitObj)
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

        if (hitObj == _exitObj) hitObj = null;
    }
}
