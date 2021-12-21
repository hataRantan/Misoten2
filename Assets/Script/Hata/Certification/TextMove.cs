using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMove : MonoBehaviour
{
    private TMPro.TextMeshProUGUI m_text = null;

    private void Awake()
    {
        m_text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    float maxRotation = 360.0f;

    // Update is called once per frame
    void Update()
    {
        // メッシュ更新
        m_text.ForceMeshUpdate();

        var textInfo = m_text.textInfo;
        
        //文字数の確認
        if (textInfo.characterCount == 0)
        {
            return;
        }

        //文字毎にloop
        for (int index = 0; index < textInfo.characterCount; index++)
        {
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

            //頂点(dest->destinationの略)
            Vector3[] destVertices = textInfo.meshInfo[materialIndex].vertices;

            float angle = maxRotation * Mathf.Sin(Time.time + 10 * index);

            Vector3 rotatedCenterVertex = (destVertices[vertexIndex + 1] + destVertices[vertexIndex + 2]) / 2;

            Vector3 offset = rotatedCenterVertex;
            destVertices[vertexIndex + 0] += -offset;
            destVertices[vertexIndex + 1] += -offset;
            destVertices[vertexIndex + 2] += -offset;
            destVertices[vertexIndex + 3] += -offset;


            //回転
            Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, 0, angle), Vector3.one);

            destVertices[vertexIndex + 0] = matrix.MultiplyPoint3x4(destVertices[vertexIndex + 0]);
            destVertices[vertexIndex + 1] = matrix.MultiplyPoint3x4(destVertices[vertexIndex + 1]);
            destVertices[vertexIndex + 2] = matrix.MultiplyPoint3x4(destVertices[vertexIndex + 2]);
            destVertices[vertexIndex + 3] = matrix.MultiplyPoint3x4(destVertices[vertexIndex + 3]);

            destVertices[vertexIndex + 0] += offset;
            destVertices[vertexIndex + 1] += offset;
            destVertices[vertexIndex + 2] += offset;
            destVertices[vertexIndex + 3] += offset;
        }

        //ジオメトリ更新
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            //メッシュ情報
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            m_text.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
}
