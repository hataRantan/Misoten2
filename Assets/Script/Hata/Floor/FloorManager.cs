using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MyUpdater
{
    [Header("床一枚の大きさ")]
    [SerializeField] float floorSize = 5;
    public float FloorSize { get { return floorSize; } }

    [Header("床の縦、横の枚数(2以上とすること)")]
    [SerializeField] Vector2Int floorNumber = new Vector2Int(5, 5);
    public Vector2Int FloorNumber { get { return floorNumber; } }

    [Header("生成するCube")]
    [SerializeField] GameObject createCube = null;

    [Header("生成する床の親")]
    [SerializeField] Transform floorParent = null;

    [Header("床のテクスチャ"), SerializeField]
    Material m_floorMat = null;

    [Header("生成する壁")]
    [SerializeField] GameObject createWall = null;

    [Header("生成する壁の親")]
    [SerializeField] Transform wallParent = null;

    [Header("生成する縦の柵")]
    [SerializeField] GameObject createFenceVer = null;

    [Header("生成する横の柵")]
    [SerializeField] GameObject createFenceBe = null;

    [Header("柵の親")]
    [SerializeField] Transform fenceParent = null;

    [Header("柵のマテリアル")]
    [SerializeField] Material fenceCobineMat = null;

    //生成したCube一覧
    //private List<GameObject> createdCubes;
    private GameObject[,] createdCubes;
    //ステージの大きさ
    public Vector2 StageSize { get; private set; }
    //ステージの端
    public Vector2 StageMinEdge { get; private set; }
    public Vector2 StageMaxEdge { get; private set; }
    //デバック用フラグ
    private bool isDebug = false;

    private enum CreateDirect
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }


#if isDebug
    Vector3[] kado;
#endif

    /// <summary>
    /// 地面作成
    /// </summary>
    public override void MyFastestInit()
    {
        ///-------------------------------------------------------
        /// ステージの床を自動生成
        ///-------------------------------------------------------

        //createdCubes = new List<GameObject>();
        createdCubes = new GameObject[floorNumber.x, floorNumber.y];

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
        //Transform parent = this.gameObject.transform;

        //親の位置
        Vector3 parentPos = this.gameObject.transform.position;

        for (int widthIdx = 0; widthIdx < floorNumber.x; widthIdx++)
        {
            createPos.x = widthFirst + widthIdx * floorSize + floorSize / 2 + parentPos.x;

            for (int heightIdx = 0; heightIdx < floorNumber.y; heightIdx++)
            {
                createPos.z = heightFirst + heightIdx * floorSize + floorSize / 2 + parentPos.z;

                //床生成クラスの生成
                GameObject createFloor = Instantiate(createCube, createPos, Quaternion.identity);
                
                ////床生成
                //createFloor.GetComponent<FloorCube>().CreateCube(floorSize);
                //Planeのサイズと生成するプレハブのサイズの差=25.0f
                float sizeRaito =  25.0f;
                //floorSize=15.0fの場合、Planeのサイズは1.5f
                float createdCubeSize = (floorSize / 10.0f) * sizeRaito;
                //サイズ変更
                createFloor.transform.localScale = new Vector3(createdCubeSize, createdCubeSize, createdCubeSize);
                //cubeの頂点を0.0fに合わせる
                Vector3 top = new Vector3(0.0f, createFloor.transform.GetChild(0).transform.position.y, 0.0f);
                createFloor.transform.position -= top;

                ////床を保存する
                createdCubes[widthIdx, heightIdx] = createFloor;

                //親を自身に設定
                createFloor.transform.parent = floorParent;
                //デバック用に名前を変更
                createFloor.name = "Floor：" + widthIdx.ToString() + "-" + heightIdx.ToString();
            }
        }

        floorParent.GetComponent<MeshRenderer>().CombineChildren(m_floorMat);
        floorParent.gameObject.AddComponent<BoxCollider>();

        //ステージの大きさを求める
        StageSize = new Vector2((createPos.x) - (widthFirst - floorSize / 2) - parentPos.x
                              , (createPos.z) - (heightFirst - floorSize / 2) - parentPos.z);

        //ステージの端を求める
        StageMinEdge = new Vector2(-StageSize.x / 2.0f, -StageSize.y / 2.0f);
        StageMaxEdge = -StageMinEdge;

        ///-------------------------------------------------------
        /// ステージの壁を自動生成
        ///-------------------------------------------------------

        //壁の高さ
        const float wallHigh = 100.0f;
        //壁の奥行
        const float wallInside = 1.0f;
        //左右の壁の大きさを求める
        Vector3 wallSize = new Vector3(StageSize.y, wallHigh, wallInside);

        //右の壁を生成
        CreateWall(new Vector3(StageSize.x / 2.0f + wallInside + parentPos.x, wallSize.y / 2.0f, 0.0f + parentPos.z), new Vector3(0.0f, 90.0f, 0), wallSize);
        //左の壁を生成
        CreateWall(new Vector3(-StageSize.x / 2.0f - wallInside + parentPos.x, wallSize.y / 2.0f, 0.0f + parentPos.z), new Vector3(0.0f, 90.0f, 0), wallSize);
        //上の壁を生成
        CreateWall(new Vector3(0.0f + parentPos.x, wallSize.y / 2.0f, -StageSize.y / 2.0f - wallInside + parentPos.z), new Vector3(0.0f, 0, 0), wallSize);
        //下の壁を生成
        CreateWall(new Vector3(0.0f + parentPos.x, wallSize.y / 2.0f, StageSize.y / 2.0f + wallInside + parentPos.z), new Vector3(0.0f, 0, 0), wallSize);


        ///-------------------------------------------------------
        /// ステージの柵を自動生成
        ///-------------------------------------------------------
        SetUpFence(parentPos);
        //メッシュを統合する
        fenceParent.GetComponent<MeshRenderer>().CombineChildren(fenceCobineMat);

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

    /// <summary>
    /// 壁生成
    /// </summary>
    /// <param name="_pos"> 生成位置 </param>
    /// <param name="_euler"> 生成の角度 </param>
    /// <param name="_size"> 生成のサイズ </param>
    private void CreateWall(Vector3 _pos, Vector3 _euler, Vector3 _size)
    {
        //生成
        GameObject wall = Instantiate(createWall, _pos, Quaternion.Euler(_euler));
        //サイズ変更
        wall.transform.localScale = _size;
        //親を設定
        wall.transform.parent = wallParent;
    }

    private void SetUpFence(Vector3 _parentPos)
    {
        //柵の距離感
        float fenceDiff = floorSize / 4.0f;
        //パネルに相対する柵の位置
        float floorOffset = floorSize / 2.0f;
        //柵の角度
        Vector3 fenceEuler = new Vector3(-90.0f, 0.0f, 0.0f);
       
        //柵のサイズ
        //床：15に対して200,300,300
        Vector3 verSize = new Vector3(13.3f, 20.0f, 20.0f) * floorSize;
        //床15に対して400,14,35
        Vector3 beSize = new Vector3(26.7f, 0.93f, 2.33f) * floorSize;
        //縦の柵の位置
        Vector3 verPos = Vector3.zero;
        //横の柵の位置
        Vector3 bePos = Vector3.zero;

        //床の位置
        int floorZ = (int)createdCubes.GetLongLength(1) - 1;
        //下の柵を作成
        for (int x = 0; x < createdCubes.GetLength(0); x++)
        {
            if (x != 0)
                CreateUpDownFence(createdCubes[x, floorZ].transform.position, true);
            else
                CreateUpDownFence(createdCubes[x, floorZ].transform.position, true, false);
        }
        //床の位置
        floorZ = 0;
        //下の柵を作成
        for (int x = 0; x < createdCubes.GetLength(0); x++)
        {
            if (x != 0)
                CreateUpDownFence(createdCubes[x, floorZ].transform.position, false);
            else
                CreateUpDownFence(createdCubes[x, floorZ].transform.position, false, false);
        }

      

        fenceEuler = new Vector3(-90.0f, 90.0f, 0.0f);

        int floorX = createdCubes.GetLength(1) - 1;
        for (int z = 0; z < createdCubes.GetLength(1); z++)
        {
            if (z != 0)
                CreateLeftRightFence(createdCubes[floorX, z].transform.position, true);
            else
                CreateLeftRightFence(createdCubes[floorX, z].transform.position, true, false);
        }

        floorX = 0;
        for (int z = 0; z < createdCubes.GetLength(1); z++)
        {
            if (z != 0)
                CreateLeftRightFence(createdCubes[floorX, z].transform.position, false);
            else
                CreateLeftRightFence(createdCubes[floorX, z].transform.position, false, false);
        }


        //上と下の柵の作成の場合
        void CreateUpDownFence(Vector3 _floorPos, bool _isDown, bool _isCreateBe = true)
        {
            //高さをリセット
            _floorPos.y = _parentPos.y;
            //左側の縦柵の位置
            if (_isDown)
                verPos = _floorPos + new Vector3(-fenceDiff, 0.0f, floorOffset);
            else
                verPos = _floorPos + new Vector3(-fenceDiff, 0.0f, -floorOffset);
            //左側の柵を作成
            float floorY = CreateVerFence(verPos, fenceEuler).transform.position.y;
            //右側の縦柵の位置
            if (_isDown)
                verPos = _floorPos + new Vector3(fenceDiff, 0.0f, floorOffset);
            else
                verPos = _floorPos + new Vector3(fenceDiff, 0.0f, -floorOffset);

            //右側の柵を作成し、横柵の作成の位置を取得する
            FencePos bePoses = CreateVerFence(verPos, fenceEuler).GetComponent<FencePos>();

            //上の横柵の位置
            if (_isDown)
                bePos = _floorPos + new Vector3(0.0f, bePoses.GetFirstPos.y, floorOffset);
            else
                bePos = _floorPos + new Vector3(0.0f, bePoses.GetFirstPos.y, -floorOffset);

            //上の横柵の作成
            CreateBeFence(bePos, fenceEuler);
            //下の横柵の位置
            if (_isDown)
                bePos = _floorPos + new Vector3(0.0f, bePoses.GetSecondPos.y, floorOffset);
            else
                bePos = _floorPos + new Vector3(0.0f, bePoses.GetSecondPos.y, -floorOffset);

            //下の横柵の作成
            CreateBeFence(bePos, fenceEuler);

            //横は最低二つ、最高四つ
            if (_isCreateBe)
            {
                //上の横柵の位置
                if (_isDown)
                    bePos = _floorPos + new Vector3(-floorOffset, bePoses.GetFirstPos.y , floorOffset);
                else
                    bePos = _floorPos + new Vector3(-floorOffset, bePoses.GetFirstPos.y , -floorOffset);

                //上の横柵の作成
                CreateBeFence(bePos, fenceEuler);
                //下の横柵の位置
                if (_isDown)
                    bePos = _floorPos + new Vector3(-floorOffset, bePoses.GetSecondPos.y , floorOffset);
                else
                    bePos = _floorPos + new Vector3(-floorOffset, bePoses.GetSecondPos.y , -floorOffset);

                //下の横柵の作成
                CreateBeFence(bePos, fenceEuler);
            }
        }

        //左と右の柵の作成
        void CreateLeftRightFence(Vector3 _floorPos, bool _isLeft, bool _isCreateBe = true)
        {
            //高さをリセット
            _floorPos.y = _parentPos.y;

            //上側の縦柵の位置
            if (_isLeft)
                verPos = _floorPos + new Vector3(floorOffset, 0.0f, fenceDiff);
            else
                verPos = _floorPos + new Vector3(-floorOffset, 0.0f, fenceDiff);
            //左側の柵を作成
            float floorY = CreateVerFence(verPos, fenceEuler).transform.position.y;
            //右側の縦柵の位置
            if (_isLeft)
                verPos = _floorPos + new Vector3(floorOffset, 0.0f, -fenceDiff);
            else
                verPos = _floorPos + new Vector3(-floorOffset, 0.0f, -fenceDiff);

            //右側の柵を作成し、横柵の作成の位置を取得する
            FencePos bePoses = CreateVerFence(verPos, fenceEuler).GetComponent<FencePos>();

            //上の横柵の位置
            if (_isLeft)
                bePos = _floorPos + new Vector3(floorOffset, bePoses.GetFirstPos.y , 0.0f);
            else
                bePos = _floorPos + new Vector3(-floorOffset, bePoses.GetFirstPos.y, 0.0f);

            //上の横柵の作成
            CreateBeFence(bePos, fenceEuler);
            //下の横柵の位置
            if (_isLeft)
                bePos = _floorPos + new Vector3(floorOffset, bePoses.GetSecondPos.y , 0.0f);
            else
                bePos = _floorPos + new Vector3(-floorOffset, bePoses.GetSecondPos.y, 0.0f);

            //下の横柵の作成
            CreateBeFence(bePos, fenceEuler);

            //横は最低二つ、最高四つ
            if (_isCreateBe)
            {
                //上の横柵の位置
                if (_isLeft)
                    bePos = _floorPos + new Vector3(floorOffset, bePoses.GetFirstPos.y  , -floorOffset);
                else
                    bePos = _floorPos + new Vector3(-floorOffset, bePoses.GetFirstPos.y , -floorOffset);

                //上の横柵の作成
                CreateBeFence(bePos, fenceEuler);
                //下の横柵の位置
                if (_isLeft)
                    bePos = _floorPos + new Vector3(floorOffset, bePoses.GetSecondPos.y  , -floorOffset);
                else
                    bePos = _floorPos + new Vector3(-floorOffset, bePoses.GetSecondPos.y , -floorOffset);

                //下の横柵の作成
                CreateBeFence(bePos, fenceEuler);
            }
        }

        //縦の柵を作成
        GameObject CreateVerFence(Vector3 _pos,Vector3 _euler)
        {
            //縦の柵を作成
            GameObject fenceVer = Instantiate(createFenceVer, _pos, Quaternion.Euler(_euler));
            //大きさ変更
            fenceVer.transform.localScale = verSize;
            //親を指定(ヒエラルキーがすっきりするように)
            fenceVer.transform.parent = fenceParent;
            //柵の高さを知る
            Vector3 bottomPos = fenceVer.transform.GetChild(0).transform.position;
            //床の高さとボトムポイントの差分を求める
            float diff = _pos.y - bottomPos.y;
            //縦の柵の高さを変更
            //verPos.y += diff;
            //fenceVer.transform.position = verPos;
            _pos.y += diff;
            fenceVer.transform.position = _pos;

            return fenceVer;
        }

        //横の柵を作成
        void CreateBeFence(Vector3 _pos, Vector3 _euler)
        {
            //横の柵を作成
            GameObject fenceBe = Instantiate(createFenceBe, _pos, Quaternion.Euler(_euler));
            //大きさ変更
            fenceBe.transform.localScale = beSize;
            //親を指定(ヒエラルキーがすっきりするように)
            fenceBe.transform.parent = fenceParent;
        }
    }


    public override void MyUpdate() { }

    /// <summary>
    /// 指定の番号の床の四隅の位置を知る 0:左下、 1:右下、2:左上、3:右上
    /// </summary>
    public Vector3 GetFourCornersPos(int _cornerNum)
    {
        //戻り値
        Vector3 rePos = Vector3.zero;

        switch(_cornerNum)
        {
            case 0://左下
                rePos = createdCubes[createdCubes.GetLength(0) - 1, createdCubes.GetLength(1) - 1].transform.position;
                break;

            case 1://右下
                rePos = createdCubes[0, createdCubes.GetLength(1) - 1].transform.position;
                break;

            case 2://左上
                rePos = createdCubes[createdCubes.GetLength(0) - 1, 0].transform.position;
                break;
         
            case 3://右上
                rePos = createdCubes[0, 0].transform.position;
                break;

            default:
                Debug.LogError("四隅以外の位置を指定している");
                break;
        }

        return rePos;
    }


    /// <summary>
    /// 床の個数を返す
    /// </summary>
    public int GetFloorLength(int _num)
    {
        //0~1に制限
        _num = Mathf.Clamp(_num, 0, 1);

        return createdCubes.GetLength(_num);
    }

    /// <summary>
    /// 指定の添え字番号から床の位置を渡す
    /// </summary>
    /// <param name="_pos">検索したい添え字番号</param>
    public Vector3 GetFloorPos(Vector2Int _pos)
    {
        //添え字の番号に制限
        _pos.x = Mathf.Clamp(_pos.x, 0, createdCubes.GetLength(0) - 1);
        _pos.y = Mathf.Clamp(_pos.y, 0, createdCubes.GetLength(1) - 1);

        return createdCubes[_pos.x, _pos.y].transform.position;
    }

    /// <summary>
    /// 指定の位置は、何番目の床の上なのかを渡す
    /// </summary>
    public Vector2Int GetFloorNumber(Vector3 _pos)
    {
        //戻り値
        Vector2Int reSub = Vector2Int.zero;

        //どの床の上か探す
        for (int width = 0; width < createdCubes.GetLength(0); width++)
        {
            for (int height = 0; height < createdCubes.GetLength(1); height++)
            {
                //床の位置を取得
                Vector3 cubePos = createdCubes[width, height].transform.position;
                float offset = floorSize / 2;

                if (_pos.x >= cubePos.x - offset && _pos.x <= cubePos.x + offset
                    && _pos.z >= cubePos.z - offset && _pos.z <= cubePos.z + offset)
                {
                    
                    reSub.x = width;
                    reSub.y = height;

                    break;
                }
            }
        }

        return reSub;
    }
}
