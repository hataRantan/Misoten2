using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RawImageのサイズを多少維持するクラス ＊Webカメラの影響でサイズが変更するため
/// </summary>
public class ReSizeImage : MonoBehaviour
{
    //操作対象
    private RectTransform m_rect = null;

    [Header("サイズの倍率")]
    [Range(1.0f, 3.0f)]
    [SerializeField] float size = 3.0f;

    //維持するサイズ
    private Vector2 m_targetSize = new Vector2(0, 0);

    //Webカメラの影響によるサイズ変更した際の大きさ
    private readonly Vector2 cameraSize = new Vector2(640, 480);

    private void Awake()
    {
        m_rect = this.gameObject.GetComponent<RectTransform>();

        m_targetSize = new Vector2(cameraSize.x * size, cameraSize.y * size);
    }

    // Update is called once per frame
    void Update()
    {
        //描画していないなら、変更不必要
        if (!gameObject.activeSelf) return;

        //サイズ変更があれば、初期のサイズに戻す
        if (!(Mathf.Approximately(m_rect.sizeDelta.x, m_targetSize.x)) || !(Mathf.Approximately(m_rect.sizeDelta.y, m_targetSize.y)))
        {
            m_rect.sizeDelta = m_targetSize;
        }
    }
}
