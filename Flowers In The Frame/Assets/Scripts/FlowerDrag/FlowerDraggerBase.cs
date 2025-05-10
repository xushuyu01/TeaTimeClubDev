using UnityEngine;
using UnityEngine.EventSystems;

public class FlowerDraggerBase : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    protected Canvas canvas;
    protected RectTransform rectTransform;
    protected CanvasGroup canvasGroup;

    protected Vector3 originalPosition;

    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();

        originalPosition = transform.localPosition;
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        originalPosition = transform.localPosition;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // 默认行为：什么也不做，由子类扩展（如回弹、合法区域判断等）
    }

    /// <summary>
    /// 手动恢复原位置（子类可调用）
    /// </summary>
    protected void ResetPosition()
    {
        transform.localPosition = originalPosition;
    }
}

