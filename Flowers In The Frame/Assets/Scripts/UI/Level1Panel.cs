using UnityEngine;

public class Level1Panel : LevelPanelBase
{
    // �������ؿ��������߼�������д������
    protected override void Start()
    {
        base.Start(); // һ��Ҫ���û��࣬���ܰ󶨰�ť����¼�
        settingPanel.SetActive(false);
        // ���Լ��Ĺؿ���ʼ���߼�
        Debug.Log("Level 1 ����ʼ�����");
    }
}
