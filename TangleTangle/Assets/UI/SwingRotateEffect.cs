using UnityEngine;

public class SwingRotateEffect : MonoBehaviour
{
    public float angle = 10f;        // 좌우로 흔들릴 최대 각도
    public float speed = 2f;         // 흔들리는 속도

    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation;
    }

    void Update()
    {
        float swing = Mathf.Sin(Time.time * speed) * angle;
        transform.rotation = initialRotation * Quaternion.Euler(0f, 0f, swing);
    }
}