using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTimeInit
{
    //関数をゲーム生成直後に一度だけawakeより先に呼び出す設定にする属性
    //ただし必ず呼ばれるので注意が必要
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitBeforeSceneLoad()
    {
        //生成するオブジェクトリストの呼び込み
        RunTimeInitObjects list = Resources.Load("Init/RunTimeInit") as RunTimeInitObjects;

        if (list == null) return;

        foreach(var obj in list.initObj)
        {
            var instance = GameObject.Instantiate(obj.obj);
            GameObject.DontDestroyOnLoad(instance);
        }

    }
}
