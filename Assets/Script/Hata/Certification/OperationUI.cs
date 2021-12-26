using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationUI : MonoBehaviour
{
    [Header("スティックのUI")]
    [SerializeField] Image m_stickUI = null;

    [Header("アイテム取得のUI")]
    [SerializeField] Image[] m_getUI = null;

    [Header("アイテム使用のUI")]
    [SerializeField] Image[] m_actionUI = null;

    [Header("吹き出し")]
    [SerializeField] Image m_bloeUI = null;

    //使用コントローラ
    int m_useType = 0;
    MyPlayerInput.Type m_device = MyPlayerInput.Type.PS4;

    //初期位置
    Vector2 m_firstUIPos = Vector2.zero;

    [Header("上昇に掛かる時間")]
    [SerializeField]
    [Range(0.0f, 5.0f)]
    float m_waveTime = 2.0f;
    float cTimer = 0.0f;

    [Header("上昇量")]
    [SerializeField]
    [Range(0.0f, 100.0f)]
    float m_wavePower = 50.0f;

    //ToDo：受付の入力コントローラを取得する
    public void Init(int _playerIdx)
    {
        //移動以外のUIを全て非常時に変更
        for (int idx = 0; idx < m_getUI.Length; idx++)
        {
            m_getUI[idx].enabled = false;
            m_actionUI[idx].enabled = false;
        }

        m_firstUIPos = m_getUI[0].rectTransform.anchoredPosition;
        m_firstUIPos += this.gameObject.GetComponent<RectTransform>().anchoredPosition;
       // m_firstUIPos += gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition;

        //接続デバイスの種類を取得する
        m_device = MyRapperInput.Instance.GetDeviceType(_playerIdx);
        m_useType = (int)m_device;

        ChangeUI(CertificationPlayer.State.MOVE);
    }

    public void NonDisply()
    {
        m_stickUI.color = new Color(1, 1, 1, 0);
        for (int idx = 0; idx < m_getUI.Length; idx++)
        {
            m_getUI[idx].color = new Color(1, 1, 1, 0);
            m_actionUI[idx].color = new Color(1, 1, 1, 0);
        }
        m_bloeUI.color = new Color(1, 1, 1, 0.0f);
    }

    public void NonDIsplyAlpha(float _alpha)
    {
        m_stickUI.color = new Color(1, 1, 1, _alpha);
        for (int idx = 0; idx < m_getUI.Length; idx++)
        {
            m_getUI[idx].color = new Color(1, 1, 1, _alpha);
            m_actionUI[idx].color = new Color(1, 1, 1, _alpha);
        }
        m_bloeUI.color = new Color(1, 1, 1, _alpha);
    }

    /// <summary>
    /// UIの変更
    /// </summary>
    public void ChangeUI(CertificationPlayer.State _state)
    {
        switch (_state)
        {
            case CertificationPlayer.State.MOVE:
                {
                    m_stickUI.enabled = true;
                    m_getUI[m_useType].enabled = false;
                    m_actionUI[m_useType].enabled = false;
                }
                break;

            case CertificationPlayer.State.GET:
                {
                    m_stickUI.enabled = false;
                    m_getUI[m_useType].enabled = true;
                    m_actionUI[m_useType].enabled = false;
                }
                break;

            case CertificationPlayer.State.ACTION:
                {
                    m_stickUI.enabled = false;
                    m_getUI[m_useType].enabled = false;
                    m_actionUI[m_useType].enabled =true;
                }
                break;
        }
    }

    /// <summary>
    /// UIを上下させる
    /// </summary>
    public void WaveUI(CertificationPlayer.State _state)
    {
        Vector2 pos = m_firstUIPos;
        //上昇
        if (cTimer < m_waveTime)
        {
            cTimer += Time.deltaTime;

            pos.y = Easing.CubicOut(cTimer, m_waveTime, m_firstUIPos.y, m_firstUIPos.y + m_wavePower);
        }
        //下降
        else
        {
            cTimer = 0.0f;
            pos.y = 0.0f;
        }

        switch(_state)
        {
            case CertificationPlayer.State.GET:
                {
                    m_getUI[m_useType].rectTransform.anchoredPosition = pos;
                }
                break;

            case CertificationPlayer.State.ACTION:
                {
                    m_actionUI[m_useType].rectTransform.anchoredPosition = pos;
                }
                break;
        }
    }

    /// <summary>
    /// UIを初期位置に変更
    /// </summary>
    public void WaveReset()
    {
        m_getUI[m_useType].rectTransform.anchoredPosition = m_firstUIPos;
        m_actionUI[m_useType].rectTransform.anchoredPosition = m_firstUIPos;
    }
}
