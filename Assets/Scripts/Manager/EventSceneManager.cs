using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class EventSceneManager : MonoBehaviour
{
    [Header("UI")]
    [Header("Three Button Event Panel UI")]
    public GameObject threeButtonEventPanel;
    public Image threeButtonPanelImage;
    public TMPro.TextMeshProUGUI threeButtonEventPanelText;
    public Button threeButtonPanelButton1;
    public Button threeButtonPanelButton2;
    public Button threeButtonPanelButton3;
    public TMP_Text threeButtonEventPanelButtonText1;
    public TMP_Text threeButtonEventPanelButtonText2;
    public TMP_Text threeButtonEventPanelButtonText3;
    public TMPro.TextMeshProUGUI button1InfoText;
    public TMPro.TextMeshProUGUI button2InfoText;
    public TMPro.TextMeshProUGUI button3InfoText;


    [Header("Two Button Event Panel UI")]
    public Image twoButtonPanelImage;
    public GameObject twoButtonEventPanel;
    public TMP_Text twoButtonPanelTextInfo;
    public Button twoButtonPanelButton1;
    public Button twoButtonPanelButton2;
    public TMP_Text twoButtonPanelButtonText1;
    public TMP_Text twoButtonPanelButtonText2;

    [Header("One Button Event Panel UI")]
    public Image oneButtonPanelImage;
    public GameObject oneButtonEventPanel;
    public TMP_Text oneButtonPanelTextInfo;
    public Button oneButtonPanelButton;
    public TMP_Text oneButtonPanelButtonText;

    [Header("Button Sprites")]
    public Sprite redButtonSprite;
    public Sprite blueButtonSprite;
    public Sprite yellowButtonSprite;
    public Sprite greenButtonSprite;

    private Dictionary<EventEntity.ButtonColor, Sprite> buttonColorToSprite;

    private EventEntity eventEntity;
    public EventActions eventActions;

    public Image BackgroundImage;
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

        buttonColorToSprite = new Dictionary<EventEntity.ButtonColor, Sprite>
        {
            {EventEntity.ButtonColor.Red, redButtonSprite},
            {EventEntity.ButtonColor.Blue, blueButtonSprite},
            {EventEntity.ButtonColor.Yellow, yellowButtonSprite},
            {EventEntity.ButtonColor.Green, greenButtonSprite}
        };
    }

    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        EventInit();

        // eventActions の初期化
        eventActions = new EventActions();

        threeButtonPanelButton1.onClick.AddListener(PushThreeButton1);
        threeButtonPanelButton2.onClick.AddListener(PushThreeButton2);
        threeButtonPanelButton3.onClick.AddListener(PushThreeButton3);
        twoButtonPanelButton1.onClick.AddListener(PushTwoButton1);
        twoButtonPanelButton2.onClick.AddListener(PushTwoButton2);
        oneButtonPanelButton.onClick.AddListener(PushOneButton);
    }

    void EventInit()
    {
        NextSceneManager nextSceneManager = FindObjectOfType<NextSceneManager>();
        eventData = nextSceneManager.CreateEventScene(0);
        BackgroundImage.sprite = eventData.eventBackground;
        uIManager.Refresh();
    }

    public IEnumerator ShowTwoButtonEventPanel(EventEntity eventData)
    {
        this.eventEntity = eventData;
        yield return new WaitForSeconds(2f);
        twoButtonEventPanel.SetActive(true);
        twoButtonPanelTextInfo.text = eventData.twoButtonEventPanelText;

        twoButtonPanelButtonText1.text = eventData.twoButtonEventPanelButton1Text;
        twoButtonPanelButtonText2.text = eventData.twoButtonEventPanelButton2Text;
        twoButtonPanelImage.sprite = eventData.twoButtonEventPanelImage;
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

    IEnumerator CloseThreeButtonEventPanel()
    {
        threeButtonEventPanel.transform.DOScale(closeEventPanelSize, 0.2f);
        yield return new WaitForSeconds(0.2f);
        threeButtonEventPanel.SetActive(false);
        yield return new WaitForSeconds(0.5f);
    }

    public IEnumerator ShowThreeButtonEventPanel(EventEntity eventData)
    {
        this.eventEntity = eventData;
        yield return new WaitForSeconds(2f);
        threeButtonEventPanel.SetActive(true);
        threeButtonEventPanelText.text = eventData.threeButtonEventPanelText;

        button1InfoText.text = eventData.threeButtonEventPanelButton1InfoText;
        button2InfoText.text = eventData.threeButtonEventPanelButton2InfoText;
        button3InfoText.text = eventData.threeButtonEventPanelButton3InfoText;
        threeButtonPanelImage.sprite = eventData.threeButtonEventPanelImage;
        threeButtonEventPanel.transform.DOScale(openEventPanelSize, 0.2f);
    }


    public void PushThreeButton1()
    {
        StartCoroutine(ActivateThreeButton1Event());
    }


    public void PushThreeButton2()
    {
        StartCoroutine(ActivateThreeButton2Event());
    }

    public void PushThreeButton3()
    {
        StartCoroutine(ActivateThreeButton3Event());
    }


    public void PushTwoButton1()
    {
        StartCoroutine(activeTwoButton1Event());
    }

    public void PushTwoButton2()
    {
        StartCoroutine(activeTwoButton2Event());
    }

    public void PushOneButton()
    {
        StartCoroutine(ActivateOneButtonEvent());
    }

    public IEnumerator activeTwoButton1Event()
    {
        StartCoroutine(CloseTwoButtonEventPanel());
        yield return new WaitForSeconds(0.5f);

        EventEntity.EventType eventType = eventEntity.twoButtonEventPanelButton1EventType;
        ExecuteEventAction(eventType);
        uIManager.Refresh();

        // Update OneButtonEventPanel Text
        oneButtonPanelTextInfo.text = eventEntity.oneButtonEventPanelTextOnTwoButton1;

        OpenOneButtonEventPanel();
    }
    public IEnumerator activeTwoButton2Event()
    {
        StartCoroutine(CloseTwoButtonEventPanel());
        yield return new WaitForSeconds(0.5f);

        EventEntity.EventType eventType = eventEntity.twoButtonEventPanelButton2EventType;
        ExecuteEventAction(eventType);
        uIManager.Refresh();

        // Update OneButtonEventPanel Text
        oneButtonPanelTextInfo.text = eventEntity.oneButtonEventPanelTextOnTwoButton2;

        OpenOneButtonEventPanel();
    }

    private IEnumerator ActivateThreeButton1Event()
    {
        StartCoroutine(CloseThreeButtonEventPanel());
        yield return new WaitForSeconds(0.5f);

        // Event処理
        EventEntity.EventType eventType = eventEntity.threeButtonEventPanelButton1EventType;
        ExecuteEventAction(eventType);
        uIManager.Refresh();

        // Update OneButtonEventPanel Text
        oneButtonPanelTextInfo.text = eventEntity.oneButtonEventPanelTextOnThreeButton1;

        OpenOneButtonEventPanel();
    }

    private IEnumerator ActivateThreeButton2Event()
    {
        StartCoroutine(CloseThreeButtonEventPanel());
        yield return new WaitForSeconds(0.5f);

        // Event処理
        EventEntity.EventType eventType = eventEntity.threeButtonEventPanelButton2EventType;
        ExecuteEventAction(eventType);
        uIManager.Refresh();

        oneButtonPanelTextInfo.text = eventEntity.oneButtonEventPanelTextOnThreeButton2;

        OpenOneButtonEventPanel();
    }

    private IEnumerator ActivateThreeButton3Event()
    {
        StartCoroutine(CloseThreeButtonEventPanel());
        yield return new WaitForSeconds(0.5f);

        // Event処理
        EventEntity.EventType eventType = eventEntity.threeButtonEventPanelButton3EventType;
        ExecuteEventAction(eventType);
        uIManager.Refresh();

        oneButtonPanelTextInfo.text = eventEntity.oneButtonEventPanelTextOnThreeButton3;

        OpenOneButtonEventPanel();
    }

    private IEnumerator ActivateOneButtonEvent()
    {
        StartCoroutine(CloseTwoButtonEventPanel());
        StartCoroutine(CloseOneButtonEventPanel());
        Debug.Log("移動先の表示をする");
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(NextSceneManager.instance.GenerateNextScene());
    }

    public void InitializeEventScene(EventEntity eventData)
    {
        this.eventData = eventData;

        if (eventData.eventPattern == EventEntity.eventPatterns.ThreeButton)
        {
            twoButtonPanelTextInfo.text = eventData.twoButtonEventPanelText;
            twoButtonPanelButtonText1.text = eventData.twoButtonEventPanelButton1Text;
            twoButtonPanelButtonText2.text = eventData.twoButtonEventPanelButton2Text;
            twoButtonPanelImage.sprite = eventData.twoButtonEventPanelImage;
            oneButtonPanelTextInfo.text = eventData.oneButtonEventPanelText;
            oneButtonPanelButtonText.text = eventData.oneButtonEventPanelButtonText;
            oneButtonPanelImage.sprite = eventData.oneButtonEventPanelImage;
            twoButtonPanelButton1.GetComponent<Image>().sprite = ButtonColorToSprite(eventData.twoButtonEventPanelButton1Color);
            twoButtonPanelButton2.GetComponent<Image>().sprite = ButtonColorToSprite(eventData.twoButtonEventPanelButton2Color);
            oneButtonPanelButton.GetComponent<Image>().sprite = ButtonColorToSprite(eventData.oneButtonEventPanelButtonColor);

            threeButtonEventPanelButtonText1.text = eventData.threeButtonEventPanelButton1Text;
            threeButtonEventPanelButtonText2.text = eventData.threeButtonEventPanelButton2Text;
            threeButtonEventPanelButtonText3.text = eventData.threeButtonEventPanelButton3Text;
            threeButtonPanelButton1.GetComponent<Image>().sprite = ButtonColorToSprite(eventData.threeButtonEventPanelButton1Color);
            threeButtonPanelButton2.GetComponent<Image>().sprite = ButtonColorToSprite(eventData.threeButtonEventPanelButton2Color);
            threeButtonPanelButton3.GetComponent<Image>().sprite = ButtonColorToSprite(eventData.threeButtonEventPanelButton3Color);

            StartCoroutine(ShowThreeButtonEventPanel(eventData));
        }
        else
        {
            twoButtonPanelTextInfo.text = eventData.twoButtonEventPanelText;
            twoButtonPanelButtonText1.text = eventData.twoButtonEventPanelButton1Text;
            twoButtonPanelButtonText2.text = eventData.twoButtonEventPanelButton2Text;
            twoButtonPanelImage.sprite = eventData.twoButtonEventPanelImage;
            oneButtonPanelTextInfo.text = eventData.oneButtonEventPanelText;
            oneButtonPanelButtonText.text = eventData.oneButtonEventPanelButtonText;
            oneButtonPanelImage.sprite = eventData.oneButtonEventPanelImage;
            twoButtonPanelButton1.GetComponent<Image>().sprite = ButtonColorToSprite(eventData.twoButtonEventPanelButton1Color);
            twoButtonPanelButton2.GetComponent<Image>().sprite = ButtonColorToSprite(eventData.twoButtonEventPanelButton2Color);
            oneButtonPanelButton.GetComponent<Image>().sprite = ButtonColorToSprite(eventData.oneButtonEventPanelButtonColor);

            StartCoroutine(ShowTwoButtonEventPanel(eventData));
        }
    }
    private Sprite ButtonColorToSprite(EventEntity.ButtonColor buttonColor)
    {
        if (buttonColorToSprite.TryGetValue(buttonColor, out Sprite sprite))
        {
            return sprite;
        }
        else
        {
            Debug.LogError("Invalid button color.");
            return null;
        }
    }

    public void ExecuteEventAction(EventEntity.EventType eventType)
    {
        switch (eventType)
        {
            case EventEntity.EventType.FullHeal:
                eventActions.FullHeal();
                break;
            case EventEntity.EventType.HalfHeal:
                eventActions.HalfHeal();
                break;
            case EventEntity.EventType.Leave:
                eventActions.Leave();
                break;
            default:
                Debug.LogError("Invalid event type.");
                break;
        }
    }
}
