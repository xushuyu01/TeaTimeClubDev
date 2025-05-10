using UnityEngine;
using UnityEngine.EventSystems;

public class FlowerDraggerWithArea : FlowerDraggerBase
{
    public RectTransform validDropArea; // �ⲿ����Ϸ�����

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData); // ���� blocksRaycasts = true

        if (validDropArea == null || !RectTransformUtility.RectangleContainsScreenPoint(
                validDropArea, Input.mousePosition, canvas.worldCamera))
        {
            ResetPosition(); // ���û���Ļ�λ����
        }
    }
}
