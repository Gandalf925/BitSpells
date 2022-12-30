using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAction
{
    public IntentType intentType;
    public enum IntentType { Attack, Block, StrategicBuff, StrategicDebuff, AttackDebuff }
    public int value;
    public int chance;
    public Sprite icon;
}
