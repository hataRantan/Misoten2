using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItemSelect : MyUpdater
{
    //アイテムのランク
    public enum Rank
    {
        S, A, B, C, D
    }
    //アイテムとそれに対応するランク
    [System.Serializable]
    public struct RankItem
    {
        public Rank rank;
        public GameObject item;
    }

    [Header("通常排出させるアイテム")]
    [SerializeField]
    List<RankItem> m_normalItems = new List<RankItem>();
    //ランクごとにアイテムを分割
    Dictionary<Rank, List<int>> m_divisionRank = new Dictionary<Rank, List<int>>();

    [Header("出現させる強力なアイテム")]
    [SerializeField]
    List<GameObject> m_powerfulItems = new List<GameObject>();

    //ランクとそれに対する重み
    [System.Serializable]
    struct RankWeight
    {
        public Rank rank;

        [Header("ランクに対する重み（0なら出現しない）")]
        [Range(0, 100)]
        public int weight;
    }

    [Header("通常アイテムの排出確率")]
    [SerializeField]
    RankWeight[] m_weights = new RankWeight[5];
    Dictionary<Rank, int> m_weightDic = new Dictionary<Rank, int>();

    public override void MyFastestInit()
    {
        //関数で扱える形に変換
        foreach(var weight in m_weights)
        {
            if (!m_weightDic.ContainsKey(weight.rank))
                m_weightDic.Add(weight.rank, weight.weight);
        }

        //アイテムをランクごとに変換
        for (int idx = 0; idx < m_normalItems.Count; ++idx)
        {
            if(!m_divisionRank.ContainsKey(m_normalItems[idx].rank))
            {
                List<int> subscript = new List<int>() { idx };
                m_divisionRank.Add(m_normalItems[idx].rank, subscript);
            }
            else
            {
                m_divisionRank[m_normalItems[idx].rank].Add(idx);
            }
        }

        //存在しないアイテムのキーを削除する
        var keys = m_weightDic.Keys;
        List<Rank> noRank = new List<Rank>();
        foreach(var key in keys)
        {
            if(!m_divisionRank.ContainsKey(key))
            {
                noRank.Add(key);
            }
        }
        foreach(var rank in noRank)
        {
            m_weightDic[rank] = 0;
        }
    }

    //通常アイテムの生成
    public List<GameObject> CreateNormalItems(List<Vector3> _generatedPos)
    {
        List<GameObject> createdItems = new List<GameObject>();

        foreach(var pos in _generatedPos)
        {
            createdItems.Add(Instantiate(NormalItemSelect(), pos, Quaternion.identity));
        }

        return createdItems;
    }

    public GameObject CreatePowerfulItem()
    {
        int idx = Random.Range(0, m_powerfulItems.Count);

        return Instantiate(m_powerfulItems[idx], Vector3.zero, Quaternion.identity);
    }

    //通常アイテムの中から生成するアイテムを決定する
    private GameObject NormalItemSelect()
    {
        //出現指せるアイテムのランクを取得
        Rank rank = WeightProbability.GetWeightRandm(ref m_weightDic);
        //指定のランクの中からアイテムを選択(同じ確率)
        //Debug.Log("rank：" + rank);
        //int i = m_divisionRank[rank][Random.Range(0, m_divisionRank[rank].Count)];
        return m_normalItems[m_divisionRank[rank][Random.Range(0, m_divisionRank[rank].Count)]].item;
    }

    //使用せず
    public override void MyUpdate() { }
}
