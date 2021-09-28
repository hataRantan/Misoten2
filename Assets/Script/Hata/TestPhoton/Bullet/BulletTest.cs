using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : MonoBehaviour
{
    private Vector3 velocity;

    // 弾のIDを返すプロパティ
    public int Id { get; private set; }
    // 弾を発射したプレイヤーのIDを返すプロパティ
    public int OwnerId { get; private set; }
    // 同じ弾かどうかをIDで判定するメソッド
    public bool Equals(int id, int ownerId) => id == Id && ownerId == OwnerId;

    public void Init(int id, int ownerId, Vector3 origin, float angle)
    {
        Id = id;
        OwnerId = ownerId;

        transform.position = origin;
        velocity = 9f * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    private void Update()
    {
        transform.Translate(velocity * Time.deltaTime);
    }

    // 画面外に移動したら削除する
    // （Unityのエディター上ではシーンビューの画面も影響するので注意）
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
