using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerGameOverHits : MonoBehaviour
{
    [Header("１人の場合の衝突位置")]
    [SerializeField] Transform oneHit = null;

    [Header("２人の衝突位置")]
    [SerializeField] Transform[] twoHits = new Transform[2];

    [Header("3人目の衝突位置")]
    [SerializeField] Transform[] threeHits = new Transform[3];

    [Header("4人の場合の衝突位置")]
    [SerializeField] Transform[] fourHits = new Transform[4];

    public List<Transform> GetPlayerHItCamera(int _playerNum)
    {
        //プレイヤーの人数を1~4に制限する
        _playerNum = Mathf.Clamp(_playerNum, 1, 4 + 1);

        List<Transform> reTransforms = new List<Transform>();


        switch(_playerNum)
        {
            case 1:
                {
                    reTransforms.Add(oneHit);
                }
                break;

            case 2:
                {
                    SetTransfrom(twoHits);
                }
                break;

            case 3:
                {
                    SetTransfrom(threeHits);
                }
                break;

            case 4:
                {
                    SetTransfrom(fourHits);
                }
                break;
        }


        //指定のトランスフォームをセット
        void SetTransfrom(Transform[] _add)
        {
            foreach(var add in _add)
            {
                reTransforms.Add(add);
            }
        }

        return reTransforms;
    }
}
