using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LevelPanelBase : MonoBehaviour
{
    [Header("通用功能按钮")]
    public Button btnSetting;
    public Button btnScreenshot;
    public Button btnAlbum;

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
}
