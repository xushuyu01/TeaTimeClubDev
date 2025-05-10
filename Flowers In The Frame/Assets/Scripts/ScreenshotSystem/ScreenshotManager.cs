using System.IO;
using System.Collections;
using UnityEngine;
using System;

public class ScreenshotManager : MonoBehaviour
{
    public RectTransform screenshotArea; // 拖拽进来的截图 UI 区域
    public int levelID;

    public LevelPanelBase levelPanelBase; // 外部赋值
    public RectTransform albumButton;     

    public void CaptureAndSaveScreenshot()
    {
        string filename = $"photo_level_{System.DateTime.Now.ToString("yyyyMMdd_HHmmss")}.png";
        string fullPath = Path.Combine(Application.persistentDataPath, filename);

        ScreenCapture.CaptureScreenshot(filename);
        StartCoroutine(WaitForScreenshotThenFly(fullPath));

        // 延迟一帧后刷新相册（因为截图是异步的）
        StartCoroutine(DelayedRefreshAlbum());
        StartCoroutine(CaptureRoutine());
    }
    IEnumerator DelayedRefreshAlbum()
    {
        yield return new WaitForEndOfFrame(); // 等一帧，保证文件写入完成

        var album = FindObjectOfType<PhotoAlbum>();
        if (album != null)
        {
            album.RefreshAlbum();
        }
    }

    private IEnumerator CaptureRoutine()
    {
        yield return new WaitForEndOfFrame();

        // 获取整个屏幕截图
        Texture2D screenTex = ScreenCapture.CaptureScreenshotAsTexture();

        // 获取截图区域的屏幕位置（注意：使用 null 代表屏幕空间）
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, screenshotArea.position);
        Vector2 size = screenshotArea.rect.size;
        Vector2 scale = screenshotArea.lossyScale;

        int width = Mathf.RoundToInt(size.x * scale.x);
        int height = Mathf.RoundToInt(size.y * scale.y);
        int x = Mathf.Clamp(Mathf.RoundToInt(screenPos.x - width / 2f), 0, screenTex.width - 1);
        int y = Mathf.Clamp(Mathf.RoundToInt(screenPos.y - height / 2f), 0, screenTex.height - 1);

        // 裁剪区域
        Texture2D cropped = new Texture2D(width, height, TextureFormat.RGB24, false);
        cropped.SetPixels(screenTex.GetPixels(x, y, width, height));
        cropped.Apply();

        // 使用时间戳来确保文件名唯一
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string path = Path.Combine(Application.persistentDataPath, $"photo_level_{levelID}_{timestamp}.png");

        // 保存图片
        byte[] bytes = cropped.EncodeToPNG();
        File.WriteAllBytes(path, bytes);
        levelPanelBase.PlayScreenshotFlyToAlbum(path, albumButton);

        Debug.Log("Screenshot saved to: " + path);

        // 释放内存
        Destroy(screenTex);
        Destroy(cropped);
    }


    private IEnumerator WaitForScreenshotThenFly(string fullPath)
    {
        while (!File.Exists(fullPath))
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.1f); // 多给点时间确保写完

        // 这里开始飞行动画
        //levelPanelBase.PlayScreenshotFlyToAlbum(fullPath, albumButton);
    }

}
