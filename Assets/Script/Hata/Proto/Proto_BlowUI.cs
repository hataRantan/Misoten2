using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Proto_BlowUI : MonoBehaviour
{
    [Header("PL1用のバー")]
    [SerializeField] Image pl1Bar = null;

    [Header("PL2用のバー")]
    [SerializeField] Image pl2Bar = null;

    /// <summary>
    /// 初期状態
    /// </summary>
    public void MyStart()
    {
        for (int idx = 0; idx < 2; idx++)
        {
            SetFill(idx, 0.0f);
        }
    }

    public void SetFill(int _plNum, float _fillAmout)
    {
        if (_plNum == 0)
        {
            pl1Bar.fillAmount = _fillAmout;
        }
        else if (_plNum == 1)
        {
            pl2Bar.fillAmount = _fillAmout;
        }
    }
}
