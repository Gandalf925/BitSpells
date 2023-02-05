using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Coffee.UIExtensions;
using TMPro;
using DG.Tweening;

public class EnemyView : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text maxHPText;
    [SerializeField] TMP_Text currentHPText;
    [SerializeField] TMP_Text currentBlockText;
    [SerializeField] Slider hpBarSlider;
    [SerializeField] Image icon;
    [SerializeField] UIDissolve enemyIconPanel;

    public Image ShieldFrameIcon;
    public TMP_Text ShieldFrameText;

    public void Show(EnemyModel enemyModel)
    {
        nameText.text = enemyModel.name;
        maxHPText.text = enemyModel.maxHP.ToString();
        currentHPText.text = enemyModel.currentHP.ToString();
        currentBlockText.text = enemyModel.block.ToString();
        hpBarSlider.maxValue = enemyModel.maxHP;
        hpBarSlider.minValue = 0;
        icon.sprite = enemyModel.icon;
    }

    public IEnumerator Disapear()
    {
        enemyIconPanel = GetComponent<UIDissolve>();
        enemyIconPanel.location = Mathf.Lerp(enemyIconPanel.location, 0, Time.deltaTime);
        yield return new WaitForSeconds(2f);
    }

    public void Refresh(EnemyModel enemyModel)
    {
        currentHPText.text = enemyModel.currentHP.ToString();
        hpBarSlider.value = enemyModel.currentHP;
        currentBlockText.text = enemyModel.block.ToString();
        DisplayBlock(enemyModel);
    }

    public void DisplayBlock(EnemyModel model)
    {
        if (model.block > 0)
        {
            ShieldFrameIcon.gameObject.SetActive(true);
            ShieldFrameText.text = model.block.ToString();
        }
        else
        {
            ShieldFrameIcon.gameObject.SetActive(false);
        }
    }
}
