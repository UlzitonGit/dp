using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Spiketrap : MonoBehaviour
{
    [SerializeField] private PlayerMovementAdvanced playerMovement;//
    [SerializeField] protected PlayerCam playerCam;//
    public GameObject RestartMenu;
    public LayerMask Player;

    public Image[] images; // Сюда добавь изображения, чью альфу надо менять
    public float fadeDuration = 2f; // Время на полное проявление
    private Coroutine fadeCoroutine;
    private bool isFullyVisible = false;

    void Start()
    {
        SetAlpha(0f);
    }

    void Update()
    {
        if (!isFullyVisible && Input.GetMouseButtonDown(0) && fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            SetAlpha(1f);
            isFullyVisible = true;
        }
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float alpha = elapsed / fadeDuration;
            SetAlpha(alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Time.timeScale = 0;
        SetAlpha(1f);
        isFullyVisible = true;
    }

    void SetAlpha(float alpha)
    {
        foreach (Image img in images)
        {
            Color c = img.color;
            c.a = alpha;
            img.color = c;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & Player.value) != 0)
        {
            playerMovement.enabled = false;//
            playerCam.enabled = false;//
            RestartMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            fadeCoroutine = StartCoroutine(FadeIn());
        }


    }
}
