using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTest : MonoBehaviour
{
    string last = "SampleScene";
    string next = "New Scene";

    [SerializeField] UnityEngine.UI.Image image = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneLoader.Instance.CallLoadSceneDefault(next);
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            SceneLoader.Instance.CallLastSceneDefault();
        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(TestImage());
        }
    }

    private IEnumerator TestImage()
    {
        image.fillAmount = 0.0f;

        ProcessTimer timer = new ProcessTimer();
        timer.Restart();

        while(timer.TotalSeconds<1.0f)
        {
            image.fillAmount = Easing.QuadIn(timer.TotalSeconds, 1.0f, 0.0f, 1.0f);

            yield return null;
        }
    }
}
