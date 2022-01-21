using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyPlayerStateClass;
/// <summary>
/// プレイヤーが操作する対象
/// </summary>
public class MyPlayerObject : MyUpdater
{
    /// <summary>
    /// プレイヤーの初期データ
    /// </summary>
    [Header("プレイヤーのデータ")]
    [SerializeField] PlayerBasicData m_plData = null;
    //初期データ
    public PlayerBasicData PlayerData { get { return m_plData; } }
    //プレイヤーの番号
    public int PlayerNumber { get; private set; }

    /// <summary>
    /// アイテム関係
    /// </summary>
    [Header("通常アイテム")]
    [SerializeField] MyItemInterface m_normalItem = null;
    public MyItemInterface NormalImte { get { return m_normalItem; } }
    //使用しているアイテム
    private MyItemInterface m_currentItem = null;
    public MyItemInterface CurrentItem { get { return m_currentItem; } }

    [Header("ダメージエフェクト"), SerializeField]
    GameObject hitEffect = null;
    [Header("ダメージエフェクトのサイズ"), SerializeField, Range(1.0f, 10.0f)]
    private float hitSize = 5.0f;
    public float GetHitSize { get { return hitSize; } }
    public GameObject GetHitEffect { get { return hitEffect; } }
    
    /// <summary>
    /// プレイヤーの状態関係
    /// </summary>
    public enum MyPlayerState
    {
        MOVE,
        ACTION,
        DAMAGE,
        DEAD,
        END,
        WARP,
        NONE    //通常は使わない
    }
    //現在の状態
    public MyPlayerState m_State { get; private set; }
    //プレイヤーの状態マシーン
    private IStateSpace.StateMachineBase<MyPlayerState, MyPlayerObject> m_machine;

    //プレイヤーのUIへの参照
    public MyPlayerUI PlayerUI { get; private set; }

    //自身の情報（アイテムなどに渡すために使用する）
    private MyPlayerInfo m_info = new MyPlayerInfo();
    public MyPlayerInfo PlayerInfo { get { return m_info; } }
    //trueなら生存しているフラグ
    public bool IsSurvival { get; private set; }

    [Header("プレイヤーの見た目")]
    [SerializeField] SkinnedMeshRenderer m_playerRender = null;

    [Header("プレイヤーの足元")]
    [SerializeField] Transform m_playerBottom = null;

    /// <summary>
    /// 生成時の初期化
    /// </summary>
    public void CreatedInit(int _playerNum, Color _outLine)
    {
        //生存フラグをOn
        IsSurvival = true;
        //プレイヤー番号の取得
        PlayerNumber = _playerNum;
        //状態生成
        m_machine = new IStateSpace.StateMachineBase<MyPlayerState, MyPlayerObject>();
        //状態の追加
        m_machine.AddState(MyPlayerState.MOVE, new MoveState(), this);
        m_machine.AddState(MyPlayerState.ACTION, new ActionState(), this);
        m_machine.AddState(MyPlayerState.DAMAGE, new DamageState(), this);
        m_machine.AddState(MyPlayerState.DEAD, new DeadState(), this);
        m_machine.AddState(MyPlayerState.END, new EndState(), this);
        //初期状態に変更
        m_machine.InitState(MyPlayerState.MOVE);


        //顔を貼り付け
        //m_playerRender.materials[1].SetTexture("_MainTex", SavingFace.Instance.GetFace(_playerNum));
        m_playerRender.material.SetTexture("_MainTex", SavingFace.Instance.GetFace(_playerNum));

        Outline line = gameObject.AddComponent<Outline>();
        line.OutlineColor = _outLine;
        line.OutlineMode = Outline.Mode.OutlineAll;

        //情報をセットする
        m_info.SetPlayer(this, _outLine);
        
        //初期位置を設定
        m_info.InitPos(gameObject.transform.position);
        //通常アイテムを取得
        ChangeNormalItem();
        //床位置を合わせる
        AdjustFloor();

        
    }

    /// <summary>
    /// 床の高さに合わせる
    /// </summary>
    public void AdjustFloor()
    {
        const float floorY = 0.0f;
        //足元と床位置の差を求める
        float diff = floorY - m_playerBottom.position.y;
        Vector3 pos = m_info.Trans.position;
        //差分だけY軸を移動する
        m_info.Trans.position = new Vector3(pos.x, pos.y + diff, pos.z);
    }

    /// <summary>
    /// 通常状態に移行する
    /// </summary>
    public void ChangeNormalItem()
    {
        ChangeItem(m_normalItem);
    }

    /// <summary>
    /// アイテムを破棄する（操作不可状態にする）
    /// </summary>
    public void NonItem()
    {
        //前アイテムの後処理
        if (m_currentItem) m_currentItem.Exit();

        //見た目は描画される
        m_info.Render.enabled = true;

        m_currentItem = null;
    }

    /// <summary>
    /// アイテムの変更
    /// </summary>
    private void ChangeItem(MyItemInterface _item)
    {
        //現在のアイテムの後処理
        if (m_currentItem) m_currentItem.Exit();

        //アイテム入れ替え
        m_currentItem = _item;

        //新しいアイテムの初期化
        m_currentItem.Init(m_info);

        //移動状態に移行する
        m_info.SetLateState(MyPlayerState.MOVE);
    }


    /// <summary>
    /// 物理処理
    /// </summary>
    public override void MyFixedUpdate()
    {
        m_machine.FixedUpdateState();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    public override void MyUpdate()
    {
        m_machine.UpdateState();
    }
    
    /// <summary>
    /// 後処理
    /// </summary>
    public override void MyLateUpdate()
    {
        //アイテムの変更
        if(m_info.NextItem)
        {
            //現在のアイテムと違う場合にアイテムの変更を行う
            if (m_info.NextItem != m_currentItem)
            {
                //アイテムの変更
                ChangeItem(m_info.NextItem);

                m_info.NextItem = null;
            }
        }

        //状態変更
        if (m_info.NextState != MyPlayerState.NONE)
        {
            m_machine.ChangeState(m_info.NextState);
            m_info.SetLateState(MyPlayerState.NONE);
        }

        //状態を取得する
        m_State = m_machine.currenrState;
    }

 
    /// <summary>
    /// プレイヤーのHPを渡す
    /// </summary>
    public int GetHP() { return m_info.Hp; }

    /// <summary>
    /// プレイヤーのUIへの参照を渡す
    /// </summary>
    public void SetUI(MyPlayerUI _ui) { PlayerUI = _ui; }

    /// <summary>
    /// プレイヤーの現在の状態を渡す
    /// </summary>
    /// <returns></returns>
    public MyPlayerState GetCurrentState()
    {
        return m_machine.currenrState;
    }

    /// <summary>
    /// 生存終了 
    /// </summary>
    public void Abort()
    {
        IsSurvival = false;

        //描画運動停止
        m_info.StopDraw();
    }

}
