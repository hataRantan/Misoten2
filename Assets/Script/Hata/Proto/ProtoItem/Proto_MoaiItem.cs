using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_MoaiItem : Proto_ItemInterface
{
    //操作対象
    private Rigidbody rigid = null;
    
    //衝突判定
    bool isHitFloor = false;
    bool isAction = false;

    private enum Moai_Type
    {
        JUMP,
        END,
        UP,
        DOWN
    }

    private IStateSpace.StateMachineBase<Moai_Type, Proto_MoaiItem> machine = new IStateSpace.StateMachineBase<Moai_Type, Proto_MoaiItem>();

    /// <summary>
    /// 初期化
    /// </summary>
    public override void Init()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        gameObject.layer = LayerMask.NameToLayer("PlayerItem");

    }

    /// <summary>
    /// 移動
    /// </summary>
    public override void Move(Vector2 _input)
    {
        //入力値を正規化
        Vector3 normalInput = new Vector3(_input.x, 0.0f, _input.y).normalized;

        //速度
        Vector3 velocity = Vector3.zero;

        velocity.x = -normalInput.x * GetSpped();
        velocity.z = -normalInput.z * GetSpped();

        //速度を追加
        rigid.velocity = velocity;
    }

    public override void ActionInit()
    {
        isHitFloor= false;
        isAction = false;

        machine.AddState(Moai_Type.JUMP, new JumpUpState(), this);
        machine.AddState(Moai_Type.DOWN, new DownState(), this);
        machine.AddState(Moai_Type.END, new EndState(), this);

        machine.InitState(Moai_Type.JUMP);
    }

    public override bool Action()
    {
        machine.UpdateState();

        return isHitFloor;
    }

    private class UpState : IStateSpace.IState<Moai_Type, Proto_MoaiItem>
    {
        //指定の高さまで上昇
        private float targetY = 3000.0f;

        public override void Entry()
        {

            if (board.isAction) return;

            //上昇
            board.rigid.useGravity = false;
            board.rigid.AddForce(Vector3.up * targetY, ForceMode.Impulse);

            //自身の当たり判定を一時停止
            board.gameObject.GetComponent<BoxCollider>().enabled = false;

            //衝突範囲を復活
            board.gameObject.GetComponentInChildren<BoxCollider>().gameObject.layer = LayerMask.NameToLayer("PlayerItem");
            board.gameObject.GetComponentInChildren<BoxCollider>().enabled = true;

            board.isAction = true;
            board.isHitFloor = false;

        }

        public override void Exit()
        {
            //自身の当たり判定を一時停止
            board.gameObject.GetComponent<BoxCollider>().enabled = false;

            //衝突範囲を復活
            board.gameObject.GetComponentInChildren<BoxCollider>().gameObject.layer = LayerMask.NameToLayer("PlayerItem");
            board.gameObject.GetComponentInChildren<BoxCollider>().enabled = false;
        }

        public override Moai_Type Update()
        {
            if (board.isHitFloor) return Moai_Type.END;

            return Moai_Type.JUMP;
        }
    }

    /// <summary>
    /// 特になにもしない
    /// </summary>
    private class EndState : IStateSpace.IState<Moai_Type, Proto_MoaiItem>
    {
        public override void Entry()
        {
        }

        public override void Exit()
        {
        }

        public override Moai_Type Update()
        {
            return Moai_Type.END;
        }
    }

    /// <summary>
    /// モアイを上昇させる
    /// </summary>
    private class JumpUpState : IStateSpace.IState<Moai_Type, Proto_MoaiItem>
    {
        //上昇の高さ
        private float upY = 35.0f;
        //上昇時間
        private float upTime = 0.65f;
        private float timer = 0.0f;

        public override void Entry()
        {

            //重力を一時停止
            board.rigid.useGravity = false;

            //当たり判定を一時停止
            board.gameObject.GetComponent<BoxCollider>().enabled = false;

        }

        public override void Exit()
        {

        }

        public override Moai_Type Update()
        {
            if (timer < upTime)
            {
                //上昇
                float y = Easing.QuadOut(timer, upTime, 0.0f, upY);

                Vector3 pos = board.gameObject.transform.position;
                pos.y = y;
                board.gameObject.transform.position = pos;

                timer += Time.deltaTime;
            }
            else
            {
                return Moai_Type.DOWN;
            }

            return Moai_Type.UP;
        }
    }

    private class DownState : IStateSpace.IState<Moai_Type, Proto_MoaiItem>
    {
        //落ちる時間
        float downTime = 0.65f;
        float timer = 0.0f;
        //現在位置
        float firstY = 0.0f;

        public override void Entry()
        {
            //現在の高さを知る
            firstY = board.gameObject.transform.position.y;

            //衝突範囲を復活
            board.gameObject.GetComponentInChildren<BoxCollider>().gameObject.layer = LayerMask.NameToLayer("PlayerItem");
            board.gameObject.GetComponentInChildren<BoxCollider>().enabled = true;
            board.gameObject.GetComponentInChildren<BoxCollider>().gameObject.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);

            board.isHitFloor = false;
            board.isAction = true;
        }

        public override void Exit()
        {

        }

        public override Moai_Type Update()
        {
            //入力値を正規化
            Vector3 normalInput = new Vector3(board.playerInput.Get_LeftStick().x, 0.0f, board.playerInput.Get_LeftStick().y).normalized;

            //速度
            Vector3 velocity = Vector3.zero;

            velocity.x = -normalInput.x * board.GetSpped();
            velocity.z = -normalInput.z * board.GetSpped();

            //速度を追加
            board.rigid.velocity = velocity;

            if (timer < downTime)
            {
                //上昇
                float y = -Easing.QuadOut(timer, downTime, -firstY, -0.0f);

                Vector3 pos = board.gameObject.transform.position;
                pos.y = y;
                board.gameObject.transform.position = pos;

                timer += Time.deltaTime;
            }
            else
            {
                Vector3 pos = board.gameObject.transform.position;
                pos.y = 0.0f;
                board.gameObject.transform.position = pos;

                return Moai_Type.END;
            }

            return Moai_Type.DOWN;
        }
    }


    private void OnCollisionEnter(Collision _other)
    {
        //取得前でなければ、ヒットなし
        if (!isAction) return;

        //プレイヤー、プレイヤーが所持するアイテム、壁に衝突するとヒット判定
        if (_other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isHitFloor = true;

            Vector3 direct = _other.gameObject.transform.position - gameObject.transform.position;
            direct.y=0.0f;
            _other.gameObject.GetComponent<Proto_PlayerControl>().hitDirect = direct;

            PlayerHit(_other);

            Debug.Log("hit");
        }
        else if(_other.gameObject.layer == LayerMask.NameToLayer("PlayerItem"))
        {
            isHitFloor = true;

            GameObject player = _other.gameObject.GetComponent<Proto_ItemInterface>().GetPlayer().gameObject;

            Vector3 direct = gameObject.transform.position - player.gameObject.transform.position;
            player.GetComponent<Proto_PlayerControl>().hitDirect = direct;

            PlayerHit(_other);

            Debug.Log("hit");
        }
        else if (_other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            Debug.Log("床Hit");

            isHitFloor = true;

            //プレイヤーの体の位置を変更
            playerControl.gameObject.transform.position = gameObject.transform.position;
            //通常状態に変更
            playerControl.SetFirstItem();
            //ヒットボックス復活
            playerControl.SetHitBox(true);

            //Item管理クラスに破棄を伝える
            Proto_ItemManager.Instance.RemoveItem(gameObject);
            //このアイテムを破棄
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        //取得前でなければ、ヒットなし
        if (!isAction) return;

        //プレイヤー、プレイヤーが所持するアイテム、壁に衝突するとヒット判定
        if (_other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isHitFloor = true;

            Vector3 direct = _other.gameObject.transform.position - gameObject.transform.position;
            direct.y = 0.0f;
            _other.gameObject.GetComponent<Proto_PlayerControl>().hitDirect = direct;

            PlayerHit(_other);

            Debug.Log("hit");
        }
        else if (_other.gameObject.layer == LayerMask.NameToLayer("PlayerItem"))
        {
            isHitFloor = true;

            GameObject player = _other.gameObject.GetComponent<Proto_ItemInterface>().GetPlayer().gameObject;

            Vector3 direct = gameObject.transform.position - player.gameObject.transform.position;
            player.GetComponent<Proto_PlayerControl>().hitDirect = direct;

            PlayerHit(_other);

            Debug.Log("hit");
        }
        else if (_other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            Debug.Log("床Hit");

            isHitFloor = true;

            //プレイヤーの体の位置を変更
            playerControl.gameObject.transform.position = gameObject.transform.position;
            //通常状態に変更
            playerControl.SetFirstItem();
            //ヒットボックス復活
            playerControl.SetHitBox(true);

            //Item管理クラスに破棄を伝える
            Proto_ItemManager.Instance.RemoveItem(gameObject);
            //このアイテムを破棄
            Destroy(gameObject);
        }
    }
}
