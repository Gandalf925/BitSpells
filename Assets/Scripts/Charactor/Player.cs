using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    BattleSceneManager battleSceneManager;

    [Header("Status")]
    public int maxHP;
    public int currentHP;
    public int block;
    public int maxEnergy = 3;
    public int currentEnergy;
    public int drawAmount = 5;

    [Header("Cards")]
    public List<Card> playerDeck = new List<Card>();

    [Header("Artifacts")]
    public List<ArtifactEntity> artsList = new List<ArtifactEntity>();

    [Header("UI")]

    public Slider hpBarSlider;
    public Slider energyBarSlider;
    public TMP_Text maxHPText;
    public TMP_Text currentHPText;
    public TMP_Text maxEnergyText;
    public TMP_Text currentEnergyText;
    public Image ShieldFrameIcon;
    public TMP_Text ShieldFrameText;

    //singleton
    public static Player instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        battleSceneManager = FindObjectOfType<BattleSceneManager>();
    }

    public void PlayCard(CardUI selectedCard, Enemy enemy)
    {
        if (currentEnergy < selectedCard.data.cost)
        {
            Debug.Log("shortage of energy");
            return;
        }

        if (selectedCard.data.cardType == Card.Types.Attack)
        {
            currentEnergy -= selectedCard.data.cost;
            enemy.Damage(selectedCard);
            enemy.CheckAlive();

            // ダメージ演出とカードを消す演出を入れる

            AfterCardEffects(selectedCard);
        }

        if (selectedCard.data.cardType == Card.Types.Block)
        {
            currentEnergy -= selectedCard.data.cost;
            AddBlock(selectedCard.data.value);

            // ダメージ演出とカードを消す演出を入れる
            AfterCardEffects(selectedCard);
        }
    }

    void AfterCardEffects(CardUI selectedCard)
    {
        selectedCard.gameObject.SetActive(false);
        battleSceneManager.cardsInHand.Remove(selectedCard.data);
        battleSceneManager.discardPile.Add(selectedCard.data);

        Refresh();
        battleSceneManager.discardPileCountText.text = battleSceneManager.discardPile.Count.ToString();
    }

    public void Damage(int damage)
    {
        if (block > 0)
        {
            damage = BlockDamage(damage);
        }

        currentHP -= damage;
        Refresh();


        if (currentHP <= 0)
        {
            currentHP = 0;
            Refresh();
            battleSceneManager.BattleDefeat();
        }
        else
        {
            Refresh();
        }
    }

    public void Refresh()
    {
        maxEnergyText.text = maxEnergy.ToString();
        currentEnergyText.text = currentEnergy.ToString();
        energyBarSlider.maxValue = maxEnergy;
        energyBarSlider.value = currentEnergy;

        maxHPText.text = maxHP.ToString();
        currentHPText.text = currentHP.ToString();
        hpBarSlider.maxValue = maxHP;
        hpBarSlider.value = currentHP;
        DisplayBlock();
    }

    public void AddBlock(int amount)
    {
        block += amount;
        DisplayBlock();
    }

    private int BlockDamage(int amount)
    {
        if (block >= amount)
        {
            //block all
            block -= amount;
            amount = 0;
        }
        else
        {
            //cant block all
            amount -= block;
            block = 0;
        }

        DisplayBlock();
        return amount;
    }

    public void DisplayBlock()
    {
        if (block > 0)
        {
            ShieldFrameIcon.gameObject.SetActive(true);
            ShieldFrameText.text = block.ToString();
        }
        else
        {
            ShieldFrameIcon.gameObject.SetActive(false);
        }
    }

    public bool hasArts(string artifactName)
    {
        return artsList.Exists(artifact => artifact.name == artifactName);
    }
}

