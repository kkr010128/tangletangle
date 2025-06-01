using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteSceneChanger : MonoBehaviour
{
    public string sceneName;

    void OnMouseDown()
    {
        SceneManager.LoadScene(sceneName);
    }
}