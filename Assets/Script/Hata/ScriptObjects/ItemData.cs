using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData")]
[System.Serializable]
public class ItemData : ScriptableObject
{
    [SerializeField]
    uint attack;

    [Header("プレイヤーの通常速度に対する倍率")]
    [SerializeField]
    float speedFactor;

    [Header("取得に必要な連打数")]
    [SerializeField] uint blowsNum;

    public uint GetAttack { get { return attack; } }
    public float GetSppedFactor { get { return speedFactor; } }
    public uint GetBlowsNum { get { return blowsNum; } }
}
