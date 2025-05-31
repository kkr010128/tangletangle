using UnityEngine;

public class PlayerArrowMoveAndDrop : DropperBase
{
    public float moveSpeed = 5f;

    void Update()
    {
        float move = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) move = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) move = 1f;

        transform.position += new Vector3(move, 0, 0) * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DropFruit(); // DropperBase에 정의된 확률 기반 드롭
        }
    }
}