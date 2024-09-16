using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerFaceControl : MyUpdater
{
    [Header("操作対象")]
    [SerializeField]
    SkinnedMeshRenderer[] m_targetFaces = null;

    [Header("現在使用しているシェイプキーの個数")]
    [SerializeField]
    int m_shapeNum = 4;

    [Header("ダメージ表現回数")]
    [Range(1, 5)]
    [SerializeField]
    int m_damageNum = 2;

    [Header("顔を歪ませる時間")]
    [Range(0.0f, 1.0f)]
    [SerializeField]
    float m_damegeTime = 0.2f;

    [Header("顔を歪みから戻す時間")]
    [Range(0.0f, 1.0f)]
    [SerializeField]
    float m_damagebackTime = 0.1f;

    [Header("歪ませ状態の停止時間")]
    [Range(0.0f, 1.0f)]
    [SerializeField]
    float m_damageStopTime = 0.05f;

    //現在のプレイヤーに人数
    int m_cPlayerNum = 0;
    public override void MyFastestInit()
    {
        //人数を取得する
        m_cPlayerNum = GameInPlayerNumber.Instance.CurrentPlayerNum;

        //撮影した顔を設定する
        for (int idx = 0; idx < m_cPlayerNum; idx++)
        {
            m_targetFaces[idx].material.SetTexture("_MainTex", SavingFace.Instance.GetFace(idx));
        }
    }

    public void StopALL()
    {
        StopAllCoroutines();
        for (int idx = 0; idx < m_targetFaces.Length; ++idx)
        {
            for(int shape=0;shape<m_shapeNum;++shape)
            {
                m_targetFaces[idx].SetBlendShapeWeight(shape, 0.0f);
            }
        }
    }

    /// <summary>
    /// 顔を歪ませるコルーチン
    /// </summary>
    /// <param name="_playerNum"> ダメージを受けたプレイヤーナンバー </param>
    public IEnumerator DamageFace(int _playerNum, UnityEngine.Events.UnityAction _action)
    {
        //操作対象を制限する
        if (_playerNum < 0)
        {
            _playerNum = 0;
            Debug.Log("MyPlayerSet：人数最小エラー");
        }
        else if (_playerNum > m_targetFaces.Length)
        {
            _playerNum = m_targetFaces.Length;
            Debug.Log("MyPlayerSet：人数最大エラー");
        }

        //ランダムでシェイプキーを決定する
        int shapeIdx = Random.Range(0, m_shapeNum);
        //進行時間
        float timer = 0.0f;
        //ダメージ回数
        int currentDamageNum = 0;
        //シェイプ量
        float shape = 0;

        //シェイプ最大量
        float maxShape = 100.0f;
        //シェイプ最小量
        float minShape = 0.0f;

        while (currentDamageNum < m_damageNum)
        {
            //顔を歪ませる
            timer = 0.0f;
            while (timer < m_damegeTime)
            {
                //シェイプ最小から最大まで変更
                shape = Easing.SineIn(timer, m_damegeTime, minShape, maxShape);
                //モデルのシェイプ変更
                m_targetFaces[_playerNum].SetBlendShapeWeight(shapeIdx, shape);
                timer += Time.deltaTime;
                yield return null;

            }

            //歪み状態で一時停止
            timer = 0.0f;
            while (timer < m_damageStopTime)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            //顔を歪ませから戻る
            timer = 0.0f;
            while (timer < m_damagebackTime)
            {
                //シェイプ最小から最大まで変更
                shape = -Easing.SineIn(timer, m_damagebackTime, -maxShape, minShape);
                //モデルのシェイプ変更
                m_targetFaces[_playerNum].SetBlendShapeWeight(shapeIdx, shape);
                timer += Time.deltaTime;
                yield return null;
            }

            currentDamageNum++;
        }

        //終了確認
        _action();
    }

    public override void MyUpdate() { }
}
