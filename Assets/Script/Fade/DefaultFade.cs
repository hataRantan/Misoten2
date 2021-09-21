using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultFade : FadeBase
{
    [SerializeField] Image fadeIn = null;
    [SerializeField] Image fadeOut = null;

    [Range(1.0f, 2.0f)]
    [SerializeField] float fadeTime = 1.0f;

    public new void Awake()
    {
        base.Awake();
    }

    public override IEnumerator FadeIn()
    {
        fadeIn.fillAmount = 0.0f;

        fadeIn.gameObject.SetActive(true);
        fadeOut.gameObject.SetActive(false);

        //Debug.Log("来たよ");
        //while(true)
        //{
        //    yield return null;
        //}


        ProcessTimer timer = new ProcessTimer();
        timer.Restart();

        Debug.Log("タイム：" + timer.TotalSeconds);

        //ToDo：Easing導入すれば変わるか？
        while (timer.TotalSeconds < 1.0f)
        {
            fadeIn.fillAmount = Easing.QuadIn(timer.TotalSeconds, 1.0f, 0.0f, 1.0f);
           // Debug.Log("Timer：" + timer.TotalSeconds);

            //if(fadeIn.fillAmount>0.5f)
            //{
            //    Debug.Log("fillObj" + fadeIn.gameObject.name);
            //    Debug.Log("fill：" + fadeIn.fillAmount);
            //    UnityEditor.EditorApplication.isPaused = true;
            //}

            yield return null;
        }

        fadeIn.fillAmount = 1.0f;

        Debug.Log("タイム2：" + timer.TotalSeconds);
    }

    public override IEnumerator FadeOut()
    {
        fadeOut.fillAmount = 1.0f;

        fadeIn.gameObject.SetActive(false);
        fadeOut.gameObject.SetActive(true);

        float fill = 1.0f;

        ProcessTimer timer = new ProcessTimer();
        timer.Restart();

        while (timer.TotalSeconds < fadeTime)
        {
            fill = 1.0f - timer.TotalSeconds;
            fadeOut.fillAmount = fill;

            yield return null;
        }

        fadeOut.fillAmount = 0.0f;
    }
}
