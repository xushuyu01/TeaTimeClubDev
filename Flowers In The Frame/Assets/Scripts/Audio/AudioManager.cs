using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("��ƵԴ")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Ĭ������")]
    [Range(0f, 1f)] public float bgmVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // ������������
            bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f); // Ĭ�� 1.0
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

        // ��������
        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

}
