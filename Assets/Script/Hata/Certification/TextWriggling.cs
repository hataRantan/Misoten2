using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class TextWriggling : MonoBehaviour
{
    [SerializeField]
    private float distanceMove = 1;

    //[SerializeField]
    //private float animationSpeed = 1;

    private TextMeshProUGUI textMeshPro;

    List<Vector3[]> m_textVertices = new List<Vector3[]>();

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // メッシュ更新
        textMeshPro.ForceMeshUpdate();

        //テキストメッシュプロの情報
        var textInfo = textMeshPro.textInfo;

        //テキスト数がゼロであれば表示しない
        if (textInfo.characterCount == 0)
        {
            return;
        }

        //テキストの初期頂点位置を取得する
        for (int textCnt = 0; textCnt < textInfo.characterCount; textCnt++)
        {
            //1文字単位の情報
            var charaInfo = textInfo.characterInfo[textCnt];

            //ジオメトリない文字はスキップ
            if (!charaInfo.isVisible)
            {
                continue;
            }

            //Material参照しているindex取得
            int materialIndex = charaInfo.materialReferenceIndex;

            //頂点参照しているindex取得
            int vertexIndex = charaInfo.vertexIndex;

            m_textVertices.Add(textInfo.meshInfo[materialIndex].vertices);
        }
    }

    void Update()
    {
        // メッシュ更新
        textMeshPro.ForceMeshUpdate();

        //テキストメッシュプロの情報
        var textInfo = textMeshPro.textInfo;

        //テキスト数がゼロであれば表示しない
        if (textInfo.characterCount == 0)
        {
            return;
        }

        //1文字毎にloop
        for (int index = 0; index < textInfo.characterCount; index++)
        {
            //1文字単位の情報
            var charaInfo = textInfo.characterInfo[index];

            //ジオメトリない文字はスキップ
            if (!charaInfo.isVisible)
            {
                continue;
            }

            //Material参照しているindex取得
            int materialIndex = charaInfo.materialReferenceIndex;

            //頂点参照しているindex取得
            int vertexIndex = charaInfo.vertexIndex;

            //テキスト全体の頂点を格納(変数のdestは、destinationの略)
            Vector3[] destVertices = textInfo.meshInfo[materialIndex].vertices;


            for (int verCnt = 0; verCnt < 4; verCnt++)
            {
                var dir = Random.Range(0.0f, 360.0f) * Mathf.Deg2Rad;
                destVertices[vertexIndex + verCnt] = m_textVertices[index][vertexIndex + verCnt] + new Vector3(Mathf.Cos(dir) * distanceMove, Mathf.Sin(dir) * distanceMove, 0.0f);
            }

        }

        //ジオメトリ更新
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            //メッシュ情報を、実際のメッシュ頂点へ反映
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            textMeshPro.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }

    public void ResetStop()
    {
        // メッシュ更新
        textMeshPro.ForceMeshUpdate();

        //テキストメッシュプロの情報
        var textInfo = textMeshPro.textInfo;

        //テキスト数がゼロであれば表示しない
        if (textInfo.characterCount == 0)
        {
            return;
        }

        //1文字毎にloop
        for (int index = 0; index < textInfo.characterCount; index++)
        {
            //1文字単位の情報
            var charaInfo = textInfo.characterInfo[index];

            //ジオメトリない文字はスキップ
            if (!charaInfo.isVisible)
            {
                continue;
            }

            //Material参照しているindex取得
            int materialIndex = charaInfo.materialReferenceIndex;

            //頂点参照しているindex取得
            int vertexIndex = charaInfo.vertexIndex;

            //テキスト全体の頂点を格納(変数のdestは、destinationの略)
            Vector3[] destVertices = textInfo.meshInfo[materialIndex].vertices;


            for (int verCnt = 0; verCnt < 4; verCnt++)
            {
                var dir = Random.Range(0.0f, 360.0f) * Mathf.Deg2Rad;
                destVertices[vertexIndex + verCnt] = m_textVertices[index][vertexIndex + verCnt];
            }

        }

        //ジオメトリ更新
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            //メッシュ情報を、実際のメッシュ頂点へ反映
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            textMeshPro.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }

        enabled = false;
    }
}
