using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [System.Serializable]
    public class ResultData
    {
        public int minMoney;
        public GameObject dialogue;
        public GameObject portrait;
        public GameObject moneyBag;
    }

    [SerializeField] private GameObject resultImage;

    [SerializeField] private TextMeshProUGUI moneyText;

    [SerializeField] private ResultData[] results = new ResultData[5];

    [SerializeField] private GameObject count3;
    [SerializeField] private GameObject count2;
    [SerializeField] private GameObject count1;
    [SerializeField] private GameObject startImage;

    [SerializeField] private string gameSceneName;
    [SerializeField] private string titleSceneName;

    [SerializeField] private float countInterval = 1f;
    [SerializeField] private float scaleDuration = 0.5f;

    [SerializeField] private float countStartScale = 0.3f;
    [SerializeField] private float countEndScale = 1f;

    [SerializeField] private float startImageStartScale = 0.3f;
    [SerializeField] private float startImageEndScale = 1.5f;

    private bool countdownStarted = false;

    private ResultData currentResult;

    void Start()
    {
        resultImage.SetActive(true);

        count3.SetActive(false);
        count2.SetActive(false);
        count1.SetActive(false);
        startImage.SetActive(false);

        moneyText.text = Money.playerMoney.ToString();

        HideAllResults();
        ShowResult();
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
            StartCoroutine(Countdown());
            return;
        }

        if (Gamepad.current.leftTrigger.wasPressedThisFrame)
        {
            GoToTitle();
        }
    }

    private void ShowResult()
    {
        int playerMoney = Money.playerMoney;

        currentResult = null;

        for (int i = 0; i < results.Length; i++)
        {
            if (playerMoney >= results[i].minMoney)
            {
                if (currentResult == null ||
                    results[i].minMoney > currentResult.minMoney)
                {
                    currentResult = results[i];
                }
            }
        }

        if (currentResult == null)
        {
            return;
        }

        if (currentResult.dialogue != null)
        {
            currentResult.dialogue.SetActive(true);
        }

        if (currentResult.portrait != null)
        {
            currentResult.portrait.SetActive(true);
        }

        if (currentResult.moneyBag != null)
        {
            currentResult.moneyBag.SetActive(true);
        }
    }

    private void HideAllResults()
    {
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i].dialogue != null)
            {
                results[i].dialogue.SetActive(false);
            }

            if (results[i].portrait != null)
            {
                results[i].portrait.SetActive(false);
            }

            if (results[i].moneyBag != null)
            {
                results[i].moneyBag.SetActive(false);
            }
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

        resultImage.SetActive(false);
        moneyText.gameObject.SetActive(false);

        HideAllResults();

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