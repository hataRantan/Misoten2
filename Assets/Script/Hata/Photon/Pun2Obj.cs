using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class Pun2Obj : MonoBehaviourPunCallbacks
{
    //実行する関数は、publicにする必要あり
    [Header("生成時に呼ぶ初期化関数一覧")]
    [SerializeField] private List<UnityEvent> initEvents = new List<UnityEvent>();

    [Header("破棄時に呼ぶ破棄関数一覧")]
    [SerializeField] private List<UnityEvent> disableEvents = new List<UnityEvent>();

    private void Awake()
    {
        // Object.Instantiateの後に一度だけ必要な初期化処理を行う
    }

    private void Start()
    {
        // 生成後に一度だけ（OnEnableの後に）呼ばれる、ここで初期化処理を行う場合は要注意
    }

    public override void OnEnable()
    {
        base.OnEnable();

        // PhotonNetwork.Instantiateの生成処理後に必要な初期化処理を行う
        foreach(var init in initEvents)
        {
            init.Invoke();
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();

        // PhotonNetwork.Destroyの破棄処理前に必要な終了処理を行う
        foreach (var disble in disableEvents)
        {
            disble.Invoke();
        }
    }
}
