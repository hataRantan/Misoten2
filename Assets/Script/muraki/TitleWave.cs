using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TitleWave : MonoBehaviour
{
    [SerializeField]
    private float m_waveMove = 1;

    [Header("文字ごとの移動開始遅延時間")]
    [Range(0.0f, 2.0f)]
    [SerializeField]
    float m_textDelayTime = 0.5f;

    [Header("上下に掛かる時間")]
    [SerializeField]
    float m_waveTime = 2.0f;

    float timer = 0.0f;

    private TextMeshProUGUI textMeshPro;

    List<Vector3[]> m_textVertices = new List<Vector3[]>();

    private void Awake()
    {
        timer = 0.0f;

        textMeshPro = GetComponent<TextMeshProUGUI>();
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

        timer += Time.deltaTime;

        //1文字毎にloop
        for (int index = 0; index < textInfo.characterCount; index++)
        {
            //開始を遅延する
            if (timer < index * m_textDelayTime) continue;

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

            //0~1に変更
            float wave = Mathf.Sin(2 * Mathf.PI * (1.0f / m_waveTime) * (timer - index * m_textDelayTime)) / 2.0f + 0.5f;

            for (int verCnt = 0; verCnt < 4; verCnt++)
            {
                destVertices[vertexIndex + verCnt] += new Vector3(0.0f, wave * m_waveMove, 0.0f);
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
}
