using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_ItemAppear : MonoBehaviour
{
    [Header("アイテムの最下地点")]
    [SerializeField] Transform bottomPoint = null;

    [Header("出現時間")]
    [SerializeField] private float appearTime = 1.0f;
    //現在の出現経過時間
    float progressTime = 0.0f;

    //地面の高さ
    const float floorHeight = 0.0f;
    //最下地点と床の高さ
    float disFloor = 0.0f;

    //初期サイズ
    Vector3 firstSize = Vector3.zero;
    //最小サイズ
    Vector3 minSize = Vector3.zero;

    /// <summary>
    /// 出現演出の終了を知らすフラグ
    /// </summary>
    public bool isAppear { get; private set; }

    private void Awake()
    {
        ////出現時、最下地点と床の差を求める
        //disFloor = floorHeight - bottomPoint.position.y;
        //gameObject.transform.position += new Vector3(0.0f, disFloor, 0.0f);

        //現在のサイズを求める
        firstSize = gameObject.transform.localScale;

        gameObject.transform.localScale = minSize;

        isAppear = false;
    }

    private void Update()
    {
        //徐々に大きくする
        if (progressTime < appearTime)
        {
            progressTime += Time.deltaTime;

            //float scale = Easing.BackOut(progressTime, appearTime, 0.0f, firstSize.x, 1.5f);
            float x = Easing.BackOut(progressTime, appearTime, minSize.x, firstSize.x, 1.5f);
            float y = Easing.BackOut(progressTime, appearTime, minSize.x, firstSize.y, 1.5f);
            float z = Easing.BackOut(progressTime, appearTime, minSize.x, firstSize.z, 1.5f);

            gameObject.transform.localScale = new Vector3(x, y, z);

            AdjustBasePoint();
        }
        else
        {
            isAppear = true;
        }
    }

    /// <summary>
    /// bottomPointが床と同じ高さになるように調整する
    /// </summary>
    private void AdjustBasePoint()
    {
        disFloor = floorHeight - bottomPoint.position.y;
        gameObject.transform.position += new Vector3(0.0f, disFloor, 0.0f);
    }
}
