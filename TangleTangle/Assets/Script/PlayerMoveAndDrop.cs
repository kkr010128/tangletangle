using UnityEngine;

public class PlayerMoveAndDrop : DropperBase
{
    public float moveSpeed = 5f;
    private PlayerItemInventory inventory;

    protected override void Start()
    {
        base.Start();
        inventory = GetComponent<PlayerItemInventory>();
    }

    void Update()
    {
        float move = 0f;
        if (Input.GetKey(KeyCode.A)) move = -1f;
        if (Input.GetKey(KeyCode.D)) move = 1f;

        transform.position += new Vector3(move, 0, 0) * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.S))
            DropFruit();

        if (Input.GetKeyDown(KeyCode.W))
            inventory?.UseNextItem(); // 아이템 사용
    }
}