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

        // Ĭ����Ϊ��ʲôҲ��������������չ����ص����Ϸ������жϵȣ�
    }

    /// <summary>
    /// �ֶ��ָ�ԭλ�ã�����ɵ��ã�
    /// </summary>
    protected void ResetPosition()
    {
        transform.localPosition = originalPosition;
    }
}

