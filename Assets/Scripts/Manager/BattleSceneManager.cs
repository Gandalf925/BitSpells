using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class BattleSceneManager : MonoBehaviour
{
    [Header("Cards")]
    public List<Card> drawPile = new List<Card>();
    public List<Card> cardsInHand = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<CardUI> cardsInHandGameObjects = new List<CardUI>();


    [Header("Items")]
    [SerializeField] GameObject itemDisplay;
    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemAmount;
    public int currentItemIndex = 0;


    [Header("Enemies")]
    public Enemy enemy;
    [SerializeField] Enemy enemyPrefab;
    public List<Enemy> enemies;
    public EnemyEntity[] eEntityArr;
    public Transform enemyPositionPanel;

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
    public GameObject EndPanel;
    public GameObject ClearPopup;
    public GameObject DefeatPopup;
    public GameObject BackGroundPanel;
    public Transform TurnFrameStart;
    public Transform TurnFrameCenter;
    public Transform TurnFrameEnd;
    public GameObject uncontrollablePanel;
    [SerializeField] GameObject nextArrow;
    [SerializeField] GameObject previousArrow;
    public GameObject useItemPanel;
    [SerializeField] Image useItemIcon;
    [SerializeField] TMP_Text useItemInfo;
    [SerializeField] TMP_Text useItemName;
    UIManager uIManager;
    GameManager gameManager;




    [Header("Utils")]
    bool isPlayerTurn = true;
    bool isGameClear = false;
    bool isGameOver = false;
    bool isOpenUseItemPanel = false;


    private void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
        InitBattle();
    }

    void Update()
    {
        enemies.RemoveAll(item => item == null);
        if (enemies.Count == 0)
        {
            StopAllCoroutines();
            if (!isGameClear)
            {
                isGameClear = true;
                uIManager.hideUIToggle.gameObject.SetActive(false);
                BattleClear();
            }
        }

        if (Player.instance.currentHP == 0)
        {
            StopAllCoroutines();
            if (!isGameOver)
            {
                isGameOver = true;
                uIManager.hideUIToggle.gameObject.SetActive(false);
                BattleDefeat();
            }
        }
    }


    // Battle開始時の処理
    // 敵の生成、デッキの生成、手札の配布など
    void InitBattle()
    {
        // Enemyの初期化
        for (int i = 0; i < eEntityArr.Length; i++)
        {
            enemy.gameManager = GameManager.instance;
            enemy.battleSceneManager = FindObjectOfType<BattleSceneManager>();
            enemy = Instantiate(enemyPrefab, enemyPositionPanel, false);
            enemies.Add(enemy);
            enemies[i].CreateEnemy(eEntityArr[i].name);
            enemies[i].DisplayNextAction();
        }

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

        discardPile.AddRange(GameManager.instance.player.playerDeck);
        ShuffleCards();
        DrawCards(Player.instance.drawAmount);

        // PlayerのStatusを初期化
        gameManager.artifact.playArtifactEffect();
        gameManager.player.currentEnergy = gameManager.player.maxEnergy;
        gameManager.player.currentHP = gameManager.player.maxHP; // 回復ポイントまで回復できないよう変更予定

        uIManager.Refresh();

        // アイテム関連の初期化

        if (Player.instance.MyItemList.Count <= 0)
        {
            itemDisplay.SetActive(false);
            nextArrow.SetActive(false);
            previousArrow.SetActive(false);
            itemAmount = null;
            itemName = null;
        }
        else
        {
            nextArrow.SetActive(true);
            previousArrow.SetActive(true);
            itemDisplay.SetActive(true);
            ShowItem();
        }
    }

    void TurnCalc()
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

    void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        TurnCalc();
    }

    IEnumerator PlayerTurn()
    {
        Debug.Log("Player Turn");
        StartCoroutine("DisplayPlayerTurnTextFrame");
        turnEndButton.interactable = true;
        foreach (Enemy enemy in enemies)
        {
            enemy.DisplayNextAction();
        }

        DrawCards(Player.instance.drawAmount);
        Player.instance.currentEnergy = Player.instance.maxEnergy;

        gameManager.player.block = 0;
        uIManager.Refresh();

        yield return new WaitForSeconds(2f);
    }

    IEnumerator EnemyTurn()
    {
        Debug.Log("Enemy Turn");
        uncontrollablePanel.SetActive(true);
        // EnemuTurnTextは後で調整すること
        StartCoroutine("DisplayEnemyTurnTextFrame");
        EachTurnInit();
        turnEndButton.interactable = false;

        foreach (Enemy enemy in enemies)
        {
            enemy.model.block = 0;
            enemy.view.Refresh(enemy.model);
        }
        yield return new WaitForSeconds(3f);

        cardsInHand = new List<Card>();

        foreach (Enemy enemy in enemies)
        {
            StartCoroutine(enemy.TakeTurn());
        }
        yield return new WaitForSeconds(1f);
        uncontrollablePanel.SetActive(false);
        ChangeTurn();
    }

    void EachTurnInit()
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
        playerTurnTextFrame.transform.position = TurnFrameStart.position;
        playerTurnTextFrame.transform.DOMove(TurnFrameCenter.position, 0.5f);
        yield return new WaitForSeconds(1f);
        playerTurnTextFrame.transform.DOMove(TurnFrameEnd.position, 0.5f);
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
        enemyTurnTextFrame.transform.position = TurnFrameStart.position;
        enemyTurnTextFrame.transform.DOMove(TurnFrameCenter.position, 0.5f);
        yield return new WaitForSeconds(1f);
        enemyTurnTextFrame.transform.DOMove(TurnFrameEnd.position, 0.5f);
        yield return new WaitForSeconds(0.5f);
        enemyTurnTextFrame.SetActive(false);
        turnTextFramePanel.SetActive(false);
    }

    void ShuffleCards()
    {
        discardPile.Shuffle();
        drawPile = discardPile;
        discardPile = new List<Card>();
        discardPileCountText.text = discardPile.Count.ToString();
    }

    void DrawCards(int amountToDraw)
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

    void DealCardInHand(Card card)
    {
        CardUI cardUI = cardsInHandGameObjects[cardsInHand.Count - 1];
        cardUI.CreateCard(card);
        cardUI.gameObject.SetActive(true);
    }

    void DiscardCard(Card card)
    {
        discardPile.Add(card);
        discardPileCountText.text = discardPile.Count.ToString();
    }

    void BattleClear()
    {
        EndPanel.SetActive(true);
        DefeatPopup.SetActive(false);
        ClearPopup.SetActive(true);
        ClearPopup.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
    }

    public void BattleDefeat()
    {
        EndPanel.SetActive(true);
        DefeatPopup.SetActive(true);
        ClearPopup.SetActive(false);
        DefeatPopup.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
    }

    void PushOKButtonAtBattleClear()
    {
        ClearPopup.SetActive(false);

        // ゲームクリア時の処理（カードの欠片抽選、次のシーンへ移行）

        // ゲームオーバー時の処理（王国へ移行）

    }

    void PushOKButtonAtGameOver()
    {
        ClearPopup.SetActive(false);
        // ゲームオーバー時の処理（王国へシーン移行）

    }

    public void ShowNextItem()
    {
        currentItemIndex = (currentItemIndex + 1) % Player.instance.MyItemList.Count;
        ShowItem();
    }

    public void ShowPreviousItem()
    {
        // 前のアイテムを表示する
        currentItemIndex = (currentItemIndex - 1 + Player.instance.MyItemList.Count) % Player.instance.MyItemList.Count;
        ShowItem();
    }

    public void OpenUseItemPanel()
    {
        if (!isOpenUseItemPanel)
        {
            isOpenUseItemPanel = !isOpenUseItemPanel;
            useItemPanel.SetActive(true);
            ShowItem();
        }
    }

    private void ShowItem()
    {
        itemName.text = Player.instance.MyItemList[currentItemIndex].name;
        itemIcon.sprite = Player.instance.MyItemList[currentItemIndex].icon;
        itemAmount.text = Player.instance.MyItemList[currentItemIndex].amount.ToString();
        useItemIcon.sprite = Player.instance.MyItemList[currentItemIndex].icon;
        useItemName.text = Player.instance.MyItemList[currentItemIndex].name;
        useItemInfo.text = Player.instance.MyItemList[currentItemIndex].useInfo;
    }

    public void CloseUseItemPanel()
    {
        if (isOpenUseItemPanel)
        {
            isOpenUseItemPanel = !isOpenUseItemPanel;
            useItemPanel.SetActive(false);
        }
    }

    public void UseItem()
    {
        switch (Player.instance.MyItemList[currentItemIndex].name)
        {
            case "Red Potion":
                Player.instance.currentHP += Player.instance.MyItemList[currentItemIndex].value;
                if (Player.instance.currentHP > Player.instance.maxHP)
                {
                    Player.instance.currentHP = Player.instance.maxHP;
                }
                uIManager.Refresh();
                gameManager.player.MyItemList[currentItemIndex].amount -= 1;
                ShowItem();
                if (Player.instance.MyItemList[currentItemIndex].amount <= 0)
                {
                    Player.instance.MyItemList[currentItemIndex].amount = 0;
                    Player.instance.MyItemList.RemoveAt(currentItemIndex);
                    ShowNextItem();
                }
                break;
        }
        isOpenUseItemPanel = !isOpenUseItemPanel;
        useItemPanel.SetActive(false);
    }

}
