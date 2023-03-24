using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "EventEntity", menuName = "BitSpells/EventEntity", order = 1)]
public class EventEntity : ScriptableObject
{
    public string eventName;
    public string eventDescription;

    [Header("Three Button Event Panel UI")]
    public string threeButtonEventPanelText;
    public string threeButtonEventPanelButton1Text;
    public string threeButtonEventPanelButton2Text;
    public string threeButtonEventPanelButton3Text;
    public string threeButtonEventPanelButton1InfoText;
    public string threeButtonEventPanelButton2InfoText;
    public string threeButtonEventPanelButton3InfoText;
    public Sprite threeButtonEventPanelImage;

    public EventType eventType1;
    public EventType eventType2;
    public EventType eventType3;

    [Header("Three Button Event Panel Text Options")]
    public ButtonText buttonTextOption1;
    public ButtonText buttonTextOption2;
    public ButtonText buttonTextOption3;

    [Header("Two Button Event Panel UI")]
    public string twoButtonEventPanelText;
    public string twoButtonEventPanelButton1Text;
    public string twoButtonEventPanelButton2Text;
    public Sprite twoButtonEventPanelImage;

    [Header("One Button Event Panel UI")]
    public string oneButtonEventPanelText;
    public string oneButtonEventPanelButtonText;
    public Sprite oneButtonEventPanelImage;

    [Header("Two Button Events")]
    public EventActions eventActions;

    [Header("StageDetali")]
    public EventType eventType;
    public StageType stageType;
    public StageDepth stageDepth;


    public void ActivateEvent(int buttonIndex)
    {
        EventType eventTypeToActivate;
        switch (buttonIndex)
        {
            case 1:
                eventTypeToActivate = eventType1;
                break;
            case 2:
                eventTypeToActivate = eventType2;
                break;
            case 3:
                eventTypeToActivate = eventType3;
                break;
            default:
                Debug.LogError("Invalid button index.");
                return;
        }

        switch (eventTypeToActivate)
        {
            case EventType.FullHeal:
                eventActions.FullHealEvent();
                break;
            // 他のイベントタイプのケースをここに追加
            case EventType.Leave:
                eventActions.Leave();
                break;

            default:
                Debug.LogError("Invalid event type.");
                break;
        }
    }



    private void OnEnable()
    {
        eventActions = new EventActions();
    }

    public void ActivateEvent()
    {
        switch (eventType)
        {
            case EventType.FullHeal:
                eventActions.FullHealEvent();
                break;
            // 他のイベントタイプのケースをここに追加
            default:
                Debug.LogError("Invalid event type.");
                break;
        }
    }

    public string GetThreeButtonEventPanelButtonText(ButtonText text)
    {
        switch (text)
        {
            case ButtonText.Option1:
                return threeButtonEventPanelButton1Text;
            case ButtonText.Option2:
                return threeButtonEventPanelButton2Text;
            case ButtonText.Option3:
                return threeButtonEventPanelButton3Text;
            default:
                Debug.LogError("Invalid ButtonOption.");
                return "";
        }
    }

    public enum ButtonText
    {
        Option1 = 1,
        Option2,
        Option3
    }


}
