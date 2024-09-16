using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 縦のフェンスが横のフェンスを生成する
/// </summary>
public class FencePos : MonoBehaviour
{
    [Header("1つめの生成位置")]
    [SerializeField] Transform firstPos = null;
    public Vector3 GetFirstPos { get { return firstPos.position; } }

    [Header("2つ目の生成位置")]
    [SerializeField] Transform secondPos = null;
    public Vector3 GetSecondPos { get { return secondPos.position; } }
}
