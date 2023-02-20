using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BattleSceneUI : MonoBehaviour
{
    [Header("Player")]

    public Slider hpBarSlider;
    public Slider energyBarSlider;
    public TMP_Text maxHPText;
    public TMP_Text currentHPText;
    public TMP_Text maxEnergyText;
    public TMP_Text currentEnergyText;
    public Image ShieldFrameIcon;
    public TMP_Text ShieldFrameText;
    public GameObject playerStatusUI;

    public void Refresh()
    {
        maxEnergyText.text = Player.instance.maxEnergy.ToString();
        currentEnergyText.text = Player.instance.currentEnergy.ToString();
        energyBarSlider.maxValue = Player.instance.maxEnergy;
        energyBarSlider.value = Player.instance.currentEnergy;

        maxHPText.text = Player.instance.maxHP.ToString();
        currentHPText.text = Player.instance.currentHP.ToString();
        hpBarSlider.maxValue = Player.instance.maxHP;
        hpBarSlider.value = Player.instance.currentHP;


        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Battle")
        {
            Player.instance.DisplayBlock();
        }
    }


}
