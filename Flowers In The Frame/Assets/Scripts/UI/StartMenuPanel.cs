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
        base.Start(); // 一定要调用基类，才能绑定按钮点击事件
        settingPanel.SetActive(false);
        // 你自己的关卡初始化逻辑
        Debug.Log("start面板初始化完成");
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
        Debug.Log("退出游戏");
        //Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
}
