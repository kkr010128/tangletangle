using UnityEngine;
using UnityEngine.UI;

public class inGameBGMController : MonoBehaviour
{
    public AudioSource bgm;     // 오디오 플레이어
    public Toggle toggle;       // UI 토글 버튼 (캔버스 하위)

    void Start()
    {
        if (toggle.isOn)
            bgm.Play();
        else
            bgm.Stop();
    }

    public void ToggleMusic(bool isOn)
    {
        if (isOn)
            bgm.Play();
        else
            bgm.Stop();
    }
}