using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Coffee.UIExtensions;

public class CardUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    GameManager gameManager;
    BattleSceneManager battleSceneManager;
    public Transform defaultParent;
    public Card data;
    public Camera mainCamera;
    float disaperSpeed = 1.8f;

    [SerializeField] TMP_Text cardNameText;
    [SerializeField] TMP_Text cardCostText;
    [SerializeField] TMP_Text cardDescriptionText;
    [SerializeField] Image cardIcon;
    [SerializeField] UIDissolve dissolveIconFrame;
    [SerializeField] UIDissolve dissolveCostFrame;
    [SerializeField] UIDissolve dissolvecardBase;

    public void CreateCard(Card _card)
    {
        data = _card;
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 0f);
        cardNameText.text = data.name;
        cardDescriptionText.text = data.description;
        cardCostText.text = data.cost.ToString();
        cardIcon.sprite = data.icon;
    }

    void Awake()
    {
        dissolveIconFrame = dissolveIconFrame.GetComponent<UIDissolve>();
        dissolveCostFrame = dissolveCostFrame.GetComponent<UIDissolve>();
        dissolvecardBase = dissolvecardBase.GetComponent<UIDissolve>();
    }

    public IEnumerator Disapear()
    {
        cardDescriptionText.text = null;
        cardCostText.text = null;
        cardNameText.text = null;
        while (dissolveIconFrame.location < 1)
        {
            dissolveIconFrame.location += Time.deltaTime * disaperSpeed;
            dissolveCostFrame.location += Time.deltaTime * disaperSpeed;
            dissolvecardBase.location += Time.deltaTime * disaperSpeed;
            dissolveIconFrame.location = Mathf.Clamp01(dissolveIconFrame.location);
            dissolveCostFrame.location = Mathf.Clamp01(dissolveCostFrame.location);
            dissolvecardBase.location = Mathf.Clamp01(dissolvecardBase.location);
            yield return null;
        }
        dissolveIconFrame.location = 0;
        dissolveCostFrame.location = 0;
        dissolvecardBase.location = 0;
    }

    // Drag＆Drop時
    public void OnBeginDrag(PointerEventData eventData)
    {
        defaultParent = transform.parent;
        transform.SetParent(defaultParent.parent, false);
        transform.DOScale(new Vector3(2.5f, 2.5f, 0f), 0.05f);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // transform.position = eventData.position;
        transform.position = ConvertPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(defaultParent, false);
        transform.DOScale(new Vector3(1.2f, 1.2f, 0f), 0.05f);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    private Vector2 GetLocalPosition(Vector2 screenPosition)
    {
        Vector2 result = Vector2.zero;

        return transform.InverseTransformPoint(screenPosition);
    }

    private Vector3 ConvertPosition(PointerEventData eventData)
    {
        Camera mainCamera = FindObjectOfType<Camera>();
        //ドラッグイベントの座標を、CanVasの中のGameObject上のlocalPositionに変更
        Vector2 localPosition = GetLocalPosition(eventData.position);

        //Canvas内のlocalPositionをCanvas内のpositionへ変更
        Vector3 position = transform.TransformPoint(localPosition);

        //Canvas内のpositionを、カメラのWorld座標系へ変更
        Vector3 nowVector3 = mainCamera.ScreenToWorldPoint(position);
        nowVector3.z = 0;

        return nowVector3;
    }
}
