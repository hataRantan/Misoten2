using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshCombine
{
    public static void CombineChildren(this MeshRenderer _thisRender, Material _setMate)
    {
        //メッシュフィルターを取得する
        MeshFilter thisFilter = _thisRender.gameObject.GetComponent<MeshFilter>();

        //メッシュフィルターがないなら処理しない
        if (thisFilter == null) return;

        //子オブジェクトのメッシュフィルター
        MeshFilter[] childrenMeshes = _thisRender.gameObject.GetComponentsInChildren<MeshFilter>();
        //子オブジェクトのメッシュフィルターの入れ物
        List<MeshFilter> mesheList = new List<MeshFilter>();

        for (int child = 0; child < childrenMeshes.Length; child++)
        {
            mesheList.Add(childrenMeshes[child]);
        }

        //統合するメッシュの配列
        CombineInstance[] combine = new CombineInstance[mesheList.Count];

        for (int child = 0; child < mesheList.Count; child++)
        {
            combine[child].mesh = mesheList[child].sharedMesh;
            combine[child].transform = mesheList[child].transform.localToWorldMatrix;
            //combine[child].transform = Matrix4x4.Translate(mesheList[child].transform.position);
            mesheList[child].gameObject.SetActive(false);
        }

        //統合したメッシュをセットする
        thisFilter.mesh = new Mesh();
        thisFilter.mesh.CombineMeshes(combine);

        //親オブジェクトが設定されているとズレてしまうので,位置ずれを回避
        thisFilter.transform.position = Vector3.zero;

        //オブジェクト再開
        _thisRender.gameObject.SetActive(true);

        //統合したメッシュにマテリアルをセットする
        _thisRender.material = _setMate;
       
    }
}
