using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class BattleSceneManager : MonoBehaviour
{
    [Header("Cards")]
    public List<Card> drawPile = new List<Card>();
    public List<Card> cardsInHand = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<CardUI> cardsInHandGameObjects = new List<CardUI>();


    [Header("Status")]
    int drawAmount = 5;

    [Header("Enemies")]
    public Enemy enemy;
    public Transform enemyPositionPanel;
    public Enemy enemyPrefab;
    public GameObject defeatEnemy;

    [Header("Manager")]
    public GameManager gameManager;

    [Header("UI")]
    public Button turnEndButton;
    Transform deck;
    Transform cemetery;
    public TMP_Text drawPileCountText;
    public TMP_Text discardPileCountText;
    public GameObject turnTextFramePanel;
    public GameObject enemyTurnTextFrame;
    public GameObject playerTurnTextFrame;



    [Header("Utils")]
    bool isPlayerTurn = true;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

    }

    void Start()
    {
        InitBattle();
    }

    // Battle開始時の処理
    // 敵の生成、デッキの生成、手札の配布など
    public void InitBattle()
    {
        // Enemyの初期化
        enemy = Instantiate<Enemy>(enemyPrefab, enemyPositionPanel, false);
        enemy.CreateEnemy();

        // 手札の初期設定
        foreach (Card card in cardsInHand)
        {
            DiscardCard(card);
        }
        foreach (CardUI cardUI in cardsInHandGameObjects)
        {
            cardUI.gameObject.SetActive(false);
        }

        discardPile = new List<Card>();
        drawPile = new List<Card>();
        cardsInHand = new List<Card>();

        discardPile.AddRange(gameManager.player.playerDeck);
        ShuffleCards();
        DrawCards(drawAmount);

        // PlayerのStatusを初期化
        gameManager.player.currentEnergy = gameManager.player.maxEnergy;
        gameManager.player.currentHP = gameManager.player.maxHP;
        gameManager.player.Refresh();

    }

    public void TurnCalc()
    {
        if (isPlayerTurn)
        {
            PlayerTurn();
        }
        else
        {
            EnemyTurn();
        }
    }

    public void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        TurnCalc();
    }

    void PlayerTurn()
    {
        Debug.Log("Player Turn");
        StartCoroutine("DisplayPlayerTurnTextFrame");

        DrawCards(drawAmount);
        gameManager.player.currentEnergy = gameManager.player.maxEnergy;
        gameManager.player.Refresh();
    }

    void EnemyTurn()
    {
        Debug.Log("Enemy Turn");
        StartCoroutine("DisplayEnemyTurnTextFrame");

        // 手札をすべて捨てる
        foreach (Card card in cardsInHand)
        {
            DiscardCard(card);
        }
        foreach (CardUI cardUI in cardsInHandGameObjects)
        {
            cardUI.gameObject.SetActive(false);
        }
        cardsInHand = new List<Card>();

        enemy.WeekAttack(gameManager.player);
        ChangeTurn();
    }

    IEnumerator DisplayPlayerTurnTextFrame()
    {
        Image turnTextFramePanelImage = turnTextFramePanel.GetComponent<Image>();

        turnTextFramePanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        playerTurnTextFrame.SetActive(true);
        turnTextFramePanelImage.DOFade(180f, 5f);
        yield return new WaitForSeconds(1f);
        turnTextFramePanelImage.DOFade(0f, 5f);
        playerTurnTextFrame.SetActive(false);
        yield return new WaitForSeconds(1f);
        turnTextFramePanel.SetActive(false);
    }

    IEnumerator DisplayEnemyTurnTextFrame()
    {
        enemyTurnTextFrame.SetActive(true);
        yield return new WaitForSeconds(1f);
        enemyTurnTextFrame.SetActive(false);
    }



    public void ShuffleCards()
    {
        discardPile.Shuffle();
        drawPile = discardPile;
        discardPile = new List<Card>();
        discardPileCountText.text = discardPile.Count.ToString();
    }

    public void DrawCards(int amountToDraw)
    {
        int cardsDrawn = 0;
        while (cardsDrawn < amountToDraw && cardsInHand.Count <= 10)
        {
            if (drawPile.Count < 1)
            {
                ShuffleCards();
            }

            cardsInHand.Add(drawPile[0]);
            DealCardInHand(drawPile[0]);
            drawPile.Remove(drawPile[0]);
            drawPileCountText.text = drawPile.Count.ToString();
            cardsDrawn++;
        }
    }

    public void DealCardInHand(Card card)
    {
        CardUI cardUI = cardsInHandGameObjects[cardsInHand.Count - 1];
        cardUI.CreateCard(card);
        cardUI.gameObject.SetActive(true);
    }

    public void DiscardCard(Card card)
    {
        discardPile.Add(card);
        discardPileCountText.text = discardPile.Count.ToString();
    }

}
