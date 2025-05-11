using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
public class LevelPanelBase : MonoBehaviour
{
    [Header("ͨ�ù��ܰ�ť")]
    public Button btnSetting;
    public Button btnScreenshot;
    public Button btnAlbum;

    public AudioClip clickSound;

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

    public void PlayScreenshotFlyToAlbum(string screenshotPath, RectTransform albumButton)
    {
        Debug.Log("PlayScreenshotFlyToAlbum �����ã�·����" + screenshotPath);
        StartCoroutine(ScreenshotFlyCoroutine(screenshotPath, albumButton));
    }

    IEnumerator ScreenshotFlyCoroutine(string path, RectTransform albumBtn)
    {
        // ���ؽ�ͼ
        byte[] imgBytes = System.IO.File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(imgBytes);
        if (!tex.LoadImage(imgBytes))
        {
            Debug.LogError("ͼƬ����ʧ�ܣ�");
        }

        // ���� UI ͼƬ
        GameObject imageGO = new GameObject("FlyingScreenshot", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        imageGO.transform.SetParent(GameObject.Find("Canvas").transform); // ȷ������ Canvas ��
        RectTransform rt = imageGO.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(300, 300); // ��ʼ�ߴ�
        rt.anchoredPosition = Vector2.zero;
        rt.localScale = Vector3.one;

        UnityEngine.UI.Image img = imageGO.GetComponent<Image>();
        img.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

        // ����Ƿ񴴽��ɹ�
        if (imageGO != null && img != null)
        {
            Debug.Log("ͼƬ�����ɹ���");
        }
        else
        {
            Debug.LogError("ͼƬ����ʧ�ܣ�");
        }

        // ��ͼƬ����ͬ�������ϲ�
        rt.SetAsLastSibling();

        // Ŀ��λ��
        Vector3 targetPos = albumBtn.position;
        Vector3 startPos = rt.position;

        float duration = 1.2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            rt.position = Vector3.Lerp(startPos, targetPos, t);
            rt.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 0.3f, t); // ��Сһ��

            yield return null;
        }

        Destroy(imageGO); // ��������������

        // ��ѡ����Ӹý�ͼ�����������
    }


}
