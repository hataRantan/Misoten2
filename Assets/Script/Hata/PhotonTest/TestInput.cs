using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    Transform transform = null;

    // Start is called before the first frame update
    void Start()
    {
        transform = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= Vector3.forward * 1.0f;
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.position -= Vector3.right * 1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * 1.0f;
        }
    }
}
