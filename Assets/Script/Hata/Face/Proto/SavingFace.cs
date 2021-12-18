﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 撮影した顔を保存するクラス
/// </summary>
public class SavingFace : Singleton<SavingFace>
{
    const int playerMaxNum = 4;

    private Texture2D[] m_saveFace = new Texture2D[playerMaxNum];

    [Header("カメラなしなどの場合に仕様する顔のテクスチャ")]
    [SerializeField] private List<Texture2D> m_spareFace = new List<Texture2D>();

    /// <summary>
    /// 顔の保存をする
    /// </summary>
    /// <param name="_face"> 保存したいテクスチャ </param>
    /// <param name="num"> ToDo：プロト用 </param>
    public void SaveFace(Texture2D _face, int _num = 0)
    {
        Clamp(ref _num);
        m_saveFace[_num] = _face;
    }

    /// <summary>
    /// 顔を取得する
    /// </summary>
    /// <param name="_num"> ToDo：プロト用 </param>
    public Texture2D GetFace(int _num=0)
    {
        Clamp(ref _num);
        return m_saveFace[_num];
    }

    private void Clamp(ref int _playerNumber)
    {
        if (_playerNumber < 0)
        {
            _playerNumber = 0;

            Debug.LogError("SaveingFace：添え字エラー");

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        else if (_playerNumber >= playerMaxNum)
        {
            _playerNumber = playerMaxNum - 1;

            Debug.LogError("SaveingFace：添え字エラー");

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
