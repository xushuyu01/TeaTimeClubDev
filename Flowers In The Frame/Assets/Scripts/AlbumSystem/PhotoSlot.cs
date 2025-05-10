using UnityEngine;
using UnityEngine.UI;

public class PhotoSlot : MonoBehaviour
{
    public RawImage photoImage;

    public void SetTexture(Texture2D texture)
    {
        photoImage.texture = texture;
        photoImage.color = Color.white;
    }

    public void Clear()
    {
        photoImage.texture = null;
        photoImage.color = new Color(1, 1, 1, 0); // 设置透明而不是隐藏
    }
}
