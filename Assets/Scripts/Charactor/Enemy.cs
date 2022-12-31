using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Base")]
    public EnemyModel model;
    public EnemyView view;
    int damage;

    [Header("Action")]
    Player player;

    public List<EnemyAction> turns = new List<EnemyAction>();
    public int turnNumber;

    [Header("Utils")]
    public GameManager gameManager;

    private void Awake()
    {
        view = GetComponent<EnemyView>();
        player = gameManager.player;
    }

    public void CreateEnemy(string enemyName)
    {
        model = new EnemyModel(enemyName);
        view.Show(model);
        GenerateTurns();
    }

    public void TakeTurn()
    {
        // 敵の行動はここに追加する
        switch (turns[turnNumber].intentType)
        {
            case EnemyAction.IntentType.Attack:
                StartCoroutine(Attack());
                break;
            case EnemyAction.IntentType.Block:
                StartCoroutine(Block());
                break;
        }
    }

    public void CheckAlive()
    {
        if (model.isAlive)
        {
            view.Refresh(model);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public IEnumerator Attack()
    {
        player.Damage(turns[turnNumber].value);
        WrapUpTurn();
        yield return new WaitForSeconds(3f);
    }

    public IEnumerator Block()
    {
        AddBlock(turns[turnNumber].value);
        WrapUpTurn();
        yield return new WaitForSeconds(3f);
    }

    public void GenerateTurns()
    {
        foreach (EnemyAction enemyAction in model.actions)
        {
            for (int i = 0; i < enemyAction.chance; i++)
            {
                turns.Add(enemyAction);
            }
        }
        turns.Shuffle();
    }

    private void WrapUpTurn()
    {
        turnNumber++;
        if (turnNumber == turns.Count)
            turnNumber = 0;
    }

    public void Damage(CardUI card)
    {
        damage = card.data.value;

        if (model.block > 0)
        {
            damage = BlockDamage(damage);
        }

        model.currentHP -= damage;
        if (model.currentHP <= 0)
        {
            model.currentHP = 0;
            model.isAlive = false;
        }
    }

    private int BlockDamage(int amount)
    {
        if (model.block >= amount)
        {
            //block all
            model.block -= amount;
            amount = 0;
        }
        else
        {
            //cant block all
            amount -= model.block;
            model.block = 0;
        }

        view.DisplayBlock(model);
        return amount;
    }

    public void AddBlock(int amount)
    {
        model.block += amount;
        view.DisplayBlock(model);
    }
}
