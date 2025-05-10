using UnityEngine;
using UnityEngine.EventSystems;

public class FlowerDraggerWithArea : FlowerDraggerBase
{
    public RectTransform validDropArea; // 外部拖入合法区域

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData); // 保持 blocksRaycasts = true

        if (validDropArea == null || !RectTransformUtility.RectangleContainsScreenPoint(
                validDropArea, Input.mousePosition, canvas.worldCamera))
        {
            ResetPosition(); // 调用基类的回位方法
        }
    }
}
