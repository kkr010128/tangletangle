using UnityEngine;
public class MusicSettings : MonoBehaviour
{
    public static MusicSettings Instance;
    public bool isMusicOn = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}