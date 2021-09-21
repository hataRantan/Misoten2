using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//指定の状態の行動を記述
public abstract class ActionBase
{
    //初期化
    public abstract void Init(GameObject _obj);

    //更新処理
    public abstract void Action(GameObject _obj);
}