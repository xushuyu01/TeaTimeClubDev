using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuPanel : LevelPanelBase
{
    public Button btnQuit;
    public GameObject startPanel;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); // һ��Ҫ���û��࣬���ܰ󶨰�ť����¼�
        settingPanel.SetActive(false);
        // ���Լ��Ĺؿ���ʼ���߼�
        Debug.Log("start����ʼ�����");
        InitStartPanelButtons();
    }


    private void InitStartPanelButtons()
    {

        if (btnQuit != null)
        {
            btnQuit.onClick.RemoveAllListeners();
            btnQuit.onClick.AddListener(QuitGame);
        }
    }

    private void QuitGame()
    {
        Debug.Log("�˳���Ϸ");
        //Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
}
