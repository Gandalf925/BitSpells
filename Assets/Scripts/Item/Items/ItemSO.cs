using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "BitSpells/ItemData", order = 0)]
public class ItemSO : ScriptableObject
{
    public int id;
    public new string name;
    public int value;
    public int amount;
    public string desctiption;
    public string useInfo;
    public Type type;
    public Sprite icon;
}

public enum Type // 実装するItemの種類
{
    UseItem,
    KeyItem,
}
