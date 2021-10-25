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
    private PlayerInput playerInput;
    private MyPlayerControll myPlayerControll;

    /// <summary>
    /// プレイヤー毎に識別させて入力処理返す
    /// </summary>
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var myPlayerControlls = FindObjectsOfType<MyPlayerControll>();
        var index = playerInput.playerIndex;
        myPlayerControll = myPlayerControlls.FirstOrDefault(m => m.GetPlayerIndex() == index);
    }

    /// <summary>
    /// 移動：[Xbox] 左スティック [PS4] 左スティック [KeyBoard] WASD
    /// </summary>
    public Vector2 move { get; private set; }
    /// <summary>
    /// 視点移動：[Xbox] 右スティック [PS4] 右スティック [KeyBoard] マウスポインター
    /// </summary>
    public Vector2 look { get; private set; }
    /// <summary>
    /// ポーズ：[Xbox] StartButton  [PS4] OptionButton [KeyBoard] P
    /// </summary>
    public bool togglePause { get; private set; }
    /// <summary>
    /// 決定：[Xbox] 現在割り当て無し [PS4] 現在割り当て無し [KeyBoard] Enter
    /// </summary>
    public bool triggerSubmit { get; private set; }

    /// <summary>
    /// トリガー：[Xbox] LB [PS4] L1 [KeyBoard] 現在割り当て無し
    /// </summary>
    public bool leftTrigger { get; private set; }
    /// <summary>
    /// トリガー：[Xbox] LT [PS4] L2 [KeyBoard] 現在割り当て無し
    /// </summary>
    public bool leftShoulder { get; private set; }
    /// <summary>
    /// トリガー：[Xbox] RB [PS4] R1 [KeyBoard] 現在割り当て無し
    /// </summary>
    public bool rightTrigger { get; private set; }
    /// <summary>
    /// トリガー：[Xbox] RT [PS4] R2 [KeyBoard] 現在割り当て無し
    /// </summary>
    public bool rightShoulder { get; private set; }

    /// <summary>
    /// 押した瞬間：[Xbox] 上矢印 [PS4] 上矢印 [KeyBoard] 上矢印
    /// </summary>
    public bool pressArrowUp { get; private set; }
    /// <summary>
    /// 押した瞬間：[Xbox] 下矢印 [PS4] 下矢印 [KeyBoard] 下矢印
    /// </summary>
    public bool pressArrowDown { get; private set; }
    /// <summary>
    /// 押した瞬間：[Xbox] 左矢印 [PS4] 左矢印 [KeyBoard] 左矢印
    /// </summary>
    public bool pressArrowLeft { get; private set; }
    /// <summary>
    /// 押した瞬間：[Xbox] 右矢印 [PS4] 右矢印 [KeyBoard] 右矢印
    /// </summary>
    public bool pressArrowRight { get; private set; }
    /// <summary>
    /// 離した瞬間：[Xbox] 上矢印 [PS4] 上矢印 [KeyBoard] 上矢印
    /// </summary>
    public bool releaseArrowUp { get; private set; }
    /// <summary>
    /// 離した瞬間：[Xbox] 下矢印 [PS4] 下矢印 [KeyBoard] 下矢印
    /// </summary>
    public bool releaseArrowDown { get; private set; }
    /// <summary>
    /// 離した瞬間：[Xbox] 左矢印 [PS4] 左矢印 [KeyBoard] 左矢印
    /// </summary>
    public bool releaseArrowLeft { get; private set; }
    /// <summary>
    /// 離した瞬間：[Xbox] 右矢印 [PS4] 右矢印 [KeyBoard] 右矢印
    /// </summary>
    public bool releaseArrowRight { get; private set; }
    /// <summary>
    /// 一定時間押下状態経過後：[Xbox] 上矢印 [PS4] 上矢印 [KeyBoard] 上矢印
    /// </summary>
    public bool holdArrowUp { get; private set; }
    /// <summary>
    /// 一定時間押下状態経過後：[Xbox] 下矢印 [PS4] 下矢印 [KeyBoard] 下矢印
    /// </summary>
    public bool holdArrowDown { get; private set; }
    /// <summary>
    /// 一定時間押下状態経過後：[Xbox] 左矢印 [PS4] 左矢印 [KeyBoard] 左矢印
    /// </summary>
    public bool holdArrowLeft { get; private set; }
    /// <summary>
    /// 一定時間押下状態経過後：[Xbox] 右矢印 [PS4] 右矢印 [KeyBoard] 右矢印
    /// </summary>
    public bool holdArrowRight { get; private set; }

    /// <summary>
    /// 押した瞬間：[Xbox] Y [PS4] △ [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool pressNorthButton { get; private set; }
    /// <summary>
    /// 押した瞬間：[Xbox] A [PS4] × [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool pressSouthButton { get; private set; }
    /// <summary>
    /// 押した瞬間：[Xbox] × [PS4] □ [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool pressWestButton { get; private set; }
    /// <summary>
    /// 押した瞬間：[Xbox] B [PS4] 〇 [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool pressEastButton { get; private set; }
    /// <summary>
    /// 離した瞬間：[Xbox] Y [PS4] △ [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool releaseNorthButton { get; private set; }
    /// <summary>
    /// 離した瞬間：[Xbox] A [PS4] × [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool releaseSouthButton { get; private set; }
    /// <summary>
    /// 離した瞬間：[Xbox] × [PS4] □ [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool releaseWestButton { get; private set; }
    /// <summary>
    /// 離した瞬間：[Xbox] B [PS4] 〇 [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool releaseEastButton { get; private set; }
    /// <summary>
    /// 一定時間押下状態経過後：[Xbox] Y [PS4] △ [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool holdNorthButton { get; private set; }
    /// <summary>
    /// 一定時間押下状態経過後：[Xbox] A [PS4] × [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool holdSouthButton { get; private set; }
    /// <summary>
    /// 一定時間押下状態経過後：[Xbox] × [PS4] □ [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool holdWestButton { get; private set; }
    /// <summary>
    ///一定時間押下状態経過後：[Xbox] B [PS4] 〇 [KeyBoard] 現在割り当てなし
    /// </summary>
    public bool holdEastButton { get; private set; }


    public void OnMove(InputAction.CallbackContext value) => move = value.ReadValue<Vector2>();
    public void OnLook(InputAction.CallbackContext value) => look = value.ReadValue<Vector2>();
    public void OnTogglePause(InputAction.CallbackContext value) => togglePause = value.performed;
    public void OnTriggerSubmit(InputAction.CallbackContext value) => triggerSubmit = value.performed;

    public void OnLeftTrigger(InputAction.CallbackContext value) => leftTrigger = value.performed;
    public void OnLeftShoulder(InputAction.CallbackContext value) => leftShoulder = value.performed;
    public void OnRightTrigger(InputAction.CallbackContext value) => rightTrigger = value.performed;
    public void OnRightShoulder(InputAction.CallbackContext value) => rightShoulder = value.performed;


    public void OnPressArrowUp(InputAction.CallbackContext value) => pressArrowUp = value.performed;
    public void OnPressArrowDown(InputAction.CallbackContext value) => pressArrowDown = value.performed;
    public void OnPressArrowLeft(InputAction.CallbackContext value) => pressArrowLeft = value.performed;
    public void OnPressArrowRight(InputAction.CallbackContext value) => pressArrowRight = value.performed;
    public void OnReleaseArrowUp(InputAction.CallbackContext value) => releaseArrowUp = value.performed;
    public void OnReleaseArrowDown(InputAction.CallbackContext value) => releaseArrowDown = value.performed;
    public void OnReleaseArrowLeft(InputAction.CallbackContext value) => releaseArrowLeft = value.performed;
    public void OnReleaseArrowRight(InputAction.CallbackContext value) => releaseArrowRight = value.performed;
    public void OnHoldArrowUp(InputAction.CallbackContext value) => holdArrowUp = value.performed;
    public void OnHoldArrowDown(InputAction.CallbackContext value) => holdArrowDown = value.performed;
    public void OnHoldArrowLeft(InputAction.CallbackContext value) => holdArrowLeft = value.performed;
    public void OnHoldArrowRight(InputAction.CallbackContext value) => holdArrowRight = value.performed;

    public void OnPressNorthButtont(InputAction.CallbackContext value) => pressNorthButton = value.performed;
    public void OnPressSouthButtont(InputAction.CallbackContext value) => pressSouthButton = value.performed;
    public void OnPressWestButtont(InputAction.CallbackContext value) => pressWestButton = value.performed;
    public void OnPressEastButtont(InputAction.CallbackContext value) => pressEastButton = value.performed;
    public void OnReleaseNorthButtont(InputAction.CallbackContext value) => releaseNorthButton = value.performed;
    public void OnReleaseSouthButtont(InputAction.CallbackContext value) => releaseSouthButton = value.performed;
    public void OnReleaseWestButtont(InputAction.CallbackContext value) => releaseWestButton = value.performed;
    public void OnReleaseEastButtont(InputAction.CallbackContext value) => releaseEastButton = value.performed;
    public void OnHoldNorthButtont(InputAction.CallbackContext value) => holdNorthButton = value.performed;
    public void OnHoldSouthButtont(InputAction.CallbackContext value) => holdSouthButton = value.performed;
    public void OnHoldWestButtont(InputAction.CallbackContext value) => holdWestButton = value.performed;
    public void OnHoldEastButtont(InputAction.CallbackContext value) => holdEastButton = value.performed;
}