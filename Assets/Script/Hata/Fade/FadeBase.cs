using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フェード処理の基底クラス
/// </summary>
public abstract class FadeBase : MonoBehaviour
{
    public void Awake()
    {
        //メインカメラを設定する
        canvas = this.gameObject.GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;

        //キャンバスとの距離を近づける
        canvas.planeDistance = 1.0f;
    }
    //処理するキャンバス
    protected Canvas canvas = null;

    public abstract IEnumerator FadeIn();

    public abstract IEnumerator FadeOut();
}
