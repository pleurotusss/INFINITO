using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 5f;
    public float shakeMagnitude = 5f;

    private Transform cameraTransform;
    private Vector3 originalPosition;
    private Coroutine shakeCoroutine;

    void Start()
    {
        cameraTransform = transform;
        originalPosition = cameraTransform.localPosition;
    }

    public void StartShake(float duration = -1, float magnitude = -1)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            cameraTransform.localPosition = originalPosition;
        }

        shakeDuration = duration > 0 ? duration : shakeDuration;
        shakeMagnitude = magnitude > 0 ? magnitude : shakeMagnitude;

        shakeCoroutine = StartCoroutine(Shake());
    }

    public void StopShake()
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            shakeCoroutine = null;
        }
        cameraTransform.localPosition = originalPosition;
    }

    private IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;
            cameraTransform.localPosition = originalPosition + randomOffset;

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalPosition;
        shakeCoroutine = null;
    }
}
