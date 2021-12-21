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

    //使用コントローラ
    int m_useType = 0;
    MyPlayerInput.Type m_device = MyPlayerInput.Type.PS4;

    //ToDo：受付の入力コントローラを取得する
    public void Init(int _playerIdx)
    {
        //移動以外のUIを全て非常時に変更
        for (int idx = 0; idx < m_getUI.Length; idx++)
        {
            m_getUI[idx].enabled = false;
            m_actionUI[idx].enabled = false;
        }

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
}
