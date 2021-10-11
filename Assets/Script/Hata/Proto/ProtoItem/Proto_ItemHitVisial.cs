using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proto_ItemHitVisial : MonoBehaviour
{
    private MeshRenderer mesh = null;
    private Color red = new Color(1.0f, 0.0f, 0.0f, 0.3f);
    private Color white = new Color(0.0f, 0.0f, 0.0f, 0.0f);

    private void Awake()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
    }


    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            mesh.material.color = red;
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            mesh.material.color = white;
        }
    }
}
