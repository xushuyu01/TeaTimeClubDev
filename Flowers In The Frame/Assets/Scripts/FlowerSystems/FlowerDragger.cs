using UnityEngine;
using UnityEngine.EventSystems;

public class FlowerDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Transform originalParent;
    private Vector3 originalPosition;

    public RectTransform validDropArea; // �ⲿ��ק��ֵ���Ϸ�����

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();

        originalParent = transform.parent;
        originalPosition = transform.localPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        originalPosition = transform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // ����Ƿ��ںϷ�������
        if (validDropArea == null || !RectTransformUtility.RectangleContainsScreenPoint(validDropArea, Input.mousePosition, canvas.worldCamera))
        {
            // ���ںϷ����򣺻ص�ԭλ��
            transform.localPosition = originalPosition;
        }
    }
}

