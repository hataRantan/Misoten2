using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            MyAudioManeger.Instance.PlaySE("se1");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MyAudioManeger.Instance.PlaySE("se2");
        }
    }
}
