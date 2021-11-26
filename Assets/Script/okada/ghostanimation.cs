using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostanimation : MonoBehaviour
{
    Animator animator;
    private bool dependence;
    private int dependenceCount;
    private int dependenceRimit;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dependence == false)//待機中
        {
            animator.SetBool("dependenceing", false);
            animator.SetBool("dependencestop", false);
        }
        if (Input.GetKeyDown("space")&&dependence==false)//憑依開始
        {
            dependence = true;
            animator.SetBool("dependenceing",true);//憑依開始モーション終了後憑依中モーションに移行
        }
        if (dependence)//憑依中
        {
            dependenceRimit++;
            //*******************　憑依中のゲージ貯め　***********************
            if (Input.GetKeyDown("space"))
            {
                dependenceCount++;
                dependenceRimit = 0;
            }
            //*******************　憑依中のゲージ貯め　***********************

            //*******************　憑依キャンセル　***********************
            if (dependenceRimit > 500)
            {
                animator.SetBool("dependencestop", true);//待機モーションへ移行
                dependence = false;
                dependenceCount = 0;
                dependenceRimit = 0;
            }
            //*******************　憑依キャンセル　***********************

            //*******************　憑依完了　***********************
            if (dependenceCount > 5)
            {
                animator.SetBool("dependenceing", false);//憑依完了モーションへ移行
                dependenceCount = 0;
                dependenceRimit = 0;
                dependence = false;
            }
            //*******************　憑依完了　***********************
        }
    }
}
