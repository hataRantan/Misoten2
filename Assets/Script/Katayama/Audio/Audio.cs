using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Audio : MonoBehaviour
{
    public AudioSource audio_BGM;
    public AudioSource audio_SE;

    public AudioClip BGM;

    public AudioClip se1;

    public void Start()
    {

        audio_BGM = gameObject.GetComponent<AudioSource>();
        audio_SE = gameObject.GetComponent<AudioSource>();

    }

    public void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            //音(sound1)を鳴らす
            audio_SE.PlayOneShot(se1);

        }
        if (Input.GetKeyDown("b"))
        {
            //音(sound1)を鳴らす
            audio_SE.PlayOneShot(BGM);


        }


    }

}
