using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonTimeTest : MonoBehaviour
{
    int startTime;

    // Start is called before the first frame update
    void Start()
    {
        //startTime = PhotonNetwork.ServerTimestamp;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("サーバー開始からの時刻" + unchecked(PhotonNetwork.ServerTimestamp - startTime));
    }
}
