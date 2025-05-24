using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionButton : MonoBehaviour
{
    public GameObject levelPrefab;       // 每一关的 Prefab（从 Inspector 拖进去）
    public int levelIndex;               // 关卡编号
    public Button button;                // 当前按钮组件
    public GameObject lockIcon;          // 如果关卡未解锁，显示锁
    public Transform levelParent;        // Prefab 实例化的位置/父物体
    public GameObject levelSelectionPanel;

    private void Start()
    {
        if (button == null)
            button = GetComponent<Button>();

        // 检查是否已解锁
        bool unlocked = IsLevelUnlocked();

        button.interactable = unlocked;
        if (lockIcon != null)
            lockIcon.SetActive(!unlocked);

        // 给按钮添加点击事件
        button.onClick.AddListener(OnLevelSelected);
    }

    private bool IsLevelUnlocked()
    {
        if (levelIndex == 0) return true;
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); // 默认解锁第1关
        return levelIndex <= unlockedLevel;
    }

    private void OnLevelSelected()
    {
        // 清理旧的关卡内容
        foreach (Transform child in levelParent)
        {
            Destroy(child.gameObject);
        }
        levelSelectionPanel.SetActive(false);

        // 实例化新的关卡内容
        if (levelPrefab != null)
        {
            Debug.Log("实例化关卡Prefab：" + levelPrefab.name);
            Instantiate(levelPrefab, levelParent);
        }
        else
        {
            Debug.LogWarning("未设置关卡Prefab！");
        }
    }

    // 这个方法放在游戏管理器或拍照成功回调处调用
    public static void UnlockNextLevel(int currentLevelIndex)
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        int nextLevel = currentLevelIndex + 1;

        if (nextLevel > unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", nextLevel);
            PlayerPrefs.Save();
            Debug.Log("解锁关卡: " + nextLevel);
        }
    }

}
