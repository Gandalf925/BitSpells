using UnityEngine;

[CreateAssetMenu(fileName = "EventEntity", menuName = "ScriptableObjects/EventEntity", order = 1)]
public class EventEntity : ScriptableObject
{
    public string eventName;
    public string eventDescription;
    public string twoButtonEventPanelText;
    public string twoButtonEventPanelButton1Text;
    public string twoButtonEventPanelButton2Text;
    public string oneButtonEventPanelText;
    public string oneButtonEventPanelButtonText;
    public EventActions eventActions;
    public EventType eventType;
    public StageType stageType;
    public StageDepth stageDepth;
    public Sprite[] eventImages;


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

}