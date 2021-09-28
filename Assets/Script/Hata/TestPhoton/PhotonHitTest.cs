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
                Destroy(bullet.gameObject);
                Debug.LogError("壊れたよ");
                break;
            }
        }
    }
}

