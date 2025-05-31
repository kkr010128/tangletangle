using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 키를 누르면 애니메이션을 전환하고 좌우 반전 처리함
public class OnKeyPress_ChangeAnime : MonoBehaviour
{
    public string downAnime = "";   // 아래 방향 : Inspector에 지정
    public string rightAnime = "";  // 오른쪽 방향 : Inspector에 지정
    public string leftAnime = "";   // 왼쪽 방향 : Inspector에 지정

    private string nowMode = "";
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        nowMode = downAnime;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKey("down"))
        {
            nowMode = downAnime;
        }
        if (Input.GetKey("right"))
        {
            nowMode = rightAnime;
            if (spriteRenderer != null) spriteRenderer.flipX = false;
        }
        if (Input.GetKey("left"))
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