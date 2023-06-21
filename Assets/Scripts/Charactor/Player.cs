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
    Artifact artifacts;
    public List<ItemSO> MyItemList = new List<ItemSO>();
    public ArtifactEntity[] equipedArts = new ArtifactEntity[3];


    [Header("Stage")]
    public StageType currentStageType;
    public StageDepth currentStageDepth;


    [Header("Utils")]
    UIManager uIManager;

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
                case "Kodachi":
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

        uIManager.Refresh();
        battleSceneManager.discardPileCountText.text = battleSceneManager.discardPile.Count.ToString();
    }

    public void Damage(int damage)
    {
        if (block > 0)
        {
            damage = BlockDamage(damage);
        }

        uIManager.PlayerUIShakeFB();
        currentHP -= damage;
        uIManager.Refresh();


        if (currentHP <= 0)
        {
            currentHP = 0;
            uIManager.Refresh();
            battleSceneManager.BattleDefeat();
        }
        else
        {
            uIManager.Refresh();
        }
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
        uIManager = FindObjectOfType<UIManager>();
        if (uIManager.ShieldFrameIcon != null && uIManager.ShieldFrameText != null)
        {
            if (Player.instance.block > 0)
            {
                uIManager.ShieldFrameIcon.gameObject.SetActive(true);
                uIManager.ShieldFrameText.text = Player.instance.block.ToString();
            }
            else
            {
                uIManager.ShieldFrameIcon.gameObject.SetActive(false);
            }
        }
    }

    public void EquipedArtifacts()
    {
        artifacts.Equiped();
    }
}

