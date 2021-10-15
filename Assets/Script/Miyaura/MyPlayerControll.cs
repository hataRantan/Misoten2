using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyPlayerControll : MonoBehaviour
{
    [SerializeField]
    MyPlayerInput input = null;
    InputAction.CallbackContext phase;
    // Update is called once per frame
    void Update()
    {
#if else
        Debug.Log("押下状態なら：上矢印" + input.pressArrowUp);

        Debug.Log("一定時間経過後に離す：上矢印" + input.holdArrowUp);

        Debug.Log("移動" + input.move);
        Debug.Log("視点" + input.look);
        Debug.Log("ポーズ" + input.togglePause);
        Debug.Log("決定" + input.triggerSubmit);

        Debug.Log("時間指定内に離したら：上矢印" + input.tapArrowUp);
        Debug.Log("時間指定内に離したら：下矢印" + input.tapArrowDown);
        Debug.Log("時間指定内に離したら：左矢印" + input.tapArrowLeft);
        Debug.Log("時間指定内に離したら：右矢印" + input.tapArrowRight);
        Debug.Log("押下状態なら：上矢印" + input.pressArrowUp);
        Debug.Log("押下状態なら：下矢印" + input.pressArrowDown);
        Debug.Log("押下状態なら：左矢印" + input.pressArrowLeft);
        Debug.Log("押下状態なら：右矢印" + input.pressArrowRight);
        Debug.Log("一定時間経過後に離す：上矢印" + input.holdArrowUp);
        Debug.Log("一定時間経過後に離す：下矢印" + input.holdArrowDown);
        Debug.Log("一定時間経過後に離す：左矢印" + input.holdArrowLeft);
        Debug.Log("一定時間経過後に離す：右矢印" + input.holdArrowRight);

        Debug.Log("時間指定内に離したら：上矢印" + input.tapNorthButton);
        Debug.Log("時間指定内に離したら：下矢印" + input.tapNorthButton);
        Debug.Log("時間指定内に離したら：左矢印" + input.tapWestButton);
        Debug.Log("時間指定内に離したら：右矢印" + input.tapEastButton);
        Debug.Log("押下状態なら：上矢印" + input.pressNorthButton);
        Debug.Log("押下状態なら：下矢印" + input.pressSouthButton);
        Debug.Log("押下状態なら：左矢印" + input.pressWestButton);
        Debug.Log("押下状態なら：右矢印" + input.pressEastButton);
        Debug.Log("一定時間経過後に離す：上矢印" + input.holdNorthButton);
        Debug.Log("一定時間経過後に離す：下矢印" + input.holdSouthButton);
        Debug.Log("一定時間経過後に離す：左矢印" + input.holdWestButton);
        Debug.Log("一定時間経過後に離す：右矢印" + input.holdEastButton);
#endif
        Debug.Log("押した瞬間：上矢印" + input.pressArrowUp);
        Debug.Log("離した瞬間：上矢印" + input.releaseArrowUp);
        Debug.Log("一定時間経過後に離す：上矢印" + input.holdArrowUp);
    }
}
