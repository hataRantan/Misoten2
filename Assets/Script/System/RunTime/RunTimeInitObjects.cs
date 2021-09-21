using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Init", menuName = "ScriptableObjects/RumTimeInit")]
[System.Serializable]
public class RunTimeInitObjects : ScriptableObject
{
    [Header("シーン開始時に生成されるオブジェクト")]
    public DontDestroyInitObj[] initObj;

}

[System.Serializable]
public class DontDestroyInitObj
{
    public GameObject obj = null;
}