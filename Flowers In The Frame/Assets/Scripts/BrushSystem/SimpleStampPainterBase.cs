using UnityEngine;
using UnityEngine.UI;

public class SimpleStampPainterBase : MonoBehaviour
{
    public RawImage rawImage;
    public Texture2D stampTexture;
    public float stampSize = 0.1f;

    Material mat;
    RenderTexture rt;

    void Start()
    {
        InitRenderTexture();
        InitMaterial();
    }
    protected virtual void InitRenderTexture()
    {
        rt = new RenderTexture(512, 512, 0, RenderTextureFormat.ARGB32);
        rt.Create();

        // 初始化为透明
        RenderTexture activeRT = RenderTexture.active;
        RenderTexture.active = rt;
        GL.Clear(true, true, new Color(0, 0, 0, 0));
        RenderTexture.active = activeRT;

        rawImage.texture = rt;
        rawImage.material = null;
    }
    protected virtual void InitMaterial()
    {
        mat = new Material(Shader.Find("Unlit/SimpleStamp"));
        mat.SetTexture("_StampTex", stampTexture);
        mat.SetVector("_StampSize", new Vector4(stampSize, stampSize, 0, 0));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rawImage.rectTransform,
                Input.mousePosition,
                null,
                out localPoint);

            Rect rect = rawImage.rectTransform.rect;
            Vector2 uv = new Vector2(
                (localPoint.x - rect.x) / rect.width,
                (localPoint.y - rect.y) / rect.height);

            mat.SetTexture("_MainTex", rt);
            mat.SetVector("_StampPos", new Vector4(uv.x, uv.y, 0, 0));
           // mat.SetVector("_StampSize", new Vector4(0.2f, 0.1f, 0, 0)); // 宽高不同比例

            RenderTexture temp = RenderTexture.GetTemporary(rt.width, rt.height);
            Graphics.Blit(rt, temp);
            Graphics.Blit(temp, rt, mat);
            RenderTexture.ReleaseTemporary(temp);
        }
    }
}
