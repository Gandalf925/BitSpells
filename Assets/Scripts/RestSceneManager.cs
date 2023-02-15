using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RestSceneManager : MonoBehaviour
{
    GameManager gameManager;
    public GameObject twoButtonEventPanel;
    public TMP_Text twoButtonPanelTextInfo;
    public Button twoButtonPanelButton1;
    public Button twoButtonPanelButton2;
    public TMP_Text twoButtonPanelButtonText1;
    public TMP_Text twoButtonPanelButtonText2;
    public Image twoButtonPanelImage;
    public GameObject oneButtonEventPanel;
    public TMP_Text oneButtonPanelTextInfo;
    public Button oneButtonPanelButton;
    public TMP_Text oneButtonPanelButtonText;
    public Image oneButtonPanelImage;
    Vector3 openEventPanelSize = new Vector3(1.35f, 1.35f, 1.35f);
    Vector3 closeEventPanelSize = new Vector3(0f, 0f, 0f);
    int healAmount = 70;
    [SerializeField] GameObject sparkParticle1;
    [SerializeField] GameObject sparkParticle2;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

    }

    void Start()
    {
        RestInit();
        StartCoroutine(FirstOpenTwoButtonEventPanel());
        twoButtonPanelButton1.onClick.AddListener(PushRestButton);
        twoButtonPanelButton2.onClick.AddListener(PushLeaveButton);
        oneButtonPanelButton.onClick.AddListener(PushLeaveButton);
    }

    void RestInit()
    {
        gameManager.player.Refresh();
    }

    IEnumerator FirstOpenTwoButtonEventPanel()
    {
        yield return new WaitForSeconds(2f);
        twoButtonEventPanel.SetActive(true);
        twoButtonEventPanel.transform.DOScale(openEventPanelSize, 0.2f);
        StartParticles();
    }

    void OpenTwoButtonEventPanel()
    {
        twoButtonEventPanel.SetActive(true);
        twoButtonEventPanel.transform.DOScale(openEventPanelSize, 0.2f);

    }
    void OpenOneButtonEventPanel()
    {
        oneButtonEventPanel.SetActive(true);
        oneButtonEventPanel.transform.DOScale(openEventPanelSize, 0.2f);
    }

    IEnumerator CloseTwoButtonEventPanel()
    {
        twoButtonEventPanel.transform.DOScale(closeEventPanelSize, 0.2f);
        yield return new WaitForSeconds(0.2f);
        twoButtonEventPanel.SetActive(false);
    }

    IEnumerator CloseOneButtonEventPanel()
    {
        oneButtonEventPanel.transform.DOScale(closeEventPanelSize, 0.2f);
        yield return new WaitForSeconds(0.2f);
        oneButtonEventPanel.SetActive(false);
    }

    public void PushRestButton()
    {
        StartCoroutine(Rest());
    }

    public void PushLeaveButton()
    {
        StartCoroutine(Leave());
    }

    public IEnumerator Rest()
    {
        EndParticles();
        StartCoroutine(CloseTwoButtonEventPanel());
        yield return new WaitForSeconds(0.5f);
        gameManager.player.currentHP += gameManager.player.maxHP * (healAmount / 100);
        if (gameManager.player.currentHP < gameManager.player.maxHP)
        {
            gameManager.player.currentHP = gameManager.player.maxHP;
        }
        gameManager.player.Refresh();

        OpenOneButtonEventPanel();
    }

    public IEnumerator Leave()
    {
        EndParticles();
        StartCoroutine(CloseTwoButtonEventPanel());
        StartCoroutine(CloseOneButtonEventPanel());
        yield return new WaitForSeconds(1f);
        Debug.Log("移動先の表示をする");
    }


    void StartParticles()
    {
        sparkParticle1.SetActive(true);
        sparkParticle2.SetActive(true);
    }
    void EndParticles()
    {
        sparkParticle1.SetActive(false);
        sparkParticle2.SetActive(false);
    }

}
