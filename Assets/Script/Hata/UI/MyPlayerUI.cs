using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MyPlayerUI : MonoBehaviour
{
    [Header("プレイヤー名")]
    [SerializeField] TextMeshProUGUI m_name = null;

    [Header("増量するHp画像")]
    [SerializeField] Image m_hpImage = null;

    [Header("プレイヤーの連打ゲージ")]
    [SerializeField] Image m_blowGauge = null;

    [Header("プレイヤーの顔")]
    [SerializeField]
    RawImage m_face = null;

    [Header("設定するテクスチャ")]
    [SerializeField]
    Texture[] m_faceTexture = null;

    //生成したゲームHp
    private List<Image> m_playerHps = new List<Image>();

    //現在取得中のアイテムの取得連打数
    private int m_maxBlow = 0;

    //前回のHp
    float m_lastHp = 0;
    //ダメージ表現のコルーチン
    private Coroutine m_damageEffect = null;
    //顔ダメージ管理クラス
    MyPlayerFaceControl m_faceDamage = null;
    //プレイヤー番号
    int m_playerNumber = 0;
    //ダメージ表現が発生中かのフラグ
    bool m_damageFlg = false;

    /// <summary>
    /// 生成時の初期化
    /// </summary>
    /// <param name="_parent"> 親に設定するキャンバス </param>
    /// <param name="_pos"> 出現させたい位置 </param>
    public void SetUp(Canvas _parent, Vector2 _pos, string _playerName, int _playerNum, int _playerMaxHp, Color _playerColor, MyPlayerFaceControl _damage)
    {
        //指定キャンバスの子に変更
        this.gameObject.transform.parent = _parent.transform;

        //管理クラスを取得
        m_faceDamage = _damage;

        //位置変更
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        rect.anchoredPosition3D = new Vector3(_pos.x, _pos.y, 0.0f);
        rect.localScale = Vector3.one;
        rect.localEulerAngles = Vector3.zero;

        //フェイスUIにテクスチャをセットする
        m_playerNumber = _playerNum;
        m_face.texture = m_faceTexture[m_playerNumber];

        //名前変更
        m_name.text = _playerName;

        //Hp画像の幅を取得
        float hpWidth = m_hpImage.rectTransform.sizeDelta.x;
        //位置を取得
        Vector2 hpPos = m_hpImage.rectTransform.anchoredPosition;

        m_playerHps.Add(m_hpImage);

        //Hpの生成
        for (int idx = 1; idx < _playerMaxHp; idx++)
        {
            //生成
            GameObject obj = Instantiate(m_hpImage.gameObject, Vector3.zero, Quaternion.identity);
            obj.transform.parent = gameObject.transform;
            //位置を調整
            RectTransform objPos = obj.GetComponent<RectTransform>();
            objPos.anchoredPosition3D = new Vector3(hpPos.x + hpWidth * idx, hpPos.y, 0.0f);
            objPos.localScale = Vector3.one;
            objPos.localEulerAngles = Vector3.zero;

            m_playerHps.Add(obj.GetComponent<Image>());
        }

        m_lastHp = _playerMaxHp;

        //テキストカラー変更
        VertexGradient gradient = m_name.colorGradient;
        gradient.bottomLeft = _playerColor;
        gradient.bottomRight = _playerColor;
        m_name.colorGradient = gradient;

        //ゲージカラー変更
        m_blowGauge.color = _playerColor;
        m_blowGauge.fillAmount = 0.0f;
    }

    /// <summary>
    /// アイテムの取得連打数を設定する
    /// </summary>
    public void SetUpBlowGauge(int _maxBlow)
    {
        m_maxBlow = _maxBlow;
    }

    /// <summary>
    /// 現在の連打数をゲージに反映させる
    /// </summary>
    public void BlowInGauge(int _currentBlowNum)
    {
        //連打の割合を求める
        float percentage = (float)_currentBlowNum / m_maxBlow;
        m_blowGauge.fillAmount = percentage;
    }

    /// <summary>
    /// ダメージをUIに反映
    /// </summary>
    public void DamageEffect(int _lastHp,int _currentHp)
    {
        if (m_lastHp != _currentHp)
        {
            //コルーチン停止
            if (m_damageFlg) StopCoroutine(m_damageEffect);

            //コルーチン開始
            m_damageFlg = true;
            m_damageEffect = StartCoroutine(m_faceDamage.DamageFace(m_playerNumber, EndDamageAction));
        }

        for (int idx = _lastHp - 1; idx > _currentHp - 1; idx--)
        {
            //配列の範囲外なら除外
            if (idx < 0) break;

            //ToDo：HpUIの減少の動きを追加
            m_playerHps[idx].enabled = false;
        }
    }

    //終了確認フラグ
    void EndDamageAction()
    {
        m_damageFlg = false;
    }
}
