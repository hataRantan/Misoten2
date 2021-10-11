using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class RoomButton : MonoBehaviour
{
    [Header("ボタンのImage")]
    [SerializeField] Image buttonImage = null;

    [Header("ルーム名")]
    [SerializeField] TextMeshProUGUI roomName = null;

    [Header("人数表示")]
    [SerializeField] TextMeshProUGUI menber = null;


    /// <summary>
    /// ルーム情報の更新
    /// </summary>
    /// <param name="_roomName"> 部屋の名前 </param>
    /// <param name="_roomMenber"> 部屋にいる人数 </param>
    /// <param name="_maxRoomMenber"> 部屋の最大人数 </param>
    public void SetRoomInfo(string _roomName,int _roomMenber,int _maxRoomMenber)
    {

    }
}
