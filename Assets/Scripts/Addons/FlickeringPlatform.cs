using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
public class FlickeringPlatform : MonoBehaviour
{
    [SerializeField] private Color flickerColor;
    [SerializeField] private float flickerSpeed = 2f;
    [SerializeField] private float disappearTime = 1f;
    [SerializeField] private float appearTime = 1f;

    private Renderer platformRenderer;
    private Collider platformCollider;
    private Color originalColor;

    private Coroutine flickerCoroutine;
    private Coroutine disapperCoroutine;

    void Start()
    {
        platformRenderer = GetComponent<Renderer>();
        platformCollider = GetComponent<Collider>();
        originalColor = platformRenderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (flickerCoroutine == null)
                flickerCoroutine = StartCoroutine(Flicker());

            if (disapperCoroutine == null)
                disapperCoroutine = StartCoroutine(Disactivate());
        }
    }

    private IEnumerator Flicker()
    {
        float t = 0f;

        while (true)
        {
            t += Time.deltaTime * flickerSpeed;
            float lerpFactor = Mathf.PingPong(t, 1f);

            platformRenderer.material.color = Color.Lerp(originalColor, flickerColor, lerpFactor);

            yield return null;
        }
    }

    private IEnumerator Disactivate()
    {
        yield return new WaitForSeconds(disappearTime);
        HidePlatform();
        yield return new WaitForSeconds(appearTime);
        ResetPlatform();
        disapperCoroutine = null;
    }

    private void HidePlatform()
    {
        platformRenderer.enabled = false;
        platformCollider.enabled = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).parent = null;
        }
    }

    private void ResetPlatform()
    {
        platformRenderer.enabled = true;
        platformCollider.enabled = true;

        platformRenderer.material.color = originalColor;

        if (flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine);
            flickerCoroutine = null;
        }
    }
}
