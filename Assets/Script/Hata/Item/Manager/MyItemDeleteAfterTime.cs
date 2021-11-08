using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItemDeleteAfterTime : MonoBehaviour
{
    [SerializeField]
    private float timer = 10.0f;
    float currentTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if(currentTime<timer)
        {
            currentTime += Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
