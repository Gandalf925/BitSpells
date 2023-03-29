using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "EventEntity", menuName = "BitSpells/EventEntity", order = 1)]
public class EventEntity : ScriptableObject
{

    [Header("Event Type")]
    public eventPatterns eventPattern;


    public string eventName;
    public string eventDescription;

    [Header("Event Background")]
    public Sprite eventBackground;

    [Header("Three Button Event Panel UI")]
    [Multiline] public string threeButtonEventPanelText;
    public EventType threeButtonEventPanelButton1EventType;
    public string threeButtonEventPanelButton1Text;
    public string threeButtonEventPanelButton1InfoText;
    public ButtonColor threeButtonEventPanelButton1Color;
    public EventType threeButtonEventPanelButton2EventType;
    public string threeButtonEventPanelButton2Text;
    public string threeButtonEventPanelButton2InfoText;
    public ButtonColor threeButtonEventPanelButton2Color;
    public EventType threeButtonEventPanelButton3EventType;
    public string threeButtonEventPanelButton3Text;
    public string threeButtonEventPanelButton3InfoText;
    public ButtonColor threeButtonEventPanelButton3Color;
    public Sprite threeButtonEventPanelImage;

    [Header("Two Button Event Panel UI")]
    [Multiline] public string twoButtonEventPanelText;
    public EventType twoButtonEventPanelButton1EventType;
    public string twoButtonEventPanelButton1Text;
    public ButtonColor twoButtonEventPanelButton1Color;
    public EventType twoButtonEventPanelButton2EventType;
    public string twoButtonEventPanelButton2Text;
    public ButtonColor twoButtonEventPanelButton2Color;
    public Sprite twoButtonEventPanelImage;


    [Header("One Button Event Panel UI")]
    public string oneButtonEventPanelText;

    [Multiline] public string oneButtonEventPanelTextOnThreeButton1;
    [Multiline] public string oneButtonEventPanelTextOnThreeButton2;
    [Multiline] public string oneButtonEventPanelTextOnThreeButton3;
    [Multiline] public string oneButtonEventPanelTextOnTwoButton1;
    [Multiline] public string oneButtonEventPanelTextOnTwoButton2;
    public string oneButtonEventPanelButtonText;
    public ButtonColor oneButtonEventPanelButtonColor;
    public Sprite oneButtonEventPanelImage;

    [Header("Two Button Events")]
    public EventActions eventActions;

    [Header("StageDetail")]
    public EventType eventType;
    public StageType stageType;
    public StageDepth stageDepth;



    private void OnEnable()
    {
        eventActions = new EventActions();
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

    public enum EventType
    {
        FullHeal,
        // 他のイベントタイプをここに追加
        Leave,
    }

    public enum ButtonText
    {
        Option1 = 1,
        Option2,
        Option3
    }
    public enum ButtonColor
    {
        Red,
        Blue,
        Yellow,
        Green
    }
    public enum eventPatterns
    {
        TwoButton,
        ThreeButton
    }

}
