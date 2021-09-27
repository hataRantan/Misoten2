using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PhotonUITest : MonoBehaviour, IPunObservable
{
    Image image = null;
    RectTransform rect = null;
    public Transform target = null;
    RectTransform canvas = null;


    public void Init()
    {
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
        image.fillAmount = fill;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos;

       
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, screenPos, Camera.main, out pos);

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
