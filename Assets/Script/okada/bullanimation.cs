using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullanimation : MonoBehaviour
{
    public Animator animator;
    MyBullItem m_mybull;
    void Start()
    {
        animator = GetComponent<Animator>();

        //無効化する
        GetComponent<bullanimation>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        m_mybull = GetComponent<MyBullItem>();
        if (Input.GetKey(KeyCode.Mouse1))//押している時
        {
            animator.SetBool("Running", true);//走り始めモーション→走るモーションループ
        }
        else if (m_mybull.isHitWall) //壁にあたったらアニメーション止める
        {
            animator.SetBool("Running", false);//走り終わりモーションへ移行
        }
    }
}
