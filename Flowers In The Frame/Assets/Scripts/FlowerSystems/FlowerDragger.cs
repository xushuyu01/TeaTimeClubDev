using UnityEngine;
using UnityEngine.EventSystems;

public class FlowerDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Transform originalParent;
    private Vector3 originalPosition;

    public RectTransform validDropArea; // 外部拖拽赋值，合法区域

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

        // 检查是否在合法区域内
        if (validDropArea == null || !RectTransformUtility.RectangleContainsScreenPoint(validDropArea, Input.mousePosition, canvas.worldCamera))
        {
            // 不在合法区域：回到原位置
            transform.localPosition = originalPosition;
        }
    }
}

