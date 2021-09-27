using Packages.Rider.Editor.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrapper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }



}

class A
{
    public virtual void MyUpdate()
    {
        Debug.Log("A");
    }
}

class B : A
{
    public override void MyUpdate()
    {
        base.MyUpdate();
        Debug.Log("B");
    }
}
