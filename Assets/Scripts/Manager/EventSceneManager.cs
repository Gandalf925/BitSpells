using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EventSceneManager : MonoBehaviour
{
    [Header("UI")]
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
    UIManager uIManager;

    EventEntity eventData;

    //singleton
    public static EventSceneManager instance;
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
    }

    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        EventInit();
        StartCoroutine(FirstOpenTwoButtonEventPanel());
        twoButtonPanelButton1.onClick.AddListener(PushButton1);
        twoButtonPanelButton2.onClick.AddListener(PushButton2);
        oneButtonPanelButton.onClick.AddListener(PushButton2);
    }

    void EventInit()
    {
        NextSceneManager nextSceneManager = FindObjectOfType<NextSceneManager>();
        eventData = nextSceneManager.CreateEventScene(0);
        uIManager.Refresh();
    }

    IEnumerator FirstOpenTwoButtonEventPanel()
    {
        yield return new WaitForSeconds(2f);
        twoButtonEventPanel.SetActive(true);
        twoButtonEventPanel.transform.DOScale(openEventPanelSize, 0.2f);
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

    public void PushButton1()
    {
        StartCoroutine(activeButton1Event());
    }

    public IEnumerator activeButton1Event()
    {
        StartCoroutine(CloseTwoButtonEventPanel());
        yield return new WaitForSeconds(0.5f);

        // Event処理
        eventData.ActivateEvent();
        uIManager.Refresh();

        OpenOneButtonEventPanel();
    }

    public void PushButton2()
    {
        StartCoroutine(CloseTwoButtonEventPanel());
        StartCoroutine(CloseOneButtonEventPanel());
        Debug.Log("移動先の表示をする");

        StartCoroutine(NextSceneManager.instance.GenerateNextScene());
    }

    public void InitializeEventScene(EventEntity eventData)
    {
        // ScriptableObjectからのデータを使用してイベントシーンを設定
        twoButtonPanelTextInfo.text = eventData.twoButtonEventPanelText;
        twoButtonPanelButtonText1.text = eventData.twoButtonEventPanelButton1Text;
        twoButtonPanelButtonText2.text = eventData.twoButtonEventPanelButton2Text;
        twoButtonPanelImage.sprite = eventData.eventImages[0];
        oneButtonPanelTextInfo.text = eventData.oneButtonEventPanelText;
        oneButtonPanelButtonText.text = eventData.oneButtonEventPanelButtonText;
        oneButtonPanelImage.sprite = eventData.eventImages[0];
    }

}
