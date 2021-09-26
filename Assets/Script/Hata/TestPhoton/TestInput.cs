using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TestInput : MonoBehaviourPunCallbacks
{
    Transform transform = null;

    // Start is called before the first frame update
    void Start()
    {
        transform = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //自身が生成したオブジェクトで無ければ、処理しない
        //photonView.IsMine	自身（ローカルプレイヤー）が管理者かどうか
        if (!photonView.IsMine) return;

        if(Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= Vector3.forward * 1.0f;
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.position -= Vector3.right * 1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * 1.0f;
        }
    }
}
