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
    }

    private void OnEnable()
    {
        RefreshText();
    }
}
