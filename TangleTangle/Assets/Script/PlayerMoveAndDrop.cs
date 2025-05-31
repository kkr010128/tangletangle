using UnityEngine;

public class PlayerMoveAndDrop : DropperBase
{
    public float moveSpeed = 5f;

    void Update()
    {
        float move = 0f;
        if (Input.GetKey(KeyCode.A)) move = -1f;
        if (Input.GetKey(KeyCode.D)) move = 1f;

        transform.position += new Vector3(move, 0, 0) * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.S))
        {
            DropFruit(); // DropperBase에 정의된 확률 기반 드롭
        }
    }
}