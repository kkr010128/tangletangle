using UnityEngine;

public class ButtonPulseEffect : MonoBehaviour
{
    public float pulseSpeed = 1f;         // 진동 속도
    public float pulseAmount = 0.02f;      // 크기 증감량

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scaleOffset = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = originalScale + Vector3.one * scaleOffset;
    }
}