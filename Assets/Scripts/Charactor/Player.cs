using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
using TMPro;
using DG.Tweening;

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

    [Header("Items")]
    public List<ItemSO> MyItemList = new List<ItemSO>();

    [Header("UI")]

    public Slider hpBarSlider;
    public Slider energyBarSlider;
    public TMP_Text maxHPText;
    public TMP_Text currentHPText;
    public TMP_Text maxEnergyText;
    public TMP_Text currentEnergyText;
    public Image ShieldFrameIcon;
    public TMP_Text ShieldFrameText;
    public GameObject playerStatusUI;

    [Header("Utils")]
    public MMF_Player feedbackPlayer;
    public GameObject playerSideUI;

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
        battleSceneManager = FindObjectOfType<BattleSceneManager>();
        feedbackPlayer = FindObjectOfType<MMF_Player>();

        // artifactが機能しなくなったので、アーティファクト機能を作り変える。

    }


    public void PlayCard(CardUI selectedCard, Enemy enemy)
    {
        if (currentEnergy < selectedCard.data.cost)
        {
            Debug.Log("shortage of energy");
            return;
        }

        currentEnergy -= selectedCard.data.cost;

        if (selectedCard.data.cardType == Card.Types.Attack)
        {
            switch (selectedCard.data.name)
            {
                case "Fire":
                    enemy.Damage(selectedCard);
                    enemy.CheckAlive();
                    break;

                case "Ice":
                    enemy.Damage(selectedCard);
                    enemy.CheckAlive();
                    break;
            }

        }

        if (selectedCard.data.cardType == Card.Types.Block)
        {
            AddBlock(selectedCard.data.value);
        }

        StopAllCoroutines();
        StartCoroutine(PlayAttackEffect(selectedCard));
        StartCoroutine(selectedCard.Disapear());
        StartCoroutine(AfterCardEffects(selectedCard));
    }


    private IEnumerator PlayAttackEffect(CardUI selectedCard)
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Transform effectPos = new GameObject().transform;

        effectPos.position = worldPos;

        GameObject effect = Instantiate(selectedCard.data.effectPrefab, effectPos) as GameObject;
        Transform parent = effect.transform.parent;

        yield return new WaitForSeconds(1f);
        Destroy(parent.gameObject);
        Destroy(effect);
    }

    IEnumerator AfterCardEffects(CardUI selectedCard)
    {
        yield return new WaitForSeconds(0.5f);
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

        PlayerUIShakeFB();
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

    private void PlayerUIShakeFB()
    {
        MMPositionShaker target = playerSideUI.GetComponent<MMPositionShaker>();
        MMF_PositionShake shakeFeedback = feedbackPlayer.GetFeedbackOfType<MMF_PositionShake>();
        shakeFeedback.TargetShaker = target;
        shakeFeedback.Play(new Vector3(5f, 5f, 5f));
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
}

