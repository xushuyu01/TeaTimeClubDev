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
        // 同时获取两种组件
        uiText = GetComponent<Text>();
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    // 删除 OnEnable()
    void Start()
    {
        uiText = GetComponent<Text>();
        tmpText = GetComponent<TextMeshProUGUI>();

        if (uiText == null && tmpText == null)
        {
            Debug.LogError($"[{gameObject.name}] 没有 Text 或 TMP 组件！");
        }
        else
        {
            RefreshText();
        }
    }

    public void RefreshText()
    {
        string localizedString = LocalizationManager.Instance.GetText(localizationKey);

        Debug.Log($"[{gameObject.name}] 尝试更新文本，Key = {localizationKey}, 文案 = {localizedString}");

        if (uiText != null)
        {
            uiText.text = localizedString;
            Debug.Log($"[{gameObject.name}] 使用 Unity UI Text 设置成功: {uiText.text}");
        }
        else if (tmpText != null)
        {
            tmpText.text = localizedString;
            Debug.Log($"[{gameObject.name}] 使用 TextMeshPro 设置成功: {tmpText.text}");
        }
        else
        {
            Debug.LogError($"[{gameObject.name}] 没有 Text 或 TMP 组件！");
        }
        if (LocalizationManager.Instance.CurrentFont != null)
        {
            uiText.font = LocalizationManager.Instance.CurrentFont;
        }//刷新字体
    }

    //private void OnEnable()
    //{
    //    RefreshText();
    //}
}
