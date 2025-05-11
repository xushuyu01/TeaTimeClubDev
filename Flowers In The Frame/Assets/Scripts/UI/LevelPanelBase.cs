using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
public class LevelPanelBase : MonoBehaviour
{
    [Header("通用功能按钮")]
    public Button btnSetting;
    public Button btnScreenshot;
    public Button btnAlbum;

    public AudioClip clickSound;

    [Header("其他界面面板")]
    protected GameObject settingPanel; // 拖入 SettingPanel 对象
    protected GameObject albumPanel;   // 拖入 AlbumPanel 对象

    protected virtual void Start()
    {
        settingPanel = GameObject.FindObjectsOfType<Transform>(true)
    .FirstOrDefault(t => t.name == "SettingPanel")?.gameObject;

        albumPanel = GameObject.FindObjectsOfType<Transform>(true)
.FirstOrDefault(t => t.name == "AlbumPanel")?.gameObject;
        InitCommonButtons();

        settingPanel.SetActive(false);
        albumPanel.SetActive(false);
    }

    protected virtual void InitCommonButtons()
    {
        if (btnSetting != null)
        {
            btnSetting.onClick.RemoveAllListeners();
            btnSetting.onClick.AddListener(OpenSettingPanel);
            btnSetting.onClick.AddListener(() => AudioManager.Instance.PlaySFX(clickSound));
        }

        if (btnScreenshot != null)
        {
            btnScreenshot.onClick.RemoveAllListeners();
            btnScreenshot.onClick.AddListener(TakeScreenshot);
        }

        if (btnAlbum != null)
        {
            btnAlbum.onClick.RemoveAllListeners();
            btnAlbum.onClick.AddListener(OpenAlbumPanel);
        }
    }

    protected virtual void OpenSettingPanel()
    {
        //Debug.Log("点击了设置按钮！");
        if (settingPanel != null)
        {
            settingPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("找不到 SettingPanel！");
        }
    }

    protected virtual void OpenAlbumPanel()
    {
        if (albumPanel != null)
            albumPanel.SetActive(true);
        else
            Debug.LogWarning("AlbumPanel 未绑定！");
    }

    protected virtual void TakeScreenshot()
    {
        ScreenshotManager screenshotManager = FindObjectOfType<ScreenshotManager>();
        if (screenshotManager != null)
        {
            screenshotManager.CaptureAndSaveScreenshot();
        }
        else
        {
            Debug.LogWarning("找不到 ScreenshotManager！");
        }
    }

    public void PlayScreenshotFlyToAlbum(string screenshotPath, RectTransform albumButton)
    {
        Debug.Log("PlayScreenshotFlyToAlbum 被调用，路径：" + screenshotPath);
        StartCoroutine(ScreenshotFlyCoroutine(screenshotPath, albumButton));
    }

    IEnumerator ScreenshotFlyCoroutine(string path, RectTransform albumBtn)
    {
        // 加载截图
        byte[] imgBytes = System.IO.File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(imgBytes);
        if (!tex.LoadImage(imgBytes))
        {
            Debug.LogError("图片加载失败！");
        }

        // 创建 UI 图片
        GameObject imageGO = new GameObject("FlyingScreenshot", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        imageGO.transform.SetParent(GameObject.Find("Canvas").transform); // 确保挂在 Canvas 下
        RectTransform rt = imageGO.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(300, 300); // 初始尺寸
        rt.anchoredPosition = Vector2.zero;
        rt.localScale = Vector3.one;

        UnityEngine.UI.Image img = imageGO.GetComponent<Image>();
        img.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

        // 检查是否创建成功
        if (imageGO != null && img != null)
        {
            Debug.Log("图片创建成功！");
        }
        else
        {
            Debug.LogError("图片创建失败！");
        }

        // 将图片放在同级的最上层
        rt.SetAsLastSibling();

        // 目标位置
        Vector3 targetPos = albumBtn.position;
        Vector3 startPos = rt.position;

        float duration = 1.2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            rt.position = Vector3.Lerp(startPos, targetPos, t);
            rt.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 0.3f, t); // 缩小一点

            yield return null;
        }

        Destroy(imageGO); // 动画结束后销毁

        // 可选：添加该截图到相册数据里
    }


}
