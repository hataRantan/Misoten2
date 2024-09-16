using UnityEngine;

//2021_10_15 hata アップデートを纏めるために変更

public abstract class MyUpdater : MonoBehaviour
{
    //---------------------------------------------------------------------------------
    // Unityの実行順に上から記述
    //
    //    更新処理を纏めるために、ラップするクラス
    // ＊注意：Awake,OnEnable,Startなど生成時、破棄時の処理は元々の関数で行う
    //---------------------------------------------------------------------------------

    //更新を行うか確認するフラグ(trueなら更新を行う)
    [Header("更新処理を行うかのフラグ(trueなら更新する)")]
    [SerializeField]
    protected bool m_isUpdate = true;
    
    /// <summary>
    /// 更新を行うか確認するフラグ
    /// </summary>
    public bool IsUpdate { get { return m_isUpdate; } }

    //自身の更新を行うクラスへの参照
    protected MyUpdaterList m_parent = null;

    /// <summary>
    /// 自身の更新を行うクラスへの参照
    /// </summary>
    public MyUpdaterList Parent { get { return m_parent; } }

    /// <summary>
    /// シーン開始時に行う一度だけ行う最も速い初期化(実行タイミングはAwake)
    /// このコンポーネントだけで完結する初期化
    /// </summary>
    public virtual void MyFastestInit() { }

    /// <summary>
    /// シーン開始中に一度だけ行う初期化(実行タイミングはStart)
    /// 他コンポーネントに干渉した上で行う初期化
    /// </summary>
    public virtual void MySecondInit() { }

    /// <summary>
    /// 物理運動を記述するUpdate(入力処理は記述しないこと)
    /// </summary>
    public virtual void MyFixedUpdate() { }

    /// <summary>
    /// 入力処理など大体の更新処理を記述するUpdate
    /// </summary>
    public abstract void MyUpdate();

    /// <summary>
    /// 最も遅い更新処理
    /// </summary>
    public virtual void MyLateUpdate() { }


    /// <summary>
    /// 更新を行うかどうか変更する関数
    /// </summary>
    /// <param name="_isUpdate"> trueなら更新、falseなら更新しない </param>
    public void SwitchUpdate(bool _isUpdate) => m_isUpdate = _isUpdate;

    /// <summary>
    /// 親を設定
    /// </summary>
    public void SetParent(MyUpdaterList _parent) => m_parent = _parent;
   
}



//class A
//{
//    public virtual void MyUpdate()
//    {
//        Debug.Log("A");
//    }
//}

//class B : A
//{
//    public override void MyUpdate()
//    {
//        base.MyUpdate();
//        Debug.Log("B");
//    }
//}
