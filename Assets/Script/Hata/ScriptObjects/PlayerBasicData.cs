
using UnityEngine;

/// <summary>
/// プレイヤーのスクリプトオブジェクト Resourceフォルダから呼び出す予定
/// </summary>
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
[System.Serializable]
public class PlayerBasicData : ScriptableObject
{
    [SerializeField] uint MaxHp;
    [SerializeField] uint Speed;

    public uint GetSpped { get { return Speed; } }
    public uint GetMaxHp { get { return MaxHp; } }
}
