using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
/// <summary>
/// 顔撮影シーンの管理クラス
/// </summary>
public class FaceSceneManager : MyUpdater
{
    [Header("顔を撮影するクラス")]
    [SerializeField] Face.FaceRecognition m_facePhoto = null;

    [Header("撮影後の写真を映す画像")]
    [SerializeField] Image m_faceImage = null;

    [Header("シャッターエフェクト")]
    [SerializeField] ShutterEffects m_shutterEffects = null;

    [Header("移動後のシーン")]
    [SerializeField] SceneObject m_nextScene = null;

    [Header("撮影時間")]
    [Range(10, 19)]
    [SerializeField] float m_photoTime = 15.0f;

    [Header("撮影時間テキスト")]
    [SerializeField] FaceTimer m_timerText = null;

    [Header("撮影人数")]
    [Range(1, 4)]
    [SerializeField] int m_photoNum = 1;
    private int m_photoCurrentNum = 0;

    //[Header("写真の出現時間")]
    //[Range(0.0f, 2.0f)]
    //[SerializeField] private float m_photoAppearTime = 1.0f;
    //[Header("写真出現クラス")]
    //[SerializeField] PhotoAppear m_photoAppear = null;

    // 撮影クラスの状態一覧
    private enum FaceType
    {
        PHOTO,           //撮影中
        PHOTO_EFFECT,    //撮影エフェクト発生
        AFTER_PHOTO      //撮影した画像を表示する
    }
    //シーン状態管理クラス
    IStateSpace.StateMachineBase<FaceType, FaceSceneManager> machine = new IStateSpace.StateMachineBase<FaceType, FaceSceneManager>();

    public override void MyFastestInit()
    {
        //状態を初期化する
        machine.AddState(FaceType.PHOTO, new PhotoState(), this);
        machine.AddState(FaceType.PHOTO_EFFECT, new PhotoEffectState(), this);
        machine.AddState(FaceType.AFTER_PHOTO, new AfterPhotoState(), this);

        //最初の状態を設定
        machine.InitState(FaceType.PHOTO);
    }

    public override void MyUpdate()
    {
        //状態更新
        machine.UpdateState();
    }


    /// <summary>
    /// 撮影中のクラス
    /// </summary>
    private class PhotoState : IStateSpace.IState<FaceType, FaceSceneManager>
    {
        float timer = 0.0f;

        //タイマー開始カウント
        float timerStartTime = 1.0f;
        bool isStart = false;


        public override void Entry()
        {
            //撮影開始
            board.m_facePhoto.RenderStart();

            //タイマーを初期化
            board.m_timerText.gameObject.SetActive(true);
            timer = 0.0f;
            board.m_timerText.TimerStart(board.m_photoTime);

            isStart = false;
        }

        public override void Exit()
        {
            //撮影停止
            board.m_facePhoto.RenderStop();

           
        }

        public override FaceType Update()
        {
            //カメラのセットアップ待ち
            if (!board.m_facePhoto.IsSetUp()) return FaceType.PHOTO;

            //ToDo：入力処理の変更必須
            if (Input.GetMouseButtonDown(0))
            {
                return FaceType.PHOTO_EFFECT;
            }

            //タイマー開始を少し遅らせる
            if (!isStart)
            {
                if (timer < timerStartTime)
                {
                    timer += Time.deltaTime;
                    return FaceType.PHOTO;
                }
                else
                {
                    timer = 0.0f;
                    isStart = true;
                }
            }
          
            //時間計測
            if (timer < board.m_photoTime)
            {
                timer += Time.deltaTime;

                //テキスト操作
                board.m_timerText.TimerSet(board.m_photoTime - timer, board.m_photoTime);
            }
            //撮影時間を超えたので、強制的に撮影
            else
            {
                return FaceType.PHOTO_EFFECT;
            }

            return FaceType.PHOTO;
        }

       
    }

    /// <summary>
    /// シャッターエフェクトのクラス
    /// </summary>
    private class PhotoEffectState : IStateSpace.IState<FaceType, FaceSceneManager>
    {
        //コールバック用変数
        private bool isEndShutterEffect = false;

        public override void Entry()
        {
            //変数初期化
            isEndShutterEffect = false;

            //シャッターエフェクト開始
            board.m_shutterEffects.CallShutterOn(PhotoAppear, EndEffect);

            //シャッター音声再生
            MyAudioManeger.Instance.PlaySE("CameraShutter");
        }

        public override void Exit()
        {

        }

        public override FaceType Update()
        {
            ///終了
            if (isEndShutterEffect)
            {
                return FaceType.AFTER_PHOTO;
            }

            return FaceType.PHOTO_EFFECT;
        }

        /// <summary>
        /// カメラ画像を消して、写真を表示する
        /// </summary>
        private void PhotoAppear()
        {
            //カメラ描画停止
            board.m_facePhoto.NotActiveCamImage();

            //テキストを停止 ToDo：テキストの変更になるかも?
            board.m_timerText.gameObject.SetActive(false);

            //顔を取得
            Texture2D face = board.m_facePhoto.FaceTrimming_Tex2D();
            //顔を保存
            SavingFace.Instance.SaveFace(face, board.m_photoCurrentNum);
            //撮影人数を記録
            board.m_photoCurrentNum++;
            //顔を貼り付け
            board.m_faceImage.sprite = board.m_facePhoto.Conversion_Tex2D_To_Sprite(face);
            //写真を描画開始
            board.m_faceImage.gameObject.SetActive(true);
            //board.m_photoAppear.InitAppear();
        }

        private void EndEffect()
        {
            isEndShutterEffect = true;
        }
    }

    private class AfterPhotoState : IStateSpace.IState<FaceType, FaceSceneManager>
    {

        public override void Entry()
        {
            //board.m_photoAppear.CallAppear(board.m_photoAppearTime, this.EndEffect);
        }

        public override void Exit()
        {
            //写真を停止
            board.m_faceImage.gameObject.SetActive(false);
        }

        public override FaceType Update()
        {
            //if (!isEndAppear) return FaceType.AFTER_PHOTO;
            
            //ToDo：変更必要
            if(Input.GetMouseButtonDown(0))
            {
                //撮影終了
                if (board.m_photoNum <= board.m_photoCurrentNum)
                {
                    //次のシーンへ移動する
                    SceneLoader.Instance.CallLoadSceneDefault(board.m_nextScene);
                }
                //さらに撮影する
                else
                {
                    return FaceType.PHOTO;
                }
            }

            return FaceType.AFTER_PHOTO;
        }

      
    }
}
