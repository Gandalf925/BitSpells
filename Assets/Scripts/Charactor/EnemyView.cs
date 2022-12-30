using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyView : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text maxHPText;
    [SerializeField] TMP_Text currentHPText;
    [SerializeField] TMP_Text currentBlockText;
    [SerializeField] Slider hpBarSlider;
    [SerializeField] Image icon;

    public void Show(EnemyModel enemyModel)
    {
        nameText.text = enemyModel.enemyName;
        maxHPText.text = enemyModel.maxHP.ToString();
        currentHPText.text = enemyModel.currentHP.ToString();
        currentBlockText.text = enemyModel.block.ToString();
        hpBarSlider.maxValue = enemyModel.maxHP;
        hpBarSlider.minValue = 0;
        icon.sprite = enemyModel.icon;
    }

    public void Refresh(EnemyModel enemyModel)
    {
        currentHPText.text = enemyModel.currentHP.ToString();
        hpBarSlider.value = enemyModel.currentHP;
        currentBlockText.text = enemyModel.block.ToString();
    }
}
