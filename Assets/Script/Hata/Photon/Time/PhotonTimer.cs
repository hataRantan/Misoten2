using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// サーバー時刻関係のクラス
/// </summary>
public class PhotonTimer : MonoBehaviourPunCallbacks
{
    //-----------------------------------------------------
    //経過時間取得のための変数
    //-----------------------------------------------------

    //計測開始時間
    private float startTime = 0.0f;
    //求めた経過時間
    //public float ElapsedTime { get { return Mathf.Max(0.0f, unchecked(PhotonNetwork.ServerTimestamp - startTime) / 1000.0f); } }
}


