using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

/// <summary>
/// コントローラーの入力ボタンの識別
/// </summary>
public class MyPlayerInput : MonoBehaviour
{
    public InputControlScheme myDevice;
    public PlayerInput playerInput;
    private void Awake()
    {
        // 複製されたRapperInputを各プレイヤーの親としてセットする
        GameObject.Find("PlayerInputManager(Clone)").GetComponent<MyRapperInput>().SetChild(this);
    }

    // 未完成：入力デバイスの識別
    public string deviceName { get; private set; }
    public string defaultControlScheme { get; set; }
    public MyDeviceDisplaySetting myDeviceDisplaySetting;
    public string currentControlScheme;
    public void SetControlSheme() => defaultControlScheme = playerInput.defaultControlScheme;


    public void AllPhaseReset()
    {
        anyKey = false;
        submit = false;
        getItem = false;
        actionItem = false;

        pressArrowUp = false;
        pressArrowDown = false;
        pressArrowLeft = false;
        pressArrowRight = false;
        releaseArrowUp = false;
        releaseArrowDown = false;
        releaseArrowLeft = false;
        releaseArrowRight = false;

        pressNorthButton = false;
        pressSouthButton = false;
        pressWestButton = false;
        pressEastButton = false;
        releaseNorthButton = false;
        releaseSouthButton = false;
        releaseWestButton = false;
        releaseEastButton = false;

        leftTrigger = false;
        leftShoulder = false;
        rightTrigger = false;
        rightShoulder = false;
    }

    public bool anyKey { get; private set; }

    public Vector2 move { get; private set; }
    public Vector2 look { get; private set; }

    public bool submit { get; private set; }
    public bool getItem { get; private set; }
    public bool actionItem { get; private set; }

    public bool pressArrowUp { get; private set; }
    public bool pressArrowDown { get; private set; }
    public bool pressArrowLeft { get; private set; }
    public bool pressArrowRight { get; private set; }
    public bool releaseArrowUp { get; private set; }
    public bool releaseArrowDown { get; private set; }
    public bool releaseArrowLeft { get; private set; }
    public bool releaseArrowRight { get; private set; }
    public bool holdArrowUp { get; private set; }
    public bool holdArrowDown { get; private set; }
    public bool holdArrowLeft { get; private set; }
    public bool holdArrowRight { get; private set; }


    public bool pressNorthButton { get; private set; }
    public bool pressSouthButton { get; private set; }
    public bool pressWestButton { get; private set; }
    public bool pressEastButton { get; private set; }
    public bool releaseNorthButton { get; private set; }
    public bool releaseSouthButton { get; private set; }
    public bool releaseWestButton { get; private set; }
    public bool releaseEastButton { get; private set; }
    public bool holdNorthButton { get; private set; }
    public bool holdSouthButton { get; private set; }
    public bool holdWestButton { get; private set; }
    public bool holdEastButton { get; private set; }

    public bool leftTrigger { get; private set; }
    public bool leftShoulder { get; private set; }
    public bool rightTrigger { get; private set; }
    public bool rightShoulder { get; private set; }

    // KeyBind(ActionMapを参照する関数群)
    public void OnAnyKey(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Started:
                anyKey = true;
                break;
        }
    }
    public void OnMove(InputAction.CallbackContext value) => move = value.ReadValue<Vector2>();
    public void OnLook(InputAction.CallbackContext value) => look = value.ReadValue<Vector2>();
    public void OnSubmit(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Started:
                submit = true;
                break;
        }
    }
    public void OnGetItem(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Started:
                getItem = true;
                break;
        }
    }
    public void OnActionItem(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Started:
                actionItem = true;
                break;
        }
    }

    public void OnPressArrowUp(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Started:
                pressArrowUp = true;
                break;
        }
    }
    public void OnPressArrowDown(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Started:
                pressArrowDown = true;
                break;
        }
    }
    public void OnPressArrowLeft(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Started:
                pressArrowLeft = true;
                break;
        }

    }
    public void OnPressArrowRight(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Started:
                pressArrowRight = true;
                break;
        }
    }

    public void OnReleaseArrowUp(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Performed:
                releaseArrowUp = true;
                break;
        }
    }
    public void OnReleaseArrowDown(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Performed:
                releaseArrowDown = true;
                break;
        }
    }
    public void OnReleaseArrowLeft(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Performed:
                releaseArrowLeft = true;
                break;
        }
    }
    public void OnReleaseArrowRight(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Performed:
                releaseArrowRight = true;
                break;
        }
    }

    public void OnHoldArrowUp(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Performed:
                holdArrowUp = value.ReadValueAsButton();
                break;
            case InputActionPhase.Canceled:
                holdArrowUp = value.ReadValueAsButton();
                break;
        }
    }
    public void OnHoldArrowDown(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Performed:
                holdArrowDown = value.ReadValueAsButton();
                break;
            case InputActionPhase.Canceled:
                holdArrowDown = value.ReadValueAsButton();
                break;
        }
    }
    public void OnHoldArrowLeft(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Performed:
                holdArrowLeft = value.ReadValueAsButton();
                break;
            case InputActionPhase.Canceled:
                holdArrowLeft = value.ReadValueAsButton();
                break;
        }
    }
    public void OnHoldArrowRight(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Performed:
                holdArrowRight = value.ReadValueAsButton();
                break;
            case InputActionPhase.Canceled:
                holdArrowRight = value.ReadValueAsButton();
                break;
        }
    }


    // 上下左右のボタン
    public void OnPressNorthButton(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Started:
                pressNorthButton = true;
                break;
        }
    }
    public void OnPressSouthButton(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Started:
                pressSouthButton = true;
                break;
        }
    }
    public void OnPressWestButton(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Started:
                pressWestButton = true;
                break;
        }
    }
    public void OnPressEastButton(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Started:
                pressEastButton = true;
                break;
        }
    }

    public void OnReleaseNorthButton(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Performed:
                releaseNorthButton = true;
                break;
        }
    }
    public void OnReleaseSouthButton(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Performed:
                releaseSouthButton = true;
                break;
        }
    }
    public void OnReleaseWestButton(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Performed:
                releaseWestButton = true;
                break;
        }
    }
    public void OnReleaseEastButton(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Performed:
                releaseEastButton = true;
                break;
        }
    }

    public void OnHoldNorthButton(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Performed:
                holdNorthButton = value.ReadValueAsButton();
                break;
            case InputActionPhase.Canceled:
                holdNorthButton = value.ReadValueAsButton();
                break;
        }
    }
    public void OnHoldSouthButton(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Performed:
                holdSouthButton = value.ReadValueAsButton();
                break;
            case InputActionPhase.Canceled:
                holdSouthButton = value.ReadValueAsButton();
                break;
        }
    }
    public void OnHoldWestButton(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Performed:
                holdWestButton = value.ReadValueAsButton();
                break;
            case InputActionPhase.Canceled:
                holdWestButton = value.ReadValueAsButton();
                break;
        }
    }
    public void OnHoldEastButton(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Performed:
                holdEastButton = value.ReadValueAsButton();
                break;
            case InputActionPhase.Canceled:
                holdEastButton = value.ReadValueAsButton();
                break;
        }
    }

    public void OnLeftTrigger(InputAction.CallbackContext value)
    {

        switch (value.phase)
        {
            case InputActionPhase.Started:
                leftTrigger = true;
                break;
        }
    }
    public void OnLeftShoulder(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Started:
                leftShoulder = true;
                break;
        }
    }
    public void OnRightTrigger(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Started:
                rightTrigger = true;
                break;
        }
    }
    public void OnRightShoulder(InputAction.CallbackContext value)
    {
        switch (value.phase)
        {
            case InputActionPhase.Started:
                rightShoulder = true;
                break;
        }
    }
}