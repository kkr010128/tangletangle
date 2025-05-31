using UnityEngine;

public class OnASDKeyPress_ChangeAnime : MonoBehaviour
{
    public string downAnime = "";   // A: 왼쪽, S: 아래, D: 오른쪽
    public string rightAnime = "";
    public string leftAnime = "";

    private string nowMode = "";
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        nowMode = downAnime;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            nowMode = downAnime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            nowMode = rightAnime;
            if (spriteRenderer != null) spriteRenderer.flipX = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            nowMode = leftAnime;
            if (spriteRenderer != null) spriteRenderer.flipX = true;
        }
    }

    void FixedUpdate()
    {
        GetComponent<Animator>().Play(nowMode);
    }
}