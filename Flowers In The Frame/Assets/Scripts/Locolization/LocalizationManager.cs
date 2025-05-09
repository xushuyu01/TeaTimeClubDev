using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum Language { English, Chinese }

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    private Dictionary<string, string> localizedText;
    private Language currentLanguage;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // ������ļ���������
            LoadLocalizationData();

            Debug.Log("LocalizationManager initialized!");
            Debug.Log("Loaded localization keys: " + string.Join(", ", localizedText.Keys));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadLocalizationData()
    {
        // �����Ǵ� CSV �ļ��м�������
        TextAsset csv = Resources.Load<TextAsset>("Excel/localization");  // Ĭ��ΪӢ��
        if (csv != null)
        {
            localizedText = ParseCSV(csv.text);
            Debug.Log("Default localization loaded!");
        }
        else
        {
            Debug.LogError("Default localization file not found!");
        }
    }

    public void LoadLocalization(Language language)
    {
        localizedText.Clear();

        currentLanguage = language;

        string fileName = $"Excel/localization"; // ���ü� _en �� _zh����Ϊ CSV �ļ��Ѿ���������������
        TextAsset csv = Resources.Load<TextAsset>(fileName);

        if (csv == null)
        {
            Debug.LogError("Localization CSV not found: " + fileName);
            return;
        }

        localizedText = ParseCSV(csv.text);

        Debug.Log($"Localization loaded for {language} with keys: {string.Join(", ", localizedText.Keys)}");

        PlayerPrefs.SetString("Language", language.ToString());
        PlayerPrefs.Save();

        Debug.Log("�������ԣ�" + language);
        Debug.Log("���ص��� key ������" + localizedText.Count);
    }


    public string GetText(string key)
    {
        if (localizedText == null)
        {
            Debug.LogError("Localization data is not loaded yet!");
            return $"#{key}";
        }

        if (localizedText.TryGetValue(key, out string value))
        {
            return value;
        }
        else
        {
            Debug.LogError($"Localization key '{key}' not found!");
            return $"#{key}";
        }
    }


    private Dictionary<string, string> ParseCSV(string csvText)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        var lines = csvText.Split('\n');

        if (lines.Length < 2)
        {
            Debug.LogError("CSV ��ʽ������������");
            return result;
        }

        // ��ȡ��ͷ
        string[] header = lines[0].Split(',');

        int keyIndex = 0;
        int valueIndex = currentLanguage == Language.Chinese ? 2 : 1;

        for (int i = 1; i < lines.Length; i++) // ��1��ʼ��������ͷ
        {
            var line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            var columns = line.Split(',');

            if (columns.Length > valueIndex)
            {
                string key = columns[keyIndex].Trim();
                string value = columns[valueIndex].Trim();

                if (!result.ContainsKey(key))
                    result.Add(key, value);
            }
            else
            {
                Debug.LogWarning($"CSV ��ʽ�����ȱʧ�У�{line}");
            }
        }

        return result;
    }

}

