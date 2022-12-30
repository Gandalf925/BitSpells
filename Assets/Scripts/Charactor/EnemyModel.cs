using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class EnemyModel
{
    public string enemyName;
    public int maxHP;
    public int currentHP;
    public int block;
    public int str;
    public Sprite icon;
    public bool isAlive = true;

    public EnemyModel()
    {
        var op = Addressables.LoadAssetAsync<EnemyEntity>("Assets/Entities/EnemyEntity/FrontierLoad.asset");

        // WaitForCompletionで同期的にロード完了を待機
        var enemyData = op.WaitForCompletion();

        enemyName = enemyData.name;
        maxHP = enemyData.maxHp;
        currentHP = enemyData.maxHp;
        block = enemyData.currentBlock;
        str = enemyData.str;
        icon = enemyData.icon;

        // 使い終わったらリリース
        Addressables.Release(op);
    }

    public void Damage(CardUI card)
    {
        currentHP -= card.data.value;
        if (currentHP <= 0)
        {
            currentHP = 0;
            isAlive = false;
        }
    }
}
