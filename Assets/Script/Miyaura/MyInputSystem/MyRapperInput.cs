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
        for (int idx = 0; idx < controlls.Length; idx++)
        {
            // セットされていないオブジェクトに親として登録する
            if (controlls[idx] == null)
            {
                controlls[idx] = _input;
                _input.gameObject.transform.parent = gameObject.transform;
                break;
            }
        }
    }

    public int GetConnectNum()
    {
        return gameObject.transform.childCount;
    }

    public MyPlayerInput.Type GetDeviceType(int _playerIdx)
    {
        NumClamp(ref _playerIdx);

        return controlls[_playerIdx].InputType;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// プレイヤー人数の最小・最大値が正しく入っているか
    /// </summary>
    /// <param name="_num">プレイヤー識別番号</param>
    private void NumClamp(ref int _num)
    {
        if (0 <= _num || 3 >= _num) return;

        Debug.LogError("NumClampError:0～3以外の値が入っています");

        //  最小値以下なら０、最大値以上なら３を返す
        _num = Mathf.Clamp(_num, 0, 3);
    }
    /// <summary>
    /// InputSystemのphase操作におけるfalseを返却するための関数
    /// </summary>
    public void InputSystemPhaseReset()
    {
        for (int idx = 0; idx < controlls.Length; idx++)
        {
            if (controlls[idx] == null) break;
            controlls[idx].AllPhaseReset();
        }
    }
    /// <summary>
    /// pressOnly：[KeyBoard&Mouse] anyKey/マウス左右  [PS4] Stick以外  [Xbox] Stick以外
    /// </summary>
    public bool AnyKey()
    {
        //NumClamp(ref _playerNum);
        //if (controlls[_playerNum] == null) return false;

        for (int idx = 0; idx < controlls.Length; idx++)
        {
            if (controlls[idx] == null) continue;

            if (controlls[idx].anyKey) return true;
        }

        return false;
    }
    /// <summary>
    /// [KeyBoard&Mouse] WASD  [PS4] 左スティック  [Xbox] 左スティック
    /// </summary>
    public Vector2 Move(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return Vector2.zero;
        return controlls[_playerNum].move;
    }
    /// <summary>
    /// [KeyBoard&Mouse] 方向キー/マウスポインター  [PS4] 右スティック  [Xbox] 右スティック
    /// </summary>
    public Vector2 Look(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return Vector2.zero;
        return controlls[_playerNum].look;
    }
    /// <summary>
    /// 決定：[KeyBoard&Mouse] EnterKey/マウス左  [PS4] ×  [Xbox] A 
    /// </summary>
    public bool Submit(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].submit;
    }
    /// <summary>
    /// アイテム取得：[KeyBoard&Mouse] マウス左  [PS4] ×  [Xbox] A
    /// </summary>
    public bool GetItem(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].getItem;
    }
    /// <summary>
    /// アイテム使用：[KeyBoard&Mouse] マウス右  [PS4] □  [Xbox] ×
    /// </summary>
    public bool ActionItem(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].actionItem;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 押した瞬間：[KeyBoard&Mouse] ↑  [PS4] ↑  [Xbox] ↑ 
    /// </summary>
    public bool PressArrowUp(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].pressArrowUp;
    }
    /// <summary>
    /// 押した瞬間：[KeyBoard&Mouse] ↓  [PS4] ↓  [Xbox] ↓
    /// </summary>
    public bool PressArrowDown(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].pressArrowDown;
    }
    /// <summary>
    /// 押した瞬間：[KeyBoard&Mouse] ← [PS4] ←  [Xbox] ←
    /// </summary>
    public bool PressArrowLeft(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].pressArrowLeft;
    }
    /// <summary>
    /// 押した瞬間：[KeyBoard&Mouse] →  [PS4] →  [Xbox] → 
    /// </summary>
    public bool PressArrowRight(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].pressArrowRight;
    }
    /// <summary>
    /// 離した瞬間：[KeyBoard&Mouse] ↑  [PS4] ↑  [Xbox] ↑
    /// </summary>
    public bool ReleaseArrowUp(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].releaseArrowUp;
    }
    /// <summary>
    /// 離した瞬間：[KeyBoard&Mouse] ↓  [PS4] ↓  [Xbox] ↓
    /// </summary>
    public bool ReleaseArrowDown(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].releaseArrowDown;
    }
    /// <summary>
    /// 離した瞬間：[KeyBoard&Mouse] ←  [PS4] ←  [Xbox] ←
    /// </summary>
    public bool ReleaseArrowLeft(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].releaseArrowLeft;
    }
    /// <summary>
    /// 離した瞬間：[KeyBoard&Mouse] →  [PS4] →  [Xbox] →
    /// </summary>
    public bool ReleaseArrowRight(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].releaseArrowRight;
    }
    /// <summary>
    /// 押下状態：[KeyBoard&Mouse] ↑  [PS4] ↑  [Xbox] ↑
    /// </summary>
    public bool HoldArrowUp(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].holdArrowUp;
    }
    /// <summary>
    /// 押下状態：[KeyBoard&Mouse] ↓  [PS4] ↓  [Xbox] ↓
    /// </summary>
    public bool HoldArrowDown(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].holdArrowDown;
    }
    /// <summary>
    /// 押下状態：[KeyBoard&Mouse] ←  [PS4] ←  [Xbox] ←
    /// </summary>
    public bool HoldArrowLeft(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);

        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].holdArrowLeft;
    }
    /// <summary>
    /// 押下状態：[KeyBoard&Mouse] →  [PS4] →  [Xbox] →
    /// </summary>
    public bool HoldArrowRight(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].holdArrowRight;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 押した瞬間：[KeyBoard&Mouse] ↑  [PS4] △  [Xbox] Y
    /// </summary>
    public bool PressNorthButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].pressNorthButton;
    }
    /// <summary>
    /// 押した瞬間：[KeyBoard&Mouse] ↓  [PS4] ×  [Xbox] A
    /// </summary>
    public bool PressSouthButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].pressSouthButton;
    }
    /// <summary>
    /// 押した瞬間：[KeyBoard&Mouse] ←  [PS4] □  [Xbox] ×
    /// </summary>
    public bool PressWestButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].pressWestButton;
    }
    /// <summary>
    /// 押した瞬間：[KeyBoard&Mouse] →  [PS4] 〇  [Xbox] B
    /// </summary>
    public bool PressEastButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].pressEastButton;
    }
    /// <summary>
    /// 離した瞬間：[KeyBoard&Mouse] ↑  [PS4] △  [Xbox] Y
    /// </summary>
    public bool ReleaseNorthButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].releaseNorthButton;
    }
    /// <summary>
    /// 離した瞬間：[KeyBoard&Mouse] ↓  [PS4] ×  [Xbox] A
    /// </summary>
    public bool ReleaseSouthButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].releaseSouthButton;
    }
    /// <summary>
    /// 離した瞬間：[KeyBoard&Mouse] ←  [PS4] □  [Xbox] ×
    /// </summary>
    public bool ReleaseWestButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].releaseWestButton;
    }
    /// <summary>
    /// 離した瞬間：[KeyBoard&Mouse] →  [PS4] 〇  [Xbox] B
    /// </summary>
    public bool ReleaseEastButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].releaseEastButton;
    }
    /// <summary>
    /// 押下状態：[KeyBoard&Mouse] ↑  [PS4] △  [Xbox] Y
    /// </summary>
    public bool HoldNorthButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].holdNorthButton;
    }
    /// <summary>
    /// 押下状態：[KeyBoard&Mouse] ↓  [PS4] ×  [Xbox] A
    /// </summary>
    public bool HoldSouthButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].holdSouthButton;
    }
    /// <summary>
    /// 押下状態：[KeyBoard&Mouse] ←  [PS4] □  [Xbox] ×
    /// </summary>
    public bool HoldWestButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].holdWestButton;
    }
    /// <summary>
    /// 押下状態：[KeyBoard&Mouse] →  [PS4] 〇  [Xbox] B
    /// </summary>
    public bool HoldEastButton(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].holdEastButton;
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 押した瞬間：[PS4] L1  [Xbox] LB 
    /// </summary>
    public bool LeftTrigger(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].leftTrigger;
    }
    /// <summary>
    /// 押した瞬間：[PS4] L2  [Xbox] LT
    /// </summary>
    public bool LeftShoulder(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].leftShoulder;
    }
    /// <summary>
    /// 押した瞬間：[PS4] R1  [Xbox] RB 
    /// </summary>
    public bool RightTrigger(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].rightTrigger;
    }
    /// <summary>
    /// 押した瞬間：[PS4] R2  [Xbox] RT
    /// </summary>
    public bool RightShoulder(int _playerNum = 0)
    {
        NumClamp(ref _playerNum);
        if (controlls[_playerNum] == null) return false;
        return controlls[_playerNum].rightShoulder;
    }
}
