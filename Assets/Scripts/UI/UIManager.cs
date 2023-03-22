using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using MoreMountains.Feedbacks;

public class UIManager : MonoBehaviour
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

    [Header("Feedbacks")]
    public MMF_Player feedbackPlayer;
    public GameObject playerSideUI;

    [Header("Battle")]
    public GameObject battlePlayerSideUI;
    public GameObject battleEnemySideUI;

    [Header("Rest")]
    public GameObject eventSceneTwoButtonEventPanel;
    public GameObject eventSceneOneButtonEventPanel;
    public GameObject eventScenePlayerSideUI;
    public GameObject eventSceneEffectHolder;

    [Header("All")]
    public Toggle hideUIToggle;

    [SerializeField] GameObject configPanel;
    public GameObject uncontrollablePanel;

    bool isOpenConfig = false;

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


    public void PlayerUIShakeFB()
    {
        feedbackPlayer = FindObjectOfType<MMF_Player>();
        MMPositionShaker target = playerSideUI.GetComponent<MMPositionShaker>();
        MMF_PositionShake shakeFeedback = feedbackPlayer.GetFeedbackOfType<MMF_PositionShake>();
        shakeFeedback.TargetShaker = target;
        shakeFeedback.Play(new Vector3(5f, 5f, 5f));
    }

    public void HideUI()
    {
        UIManager uIManager = FindObjectOfType<UIManager>();
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Rest" || sceneName == "Events")
        {
            if (!uIManager.hideUIToggle.isOn)
            {
                uIManager.eventSceneTwoButtonEventPanel.SetActive(false);
                uIManager.eventSceneOneButtonEventPanel.SetActive(false);
                uIManager.eventScenePlayerSideUI.SetActive(false);
                uIManager.eventSceneEffectHolder.SetActive(false);
            }
            else
            {
                uIManager.eventSceneTwoButtonEventPanel.SetActive(true);
                uIManager.eventSceneOneButtonEventPanel.SetActive(true);
                uIManager.eventScenePlayerSideUI.SetActive(true);
                uIManager.eventSceneEffectHolder.SetActive(true);
            }
        }
        else if (sceneName == "Battle")
        {
            if (!uIManager.hideUIToggle.isOn)
            {
                uIManager.battlePlayerSideUI.SetActive(false);
                uIManager.battleEnemySideUI.SetActive(false);
            }
            else
            {
                uIManager.battlePlayerSideUI.SetActive(true);
                uIManager.battleEnemySideUI.SetActive(true);
            }
        }
    }

    public void toggleConfigPanel()
    {
        if (!isOpenConfig)
        {
            isOpenConfig = true;
            configPanel.SetActive(true);
            hideUIToggle.gameObject.SetActive(false);
        }
        else
        {
            isOpenConfig = false;
            configPanel.SetActive(false);
            hideUIToggle.gameObject.SetActive(true);
        }
    }
}
