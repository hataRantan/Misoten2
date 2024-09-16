using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 床が上手く行かないので、生成後にずらすこ
/// </summary>
public class FloorMove : MyUpdater
{
    [Header("生成後の位置")]
    [SerializeField] Vector3 createdPos = Vector3.zero;

    public override void MyFastestInit()
    {
        this.gameObject.transform.position = createdPos;
    }

    public override void MyUpdate() { }
}
