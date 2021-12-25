using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

//マスターが部屋を立てる必要がある最初にルームに参加したものをマスターとする
//完全オンライン化するためにいくつかのサーバーを選択できるように参加するルームをセットしておく。
public class LobbyTest : MonoBehaviourPunCallbacks
{
    [Header("1ルームの最大人数")]
    [SerializeField] private int maxRoomMenber = 4;

    [Header("trueなら、ルームがロビーに登録される")]
    [SerializeField] private bool isVisible = true;

    [Header("選択するルームボタン")]
    [SerializeField] List<GameObject> roomButtons = new List<GameObject>();

    [Header("ルーム名")]
    [SerializeField] List<string> roomNames = new List<string>();

    private readonly string[] roomName = { "Room1", "Room2", "Room3", "Room4", "Room5" };

    //ルーム情報取得クラス
    private RoomList rooms = new RoomList();


    private void Start()
    {

    }


    /// <summary>
    /// ロビー入室時の処理
    /// </summary>
    public override void OnJoinedLobby()
    {
        rooms.Clear();
    }

    /// <summary>
    /// マスターサーバーのロビーにいる時に、部屋情報が更新されれば呼び出される
    /// </summary>
    /// <param name="changedRoomList"></param>
    public override void OnRoomListUpdate(List<RoomInfo> _changedRoomList)
    {
        rooms.Update(_changedRoomList);


        //ここでルーム情報の反映を行う
    }

    /// <summary>
    /// ロビー退出時の処理
    /// </summary>
    public override void OnLeftLobby()
    {
        rooms.Clear();
    }

    /// <summary>
    /// ルームに入室時の処理
    /// </summary>
    public override void OnJoinedRoom()
    { 
        //ToDo：参加者が全員来れば、シーン遷移等必要
        //プロトタイプ版では必要なし
    }

 
}
