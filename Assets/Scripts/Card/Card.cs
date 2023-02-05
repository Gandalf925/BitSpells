using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "BitSpells/CardData", order = 0)]
public class Card : ScriptableObject
{
    public int id;
    public new string name;
    public int cost;
    public int value;
    public string description;
    public Types cardType;
    public TargetTypes targetTypes;
    public Sprite icon;
    public GameObject effectPrefab;

    public enum Types
    {
        Attack,
        Block,
        Skill,
        Power
    }
    public enum TargetTypes
    {
        Self,
        Enemy
    }
}


