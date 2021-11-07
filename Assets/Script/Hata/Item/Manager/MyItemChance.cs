using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムの出現確率を操作するクラス
/// </summary>
public class MyItemChance : MyUpdater
{
    //ランクとそれに対する重み
    [System.Serializable]
    struct RankWeight
    {
        public MyItemManager.Rank rank;

        [Header("ランクに対する重み（0なら出現しない）")]
        [Range(0, 100)]
        public int weight;
    }

    [Header("序盤の重み")]
    [SerializeField] RankWeight[] m_beginWeight = new RankWeight[5];
    Dictionary<MyItemManager.Rank, int> begginDic = new Dictionary<MyItemManager.Rank, int>();

    [Header("中盤の重み")]
    [SerializeField] RankWeight[] m_middleWeight = new RankWeight[5];
    Dictionary<MyItemManager.Rank, int> middleDic = new Dictionary<MyItemManager.Rank, int>();

    [Header("終盤の重み")]
    [SerializeField] RankWeight[] m_finalWeight = new RankWeight[5];
    Dictionary<MyItemManager.Rank, int> finalDic = new Dictionary<MyItemManager.Rank, int>();

    //現在の進行状況
    MyGameProgress.GameProgress m_progress = MyGameProgress.GameProgress.BEGINNING; 

    /// <summary>
    /// 現在の進行状況を更新
    /// </summary>
    public void SetProgress(MyGameProgress.GameProgress _progress) { m_progress = _progress; }

    /// <summary>
    /// 次に出現するアイテムの確率を現在の進行状況に合わせて、求める
    /// </summary>
    public MyItemManager.Rank GetItemRank()
    {
        MyItemManager.Rank returnRank = MyItemManager.Rank.D;

        switch(m_progress)
        {
            case MyGameProgress.GameProgress.BEGINNING:
                {
                    returnRank = WeightProbability.GetWeightRandm(ref begginDic);
                    break;
                }

            case MyGameProgress.GameProgress.MIDDLE:
                {
                    returnRank = WeightProbability.GetWeightRandm(ref middleDic);
                    break;
                }

            case MyGameProgress.GameProgress.FINAL:
                {
                    returnRank = WeightProbability.GetWeightRandm(ref finalDic);
                    break;
                }
        }

        return returnRank;
    }


    public override void MyFastestInit()
    {
        //アイテムの重みづけをDictionaryに反映
        for (int rank = 0; rank < m_beginWeight.Length; rank++)
        {
            begginDic.Add(m_beginWeight[rank].rank, m_beginWeight[rank].weight);
            middleDic.Add(m_middleWeight[rank].rank, m_middleWeight[rank].weight);
            finalDic.Add(m_finalWeight[rank].rank, m_finalWeight[rank].weight);
        }
        //進行状況をリセット
        m_progress = MyGameProgress.GameProgress.BEGINNING;
        //更新不要
        m_isUpdate = false;
    }
    public override void MyUpdate() { }
   
}
