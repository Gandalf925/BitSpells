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
    public GameObject restTwoButtonEventPanel;
    public GameObject restOneButtonEventPanel;
    public GameObject eventScenePlayerSideUI;
    public GameObject eventSceneEffectHolder;

    [Header("Events")]
    public GameObject threeButtonEventPanel;
    public GameObject twoButtonEventPanel;
    public GameObject oneButtonEventPanel;

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
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Rest" || sceneName == "Events")
        {
            if (!hideUIToggle.isOn)
            {
                restTwoButtonEventPanel.SetActive(false);
                restOneButtonEventPanel.SetActive(false);
                threeButtonEventPanel.SetActive(false);
                twoButtonEventPanel.SetActive(false);
                oneButtonEventPanel.SetActive(false);
                eventScenePlayerSideUI.SetActive(false);
                if (sceneName == "Rest")
                {
                    eventSceneEffectHolder.SetActive(false);
                }
            }
            else
            {
                restTwoButtonEventPanel.SetActive(true);
                restOneButtonEventPanel.SetActive(true);
                threeButtonEventPanel.SetActive(true);
                twoButtonEventPanel.SetActive(true);
                oneButtonEventPanel.SetActive(true);
                eventScenePlayerSideUI.SetActive(true);
                if (sceneName == "Rest")
                {
                    eventSceneEffectHolder.SetActive(true);
                }
            }
        }
        else if (sceneName == "Battle")
        {
            if (!hideUIToggle.isOn)
            {
                battlePlayerSideUI.SetActive(false);
                battleEnemySideUI.SetActive(false);
            }
            else
            {
                battlePlayerSideUI.SetActive(true);
                battleEnemySideUI.SetActive(true);
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
