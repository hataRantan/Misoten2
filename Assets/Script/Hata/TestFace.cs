using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFace : MonoBehaviour
{
    [SerializeField]
    SkinnedMeshRenderer render = null;


    // Start is called before the first frame update
    void Start()
    {
        render.material.SetTexture("_MainTex", SavingFace.Instance.GetFace(0));
    }

}
