using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public static class FixedSizeImage
{
    /// <summary>
    /// テクスチャが変更されてもRawImageのサイズを指定のサイズに変更する関数
    /// </summary>
    /// <param name="_rawImage"> 呼び出し元 </param>
    /// <param name="_oroginSize"> 変更後のサイズ：（初期サイズにしたいなら、rawImage.rectTransform.rect.size）</param>
    public static void FixedSize(this RawImage _rawImage, Vector3 _oroginSize)
    {
        var textureSize = new Vector2(_rawImage.texture.width, _rawImage.texture.height);

        var heightScale = _oroginSize.y / textureSize.y;
        var widthScale = _oroginSize.x / textureSize.x;
        var rectSize = textureSize * Mathf.Min(heightScale, widthScale);

        var anchorDiff = _rawImage.rectTransform.anchorMax - _rawImage.rectTransform.anchorMin;
        var parentSize = (_rawImage.transform.parent as RectTransform).rect.size;
        var anchorSize = parentSize * anchorDiff;

        _rawImage.rectTransform.sizeDelta = rectSize - anchorSize;
    }

    public static void FixedSize(this Image _image,Vector3 _originSize)
    {
        var textureSize = new Vector2(_image.sprite.rect.width, _image.sprite.rect.height);

        var heightScale = _originSize.y / textureSize.y;
        var widthScale = _originSize.x / textureSize.x;
        var rectSize = textureSize * Mathf.Min(heightScale, widthScale);

        var anchorDiff = _image.rectTransform.anchorMax - _image.rectTransform.anchorMin;
        var parentSize = (_image.transform.parent as RectTransform).rect.size;
        var anchorSize = parentSize * anchorDiff;

        _image.rectTransform.sizeDelta = rectSize - anchorSize;
    }

}
