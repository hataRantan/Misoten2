using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRapperInput : Singleton<MyRapperInput>
{
    [Header("入力受付")]
    [SerializeField] MyPlayerInput[] controlls = new MyPlayerInput[4];

    /// <summary>
    /// 複製されたInputSystemを未登録のControllsにセットする
    /// </summary>
    public void SetChild(MyPlayerInput _input)
    {
       for(int idx=0;idx<controlls.Length;idx++)
        {
            // セットされていないオブジェクトに親として登録する
            if(controlls[idx]==null)
            {
                controlls[idx] = _input;
                _input.gameObject.transform.parent = gameObject.transform;
                break;
            }
        }
    }

    /// <summary>
    /// ポーズ：[Xbox] Menu  [PS4] OptionButton [KeyBoard] P
    /// </summary>
    public bool TogglePauseButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

       if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].togglePause;
    }
    /// <summary>
    /// 決定：[Xbox] A [PS4] × [KeyBoard] Enter
    /// </summary>
    public bool TriggerSubmitButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].triggerSubmit;
    }
    /// <summary>
    /// アイテム取得：[Xbox] A [PS4] × [KeyBoard] マウス左クリック
    /// </summary>
    public bool GetItem(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].getItem;
    }
    /// <summary>
    /// アイテム使用：[Xbox] × [PS4] □ [KeyBoard] マウス右クリック
    /// </summary>
    public bool ActionItem(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].actionItem;
    }


    /// <summary>
    /// 移動：[Xbox] 左スティック [PS4] 左スティック [KeyBoard] WASD
    /// </summary>
    public Vector2 Move(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return Vector2.zero;
        }
        return controlls[_playerNum].move;
    }
    /// <summary>
    /// 視点移動：[Xbox] 右スティック [PS4] 右スティック [KeyBoard] マウスポインター
    /// </summary>
    public Vector2 Look(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return Vector2.zero;
        }
        return controlls[_playerNum].look;
    }


    /// <summary>
    /// トリガー：[Xbox] LB [PS4] L1 [KeyBoard] 現在割り当て無し
    /// </summary>
    public bool LeftTriggerButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].leftTrigger;
    }
    /// <summary>
    /// トリガー：[Xbox] LT [PS4] L2 [KeyBoard] 現在割り当て無し
    /// </summary>
    public bool LeftShoulderButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].leftShoulder;
    }
    /// <summary>
    /// トリガー：[Xbox] RB [PS4] R1 [KeyBoard] 現在割り当て無し
    /// </summary>
    public bool RightTriggerButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].rightTrigger;
    }
    /// <summary>
    /// トリガー：[Xbox] RT [PS4] R2 [KeyBoard] 現在割り当て無し
    /// </summary>
    public bool RightShoulderButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].rightShoulder;
    }


    /// <summary>
    /// 押した瞬間：[Xbox] 上矢印 [PS4] 上矢印 [KeyBoard] 上矢印
    /// </summary>
    public bool PressUpButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].pressArrowUp;
    }
    /// <summary>
    /// 押した瞬間：[Xbox] 下矢印 [PS4] 下矢印 [KeyBoard] 下矢印
    /// </summary>
    public bool PressDownButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

         if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].pressArrowDown;

    }
    /// <summary>
    /// 押した瞬間：[Xbox] 左矢印 [PS4] 左矢印 [KeyBoard] 左矢印
    /// </summary>
    public bool PressLeftButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].pressArrowLeft;
    }
    /// <summary>
    /// 押した瞬間：[Xbox] 右矢印 [PS4] 右矢印 [KeyBoard] 右矢印
    /// </summary>
    public bool PressRightButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].pressArrowRight;
    }
    /// <summary>
    /// 離した瞬間：[Xbox] 上矢印 [PS4] 上矢印 [KeyBoard] 上矢印
    /// </summary>
    public bool ReleaseUpButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].releaseArrowUp;
    }
    /// <summary>
    /// 離した瞬間：[Xbox] 下矢印 [PS4] 下矢印 [KeyBoard] 下矢印
    /// </summary>
    public bool ReleaseDownButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].releaseArrowDown;
    }
    /// <summary>
    /// 離した瞬間：[Xbox] 左矢印 [PS4] 左矢印 [KeyBoard] 左矢印
    /// </summary>
    public bool ReleaseLeftbutton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].releaseArrowLeft;
    }
    /// <summary>
    /// 離した瞬間：[Xbox] 右矢印 [PS4] 右矢印 [KeyBoard] 右矢印
    /// </summary>
    public bool ReleaseRightbutton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].releaseArrowRight;
    }
    /// <summary>
    /// 一定時間押下状態経過後：[Xbox] 上矢印 [PS4] 上矢印 [KeyBoard] 上矢印
    /// </summary>
    public bool HoldUpButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].holdArrowUp;
    }
    /// <summary>
    /// 一定時間押下状態経過後：[Xbox] 下矢印 [PS4] 下矢印 [KeyBoard] 下矢印
    /// </summary>
    public bool HoldDownButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].holdArrowDown;
    }
    /// <summary>
    /// 一定時間押下状態経過後：[Xbox] 左矢印 [PS4] 左矢印 [KeyBoard] 左矢印
    /// </summary>
    public bool HoldLeftButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].holdArrowLeft;
    }
    /// <summary>
    /// 一定時間押下状態経過後：[Xbox] 右矢印 [PS4] 右矢印 [KeyBoard] 右矢印
    /// </summary>
    public bool HoldRightButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].holdArrowRight;
    }



    /// <summary>
    /// 押した瞬間：[Xbox] Y [PS4] △ [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool PressNorthButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].pressNorthButton;
    }
    /// <summary>
    /// 押した瞬間：[Xbox] A [PS4] × [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool PressSouthButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].pressSouthButton;
    }
    /// <summary>
    /// 押した瞬間：[Xbox] × [PS4] □ [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool PressWestButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].pressWestButton;
    }
    /// <summary>
    /// 押した瞬間：[Xbox] B [PS4] 〇 [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool PressEastButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].pressEastButton;
    }
    /// <summary>
    /// 離した瞬間：[Xbox] Y [PS4] △ [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool ReleaseNorthButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].releaseNorthButton;
    }
    /// <summary>
    /// 離した瞬間：[Xbox] A [PS4] × [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool ReleaseSouthButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].releaseSouthButton;
    }
    /// <summary>
    /// 離した瞬間：[Xbox] × [PS4] □ [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool ReleaseWestButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].releaseWestButton;
    }
    /// <summary>
    /// 離した瞬間：[Xbox] B [PS4] 〇 [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool ReleaseEastButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].releaseEastButton;
    }
    /// <summary>
    /// 一定時間押下状態経過後：[Xbox] Y [PS4] △ [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool HoldNorthButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].holdNorthButton;
    }
    /// <summary>
    /// 一定時間押下状態経過後：[Xbox] A [PS4] × [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool HoldSouthButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].holdSouthButton;
    }
    /// <summary>
    /// 一定時間押下状態経過後：[Xbox] × [PS4] □ [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool HoldWestButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].holdWestButton;
    }
    /// <summary>
    ///一定時間押下状態経過後：[Xbox] B [PS4] 〇 [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool HoldEastButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if(controlls[_playerNum] ==null)
        {
            return false;
        }
        return controlls[_playerNum].holdEastButton;
    }



    /// <summary>
    /// プレイヤー人数の最小・最大値が正しく入っているか
    /// </summary>
    /// <param name="_num">プレイヤー識別番号</param>
    public void NumClamp(ref int _num)
    {
        if (0 <= _num || 3 >= _num) return;

        Debug.LogError("NumClampError:0～3以外の値が入っています");

        //  最小値以下なら０、最大値以上なら３を返す
        _num = Mathf.Clamp(_num, 0, 3);
    }
}
