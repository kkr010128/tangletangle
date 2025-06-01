using UnityEngine;
using UnityEngine.UI;

public class BGMController : MonoBehaviour
{
    public AudioSource bgm;
    public Toggle toggle; // 직접 연결

    void Start()
    {
        toggle.isOn = MusicSettings.Instance.isMusicOn;
    }

    public void ToggleMusic(bool isOn)
    {
        MusicSettings.Instance.isMusicOn = isOn;
        if (isOn) bgm.Play();
        else bgm.Stop();
    }
}