using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionButton : MonoBehaviour
{
    public GameObject levelPrefab;       // ÿһ�ص� Prefab���� Inspector �Ͻ�ȥ��
    public int levelIndex;               // �ؿ����
    public Button button;                // ��ǰ��ť���
    public GameObject lockIcon;          // ����ؿ�δ��������ʾ��
    public Transform levelParent;        // Prefab ʵ������λ��/������
    public GameObject levelSelectionPanel;

    private void Start()
    {
        if (button == null)
            button = GetComponent<Button>();

        // ����Ƿ��ѽ���
        bool unlocked = IsLevelUnlocked();

        button.interactable = unlocked;
        if (lockIcon != null)
            lockIcon.SetActive(!unlocked);

        // ����ť��ӵ���¼�
        button.onClick.AddListener(OnLevelSelected);
    }

    private bool IsLevelUnlocked()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 2); // Ĭ�Ͻ�����1��
        return levelIndex <= unlockedLevel;
    }

    private void OnLevelSelected()
    {
        // ����ɵĹؿ�����
        foreach (Transform child in levelParent)
        {
            Destroy(child.gameObject);
        }
        levelSelectionPanel.SetActive(false);

        // ʵ�����µĹؿ�����
        if (levelPrefab != null)
        {
            Debug.Log("ʵ�����ؿ�Prefab��" + levelPrefab.name);
            Instantiate(levelPrefab, levelParent);
        }
        else
        {
            Debug.LogWarning("δ���ùؿ�Prefab��");
        }
    }

}
