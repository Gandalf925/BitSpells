using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Base")]
    public EnemyModel model;
    EnemyView view;

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

    public void CreateEnemy()
    {
        model = new EnemyModel();
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
        player.Refresh();
        WrapUpTurn();
        yield return new WaitForSeconds(3f);
    }

    public IEnumerator Block()
    {
        model.Block(turns[turnNumber].value);
        view.Refresh(model);
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
}
