using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private GameObject resultImage;

    [SerializeField] private GameObject count3;
    [SerializeField] private GameObject count2;
    [SerializeField] private GameObject count1;
    [SerializeField] private GameObject startImage;

    // 在 Unity Inspector 中输入场景名
    [SerializeField] private string gameSceneName;
    [SerializeField] private string titleSceneName;

    [SerializeField] private float countInterval = 1f;

    [SerializeField] private float scaleDuration = 0.5f;

    [SerializeField] private float countStartScale = 0.3f;
    [SerializeField] private float countEndScale = 1f;

    [SerializeField] private float startImageStartScale = 0.3f;
    [SerializeField] private float startImageEndScale = 1.5f;

    private bool countdownStarted = false;

    void Start()
    {
        resultImage.SetActive(true);

        count3.SetActive(false);
        count2.SetActive(false);
        count1.SetActive(false);
        startImage.SetActive(false);
    }

    void Update()
    {
        if (Gamepad.current == null)
        {
            return;
        }

        if (countdownStarted)
        {
            return;
        }

        if (Gamepad.current.rightTrigger.wasPressedThisFrame)
        {
            countdownStarted = true;

            resultImage.SetActive(false);

            StartCoroutine(Countdown());
            return;
        }

        if (Gamepad.current.leftTrigger.wasPressedThisFrame)
        {
            GoToTitle();
        }
    }

    private IEnumerator Countdown()
    {
        yield return StartCoroutine(
            ShowImage(count3, countStartScale, countEndScale)
        );

        yield return StartCoroutine(
            ShowImage(count2, countStartScale, countEndScale)
        );

        yield return StartCoroutine(
            ShowImage(count1, countStartScale, countEndScale)
        );

        yield return StartCoroutine(
            ShowImage(startImage, startImageStartScale, startImageEndScale)
        );

        SceneManager.LoadScene(gameSceneName);
    }

    private IEnumerator ShowImage(
        GameObject image,
        float startScale,
        float endScale)
    {
        image.SetActive(true);

        RectTransform rect = image.GetComponent<RectTransform>();

        rect.localScale = Vector3.one * startScale;

        float timer = 0f;

        while (timer < scaleDuration)
        {
            timer += Time.deltaTime;

            float t = timer / scaleDuration;

            float scale = Mathf.Lerp(
                startScale,
                endScale,
                t
            );

            rect.localScale = Vector3.one * scale;

            yield return null;
        }

        rect.localScale = Vector3.one * endScale;

        float waitTime = countInterval - scaleDuration;

        if (waitTime > 0f)
        {
            yield return new WaitForSeconds(waitTime);
        }

        image.SetActive(false);
    }

    private void GoToTitle()
    {
        SceneManager.LoadScene(titleSceneName);
    }
}