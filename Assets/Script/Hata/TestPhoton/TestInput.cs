using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TestInput : MonoBehaviourPunCallbacks
{
    Transform transform = null;

    [Header("テスト用オブジェクト")]
    [SerializeField] GameObject uiTest = null;
    PhotonUITest ui = null;

    float uiFill = 1.0f;

    public void Init()
    {
        //if (!photonView.IsMine) return;

        //GameObject obj = PUN2Creater.Instance.CreateNetworkObj(uiTest, Vector3.zero, Quaternion.identity);
        //ui = obj.GetComponent<PhotonUITest>();
        //ui.SetPlayer(this.gameObject.transform);

    }

    public void Disble()
    {

    }

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
        //if (!photonView.IsMine) return;

        if (Input.GetKey(KeyCode.O))
        {
            GameObject obj = PUN2Creater.Instance.CreateNetworkObj(uiTest, Vector3.zero, Quaternion.identity);
            ui = obj.GetComponent<PhotonUITest>();
            ui.SetPlayer(this.gameObject.transform);
        }

        bool flg = true;

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * 1.0f;
            flg = false;
            uiFill -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= Vector3.forward * 1.0f;
            flg = false;
            uiFill -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= Vector3.right * 1.0f;
            flg = false;
            uiFill -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * 1.0f;
            flg = false;
            uiFill -= Time.deltaTime;
        }

        if (flg)
        {
            if (uiFill < 1.0f) uiFill += Time.deltaTime;
            else uiFill = 1.0f;
        }

        if (uiFill < 0.0f) uiFill = 0.0f;

        if (ui != null) ui.SetFill(uiFill);
    }
}
