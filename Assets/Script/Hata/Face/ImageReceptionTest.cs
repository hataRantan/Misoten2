using System;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;

public class ImageReceptionTest : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer target = null;


    public void Test(ref Texture2D _face)
    {
        //画像の読み込み
        Mat mat = OpenCvSharp.Unity.TextureToMat(_face);

        //グレースケール
        Mat gray = new Mat();
        Cv2.CvtColor(mat, gray, ColorConversionCodes.BGR2GRAY);

        // カスケード分類器の準備
        CascadeClassifier haarCascade = new CascadeClassifier("Assets/OpenCV+Unity/Demo/Face_Detector/haarcascade_frontalface_default.xml");

        // 顔検出
        OpenCvSharp.Rect[] faces = haarCascade.DetectMultiScale(gray);

        // 顔の位置を描画
        //foreach (OpenCvSharp.Rect face in faces)
        //{
        //    Cv2.Rectangle(mat, face, new Scalar(0, 0, 255), 3);
        //}

        //トリミング
        mat = mat.SubMat(faces[0]);

        // 書き出し
        Texture2D outTexture = new Texture2D(mat.Width, mat.Height, TextureFormat.ARGB32, false);
        OpenCvSharp.Unity.MatToTexture(mat, outTexture);

        // 表示
        //GetComponent<RawImage>().texture = outTexture;
        Sprite sprite = Sprite.Create(outTexture, new UnityEngine.Rect(0, 0, outTexture.width, outTexture.height), Vector2.zero);
        target.sprite = sprite;
    }



}
