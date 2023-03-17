using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RestSceneManager : MonoBehaviour
{
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
    UIManager uIManager;

    [SerializeField] GameObject sparkParticle1;
    [SerializeField] GameObject sparkParticle2;


    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        RestInit();
        StartCoroutine(FirstOpenTwoButtonEventPanel());
        twoButtonPanelButton1.onClick.AddListener(PushRestButton);
        twoButtonPanelButton2.onClick.AddListener(Leave);
        oneButtonPanelButton.onClick.AddListener(Leave);
    }

    void RestInit()
    {
        uIManager.Refresh();
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
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator CloseOneButtonEventPanel()
    {
        oneButtonEventPanel.transform.DOScale(closeEventPanelSize, 0.2f);
        yield return new WaitForSeconds(0.2f);
        oneButtonEventPanel.SetActive(false);
        yield return new WaitForSeconds(0.5f);
    }

    public void PushRestButton()
    {
        StartCoroutine(Rest());
    }

    public IEnumerator Rest()
    {
        EndParticles();
        StartCoroutine(CloseTwoButtonEventPanel());
        yield return new WaitForSeconds(0.5f);
        Player.instance.currentHP += Player.instance.maxHP * (healAmount / 100);
        if (Player.instance.currentHP < Player.instance.maxHP)
        {
            Player.instance.currentHP = Player.instance.maxHP;
        }
        uIManager.Refresh();

        OpenOneButtonEventPanel();
    }

    public void Leave()
    {
        EndParticles();
        StartCoroutine(CloseTwoButtonEventPanel());
        StartCoroutine(CloseOneButtonEventPanel());
        Debug.Log("移動先の表示をする");

        StartCoroutine(NextSceneManager.instance.GenerateNextScene());
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
