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

        // ���� Toggle ״̬
        toggleChinese.isOn = savedLang == "zh";
        toggleEnglish.isOn = savedLang == "en";

        // �ֶ�����һ�ζ�Ӧ���Լ����߼�
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
        LocalizedText[] allTexts = FindObjectsOfType<LocalizedText>(true); // ������������
        foreach (var text in allTexts)
        {
            text.RefreshText();
        }
    }
}
