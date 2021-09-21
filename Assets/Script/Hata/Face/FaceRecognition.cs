
namespace Face
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using OpenCvSharp;

    /// <summary>
    /// 顔認証クラス
    /// </summary>
    public class FaceRecognition : MonoBehaviour
    {
        //Webカメラの内容を映す対象]
        [Header("カメラを映す対象")]
        [SerializeField]
        private RawImage m_camImage = null;

        //カメラ認証に必要なデータ一覧
        [SerializeField] TextAsset facesAsset;
        [SerializeField] TextAsset eyesAsset;
        [SerializeField] TextAsset shapesAsset;

        //使用するカメラデバイス
        private Nullable<WebCamDevice> m_webCamDev = null;

        //Webカメラの映像
        private WebCamTexture m_webCamTex = null;

        //認証した顔のテクスチャ
        private Texture2D m_renderTex = null;

        //前面カメラの使用フラグ、通常はtrue
        private const bool forceFrontalCam= true;

        //回転、反転などを補正するためのWebCamのテクスチャパラメータ。
        private Unity.TextureConversionParams m_webCamParams;

        //顔認証クラス
        private OpenCvSharp.Demo.FaceProcessorLive<WebCamTexture> processor;

        //OpenCVが利用する画像
        private Mat m_mat;

        //Webカメラの描画フラグ
        private bool isRender = false;

        //カメラのデバイス名、完全なリストは m_webCamTexs.devices 列挙器から取得可
        private string DeviceName
        {
            get { return (m_webCamDev != null) ? m_webCamDev.Value.name : null; }
            set
            {
                if (value == DeviceName)
                    return;

                if (null != m_webCamTex && m_webCamTex.isPlaying)
                    m_webCamTex.Stop();

                //使用するデバイスのインデックスを取得
                int cameraIndex = -1;
                for (int i = 0; i < WebCamTexture.devices.Length && -1 == cameraIndex; i++)
                {
                    //2021/09/12 hata Webカメラを使用するためにコメントアウト
                    //if (WebCamTexture.devices[i].name == value)
                    cameraIndex = i;
                }

                // デバイスをセット
                if (-1 != cameraIndex)
                {
                    m_webCamDev = WebCamTexture.devices[cameraIndex];
                    m_webCamTex = new WebCamTexture(m_webCamDev.Value.name);

                    // read device params and make conversion map
                    ReadTextureConversionParameters();

                    m_webCamTex.Play();
                }
                else
                {
                    //例外処理(恐らくカメラが認識出来ない場合に呼び出される)
                    throw new ArgumentException(String.Format("{0}: provided DeviceName is not correct device identifier", this.GetType().Name));
                }
            }
        }

        private void Awake()
        {
            //デバイスの取得
            if (WebCamTexture.devices.Length > 0)
                DeviceName = WebCamTexture.devices[WebCamTexture.devices.Length - 1].name;

            //顔認証の生成
            processor = new OpenCvSharp.Demo.FaceProcessorLive<WebCamTexture>();
            processor.Initialize(facesAsset.text, eyesAsset.text, shapesAsset.bytes);

            // 顔認証のパラメータの設定
            
            processor.DataStabilizer.Enabled = true;       
            //ピクセルの閾値
            processor.DataStabilizer.Threshold = 2.0;      
            //データを算出するために使用するサンプル数
            processor.DataStabilizer.SamplesCount = 2;

            // 顔認証の高速化のための設定

            // 処理された画像は、長辺方向にN pxにプレスケールする
            processor.Performance.Downscale = 256;          
            //n番目のフレームごとに処理する(0なら毎フレーム行う)
            processor.Performance.SkipRate = 0;


            //レンダリング処理を停止
            RenderStop();
        }

        private void Update()
        {
            if (!isRender) return;

            //カメラテクスチャの有無と更新を確認
            if (m_webCamTex != null && m_webCamTex.didUpdateThisFrame)
            {
                ReadTextureConversionParameters();

                //Webカメラの内容をTexture2Dにしてレンダリングする
                if (ProcessTexture(m_webCamTex, ref m_renderTex))
                {
                    RenderFrame();
                }
            }
        }


        /// <summary>
        /// Webカメラの内容の描画を開始する　＊この後にUpdateを行うこと
        /// </summary>
        public void RenderStart()
        {
            isRender = true;
            m_camImage.gameObject.SetActive(true);
        }

        /// <summary>
        /// 描画を停止する
        /// </summary>
        public void RenderStop()
        {
            isRender = false;
            m_camImage.gameObject.SetActive(false);
        }

        /// <summary>
        /// Webカメラから顔をトリミングする
        /// </summary>
        /// <returns> Textrue2Dクラスで返却する </returns>
        public Texture2D FaceTrimming_Tex2D()
        {
            //画像の読み込み
            Mat mat = m_mat;

            //グレースケール化
            Mat gray = new Mat();
            Cv2.CvtColor(mat, gray, ColorConversionCodes.BGR2GRAY);

            // カスケード分類器の準備
            //ToDo：注意が必要かもしれん
            CascadeClassifier haarCascade = new CascadeClassifier("Assets/OpenCV+Unity/Demo/Face_Detector/haarcascade_frontalface_default.xml");

            // 顔検出
            OpenCvSharp.Rect[] faces = haarCascade.DetectMultiScale(gray);

            //ToDo：顔検出が行えない場合の処理が必要
            //トリミング
            mat = mat.SubMat(faces[0]);

            // OpenCVのデータをUnityの扱えるデータに変換
            Texture2D outTexture = new Texture2D(mat.Width, mat.Height, TextureFormat.ARGB32, false);
            OpenCvSharp.Unity.MatToTexture(mat, outTexture);

            return outTexture;
        }

        /// <summary>
        /// Webカメラから顔をトリミングする
        /// </summary>
        /// <returns> Spriteクラスで返却する </returns>
        public Sprite FaceTrimming_Sprite()
        {
            //ToDo：ここか怪しいので要注意
            RenderStop();

            //顔のTextrue2Dクラスを取得
            Texture2D faceTex = FaceTrimming_Tex2D();

            //Texture2DクラスからSpriteクラスを生成
            Sprite returnSprite= Sprite.Create(faceTex, new UnityEngine.Rect(0, 0, faceTex.width, faceTex.height), Vector2.zero);

            return returnSprite;
        }

        /// <summary>
        /// OpenCVのためのパラメータの準備
        /// </summary>
        private void ReadTextureConversionParameters()
        {
            Unity.TextureConversionParams parameters = new Unity.TextureConversionParams();

            //正面カメラのY軸を反転させる
            parameters.FlipHorizontally = forceFrontalCam || m_webCamDev.Value.isFrontFacing;

            
            // ipad用の設定項目
            // compensate vertical flip
            //parameters.FlipVertically = webCamTexture.videoVerticallyMirrored;

            // 回転に対応
            if (0 != m_webCamTex.videoRotationAngle)
                parameters.RotationAngle = m_webCamTex.videoRotationAngle; // cw -> ccw

            m_webCamParams = parameters;

            //UnityEngine.Debug.Log (string.Format("front = {0}, vertMirrored = {1}, angle = {2}", webCamDevice.isFrontFacing, webCamTexture.videoVerticallyMirrored, webCamTexture.videoRotationAngle));
        }

        /// <summary>
		/// サーフェスにレンダリング
		/// </summary>
		private void RenderFrame()
        {
            if (m_renderTex != null)
            {
                m_camImage.GetComponent<RawImage>().texture = m_renderTex;

                m_camImage.GetComponent<RectTransform>().sizeDelta = new Vector2(m_renderTex.width, m_renderTex.height);
            }
        }

        /// <summary>
        /// Webカメラの内容をTexture2Dに変換する
        /// </summary>
        /// <param name="input"> 使用しているWebカメラの内容 </param>
        /// <param name="output"> Texture2Dに変換したWebカメラの内容 </param>
        /// <returns></returns>
        private bool ProcessTexture(WebCamTexture input, ref Texture2D output)
        {
            // Webカメラの内容から検出
            processor.ProcessTexture(input, m_webCamParams);

            //Matクラスを保存
            m_mat = processor.Image;

            //MatクラスをTexture2Dクラスに変換
            output = Unity.MatToTexture(processor.Image, output);

            return true;
        }
    }
}

