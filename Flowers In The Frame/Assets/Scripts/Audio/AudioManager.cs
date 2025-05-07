using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("音频源")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("默认音量")]
    [Range(0f, 1f)] public float bgmVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 加载音量设置
            bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f); // 默认 1.0
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

            ApplyVolumes();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetVolume(AudioType type, float volume)
    {
        if (type == AudioType.BGM)
        {
            bgmVolume = volume;
            bgmSource.volume = bgmVolume;
        }
        else if (type == AudioType.SFX)
        {
            sfxVolume = volume;
            sfxSource.volume = sfxVolume;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume;
        bgmSource.Play();
    }

    private void ApplyVolumes()
    {
        bgmSource.volume = bgmVolume;
        sfxSource.volume = sfxVolume;

        // 储存设置
        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

}
