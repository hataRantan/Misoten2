using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class SimplePun : MonoBehaviourPunCallbacks
{
    private PrefubTestNet test = null;

    [SerializeField]
    GameObject Player = null;

    [SerializeField]
    GameObject imageTest = null;

    GameObject testObj = null;

    //[Header("テストCube")]
    //[SerializeField] GameObject cube = null;

    // Use this for initialization
    void Start()
    {
        //旧バージョンでは引数必須でしたが、PUN2では不要です。
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();

        test = gameObject.GetComponent<PrefubTestNet>();
    }

    void OnGUI()
    {
        //ログインの状態を画面上に出力
        //GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
        //Debug.Log("ログイン状態：" + PhotonNetwork.NetworkClientState.ToString());
    }


    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    //ここで、ルームを作成したりする
    //ルームに入室前に呼び出される
    public override void OnConnectedToMaster()
    {
        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック
    //同じゲームサーバー（ルーム）のメンバーとのみ通信可能
    //ルームに入室後に呼び出される
    public override void OnJoinedRoom()
    {
        //プレイヤーのニックネームを変更
        //ToDo：プレイヤーごとに共通なため、変更必要だと思われる
        PhotonNetwork.NickName = "My";

        //キャラクターを生成
        //第一引数のプレハブは、Resources下にある必要があり
        //GameObject monster = PhotonNetwork.Instantiate("Hata/Photon/Cube", Vector3.zero, Quaternion.identity, 0);
        //PhotonNetwork.Instantiate("none", Vector3.zero, Quaternion.identity);

        //GameObject image = PUN2Creater.Instance.CreateNetworkObj(imageTest, Vector3.zero, Quaternion.identity);

        GameObject cube = PhotonNetwork.Instantiate(Player.name, Vector3.zero, Quaternion.identity);
        GameObject image = PhotonNetwork.Instantiate(imageTest.name, Vector3.zero, Quaternion.identity);

        cube.GetComponent<TestInput>().SetUI(image.GetComponent<PhotonUITest>());
        image.GetComponent<PhotonUITest>().SetPlayer(cube.transform);

        // オブジェクト同期の頻度を調整する
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            PhotonNetwork.SendRate = 30; // 1秒間にメッセージ送信を行う回数
            PhotonNetwork.SerializationRate = 30; // 1秒間にオブジェクト同期を行う回数
        }
    }

    public void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Return))
        //{
        //    PUN2Creater.Instance.Destroy(testObj);
        //}
        //if(Input.GetKeyDown(KeyCode.L))
        //{
        //    PUN2Creater.Instance.CreateNetworkObj(Player, Vector3.zero, Quaternion.identity);
        //}
    }
}