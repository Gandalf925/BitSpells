using UnityEngine;
using UnityEngine.EventSystems;

public class CardDropAction : MonoBehaviour, IDropHandler
{

    BattleSceneManager battleSceneManager;
    GameManager gameManager;

    private void Awake()
    {
        battleSceneManager = FindObjectOfType<BattleSceneManager>();
        gameManager = FindObjectOfType<GameManager>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        CardUI cardUI = eventData.pointerDrag.GetComponent<CardUI>();
        Enemy target = GetComponent<Enemy>();
        if (cardUI != null)
        {
            gameManager.player.PlayCard(cardUI, target);
        }
        cardUI.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
