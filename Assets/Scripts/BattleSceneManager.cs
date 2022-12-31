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
    public GameObject enemyTurnTextFrame;
    public GameObject playerTurnTextFrame;
    public GameObject turnTextFramePanel;
    public Transform handPanel;



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
            StartCoroutine(PlayerTurn());
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }
    }

    public void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        TurnCalc();
    }

    IEnumerator PlayerTurn()
    {
        Debug.Log("Player Turn");
        StartCoroutine("DisplayPlayerTurnTextFrame");
        turnEndButton.interactable = true;
<<<<<<< HEAD
=======

>>>>>>> 318596bfd60559b1f59a04ccd5cfbe9e9388851b
        DrawCards(drawAmount);
        gameManager.player.currentEnergy = gameManager.player.maxEnergy;
        gameManager.player.block = 0;
        gameManager.player.Refresh();
        yield return new WaitForSeconds(2f);
    }

    IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy Turn");
        // EnemuTurnTextは後で調整すること
        StartCoroutine("DisplayEnemyTurnTextFrame");
        EachTurnInit();
        turnEndButton.interactable = false;
        yield return new WaitForSeconds(3f);

        cardsInHand = new List<Card>();

        enemy.TakeTurn();
        ChangeTurn();
    }

    private void EachTurnInit()
    {
        // 手札をすべて捨てる
        foreach (Card card in cardsInHand)
        {
            DiscardCard(card);
        }
        foreach (CardUI cardUI in cardsInHandGameObjects)
        {
            cardUI.transform.SetParent(handPanel);
            cardUI.gameObject.SetActive(false);
        }
    }

    IEnumerator DisplayPlayerTurnTextFrame()
    {
        Image turnTextFramePanelImage = turnTextFramePanel.GetComponent<Image>();
        turnTextFramePanel.SetActive(true);
        enemyTurnTextFrame.SetActive(false);
        playerTurnTextFrame.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        playerTurnTextFrame.transform.position = new Vector3(2500, 600, 0);
        playerTurnTextFrame.transform.DOMove(new Vector3(1000, 600, 0), 0.5f);
        yield return new WaitForSeconds(1f);
        playerTurnTextFrame.transform.DOMove(new Vector3(-1000, 600, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        playerTurnTextFrame.SetActive(false);
        turnTextFramePanel.SetActive(false);
    }

    IEnumerator DisplayEnemyTurnTextFrame()
    {
        Image turnTextFramePanelImage = turnTextFramePanel.GetComponent<Image>();
        turnTextFramePanel.SetActive(true);
        enemyTurnTextFrame.SetActive(true);
        playerTurnTextFrame.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        enemyTurnTextFrame.transform.position = new Vector3(2500, 600, 0);
        enemyTurnTextFrame.transform.DOMove(new Vector3(1000, 600, 0), 0.5f);
        yield return new WaitForSeconds(1f);
        enemyTurnTextFrame.transform.DOMove(new Vector3(-1000, 600, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        enemyTurnTextFrame.SetActive(false);
        turnTextFramePanel.SetActive(false);
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
