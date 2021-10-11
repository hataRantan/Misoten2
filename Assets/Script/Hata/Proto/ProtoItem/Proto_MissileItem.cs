﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_MissileItem :Proto_ItemInterface
{
    [Header("回転速度")]
    [SerializeField] float rotateSpeed = 10.0f;

    //操作対象
    private Rigidbody rigid = null;

    //移動方向
    private Vector3 moveDirect = Vector3.zero;

    //ヒットしたかどうか
    private bool isHit = false;

    //アクション中か
    private bool isAction = false;

    public override void Init()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        gameObject.layer = LayerMask.NameToLayer("PlayerItem");
        isAction = false;

        rigid.useGravity = false;
        gameObject.transform.position += Vector3.up * 2.0f;
    }

    /// <summary>
    /// 回転
    /// </summary>
    public override void Move(Vector2 _input)
    {
        //入力値
        float inputPower = _input.x;

        //回転速度
        float rotate = rotateSpeed * Time.deltaTime * inputPower;

        rigid.AddTorque(new Vector3(0.0f, rotate, 0.0f), ForceMode.Acceleration);

        //Debug.DrawRay(gameObject.transform.position, -gameObject.transform.up * 50.0f, Color.red);
    }

    public override void ActionInit()
    {
        //移動方向を決定
        moveDirect = -gameObject.transform.up;

        //moveDirect.y = 0.0f;
        //移動力を決定
        rigid.velocity = moveDirect * GetSpped();

        isHit = false;
        isAction = true;
    }

    public override bool Action()
    {
        return isHit;
    }

    private void OnCollisionEnter(Collision _other)
    {
        //プレイヤー、プレイヤーが所持するアイテム、壁に衝突するとヒット判定
        if(_other.gameObject.layer== LayerMask.NameToLayer("Player") 
            ||_other.gameObject.layer== LayerMask.NameToLayer("PlayerItem") 
            ||_other.gameObject.layer== LayerMask.NameToLayer("Wall"))
        {
            //取得前でなければ、ヒットなし
            if (!isAction) return;

            isHit = true;

            PlayerHit(_other);
        }
    }
}
