using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonSFX : MonoBehaviour
{
    public AudioClip clickSound;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlayClickSound);
    }

    private void PlayClickSound()
    {
        if (clickSound != null)
        {
            AudioManager.Instance.PlaySFX(clickSound);
        }
    }
}
