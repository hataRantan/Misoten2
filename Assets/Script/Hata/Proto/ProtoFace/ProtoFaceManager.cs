using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
/// <summary>
/// プロトタイプ用、顔撮影シーン管理クラス
/// </summary>
public class ProtoFaceManager : MonoBehaviour
{
    [Header("顔認証クラス")]
    [SerializeField] Face.FaceRecognition facePhoto = null;

    [Header("表示するテキスト")]
    [SerializeField] TextMeshProUGUI textPro = null;

    [Header("撮影した顔の写真")]
    [SerializeField] Image faceImage = null;

    [Header("移動後のシーン")]
    [SerializeField] SceneObject nextScene = null;

    //入力値
    private protoInput inputer = null;

    private enum ProtoFaceEvent
    {
        FIRST_PHOTO,
        SECOND_PHOTO
    }

    //シーン状態管理クラス
    IStateSpace.StateMachineBase<ProtoFaceEvent, ProtoFaceManager> machine = new IStateSpace.StateMachineBase<ProtoFaceEvent, ProtoFaceManager>();

    private void Start()
    {
        inputer = gameObject.GetComponent<protoInput>();
        inputer.isController = false;
        
        //状態作成
        machine.AddState(ProtoFaceEvent.FIRST_PHOTO, new FirstFace(), this);
        machine.AddState(ProtoFaceEvent.SECOND_PHOTO, new SecondFace(), this);

        //初期状態を設定
        machine.InitState(ProtoFaceEvent.FIRST_PHOTO);
    }

    private void Update()
    {
        machine.UpdateState();
    }


    private class FirstFace : IStateSpace.IState<ProtoFaceEvent, ProtoFaceManager>
    {
        //撮影した顔
        Texture2D face = null;

        public override void Entry()
        {
            //テキスト変更
            board.textPro.text = "撮影中";
            //カメラ描画開始
            board.facePhoto.RenderStart();

            //撮影後の描画停止
            board.faceImage.gameObject.SetActive(false);

            face = null;
        }

        public override void Exit()
        {

        }

        public override ProtoFaceEvent Update()
        {
            //RawImageに描画開始
            board.facePhoto.MyUpdate();

            //ネストが深くなって良くないけど、プロトなんで適当に記述
            if (face == null)
            {
                //if (Input.GetKeyDown(KeyCode.Return))
                if(board.inputer.Get_AButtonDown())
                {
                    //顔を取得
                    face = board.facePhoto.FaceTrimming_Tex2D();

                    //顔を保存
                    SavingFace.Instance.SaveFace(face);

                    //スプライトに変換
                    board.faceImage.sprite = board.facePhoto.Conversion_Tex2D_To_Sprite(face);
                    //撮影した顔の描画開始
                    board.faceImage.gameObject.SetActive(true);

                    //テキスト変更
                    board.textPro.text = "撮影結果";
                }
            }
            else
            {

                if (board.inputer.Get_AButtonDown())
                    return ProtoFaceEvent.SECOND_PHOTO;
            }

            return ProtoFaceEvent.FIRST_PHOTO;
        }
    }

    private class SecondFace : IStateSpace.IState<ProtoFaceEvent, ProtoFaceManager>
    {
        //撮影した顔
        Texture2D face = null;

        public override void Entry()
        {
            //テキスト変更
            board.textPro.text = "撮影中";
            //カメラ描画開始
            board.facePhoto.RenderStart();

            //撮影後の描画停止
            board.faceImage.gameObject.SetActive(false);

            face = null;
        }

        public override void Exit()
        {

        }

        public override ProtoFaceEvent Update()
        {
            //RawImageに描画開始
            board.facePhoto.MyUpdate();

            //ネストが深くなって良くないけど、プロトなんで適当に記述
            if (face == null)
            {
                if (board.inputer.Get_AButtonDown())
                {
                    //顔を取得
                    face = board.facePhoto.FaceTrimming_Tex2D();

                    //顔を保存
                    SavingFace.Instance.SaveFace(face, 1);

                    //スプライトに変換
                    board.faceImage.sprite = board.facePhoto.Conversion_Tex2D_To_Sprite(face);
                    //撮影した顔の描画開始
                    board.faceImage.gameObject.SetActive(true);

                    //テキスト変更
                    board.textPro.text = "撮影結果";
                }
            }
            else
            {
                if (board.inputer.Get_AButtonDown())
                {
                    board.facePhoto.Release();
                    SceneLoader.Instance.CallLoadSceneDefault(board.nextScene.GetName);
                }
            }

            return ProtoFaceEvent.SECOND_PHOTO;
        }
    }

}
