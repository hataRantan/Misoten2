using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTUTU : MonoBehaviour
{
    [SerializeField] Transform player = null;
    RectTransform rect = null;
    Canvas canvas = null;
    
    // Start is called before the first frame update
    void Start()
    {
        rect = this.gameObject.GetComponent<RectTransform>();
        canvas = this.gameObject.transform.parent.gameObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        Vector2 pos;
        // UIのオブジェクトをワールド座標からスクリーン座標に変換する。
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(Camera.main, player.position);
        // スクリーン座標からローカル座標(親UIからのCenterからの相対座標)を取得する。
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.gameObject.GetComponent<RectTransform>(), screenPos, Camera.main, out pos);
        //myRectTransform.position = myRectTransform.position;
        // ローカル座標を設定する。
        rect.localPosition = pos;
    }
}
