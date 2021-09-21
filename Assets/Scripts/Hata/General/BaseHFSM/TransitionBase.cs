using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//指定の状態の遷移条件を記述
public abstract class TransitionBase
{
    //初期化処理
    public abstract void Init(GameObject _obj);

    //遷移処理
    public abstract string Transition(GameObject _obj);
}


