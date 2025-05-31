using UnityEngine;

public class PlayerArrowMoveAndDrop : DropperBase
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
        if (Input.GetKey(KeyCode.LeftArrow)) move = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) move = 1f;

        transform.position += new Vector3(move, 0, 0) * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.DownArrow))
            DropFruit();

        if (Input.GetKeyDown(KeyCode.UpArrow))
            inventory?.UseNextItem(); // 아이템 사용
    }
}