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

            // 假设从文件加载数据
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
        // 假设是从 CSV 文件中加载数据
        TextAsset csv = Resources.Load<TextAsset>("Excel/localization");  // 默认为英文
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

        string fileName = $"Excel/localization"; // 不用加 _en 或 _zh，因为 CSV 文件已经包含两种语言列
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

        Debug.Log("加载语言：" + language);
        Debug.Log("加载到的 key 数量：" + localizedText.Count);
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
            Debug.LogError("CSV 格式错误，行数不足");
            return result;
        }

        // 读取表头
        string[] header = lines[0].Split(',');

        int keyIndex = 0;
        int valueIndex = currentLanguage == Language.Chinese ? 2 : 1;

        for (int i = 1; i < lines.Length; i++) // 从1开始，跳过表头
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
                Debug.LogWarning($"CSV 格式错误或缺失列：{line}");
            }
        }

        return result;
    }

}

