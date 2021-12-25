using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyPlayerMoveAction : MonoBehaviour
{
    // オブジェクトの情報取得
    Transform playerObject;
    // 移動タイプ
    MyMoveType myMoveType;
    // デフォルト角度
    Quaternion defaultRotation;
    // 物理演算毎秒管理
    private float sec;

    // アニメーション中かどうかの判別
    private bool jumpFlg = false;
    private bool wideFlg = false;
    private bool bowFlg = false;
    private bool rowlingFlg = false;

    [Header("Jump:モーション速度")]
    [SerializeField] private float flightTime = 1;
    [Header("JUMP:高さ")]
    [SerializeField] private float targetY = 5.0f;
    private float timer = 0.0f;
    private float defaultYPos = 0.0f;

    [Header("WideWalk:反復角度[左右]")]
    [SerializeField] private float wideAngleZ = 20;
    [Header("WideWalk:速度")]
    [SerializeField] private float wideSpeed = 0.5f;

    [Header("Bow:反復角度[前後]")]
    [SerializeField] private float bowAngleX = 30;
    [Header("Bow:速度")]
    [SerializeField] private float bowSpeed = 0.7f;

    [Header("Rowling:速度")]
    [SerializeField] private float rowlingSpeed = 0.7f;

    /// <summary>
    /// 移動方法の種類
    /// </summary>
    private enum MyMoveType
    {
        JUMP,           // ジャンプ
        WIDE_WALK,      // テクテク
        BOW,            // お辞儀移動
        ROWLING,        // 転がり
    }

    private void Awake()
    {
        // オブジェクトの情報取得
        playerObject = this.gameObject.transform;
        // オブジェクトのデフォルト角度をセット
        defaultRotation = playerObject.rotation;
    }

    public void MoveMode(int _moveNum, Vector2 _input)
    {
        NumClamp(ref _moveNum);
        myMoveType = (MyMoveType)_moveNum;
        sec += Time.deltaTime;

        switch (myMoveType)
        {
            case MyMoveType.JUMP:
                if (!jumpFlg)
                {
                    StartCoroutine(MoveJumpCoroutine());
                }
                break;

            case MyMoveType.WIDE_WALK:
                if (!wideFlg)
                {
                    StartCoroutine(WideCoroutine());
                }
                break;

            case MyMoveType.BOW:
                if (!bowFlg)
                {
                    StartCoroutine(BowCoroutine());
                }
                break;

            case MyMoveType.ROWLING:
                if (!rowlingFlg)
                {
                    StartCoroutine(RowlingCoroutine(_input));
                }
                break;

            default:
                defaultRotation.w = 1.0f;
                playerObject.rotation = defaultRotation;
                return;

        }

    }

    private IEnumerator MoveJumpCoroutine()
    {
        jumpFlg = true;

        //var startPos = playerObject.position;  // 初期位置
        //var diffY = (endPos - startPos).y;     // 始点と終点のy成分の差分
        //var vn = (diffY - gravityValue * 0.5f * flightTime * flightTime) / flightTime; // 鉛直方向の初速度vn

        //// 放物運動
        //for (var time = 0f; time < flightTime; time += (Time.deltaTime * speedRate))
        //{
        //    var p = Vector3.Lerp(startPos, endPos, time / flightTime);   //水平方向の座標を求める (x,z座標)
        //    p.y = startPos.y + vn * time + 0.5f * gravityValue * time * time; // 鉛直方向の座標 y
        //    playerObject.position = p;
        //    yield return null; //1フレーム経過
        //}
        //// 終点座標へ補正
        //playerObject.position = endPos;
        yield return null;
        jumpFlg = false;



    }
    private IEnumerator WideCoroutine()
    {
        wideFlg = true;
        int cnt = 0;
        int wideRight = (int)wideAngleZ;
        int wideLeft = (int)wideAngleZ * 2;
        while (wideFlg)
        {
            if (cnt == 0)
            {
                for (float i = 0; i < wideRight; i += wideSpeed)
                {
                    playerObject.Rotate(new Vector3(0f, 0f, -wideSpeed), Space.World);
                    yield return null;
                }
                cnt++;
            }
            else if (cnt == 1)
            {
                for (float i = 0; i < wideLeft; i += wideSpeed)
                {
                    playerObject.Rotate(new Vector3(0f, 0f, wideSpeed), Space.World);
                    yield return null;
                }
                cnt++;
            }
            else if (cnt == 2)
            {
                for (float i = 0; i < wideRight; i += wideSpeed)
                {
                    playerObject.Rotate(new Vector3(0f, 0f, -wideSpeed), Space.World);
                    yield return null;
                }
                break;
            }
        }
        playerObject.rotation = defaultRotation;
        wideFlg = false;
    }

    private IEnumerator BowCoroutine()
    {
        bowFlg = true;
        int cnt = 0;
        int bowForward = (int)bowAngleX;
        int bowBack = (int)bowAngleX * 2;
        while (bowFlg)
        {
            if (cnt == 0)
            {
                for (float i = 0; i < bowForward; i += bowSpeed)
                {
                    playerObject.Rotate(new Vector3(bowSpeed, 0f, 0f), Space.World);
                    yield return null;
                }
                cnt++;
            }
            else if (cnt == 1)
            {
                for (float i = 0; i < bowBack; i += bowSpeed)
                {
                    playerObject.Rotate(new Vector3(-bowSpeed, 0f, 0f), Space.World);
                    yield return null;
                }
                cnt++;
            }
            else if (cnt == 2)
            {
                for (float i = 0; i < bowForward; i += bowSpeed)
                {
                    playerObject.Rotate(new Vector3(bowSpeed, 0f, 0f), Space.World);
                    yield return null;
                }
                break;
            }
        }
        playerObject.rotation = defaultRotation;
        bowFlg = false;

    }
    private IEnumerator RowlingCoroutine(Vector2 _input)
    {
        rowlingFlg = true;                      // 回転中か
        int sign = (int)Mathf.Sign(_input.x);   // 正か負を判別して-1or1を返す

        for (float turn = 0; turn < 90; turn += rowlingSpeed)
        {
            playerObject.Rotate(new Vector3(0f, 0f, -sign * rowlingSpeed), Space.World);
            yield return null;
        }
        playerObject.rotation = defaultRotation;
        rowlingFlg = false;
    }


    /// <summary>
    /// MoveTypeの種類数以内かの判別
    /// </summary>
    /// <param name="_num">どの移動タイプか</param>
    private void NumClamp(ref int _num)
    {
        if (0 <= _num || 3 >= _num) return;

        Debug.LogError("NumClampError:0～3以外の値が入っています");

        //  最小値以下なら０、最大値以上なら３を返す
        _num = Mathf.Clamp(_num, 0, 3);
    }
}

// Jumpの終点座標がX移動中の挙動を確かめないといけない