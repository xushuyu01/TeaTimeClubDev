using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalizedText : MonoBehaviour
{
    public string localizationKey;

    private Text uiText;
    private TextMeshProUGUI tmpText;

    void Awake()
    {
        // ͬʱ��ȡ�������
        uiText = GetComponent<Text>();
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        uiText = GetComponent<Text>();

        if (uiText == null)
        {
            Debug.LogError($"Text component missing on {gameObject.name}");
        }
        else
        {
            Debug.Log($"Text component found on {gameObject.name}");
        }

        RefreshText();
    }


    //public void RefreshText()
    //{
    //    string localizedString = LocalizationManager.Instance.GetText(localizationKey);

    //    if (uiText != null)
    //    {
    //        uiText.text = localizedString;
    //    }
    //    else if (tmpText != null)
    //    {
    //        tmpText.text = localizedString;
    //    }
    //    else
    //    {
    //        Debug.LogError($"No Text or TMP component found on {gameObject.name}!");
    //    }

    //}
    public void RefreshText()
    {
        string localizedString = LocalizationManager.Instance.GetText(localizationKey);

        Debug.Log($"[{gameObject.name}] ���Ը����ı���Key = {localizationKey}, �İ� = {localizedString}");

        if (uiText != null)
        {
            uiText.text = localizedString;
            Debug.Log($"[{gameObject.name}] ʹ�� Unity UI Text ���óɹ�: {uiText.text}");
        }
        else if (tmpText != null)
        {
            tmpText.text = localizedString;
            Debug.Log($"[{gameObject.name}] ʹ�� TextMeshPro ���óɹ�: {tmpText.text}");
        }
        else
        {
            Debug.LogError($"[{gameObject.name}] û�� Text �� TMP �����");
        }
    }

    private void OnEnable()
    {
        RefreshText();
    }
}
