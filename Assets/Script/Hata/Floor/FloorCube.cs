using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 床のCubeの大きさ変更 
// ToDo：メッシュ統合をして軽量化を図る
/// </summary>
public class FloorCube : MonoBehaviour
{
    [Header("上面に張るマテリアル")]
    [SerializeField] Material topMaterial = null;

    [Header("側面に張るマテリアル")]
    [SerializeField] Material sideMaterial = null;

    [Header("底面に張るテクスチャ")]
    [SerializeField] Material bottomMaterial = null;

    //[Header("テスト")]
    //[SerializeField] float planeSize = 1.0f;

    //生成したPlane
    private GameObject[] planes;

    /// <summary>
    /// PlaneでCube生成
    /// </summary>
    /// <param name="_pos"> 生成場所 </param>

    public void CreateCube(float _size)
    {

        const int createNum = 6;

        //必要分のゲームオブジェクトを作成
        planes = new GameObject[createNum];

        //生成位置
        Vector3 thisPos = gameObject.transform.position;

        //Topを生成
        planes[0] = CreatePlane(thisPos, new Vector3(0, 0, 0), _size, topMaterial, true);
        //側面(正面)を生成
        planes[1] = CreatePlane(thisPos + new Vector3(0.0f, -_size / 2, _size / 2), new Vector3(90.0f, 0, 0), _size, sideMaterial);
        //側面（左）を生成
        planes[2] = CreatePlane(thisPos + new Vector3(-_size / 2, -_size / 2, 0.0f), new Vector3(90.0f, 90.0f, 180.0f), _size, sideMaterial);
        //側面（右）を生成
        planes[3] = CreatePlane(thisPos + new Vector3(_size/2.0f, -_size / 2, 0.0f), new Vector3(90.0f, -90.0f, 180.0f), _size, sideMaterial);
        //側面（後ろ）を生成
        planes[4] = CreatePlane(thisPos + new Vector3(0.0f, -_size / 2, -_size / 2), new Vector3(90.0f, 180.0f, 0.0f), _size, sideMaterial);
        //底面を生成
        planes[5] = CreatePlane(thisPos + new Vector3(0.0f, -_size, 0.0f), new Vector3(0.0f, 0.0f, 180.0f), _size, bottomMaterial);
    }

   /// <summary>
   /// Planeを生成
   /// </summary>
   /// <param name="_pos"> 生成したい位置 </param>
   /// <param name="_euler"> 生成したい角度（オイラー）</param>
   /// <param name="_size"> Planeの縦と横の幅 </param>
   /// <param name="_mat"> 張り付けるマテリアル </param>
   /// <param name="isHit"> trueなら衝突判定を用いる </param>
    private GameObject CreatePlane(Vector3 _pos, Vector3 _euler, float _size, Material _mat, bool isHit = false)
    {
        //plane生成
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);

        Renderer render = plane.GetComponent<Renderer>();
        Transform transform = plane.transform;

        //マテリアルの変更
        render.material = _mat;
        
        //サイズの変更
        Vector2 currentSize = render.bounds.size;
        float raito = _size / currentSize.x;
        transform.localScale = new Vector3(raito, 1.0f, raito);

        //位置変更
        transform.position = _pos;
        //角度変更
        transform.rotation = Quaternion.Euler(_euler);

        //自身を親に設定
        transform.parent = gameObject.transform;

        //衝突判定が不必要なので削除
        if(!isHit)
        {
            Destroy(plane.GetComponent<MeshCollider>());
        }
        else
        {
            //レイヤー変更
            plane.layer = LayerMask.NameToLayer("Floor");
        }

        return plane;
    }
}
