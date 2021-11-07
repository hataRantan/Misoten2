using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_PlayerControl : MonoBehaviour
{
    //入力情報
    private protoInput inputer = null;

    //現在のアイテムの入れ物
    private Proto_ItemInterface m_item = null;

    public enum Player_Type
    {
        MOVE,
        ACTION,
        DAMAGE,
        DEAD,
        CHANGE,
        GAMEEND
    }

    //状態管理
    private IStateSpace.StateMachineBase<Player_Type, Proto_PlayerControl> machine = new IStateSpace.StateMachineBase<Player_Type, Proto_PlayerControl>();
    public IStateSpace.StateMachineBase<Player_Type, Proto_PlayerControl> GetMachine { get { return machine; } }

    //基本パラメータ
    [SerializeField]
    private PlayerBasicData m_playerData = null;
    public int m_Hp { get; private set; }

    public PlayerBasicData GetPlayerData { get { return m_playerData; } }

    //現在のアイテム
    Proto_ItemInterface currentItem = null;

    //アイテムの取得範囲内か確認する
    private bool isHitItem = false;
    //範囲内のアイテム
    public GameObject hitItem { get; private set; }

    public Vector3 hitDirect = Vector3.zero;
    
    public void MyStart()
    {
        //入力処理を取得する
        inputer = this.gameObject.GetComponent<protoInput>();

        //初期アイテムの初期化
        SetFirstItem();

        //状態作成
        machine.AddState(Player_Type.MOVE, new MoveState(),this);
        machine.AddState(Player_Type.ACTION, new ActionState(),this);
        machine.AddState(Player_Type.DAMAGE, new DamageState(), this);
        machine.AddState(Player_Type.DEAD, new DeadState(), this);
        machine.AddState(Player_Type.GAMEEND, new EndState(), this);

        machine.InitState(Player_Type.MOVE);

        m_Hp = (int)m_playerData.GetHp;
    }


    public void MyUpdate()
    {
        //currentItem.Move(inputer.Get_LeftStick());
        machine.UpdateState();

        //更新の最後に
        isHitItem = false;
    }

    /// <summary>
    /// アイテムの変更
    /// </summary>
    /// <param name="_item"></param>
    public void ChangeItem(Proto_ItemInterface _item)
    {
        //情報を追加
        _item.SetInfo(this);
        //初期化
        _item.Init();
        //使用アイテム変更
        currentItem = _item;

        machine.ChangeState(Player_Type.MOVE);
    }
    
    /// <summary>
    /// プレイヤーの初期状態に変更（ミサイルなどのアイテムを保持していない状態）
    /// </summary>
    public void SetFirstItem()
    {
        ChangeItem(this.gameObject.GetComponent<Proto_BasicItem>());
    }

    /// <summary>
    /// Triiger衝突判定
    /// </summary>
    private void OnTriggerEnter(Collider _other)
    {
        //アイテムの取得範囲内である場合
        if (_other.gameObject.layer == LayerMask.NameToLayer("ItemHitbox"))
        {
            hitItem = _other.gameObject;
        }
    }
    private void OnTriggerExit(Collider _other)
    {
        //アイテムの取得範囲内である場合
        if (_other.gameObject.layer == LayerMask.NameToLayer("ItemHitbox"))
        {
            hitItem = null;
        }
    }


    /// <summary>
    /// 移動状態
    /// </summary>
    private class MoveState : IStateSpace.IState<Player_Type, Proto_PlayerControl>
    {
        public override void Entry() { }

        public override void Exit() { }

        public override Player_Type Update()
        {
            //移動
            board.currentItem.Move(board.inputer.Get_LeftStick());

            if (board.currentItem.GetType() == typeof(Proto_BasicItem))
            {
                //アクション状態に変更
                if (board.inputer.Get_AButtonDown())
                {
                    if (board.hitItem != null) return Player_Type.ACTION;
                }
            }
            else
            {
                //アクション状態に変更
                if (board.inputer.Get_XButtonDown())
                {
                    if (board.hitItem != null) return Player_Type.ACTION;
                }

            }

            return Player_Type.MOVE;
        }
    }


    private class ActionState : IStateSpace.IState<Player_Type, Proto_PlayerControl>
    {
        public override void Entry() { board.currentItem.ActionInit(); }

        public override void Exit() { }

        public override Player_Type Update()
        {
            if (board.currentItem.Action()) return Player_Type.MOVE;

            return Player_Type.ACTION;
        }
    }

    private class DamageState : IStateSpace.IState<Player_Type, Proto_PlayerControl>
    {
        public override void Entry()
        {
            //当たり判定復活
            board.SetHitBox(true);

            Vector3 direct = board.hitDirect;
            board.gameObject.GetComponent<Rigidbody>().AddForce(direct.normalized * 100.0f, ForceMode.Impulse);
        }

        public override void Exit()
        {
        }

        public override Player_Type Update()
        {
            return Player_Type.MOVE;
        }
    }

    private class DeadState : IStateSpace.IState<Player_Type, Proto_PlayerControl>
    {
        //タイマー
        float timer = 0.0f;
        //移動時間
        float endTime = 0.35f;
        //現在位置
        Vector3 currentPos = Vector3.zero;
        //目標値
        Vector3 targetPos = Vector3.zero;
        //初期位置
        Vector3 firstPos = Vector3.zero;
        //操作対象
        private Rigidbody rigid = null;
        //速度
        private float speed = 100.0f;

        public override void Entry()
        {
            //壁との衝突をさせないためにlayerを変更させる
            board.gameObject.layer = LayerMask.NameToLayer("Default");

            rigid = board.gameObject.GetComponent<Rigidbody>();
            //重力停止
            rigid.useGravity = false;
            //回転の制限を廃止
            //rigid.constraints = RigidbodyConstraints.None;

            //回転と移動速度を0にする
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;

            //当たり判定復活
            board.SetHitBox(true);

            //カメラから少し離れた所を目標とする
            targetPos = Camera.main.gameObject.transform.GetChild(0).transform.position;

            //現在位置を保存
            currentPos = firstPos = board.gameObject.transform.position;

        }

        public override void Exit()
        {
        }

        public override Player_Type Update()
        {
            //位置を更新
            currentPos = board.gameObject.transform.position;
            
            //カメラを向く
            board.gameObject.transform.LookAt(Camera.main.transform.position);
            
            //一定距離まで近づく
            if (Vector3.Distance(currentPos, targetPos) > 3.0f)
            {
                //移動
                rigid.AddForce(board.gameObject.transform.forward * speed, ForceMode.Acceleration);
            }
            else
            {
                board.gameObject.transform.position = targetPos;
                rigid.velocity = Vector3.zero;

                // 少し待ってから終了
                if (timer < 3.0f)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    SceneLoader.Instance.CallLoadSceneDefault("ProtoFight");
                    return Player_Type.GAMEEND;
                }
            }

            return Player_Type.DEAD;
        }
    }

    private class EndState : IStateSpace.IState<Player_Type, Proto_PlayerControl>
    {
        public override void Entry()
        {
        }

        public override void Exit()
        {
        }

        public override Player_Type Update()
        {
            return Player_Type.GAMEEND;
        }
    }



    /// <summary>
    /// フラグによって当たり判定を変更する
    /// </summary>
    /// <param name="_decision"></param>
    public void SetHitBox(bool _decision)
    {
        gameObject.GetComponent<BoxCollider>().enabled = _decision;
        gameObject.GetComponent<Rigidbody>().isKinematic = !_decision;
        gameObject.GetComponent<MeshRenderer>().enabled = _decision;


        gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<MeshRenderer>().enabled = _decision;
    }

    //HPの減少
    public void HitDamage(int _damage)
    {
        //Hp減少
        m_Hp -= _damage;
        if (m_Hp < 0) m_Hp = 0;

        int idx = (!inputer.isController) ? 0 : 1;
        GameObject.Find("FightManager").GetComponent<ProtoFightScene>().GetHpUI(idx).DamageHP(m_Hp);

        //状態変更
        if (m_Hp > 0) machine.ChangeState(Player_Type.DAMAGE);
        else machine.ChangeState(Player_Type.DEAD);
    }
}
