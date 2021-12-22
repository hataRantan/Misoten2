using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rester : MyUpdater
{
    public override void MyUpdate()
    {
        MyRapperInput.Instance.InputSystemPhaseReset();
    }
}
