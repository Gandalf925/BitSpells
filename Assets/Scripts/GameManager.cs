using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Battle")]
    public GameObject battlePlayerSideUI;
    public GameObject battleEnemySideUI;

    [Header("Rest")]
    public GameObject restTwoButtonEventPanel;
    public GameObject restOneButtonEventPanel;
    public GameObject restPlayerSideUI;
    public GameObject restSparkParticle1;
    public GameObject restSparkParticle2;


    [Header("All")]
    public Player player;
    public Artifact artifact;

    [SerializeField] GameObject configPanel;
    public GameObject HideUIToggle;
    Toggle toggle;
    bool isOpenConfig = false;

    //singleton
    public static GameManager instance;

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

        toggle = HideUIToggle.GetComponent<Toggle>();
    }

    public void toggleConfigPanel()
    {
        if (!isOpenConfig)
        {
            isOpenConfig = true;
            configPanel.SetActive(true);
            HideUIToggle.SetActive(false);
        }
        else
        {
            isOpenConfig = false;
            configPanel.SetActive(false);
            HideUIToggle.SetActive(true);
        }
    }

    public void HideUI()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Rest")
        {
            if (!toggle.isOn)
            {
                restTwoButtonEventPanel.SetActive(false);
                restOneButtonEventPanel.SetActive(false);
                restPlayerSideUI.SetActive(false);
                restSparkParticle1.SetActive(false);
                restSparkParticle2.SetActive(false);
            }
            else
            {
                restTwoButtonEventPanel.SetActive(true);
                restOneButtonEventPanel.SetActive(true);
                restPlayerSideUI.SetActive(true);
                restSparkParticle1.SetActive(true);
                restSparkParticle2.SetActive(true);
            }
        }
        else if (sceneName == "Battle")
        {
            if (!toggle.isOn)
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

}
