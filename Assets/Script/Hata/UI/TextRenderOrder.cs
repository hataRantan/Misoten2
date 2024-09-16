using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextRenderOrder : MonoBehaviour
{
    [Header("テキストの描画順")]
    [SerializeField] int sortOrder = 4;

    MeshRenderer renderer = null;

    private void Awake()
    {
        renderer = gameObject.GetComponent<MeshRenderer>();

        ChangerOrder(sortOrder);
    }

    /// <summary>
    /// 描画順の変更
    /// </summary>
    public void ChangerOrder(int _sortOrder)
    {
        renderer.sortingOrder = _sortOrder;
    }
}
