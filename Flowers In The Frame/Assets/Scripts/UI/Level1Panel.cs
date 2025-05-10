using UnityEngine;

public class Level1Panel : LevelPanelBase
{
    //public RectTransform albumButton;
    
    // �������ؿ��������߼�������д������
    protected override void Start()
    {
        base.Start(); // һ��Ҫ���û��࣬���ܰ󶨰�ť����¼�
        settingPanel.SetActive(false);
        // ���Լ��Ĺؿ���ʼ���߼�
        Debug.Log("Level 1 ����ʼ�����");

        string screenshotPath = Application.persistentDataPath + "/photo_level_1_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        PlayScreenshotFlyToAlbum(screenshotPath, btnAlbum.GetComponent<RectTransform>());
    }
}
