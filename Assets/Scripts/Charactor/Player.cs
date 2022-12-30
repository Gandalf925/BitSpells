using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    BattleSceneManager battleSceneManager;
    public int maxHP;
    public int currentHP;
    public int Block;
    public List<Card> playerDeck = new List<Card>();
    public int maxEnergy = 3;
    public int currentEnergy;

    [Header("UI")]

    public Slider hpBarSlider;
    public Slider energyBarSlider;
    public TMP_Text maxHPText;
    public TMP_Text currentHPText;
    public TMP_Text maxEnergyText;
    public TMP_Text currentEnergyText;
    private void Awake()
    {
        battleSceneManager = FindObjectOfType<BattleSceneManager>();
    }

    public void PlayCard(CardUI selectedCard)
    {
        if (currentEnergy < selectedCard.data.cost)
        {
            Debug.Log("shortage of energy");
            return;
        }

        if (selectedCard.data.cardType == Card.Types.Attack)
        {
            currentEnergy -= selectedCard.data.cost;
            battleSceneManager.enemy.model.Damage(selectedCard);
            battleSceneManager.enemy.CheckAlive();

            // ダメージ演出とカードを消す演出を入れる

            selectedCard.gameObject.SetActive(false);
            battleSceneManager.cardsInHand.Remove(selectedCard.data);
            battleSceneManager.discardPile.Add(selectedCard.data);

            Refresh();
            battleSceneManager.discardPileCountText.text = battleSceneManager.discardPile.Count.ToString();
        }
    }

    public void Damage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
        }
        else
        {
            Refresh();
        }
    }

    public void Refresh()
    {
        // maxHPText.text = maxHP.ToString();
        // currentHPText.text = currentHP.ToString();
        // hpBarSlider.value = currentHP;
        // currentEnergyText.text = currentEnergy.ToString();
        // maxEnergyText.text = maxEnergy.ToString();
        // energyBarSlider.value = currentEnergy;

        maxEnergyText.text = maxEnergy.ToString();
        currentEnergyText.text = currentEnergy.ToString();
        energyBarSlider.maxValue = maxEnergy;
        energyBarSlider.value = currentEnergy;

        maxHPText.text = maxHP.ToString();
        currentHPText.text = currentHP.ToString();
        hpBarSlider.maxValue = maxHP;
        hpBarSlider.value = currentHP;
    }
}
