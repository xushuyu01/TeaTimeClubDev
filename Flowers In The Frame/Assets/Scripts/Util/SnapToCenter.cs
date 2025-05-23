using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SnapToCenter : MonoBehaviour, IEndDragHandler
{
    public ScrollRect scrollRect;
    public RectTransform content;
    public float snapSpeed = 10f;

    private bool isLerping = false;
    private Vector2 targetPos;

    void Update()
    {
        if (isLerping)
        {
            content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, targetPos, Time.deltaTime * snapSpeed);
            if (Vector2.Distance(content.anchoredPosition, targetPos) < 0.1f)
            {
                content.anchoredPosition = targetPos;
                isLerping = false;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float closestDistance = float.MaxValue;
        RectTransform closest = null;

        Vector3 viewportCenterLocal = content.InverseTransformPoint(scrollRect.viewport.position);

        foreach (RectTransform child in content)
        {
            float distance = Mathf.Abs(content.InverseTransformPoint(child.position).x - viewportCenterLocal.x);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = child;
            }
        }

        if (closest != null)
        {
            float difference = content.InverseTransformPoint(closest.position).x - viewportCenterLocal.x;
            targetPos = content.anchoredPosition - new Vector2(difference, 0);
            isLerping = true;
        }
    }
}
