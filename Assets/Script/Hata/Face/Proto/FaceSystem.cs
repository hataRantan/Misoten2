using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO：シーン管理の処理が必要
public class FaceSystem : MonoBehaviour
{
    //顔認識クラス
    private Face.FaceRecognition facePhoto = null;
    //顔をトリミングしたSprite
    private Sprite faceSprite = null;

    [Header("トリミング画像を映すオブジェクト")]
    [SerializeField] SpriteRenderer faceRender = null;

    [Header("テクスチャを張るCube")]
    [SerializeField] GameObject cube = null;

    // Start is called before the first frame update
    void Start()
    {
        facePhoto = this.gameObject.GetComponent<Face.FaceRecognition>();

        //描画開始する
        facePhoto.RenderStart();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            faceSprite = facePhoto.FaceTrimming_Sprite();

            if(faceRender!=null)
            {
                faceRender.sprite = faceSprite;

                //https://mslgt.hatenablog.com/entry/2017/01/18/073342 を参考に
                cube.GetComponent<Renderer>().material.SetTexture("_MainTex", facePhoto.FaceTrimming_Tex2D());
            }
        }
    }
}
