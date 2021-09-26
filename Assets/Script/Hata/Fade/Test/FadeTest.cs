using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTest : MonoBehaviour
{
    string last = "SampleScene";
    string next = "New Scene";


    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool flg = true;

    // Update is called once per frame
    void Update()
    {
        if (!flg) return; 

        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneLoader.Instance.CallLoadSceneDefault(next);
            flg = false;
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            SceneLoader.Instance.CallLastSceneDefault();
        }
    }


}
