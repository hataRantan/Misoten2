using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Proto_PlayerHP : MonoBehaviour
{
    [Header("プレイヤー用画像")]
    [SerializeField] GameObject hpUI = null;

    [Header("HPの位置間隔")]
    [SerializeField] float xIntervel = 25.0f;

    //実際に作成したもの
    List<Image> createdImages = new List<Image>();

    /// <summary>
    /// HPのImageを作成
    /// </summary>
    public void SetUpHPImage(uint _hp)
    {
        for (uint num = 0; num < _hp; num++)
        {
            Vector3 pos = gameObject.transform.position;
            //作成
            GameObject ui = Instantiate(hpUI, new Vector3(pos.x - xIntervel * num, pos.y, pos.z), Quaternion.identity);
            //親を設定
            ui.transform.parent = gameObject.transform;
            //大きさを変更
            ui.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            ui.transform.LookAt(Camera.main.transform.position);

            createdImages.Add(ui.GetComponent<Image>());
        }
    }

    public void DamageHP(int _hp)
    {
        for (int idx = createdImages.Count - 1; idx > (int)(_hp - 1); idx--)
        {
            createdImages[idx].enabled = false;
        }
    }
}
