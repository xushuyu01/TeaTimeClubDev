using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class LevelPanelBase : MonoBehaviour
{
    [Header("ͨ�ù��ܰ�ť")]
    public Button btnSetting;
    public Button btnScreenshot;
    public Button btnAlbum;

    [Header("�����������")]
    protected GameObject settingPanel; // ���� SettingPanel ����
    protected GameObject albumPanel;   // ���� AlbumPanel ����

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
        //Debug.Log("��������ð�ť��");
        if (settingPanel != null)
        {
            settingPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("�Ҳ��� SettingPanel��");
        }
    }

    protected virtual void OpenAlbumPanel()
    {
        if (albumPanel != null)
            albumPanel.SetActive(true);
        else
            Debug.LogWarning("AlbumPanel δ�󶨣�");
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
            Debug.LogWarning("�Ҳ��� ScreenshotManager��");
        }
    }
}
