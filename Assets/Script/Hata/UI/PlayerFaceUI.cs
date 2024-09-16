//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerFaceUI : MonoBehaviour
//{
//    [Header("操作対象")]
//    [SerializeField]
//    GameObject m_faceUI = null;

//    SkinnedMeshRenderer renderer = null;

//    [Header("ダメージ時の歪ませ量")]
//    [Range(0.0f, 100.0f)]
//    [SerializeField]
//    float[] m_damageShapes = new float[4];

//    [Header("ダメージ時のシェイプの変化時間")]
//    [SerializeField]
//    float m_shpaTime = 1.0f;

//    private void Start()
//    {
//        renderer = m_faceUI.GetComponent<SkinnedMeshRenderer>();

//        renderer.material.SetTexture("_MainTex", SavingFace.Instance.GetFace(0));

//    }

//    private void Update()
//    {
//        if(Input.GetKeyDown(KeyCode.K))
//        {
//            StartCoroutine(DamageShape());
//        }
//    }

//    private IEnumerator DamageShape()
//    {
//        float[] firstShapes = new float[4];
//        for (int idx = 0; idx < 4; idx++)
//        {
//            firstShapes[idx] = renderer.GetBlendShapeWeight(idx);
//        }

//        float timer = 0.0f;
//        while (timer < m_shpaTime)
//        {
//            for (int idx = 0; idx < 4; idx++)
//            {
//                float shape = Easing.SineIn(timer, m_shpaTime, firstShapes[idx], m_damageShapes[idx]);
//                renderer.SetBlendShapeWeight(idx, shape);
//            }

//            timer += Time.deltaTime;
//            yield return null;
//        }

//        timer = 0.0f;
//        while(timer<1.0f)
//        {
//            timer += Time.deltaTime;
//            yield return null;
//        }

//        timer = 0.0f;
//        while(timer<1.0f)
//        {
//            for (int idx = 0; idx < 4; idx++)
//            {
//                float shape = -Easing.SineIn(timer, m_shpaTime, -m_damageShapes[idx], -firstShapes[idx]);
//                renderer.SetBlendShapeWeight(idx, shape);
//            }

//            timer += Time.deltaTime;
//            yield return null;
//        }
//    }
//}
