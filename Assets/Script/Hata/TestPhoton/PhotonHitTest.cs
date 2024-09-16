using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonHitTest : MonoBehaviourPunCallbacks
{
    private void OnTriggerEnter(Collider other)
    {
        if(photonView.IsMine)
        {
            if(other.TryGetComponent<BulletTest>(out var bullet))
            {
                if (bullet.OwnerId != PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    photonView.RPC(nameof(HitBullet), RpcTarget.All, bullet.Id, bullet.OwnerId);
                }
            }
        }
    }

    [PunRPC]
    private void HitBullet(int id,int ownerId)
    {
        var bullets = FindObjectsOfType<BulletTest>();
        foreach (var bullet in bullets)
        {
            if (bullet.Equals(id, ownerId))
            {
                // 自身が発射した弾が当たった場合には、自身のスコアを増やす
                if (ownerId==PhotonNetwork.LocalPlayer.ActorNumber)
                {
                    PhotonNetwork.LocalPlayer.AddScore(10);
                }

                Destroy(bullet.gameObject);
                Debug.LogError("壊れたよ");
                break;
            }
        }
    }
}

