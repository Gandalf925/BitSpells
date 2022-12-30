using UnityEngine;
using UnityEngine.EventSystems;

public class CardDropAction : MonoBehaviour, IDropHandler
{

    BattleSceneManager battleSceneManager;
    GameManager gameManager;
    [SerializeField] Transform handPanel;


    private void Awake()
    {
        battleSceneManager = FindObjectOfType<BattleSceneManager>();
        gameManager = FindObjectOfType<GameManager>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        CardUI cardUI = eventData.pointerDrag.GetComponent<CardUI>();
        if (cardUI != null)
        {
            gameManager.player.PlayCard(cardUI);
        }
        cardUI.transform.SetParent(handPanel, false);
        cardUI.GetComponent<CanvasGroup>().blocksRaycasts = true;

    }
}
