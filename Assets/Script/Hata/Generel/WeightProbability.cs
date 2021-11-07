using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightProbability : MonoBehaviour
{
    /// <summary>
    /// 指定した重みでを求める
    /// </summary>
    /// <typeparam name="Type"> 確率で求めたい値 </typeparam>
    /// <param name="_weightDic"> 重みを指定した</param>
    public static Type GetWeightRandm<Type>(ref Dictionary<Type,int> _weightDic)
    {
        //重みの合計
        var totalWeight = 0;
        //合計を取得
        foreach(var weight in _weightDic)
        {
            totalWeight += weight.Value;
        }
        
        var value = Random.Range(1, totalWeight + 1);
        
        foreach (var weight in _weightDic)
        {
            if (weight.Value >= value)
            {
                return weight.Key;
            }
            //次のキーへ移行
            value -= weight.Value;
        }

        return default;
    }

    /// <summary>
    /// 重みづけ確率をくじ引き方式で確率を求める
    /// _weightDicのvalueが全て0の場合は,defaultを返すので注意（実行前にvalueが全て0でないことを確認すること）
    /// </summary>
    /// <typeparam name="Type"> 確率で求めたい値 </typeparam>
    /// <param name="_weightDic"> 重みを指定した </param>
    public static Type GetWeightLot<Type>(ref Dictionary<Type, int> _weightDic)
    {
        //重みの合計
        var totalWeight = 0;
        //合計を取得
        foreach (var weight in _weightDic)
        {
            totalWeight += weight.Value;
        }

        var value = Random.Range(1, totalWeight + 1);

        //キーのリストを取得
        var keyList = new List<Type>(_weightDic.Keys);

        foreach (var key in keyList)
        {
            if (_weightDic[key] >= value)
            {
                _weightDic[key]--;
                return key;
            }

            value -= _weightDic[key];
        }

        return default;
    }
}
