using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMissileItem : MyItemInterface
{
    Rigidbody rigid = null;
    SphereCollider col = null;

    private void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        col = gameObject.GetComponent<SphereCollider>();
        col.enabled = false;
    }

    public override void ActionInit()
    {
        col.enabled = true;
    }

    public override void Action()
    {
        rigid.velocity = new Vector3(-1, 0.0f, 0) * GetSpeed();
    }

    public override void FiexdAction()
    {
    }

    public override void FiexdMove()
    {
    }

    public override void Move(Vector2 _direct)
    {
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer==LayerMask.NameToLayer("Player"))
        {
            Damge(collision.gameObject.GetComponent<MyPlayerObject>().PlayerInfo, DamageAction);

            col.enabled = false;
        }
    }

    private void DamageAction()
    {

    }
}
