using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyEntity", menuName = "BitSpells/EnemyEntity", order = 0)]
public class EnemyEntity : ScriptableObject
{
    public int id;
    public string enemyName;
    [TextArea]
    public string enemyDescription;
    public int maxHp;
    public int currentHp;
    public int currentBlock;
    public Sprite icon;
    public List<EnemyAction> actions = new List<EnemyAction>();
}
