using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    // Start is called before the first frame update
    
        public Slider bgmSlider;
        public Slider sfxSlider; 

        void Start()
        {
            // 初始化 UI 状态
            bgmSlider.value = AudioManager.Instance.bgmVolume;
            sfxSlider.value = AudioManager.Instance.sfxVolume;

            // 添加事件监听
            bgmSlider.onValueChanged.AddListener((value) =>
                AudioManager.Instance.SetVolume(AudioType.BGM, value));
            sfxSlider.onValueChanged.AddListener((value) =>
                AudioManager.Instance.SetVolume(AudioType.SFX, value));
        }

    
}
