using System.IO;
using System.Collections;
using UnityEngine;
using System;

public class ScreenshotManager : MonoBehaviour
{
    public RectTransform screenshotArea; // ��ק�����Ľ�ͼ UI ����
    public int levelID;

    public void CaptureAndSaveScreenshot()
    {
        StartCoroutine(CaptureRoutine());
    }

    private IEnumerator CaptureRoutine()
    {
        yield return new WaitForEndOfFrame();

        // ��ȡ������Ļ��ͼ
        Texture2D screenTex = ScreenCapture.CaptureScreenshotAsTexture();

        // ��ȡ��ͼ�������Ļλ�ã�ע�⣺ʹ�� null ������Ļ�ռ䣩
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, screenshotArea.position);
        Vector2 size = screenshotArea.rect.size;
        Vector2 scale = screenshotArea.lossyScale;

        int width = Mathf.RoundToInt(size.x * scale.x);
        int height = Mathf.RoundToInt(size.y * scale.y);
        int x = Mathf.Clamp(Mathf.RoundToInt(screenPos.x - width / 2f), 0, screenTex.width - 1);
        int y = Mathf.Clamp(Mathf.RoundToInt(screenPos.y - height / 2f), 0, screenTex.height - 1);

        // �ü�����
        Texture2D cropped = new Texture2D(width, height, TextureFormat.RGB24, false);
        cropped.SetPixels(screenTex.GetPixels(x, y, width, height));
        cropped.Apply();

        // ʹ��ʱ�����ȷ���ļ���Ψһ
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string path = Path.Combine(Application.persistentDataPath, $"photo_level_{levelID}_{timestamp}.png");

        // ����ͼƬ
        byte[] bytes = cropped.EncodeToPNG();
        File.WriteAllBytes(path, bytes);

        Debug.Log("Screenshot saved to: " + path);

        // �ͷ��ڴ�
        Destroy(screenTex);
        Destroy(cropped);
    }
}
