using UnityEngine;
using UnityEngine.UI;


public class LanguageToggleController : MonoBehaviour
{

    public Toggle toggleChinese;
    public Toggle toggleEnglish;

    private void Start()
    {
        string savedLang = PlayerPrefs.GetString("Language", "en");

        toggleChinese.onValueChanged.AddListener(OnToggleChinese);
        toggleEnglish.onValueChanged.AddListener(OnToggleEnglish);

        // 设置 Toggle 状态
        toggleChinese.isOn = savedLang == "zh";
        toggleEnglish.isOn = savedLang == "en";

        // 手动触发一次对应语言加载逻辑
        if (savedLang == "zh")
        {
            OnToggleChinese(true);
        }
        else
        {
            OnToggleEnglish(true);
        }
    }


    public void OnToggleChinese(bool isOn)
    {
        if (isOn)
        {
            LocalizationManager.Instance.LoadLocalization(Language.Chinese);
            RefreshAllLocalizedText();

            PlayerPrefs.SetString("Language", "zh");
            PlayerPrefs.Save();
        }
    }

    public void OnToggleEnglish(bool isOn)
    {
        if (isOn)
        {
            LocalizationManager.Instance.LoadLocalization(Language.English);
            RefreshAllLocalizedText();

            PlayerPrefs.SetString("Language", "en");
            PlayerPrefs.Save();
        }
    }

    private void RefreshAllLocalizedText()
    {
        LocalizedText[] allTexts = FindObjectsOfType<LocalizedText>(true); // 包括隐藏物体
        foreach (var text in allTexts)
        {
            text.RefreshText();
        }
    }
}
