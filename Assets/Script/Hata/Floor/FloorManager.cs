using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MyUpdater
{
    [Header("床一枚の大きさ")]
    [SerializeField] float floorSize = 5;

    [Header("床の縦、横の枚数")]
    [SerializeField] Vector2 floorNumber = new Vector2(5, 5);

    [Header("生成するCube")]
    [SerializeField] GameObject createCube = null;

    //生成したCube一覧
    //private List<GameObject> createdCubes;
    private GameObject[,] createdCubes;

    //ステージの大きさ
    public Vector2 StageSize { get; private set; }
    //ステージの端
    public Vector2 StageMinEdge { get; private set; }
    public Vector2 StageMaxEdge { get; private set; }

    private bool isDebug = false;

#if isDebug
    Vector3[] kado;
#endif

    public override void MyFastestInit()
    {
        //createdCubes = new List<GameObject>();
        createdCubes = new GameObject[(int)floorNumber.x, (int)floorNumber.y];

        //横の枚数の半分の値を求める
        float widthHalf = floorNumber.x / 2.0f;
        //横の開始位置を求める
        float widthFirst = widthHalf * -floorSize;

        //縦の枚数の半分の値を求める
        float heightHalf = floorNumber.y / 2.0f;
        //縦の開始位置を求める
        float heightFirst = heightHalf * -floorSize;

        //生成位置
        Vector3 createPos = new Vector3(widthFirst, 0.0f, heightFirst);
        //生成した床に設定する親を取得
        Transform parent = this.gameObject.transform;

        for (int widthIdx = 0; widthIdx < (int)floorNumber.x; widthIdx++)
        {
            createPos.x = widthFirst + widthIdx * floorSize + floorSize / 2;

            for (int heightIdx = 0; heightIdx < (int)floorNumber.y; heightIdx++)
            {
                createPos.z = heightFirst + heightIdx * floorSize + floorSize / 2;

                //床生成クラスの生成
                GameObject createFloor = Instantiate(createCube, createPos, Quaternion.identity);
                //床生成
                createFloor.GetComponent<FloorCube>().CreateCube(floorSize);
                //床を保存する
                createdCubes[widthIdx, heightIdx] = createFloor;

                //親を自身に設定
                createFloor.transform.parent = parent;
                //デバック用に名前を変更
                createFloor.name = "Floor：" + widthIdx.ToString() + "-" + heightIdx.ToString();
            }
        }

        
        //ステージの大きさを求める
        StageSize = new Vector2((createPos.x) - (widthFirst - floorSize / 2)
                              , (createPos.z) - (heightFirst - floorSize / 2));

        //ステージの端を求める
        StageMinEdge = new Vector2(-StageSize.x / 2.0f, -StageSize.y / 2.0f);
        StageMaxEdge = -StageMinEdge;

#if isDebug
        kado = new Vector3[4];
        kado[0] = new Vector3(StageSize.x / 2.0f, 0.0f, -StageSize.y / 2.0f);
        kado[1] = new Vector3(StageSize.x / 2.0f, 0.0f, StageSize.y / 2.0f);
        kado[2] = new Vector3(-StageSize.x / 2.0f, 0.0f, StageSize.y / 2.0f);
        kado[3] = new Vector3(-StageSize.x / 2.0f, 0.0f, -StageSize.y / 2.0f);
#endif
        //Updateの更新不必要
        m_isUpdate = false;
    }

#if isDebug
    private void OnDrawGizmos()
    {
        if (kado == null) return;

        Gizmos.color = Color.red;

        for (int idx = 0; idx < kado.Length; idx++)
        {
            if (idx != kado.Length - 1)
            {
                Gizmos.DrawLine(kado[idx], kado[idx + 1]);
            }
            else
            {
                Gizmos.DrawLine(kado[idx], kado[0]);
            }
        }
    }
#endif

    public override void MyUpdate() { }
}
