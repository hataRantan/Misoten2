using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクト毎につけるMyUpderの更新クラス
/// </summary>
public class MyUpdaterList : MonoBehaviour
{
    [Header("このオブジェクトのMyUpdater一覧")]
    [SerializeField] List<MyUpdater> myUpdaters = new List<MyUpdater>();

    //追加予定のリスト
    private List<MyUpdater> beAddList = new List<MyUpdater>();
    //削除予定のリスト
    private List<MyUpdater> beRemoveList = new List<MyUpdater>();

    //このクラスの更新を行うか確認するフラグ
    private bool m_isUpdate = true;

    /// <summary>
    ///  このクラスの更新を行うか確認するフラグ
    /// </summary>
    public bool IsUpdate { get { return m_isUpdate; } }

    //このクラスを更新するManagerへの参照
    private MyUpdateManager m_parent = null;
    public MyUpdateManager Parent { get { return m_parent; } }

    /// <summary>
    /// MyUpdaterリストに追加
    /// </summary>
    public void AddUpdater(MyUpdater _addTarget)
    {
        if (!beAddList.Contains(_addTarget))
        {
            beAddList.Add(_addTarget);
        }
    }

    /// <summary>
    /// MyUpdaterリストから削除
    /// </summary>
    public void RemoveUpdater(MyUpdater _removeTarget)
    {
        if (myUpdaters.Contains(_removeTarget)) beRemoveList.Add(_removeTarget);
    }

    /// <summary>
    /// 更新処理の切り替え
    /// </summary>
    /// <param name="_isUpdate"> trueなら更新、falseなら更新しない </param>
    public void SwitchUpdate(bool _isUpdate) => m_isUpdate = _isUpdate;
    
    /// <summary>
    /// 親を設定
    /// </summary>
    public void SetParent(MyUpdateManager _manager) => m_parent = _manager;


    /// <summary>
    /// 指定のupdaterを取得する
    /// </summary>
    /// <param name="_wantUpdate"> 取得したいupdater </param>
    /// <returns> 不所持ならnullを返す </returns>
    public MyUpdater GetUpdater<T>(T _want)
    {
        foreach(var updater in myUpdaters)
        {
            if(updater.GetType().Name==_want.GetType().Name)
            {
                return updater;
            }
        }

        return null;
    }

    /// <summary>
    /// 破棄時に親のListから自身を排除
    /// </summary>
    public void OnDestroy()
    {
        if (!m_parent) return;

        m_parent.RemoveUpdaterList(this);
    }

    /// <summary>
    /// シーン開始時に行う一度だけ行う最も速い初期化(実行タイミングはAwake)
    /// </summary>
    public void MyFastestInit() 
    {
        if (myUpdaters.Count <= 0) return;

        foreach(var updater in myUpdaters)
        {
            updater.SetParent(this);
            updater.MyFastestInit();
        }
    }

    /// <summary>
    /// シーン開始中に一度だけ行う初期化(実行タイミングはStart)
    /// </summary>
    public void MySecondInit() 
    {
        if (myUpdaters.Count <= 0) return;

        foreach(var updaer in myUpdaters)
        {
            updaer.MySecondInit();
        }
    }

    /// <summary>
    /// 物理運動を記述するUpdate(入力処理は記述しないこと)
    /// </summary>
    public  void MyFixedUpdate()
    {
        if (myUpdaters.Count <= 0) return;

        foreach (var updaer in myUpdaters)
        {
            if(updaer.IsUpdate) updaer.MyFixedUpdate();
        }
    }

    /// <summary>
    /// 入力処理など大体の更新処理を記述するUpdate
    /// </summary>
    public void MyUpdate() 
    {
        if (myUpdaters.Count <= 0) return;

        foreach (var updaer in myUpdaters)
        {
            if(updaer.IsUpdate) updaer.MyUpdate();
        }
    }

    /// <summary>
    /// 最も遅い更新処理、指定のUpdaterの追加及び削除を行う
    /// </summary>
    public void MyLateUpdate() 
    {
        if (myUpdaters.Count <= 0) return;

        foreach (var updaer in myUpdaters)
        {
            if (updaer.IsUpdate) updaer.MyLateUpdate();
        }


        //MyUpdateを追加する
        if (beAddList.Count > 0)
        {
            foreach (var add in beAddList)
            {
                if (!myUpdaters.Contains(add))
                {
                    add.SetParent(this);
                    myUpdaters.Add(add);
                }
            }

            //追加済みのモノは空にする
            beAddList.Clear();
        }


        //MyUpdaterを削除する
        if (beRemoveList.Count > 0)
        {
            foreach(var remove in beRemoveList)
            {
                if(myUpdaters.Contains(remove)) myUpdaters.Remove(remove);
            }

            //削除予定のモノを空にする
            beRemoveList.Clear();
        }
    }

    /// <summary>
    /// 更新を停止する
    /// </summary>
    public void StopUpdate()
    {
        m_isUpdate = false;
    }
}
