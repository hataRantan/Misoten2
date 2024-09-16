using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MyAudioManeger.Instance.PlayBGM("bgm4");

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("a"))
        {
            MyAudioManeger.Instance.PlaySE("se1");
        }
        if (Input.GetKeyDown("b"))
        {
            MyAudioManeger.Instance.PlaySE("se2");
        }
        if (Input.GetKeyDown("c"))
        {
            MyAudioManeger.Instance.PlaySE("se3");
        }
        if (Input.GetKeyDown("f"))
        {
            MyAudioManeger.Instance.FadeOutBGM();
        }
        if (Input.GetKeyDown("s"))
        {
            MyAudioManeger.Instance.StopBGM();
        }

    }
}
