using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ArtifactEntity", menuName = "BitSpells/ArtifactEntity", order = 0)]
public class ArtifactEntity : ScriptableObject
{
    public int iD;
    public new string name;
    [TextArea]
    public string description;
    public EffectType effectType;
    public int value;
    public Sprite icon;
}

public enum EffectType
{
    PlayerAttack,
    PlayerHP,
    PlayerEnergy,
}
