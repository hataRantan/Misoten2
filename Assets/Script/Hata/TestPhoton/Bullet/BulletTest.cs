using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletTest : MonoBehaviour
{
    private Vector3 velocity;

    //弾を発射した時刻の座標
    private Vector3 originPos = Vector3.zero;
    //弾を発射した時刻
    private int timeStamp;

    // 弾のIDを返すプロパティ
    public int Id { get; private set; }
    // 弾を発射したプレイヤーのIDを返すプロパティ
    public int OwnerId { get; private set; }
    // 同じ弾かどうかをIDで判定するメソッド
    public bool Equals(int id, int ownerId) => id == Id && ownerId == OwnerId;

    public void Init(int id, int ownerId, Vector3 origin, float angle, int timeStamp)
    {
        //生成者のIDを代入
        Id = id;
        OwnerId = ownerId;

        this.originPos = origin;

        transform.position = origin;
        velocity = 9f * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));

        this.timeStamp = timeStamp;
    }

    private void Update()
    {
        // 弾を発射した時刻から現在時刻までの経過時間を求める
        float elapsedTime = Mathf.Max(0f, unchecked(PhotonNetwork.ServerTimestamp - timeStamp) / 1000f);
        // 弾を発射した時刻での座標・速度・経過時間から現在の座標を求める
        transform.position = originPos + velocity * elapsedTime;
        
        //transform.Translate(velocity * Time.deltaTime);
    }

    // 画面外に移動したら削除する
    // （Unityのエディター上ではシーンビューの画面も影響するので注意）
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
