using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シーンに1つだけのクラスであり、MyUpdaterListの更新を行う
/// </summary>
public class MyUpdateManager : MonoBehaviour
{
    [Header("MyUpdateListのリスト")]
    [SerializeField] List<MyUpdaterList> myUpdaterLists = new List<MyUpdaterList>();

    //追加予定のリスト
    private List<MyUpdaterList> beAddList = new List<MyUpdaterList>();
    //削除予定のリスト
    private List<MyUpdaterList> beRemoveList = new List<MyUpdaterList>();

    /// <summary>
    /// MyupdaterListの追加
    /// </summary>
    /// <param name="_addTarget"></param>
    public void AddUpdaterList(MyUpdaterList _addTarget)
    {
        if (!myUpdaterLists.Contains(_addTarget)) beAddList.Add(_addTarget);
    }

    /// <summary>
    /// MyUpdaterListを削除
    /// </summary>
    /// <param name="_removeTarget"></param>
    public void RemoveUpdaterList(MyUpdaterList _removeTarget)
    {
        if (myUpdaterLists.Contains(_removeTarget)) beRemoveList.Add(_removeTarget);
    }

    /// <summary>
    /// 最初の初期化
    /// </summary>
    private void Awake()
    {
        if (myUpdaterLists.Count <= 0) return;

        foreach(var updater in myUpdaterLists)
        {
            updater.SetParent(this);
            updater.MyFastestInit();
        }
    }


    /// <summary>
    /// 二回目の初期化
    /// </summary>
    private void Start()
    {
        if (myUpdaterLists.Count <= 0) return;

        foreach (var updater in myUpdaterLists)
        {
            updater.MySecondInit();
        }
    }

    /// <summary>
    /// 物理更新
    /// </summary>
    private void FixedUpdate()
    {
        if (myUpdaterLists.Count <= 0) return;

        foreach(var updater in myUpdaterLists)
        {
            if (updater.IsUpdate) updater.MyFixedUpdate();
        }

    }

    /// <summary>
    /// 入力処理など更新
    /// </summary>
    private void Update()
    {
        if (myUpdaterLists.Count <= 0) return;

        foreach (var updater in myUpdaterLists)
        {
            if (updater.IsUpdate) updater.MyUpdate();
        }
    }

    /// <summary>
    /// 最終更新、MyUpdaterListの追加及び削除
    /// </summary>
    private void LateUpdate()
    {
        if (myUpdaterLists.Count <= 0) return;

        foreach (var updater in myUpdaterLists)
        {
            if (updater.IsUpdate) updater.MyLateUpdate();
        }


        //MyUpdaterListの追加
        if(beAddList.Count>0)
        {
            foreach(var add in beAddList)
            {
                if(!myUpdaterLists.Contains(add))
                {
                    add.SetParent(this);
                    myUpdaterLists.Add(add);
                }
                else Debug.LogError("MyUpdateManagerに追加済み");
            }
            
            beAddList.Clear();
        }

        //MyUpdaterListの削除
        if(beRemoveList.Count>0)
        {
            foreach(var remove in beRemoveList)
            {
                if (myUpdaterLists.Contains(remove)) myUpdaterLists.Remove(remove);
                else Debug.LogError("MyUpdateManagerから削除済み");
            }

            beRemoveList.Clear();
        }
    }
}
