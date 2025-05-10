using UnityEngine;

public class Level1Panel : LevelPanelBase
{
    // 如果这个关卡有特殊逻辑，可以写在这里
    protected override void Start()
    {
        base.Start(); // 一定要调用基类，才能绑定按钮点击事件
        settingPanel.SetActive(false);
        // 你自己的关卡初始化逻辑
        Debug.Log("Level 1 面板初始化完成");
    }
}
