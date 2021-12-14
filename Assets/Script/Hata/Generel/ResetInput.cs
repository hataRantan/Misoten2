using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MyRapperInpputのリセットを行う
/// </summary>
public class ResetInput : MyUpdater
{
    public override void MyUpdate()
    {
        MyRapperInput.Instance.InputSystemPhaseReset();
    }
}
