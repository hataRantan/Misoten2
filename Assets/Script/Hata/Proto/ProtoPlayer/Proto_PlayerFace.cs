using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_PlayerFace : MonoBehaviour
{
    [Header("スプライトを入れる対象")]
    [SerializeField] GameObject faceObj = null;

    public void SetFace(Texture2D _face)
    {
        if (faceObj == null) return;

        //https://mslgt.hatenablog.com/entry/2017/01/18/073342 を参考に
        faceObj.GetComponent<Renderer>().material.SetTexture("_MainTex", _face);

        //テクスチャが上下逆様になるので、scaleに-1を掛けて反転させる
        var scale = gameObject.transform.localScale;
        scale.x *= -1;
        scale.y *= -1;
        gameObject.transform.localScale = scale;
    }
}
