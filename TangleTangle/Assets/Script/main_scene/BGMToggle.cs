using UnityEngine;
using UnityEngine.UI;

public class BGMToggle : MonoBehaviour
{
    public Toggle bgmToggle;
    public AudioSource bgmSource;

    void Start()
    {
        bgmToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            bgmSource.Play();
        }
        else
        {
            bgmSource.Pause();
        }
    }
}