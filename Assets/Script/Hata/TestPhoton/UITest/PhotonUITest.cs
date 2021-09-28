using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

//キャンバスをネットワークオブジェクトにすべきでは？
public class PhotonUITest : MonoBehaviourPunCallbacks
//, IPunObservable
{
    Image image = null;
    RectTransform rect = null;
    Transform target = null;
    RectTransform canvas = null;


    public void Init()
    {
        //if (!photonView.IsMine) return;

        image = this.gameObject.GetComponent<Image>();
        rect = this.gameObject.GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();

        rect.parent = canvas;
    }

    public void SetPlayer(Transform _player)
    {
        target = _player;
    }


    public void SetFill(float fill)
    {
        if (!photonView.IsMine) return;

        image.fillAmount = fill;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;

        Vector2 pos;

        if (target == null)
        {
            int i = 0;
        }

        Vector2 screenPos = Vector2.zero;
        pos = screenPos;

        if (target != null)
        {
            screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, screenPos, Camera.main, out pos);
        }
         
        rect.localPosition = pos;
    }

    //定期的に行われる同期関数です
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 自身のアバターのスタミナを送信する
            stream.SendNext(image.fillAmount);
        }
        else
        {
            // 他プレイヤーのアバターのスタミナを受信する
            image.fillAmount = (float)stream.ReceiveNext();
        }
    }
}
