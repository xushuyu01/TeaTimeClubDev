using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    // Start is called before the first frame update
    
        public Slider bgmSlider;
        public Slider sfxSlider; 

        void Start()
        {
            // ��ʼ�� UI ״̬
            bgmSlider.value = AudioManager.Instance.bgmVolume;
            sfxSlider.value = AudioManager.Instance.sfxVolume;

            // ����¼�����
            bgmSlider.onValueChanged.AddListener((value) =>
                AudioManager.Instance.SetVolume(AudioType.BGM, value));
            sfxSlider.onValueChanged.AddListener((value) =>
                AudioManager.Instance.SetVolume(AudioType.SFX, value));
        }

    
}
