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

    // ɾ�� OnEnable()
    void Start()
    {
        uiText = GetComponent<Text>();
        tmpText = GetComponent<TextMeshProUGUI>();

        if (uiText == null && tmpText == null)
        {
            Debug.LogError($"[{gameObject.name}] û�� Text �� TMP �����");
        }
        else
        {
            RefreshText();
        }
    }

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
        if (LocalizationManager.Instance.CurrentFont != null)
        {
            uiText.font = LocalizationManager.Instance.CurrentFont;
        }//ˢ������
    }

    //private void OnEnable()
    //{
    //    RefreshText();
    //}
}
