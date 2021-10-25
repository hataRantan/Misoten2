using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MyPlayerControll : MonoBehaviour
{
    // 入力処理
    [SerializeField]
    MyPlayerInput input = null;
    // プレイヤーの識別
    [SerializeField]
    private int playerIndex = 0;

    public MyPlayerInput Input { get { return input; } }

    // 移動速度
    [SerializeField]
    private float moveSpeed = 3f;
    // ジャンプの高さ
    [SerializeField]
    private float jumpHeight = 1.0f;
    // 重力計算
    [SerializeField]
    private float gravityValue = -9.81f;

    // キャラクターコントロールを使用する場合
    private CharacterController controller;

    private Vector3 playerVelocity;

    // 地面と接地しているかのフラグ
    private bool groundPlayer;

    private Vector2 movementInput = Vector2.zero;
    private bool jumped = false;

    /// <summary>
    /// キャラクターコントロール取得
    /// </summary>
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    /// <summary>
    /// プレイヤー人数カウント
    /// </summary>
    /// <returns></returns>
    public int GetPlayerIndex()
    {
        return playerIndex;
    }


    // Update is called once per frame
    void Update()
    {
        groundPlayer = controller.isGrounded;
        if (groundPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector3 moveDirection = new Vector3(input.move.x, 0, input.move.y);
        controller.Move(moveDirection * Time.deltaTime * moveSpeed);


        //if (moveDirection != Vector3.zero)
        //{
        //    gameObject.transform.forward = moveDirection;
        //}
        if (MyRapperInput.Instance.PressUpButton() && groundPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        if (MyRapperInput.Instance.RightTriggerButton() && groundPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        if (MyRapperInput.Instance.RightShoulderButton() && groundPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
