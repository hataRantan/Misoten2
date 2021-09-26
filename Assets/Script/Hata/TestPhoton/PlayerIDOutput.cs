
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using TMPro;

public class PlayerIDOutput : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        //プレイヤーの名前を出力
        TextMeshPro text = gameObject.GetComponentInChildren<TextMeshPro>();
        //$"" 文字列補間式
        //プレイヤー名（PプレイヤーID）を表示する
        //Owerについて
        //基本的に生成したプレイヤーが所有権を持つことになる
        //photonView.Ower ：所有者のプレイヤーオブジェクト
        text.text = $"{photonView.Owner.NickName}({photonView.OwnerActorNr})";
    }

    private void Update()
    {
        int i = 0;
    }
}
