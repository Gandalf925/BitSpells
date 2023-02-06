using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Coffee.UIExtensions;

public class Enemy : MonoBehaviour
{
    [Header("Base")]
    public EnemyModel model;
    public EnemyView view;
    int damage;

    [Header("Action")]
    Player player;
    public Image NextActionImage;
    public TMP_Text NextActionValue;

    public List<EnemyAction> turns = new List<EnemyAction>();
    public int turnNumber;

    [Header("Utils")]
    public GameManager gameManager;
    public BattleSceneManager battleSceneManager;
    public MMF_Player feedbackPlayer;
    [SerializeField] UIDissolve disapearEnemyIconPanel;
    [SerializeField] UIDissolve disapearEnemyBase;
    [SerializeField] UIDissolve disapearEnemyNameFrame;

    private void Awake()
    {
        view = GetComponent<EnemyView>();
        player = gameManager.player;
        feedbackPlayer = FindObjectOfType<MMF_Player>();
    }


    public void CreateEnemy(string enemyName)
    {
        model = new EnemyModel(enemyName);
        view.Show(model);
        GenerateTurns();
    }

    public IEnumerator TakeTurn()
    {
        StopAllCoroutines();
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
        yield return new WaitForSeconds(5f);
    }

    public void DisplayNextAction()
    {
        NextActionImage.sprite = turns[turnNumber].icon;
        if (turns[turnNumber].value > 0)
        {
            NextActionValue.text = turns[turnNumber].value.ToString();
        }
        else
        {
            NextActionValue.text = "";
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
            StopAllCoroutines();
            StartCoroutine(Disapear());

            battleSceneManager.enemies.RemoveAll(item => item == null);
        }
    }

    public IEnumerator Disapear()
    {
        battleSceneManager.uncontrollablePanel.SetActive(true);
        while (disapearEnemyIconPanel.location < 1)
        {
            disapearEnemyIconPanel.location += Time.deltaTime * 2.2f;
            disapearEnemyBase.location += Time.deltaTime * 2.2f;
            disapearEnemyNameFrame.location += Time.deltaTime * 2.2f;
            disapearEnemyIconPanel.location = Mathf.Clamp01(disapearEnemyIconPanel.location);
            disapearEnemyBase.location = Mathf.Clamp01(disapearEnemyIconPanel.location);
            disapearEnemyNameFrame.location = Mathf.Clamp01(disapearEnemyIconPanel.location);
            // アタッチしたスクリプトの変数にcurrentValueを適用する
            yield return null;
        }
        battleSceneManager.uncontrollablePanel.SetActive(false);
        Destroy(this.gameObject);
    }

    public IEnumerator Attack()
    {
        StopAllCoroutines();
        player.Damage(turns[turnNumber].value);
        WrapUpTurn();
        yield return new WaitForSecondsRealtime(1f);
    }

    public IEnumerator Block()
    {
        StopAllCoroutines();
        AddBlock(turns[turnNumber].value);
        WrapUpTurn();
        yield return new WaitForSecondsRealtime(1f);
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
        {
            turnNumber = 0;
        }
    }

    public void Damage(CardUI card)
    {
        MMPositionShaker target = GetComponent<MMPositionShaker>();
        EnemyShakeFB();
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

    private void EnemyShakeFB()
    {
        MMPositionShaker target = GetComponent<MMPositionShaker>();
        MMF_PositionShake shakeFeedback = feedbackPlayer.GetFeedbackOfType<MMF_PositionShake>();
        shakeFeedback.TargetShaker = target;
        shakeFeedback.Play(new Vector3(5f, 5f, 5f));
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
