using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unityの実行順を管理するためのBaseクラス
/// </summary>
public abstract class BaseCom : MonoBehaviour
{
    public abstract void BaseStart();

    public abstract void BaseUpdate();

    public virtual void BaseFixedUpdate() { }

    public virtual void BaseLateUpdate() { }
}
