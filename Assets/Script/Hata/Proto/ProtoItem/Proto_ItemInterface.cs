using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class Proto_ItemInterface : MonoBehaviour
{
    //操作対象の位置情報
    protected Transform playerTransform = null;
    //操作対象のRigidBody
    protected Rigidbody playerRigid = null;
    //プレイヤーの基本情報
    protected PlayerBasicData playerData = null;
    //プレイヤーの入力情報
    protected protoInput playerInput = null;
    //プレイヤーへの参照
    protected Proto_PlayerControl playerControl = null;
    
    //アイテム情報
    [Header("アイテムのデータ")]
    [SerializeField]
    protected ItemData m_data = null;
   
    public abstract void Move(Vector2 _input);

    public abstract bool Action();

    public bool ActionKey()
    {
        if (playerInput.Get_AButtonDown()) return true;
        else return false;
    }

    public abstract void Init();

    public abstract void ActionInit();

    /// <summary>
    /// プレイヤーの基本速度＊アイテムの速度倍率
    /// </summary>
    protected float GetSpped()
    {
        return playerData.GetSpped * m_data.GetSppedFactor;
    }

    /// <summary>
    /// 必要なプレイヤーの情報を取得する
    /// </summary>
    public virtual void SetInfo(Proto_PlayerControl _player)
    {
        playerTransform = _player.gameObject.transform;
        playerRigid = _player.gameObject.GetComponent<Rigidbody>();
        playerData = _player.GetPlayerData;
        playerInput = _player.gameObject.GetComponent<protoInput>();
        playerControl = _player;
    }

    public ItemData GetData { get { return m_data; } }


    /// <summary>
    /// ダメージ判定
    /// </summary>
    public void PlayerHit(Collision _other)
    {
        Debug.Log("hit");

        //プレイヤーの体の位置を変更
        playerControl.gameObject.transform.position = gameObject.transform.position;
        //通常状態に変更
        playerControl.SetFirstItem();
        //ヒットボックス復活
        playerControl.SetHitBox(true);
        
        if(_other.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
            //ダメージを与える
            _other.gameObject.GetComponent<Proto_PlayerControl>().HitDamage((int)m_data.GetAttack);
            
        }
        else if (_other.gameObject.layer == LayerMask.NameToLayer("PlayerItem"))
        {
            Proto_ItemInterface item = _other.gameObject.GetComponent<Proto_ItemInterface>();

            if (item == null) item = _other.gameObject.GetComponentInParent<Proto_ItemInterface>();

            item.playerControl.HitDamage((int)m_data.GetAttack);
        }

        //Item管理クラスに破棄を伝える
        Proto_ItemManager.Instance.RemoveItem(gameObject);
        //このアイテムを破棄
        Destroy(this.gameObject);
    }
    /// <summary>
    /// ダメージ判定
    /// </summary>
    public void PlayerHit(Collider _other)
    {
        Debug.Log("hit");

        //プレイヤーの体の位置を変更
        playerControl.gameObject.transform.position = gameObject.transform.position;
        //通常状態に変更
        playerControl.SetFirstItem();
        //ヒットボックス復活
        playerControl.SetHitBox(true);

        if (_other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //ダメージを与える
            _other.gameObject.GetComponent<Proto_PlayerControl>().HitDamage((int)m_data.GetAttack);

        }
        else if (_other.gameObject.layer == LayerMask.NameToLayer("PlayerItem"))
        {
            Proto_ItemInterface item = _other.gameObject.GetComponent<Proto_ItemInterface>();

            if (item == null) item = _other.gameObject.GetComponentInParent<Proto_ItemInterface>();

            item.playerControl.HitDamage((int)m_data.GetAttack);
        }

        //Item管理クラスに破棄を伝える
        Proto_ItemManager.Instance.RemoveItem(gameObject);
        //このアイテムを破棄
        Destroy(this.gameObject);
    }

    public Proto_PlayerControl GetPlayer() { return playerControl; }
}
