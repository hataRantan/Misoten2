using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protoInput : MonoBehaviour
{
    //キーボードかコントローラのどちらかを使うかのフラグ
    public bool isController { get; set; }

    private void Awake()
    {
        isController = true;
    }

    /// <summary>
    /// 左スティックの入力（キーボードならWASD）
    /// </summary>
    public Vector2 Get_LeftStick()
    {
        Vector2 input = Vector2.zero;

        if(!isController)
        {
            if (Input.GetKey(KeyCode.W))
            {
                input.y = 1.0f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                input.y = -1.0f;
            }

            if(Input.GetKey(KeyCode.D))
            {
                input.x = 1.0f;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                input.x = -1.0f;
            }
        }
        else
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        return input;
    }

    /// <summary>
    /// Aボタンの入力した瞬間(キーボードならEnter)
    /// </summary>
    /// <returns></returns>
    public bool Get_AButtonDown()
    {
        bool isInput = false;

        if(!isController)
        {
            isInput = Input.GetMouseButtonDown(0);
        }
        else
        {
            isInput = Input.GetKeyDown("joystick button 0");
        }

        return isInput;
    }

    /// <summary>
    ///  Aボタンを推している間
    /// </summary>
    public bool Get_AButton()
    {
        bool isInput = false;

        if (!isController)
        {
            isInput = Input.GetMouseButton(0);
        }
        else
        {
            isInput = Input.GetKey("joystick button 0");
        }

        Debug.Log(isInput);
        return isInput;
    }

    /// <summary>
    /// Xボタンを押した瞬間
    /// </summary>
    public bool Get_XButtonDown()
    {
        bool isInput = false;

        if(!isController)
        {
            isInput = Input.GetMouseButtonDown(1);
        }
        else
        {
            isInput = Input.GetKeyDown("joystick button 2");
        }

        return isInput;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool Get_XButton()
    {
        bool isInput = false;

        if (!isController)
        {
            isInput = Input.GetMouseButton(1);
        }
        else
        {
            isInput = Input.GetKey("joystick button 2");
        }

        return isInput;
    }

    
}
