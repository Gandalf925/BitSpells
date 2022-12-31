using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using TMPro;

public class EnemyModel
{
    public string enemyName;
    public int maxHP;
    public int currentHP;
    public int block;
    public Sprite icon;
    public List<EnemyAction> actions;
    public bool isAlive = true;

    public EnemyModel(string enemyName)
    {
        var op = Addressables.LoadAssetAsync<EnemyEntity>("EnemyEntity/" + enemyName);

        // WaitForCompletionで同期的にロード完了を待機
        var enemyData = op.WaitForCompletion();

        enemyName = enemyData.name;
        maxHP = enemyData.maxHp;
        currentHP = enemyData.maxHp;
        block = enemyData.currentBlock;
        icon = enemyData.icon;
        actions = enemyData.actions;

        // 使い終わったらリリース
        Addressables.Release(op);
    }


}
