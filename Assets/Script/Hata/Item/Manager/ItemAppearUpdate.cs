using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAppearUpdate : MyUpdater
{
    [Header("アイテム出現のエフェクト")]
    [SerializeField] GameObject m_effect = null;

    [Header("アイテムの出現エフェクトの時間")]
    [Range(0.0f, 5.0f)]
    [SerializeField]
    float m_effectTime = 2.0f;

    [Header("アイテム出現にかかる時間")]
    [Range(0.0f, 5.0f)] 
    [SerializeField]
    float m_appearTime = 0.0f;

    [Header("アイテムのサイズ振れ幅")]
    [Range(0.0f, 3.0f)]
    [SerializeField]
    float m_sizeRange = 1.5f;

    [Header("アイテム出現のエフェクトのサイズ")]
    [Range(1.0f, 10.0f)]
    [SerializeField]
    float m_effectSize = 0.0f;

    [Header("アイテム出現開始時のパーティクル量")]
    [Range(0, 100)]
    int m_firstParticleAmout = 10;

    [Header("アイテム出現の最大パーティクル量")]
    [Range(0, 1000)]
    int m_maxParticleAmout = 100;

    /// <summary>
    /// 出現中のアイテムの入れ物
    /// </summary>
    private class AppearNow
    {
        //アイテム本体
        public MyItemInterface item;
        //操作するアイテムの位置情報
        public Transform itemPos;
        //アイテムの底
        public Transform bottomPos;
        //アイテムの最終的なサイズ
        public Vector3 endSize;
        //パーティクル本体
        public ParticleSystem particle;
        //アイテム出現のエフェクトの出現量
        public ParticleSystem.EmissionModule emission;
        //出現の経過時間
        public float timer;

        public AppearNow(GameObject _createdItem, GameObject _effect, float _duration)
        {
            //アイテム本体を取得
            item = _createdItem.GetComponent<MyItemInterface>();
            //アイテム本体の位置情報を取得
            itemPos = _createdItem.gameObject.transform;
            //アイテムの底を取得
            bottomPos = item.Bottom;
            //アイテムの最終サイズ
            endSize = itemPos.transform.localScale;
            //サイズを0に変更
            itemPos.transform.localScale = Vector3.zero;

            //パーティクルの取得
            particle = _effect.GetComponent<ParticleSystem>();

            //パーティクルの停止
            //継続時間は停止中のみ操作できるため
            particle.Stop();

            //パーティクルの放出量を変更
            emission = particle.emission;

            //パーティクルの生存時間を決定
            ParticleSystem.MainModule mainSettings = particle.main;
            mainSettings.duration = _duration;

            //パーティクルの再開
            particle.Play();

            timer = 0.0f;
        }
    }

    // 出現中のアイテム
    private List<AppearNow> m_nowAppearItem = new List<AppearNow>();

    public override void MyUpdate()
    {
        if (m_nowAppearItem.Count <= 0) return;

        for (int idx = m_nowAppearItem.Count - 1; idx >= 0; idx--)
        {
            //パーティクルの終了確認
            if (!m_nowAppearItem[idx].particle.gameObject.activeSelf)
            {
                Destroy(m_nowAppearItem[idx].particle.gameObject);
                m_nowAppearItem.RemoveAt(idx);
            }
            else
            {
                //アイテムのエフェクト時間
                if (m_nowAppearItem[idx].timer < m_effectTime)
                {
                    //放出量を変更
                    m_nowAppearItem[idx].emission.rateOverTime = Easing.CubicOut(m_nowAppearItem[idx].timer, m_effectTime, m_firstParticleAmout, m_maxParticleAmout);

                    //時間経過
                    m_nowAppearItem[idx].timer += Time.deltaTime;
                }
                //アイテム出現時間
                else if (m_nowAppearItem[idx].timer < m_effectTime + m_appearTime)
                {
                    //現在のサイズ
                    Vector3 size = Vector3.zero;
                    size.x = Easing.BackOut(m_nowAppearItem[idx].timer - m_effectTime, m_appearTime, 0.0f, m_nowAppearItem[idx].endSize.x, m_sizeRange);
                    size.y = Easing.BackOut(m_nowAppearItem[idx].timer - m_effectTime, m_appearTime, 0.0f, m_nowAppearItem[idx].endSize.y, m_sizeRange);
                    size.z = Easing.BackOut(m_nowAppearItem[idx].timer - m_effectTime, m_appearTime, 0.0f, m_nowAppearItem[idx].endSize.z, m_sizeRange);
                    m_nowAppearItem[idx].itemPos.localScale = size;

                    //位置変更
                    float diff = 0.0f - m_nowAppearItem[idx].bottomPos.position.y; //床の高さ0.0fとの差分を求める

                    m_nowAppearItem[idx].itemPos.position = new Vector3(m_nowAppearItem[idx].itemPos.position.x, m_nowAppearItem[idx].itemPos.position.y + diff, m_nowAppearItem[idx].itemPos.position.z);

                    //時間経過
                    m_nowAppearItem[idx].timer += Time.deltaTime;

                    //サイズ変更終了後の調整
                    if (m_nowAppearItem[idx].timer >= m_effectTime + m_appearTime)
                    {
                        //サイズ調整
                        m_nowAppearItem[idx].itemPos.localScale = m_nowAppearItem[idx].endSize;
                        //位置調整
                        diff = 0.0f - m_nowAppearItem[idx].bottomPos.position.y;
                        m_nowAppearItem[idx].itemPos.position = new Vector3(m_nowAppearItem[idx].itemPos.position.x, m_nowAppearItem[idx].itemPos.position.y + diff, m_nowAppearItem[idx].itemPos.position.z);
                        //アイテム取得範囲復活
                        m_nowAppearItem[idx].item.SwitchGetRangeEnabled(true);
                    }
                }
            }
        }
    }



    /// <summary>
    /// アイテムの出現開始
    /// </summary>
    public void StartAppear(GameObject _createdItem, float _scaleFactor = 1.0f)
    {
        //アイテムの取得範囲を停止する
        _createdItem.GetComponent<MyItemInterface>().SwitchGetRangeEnabled(false);

        //パーティクルを生成する
        GameObject effect = Instantiate(m_effect, _createdItem.transform.position, Quaternion.identity);

        float size = m_effectSize * _scaleFactor;
        //パーティクルのサイズ変更
        effect.transform.localScale = new Vector3(size, size, size);

        //出現するアイテムを追加
        m_nowAppearItem.Add(new AppearNow(_createdItem, effect, m_effectTime));
    }
}
