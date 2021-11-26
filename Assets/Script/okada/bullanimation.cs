using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullanimation : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))//押している間
        {
            animator.SetBool("Running", true);//走り始めモーション→走るモーションループ
        }
        else
        {
            animator.SetBool("Running", false);//走り終わりモーションへ移行
        }
    }
}
