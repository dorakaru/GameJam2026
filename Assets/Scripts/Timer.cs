using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _timeLimit;

    [SerializeField] private float _timer;
    [SerializeField] private GameObject timerImage;
    [SerializeField] private HandController handController;

    [SerializeField] private GameObject finishImage;

    [SerializeField] private float resultWaitTime = 2f;

    [SerializeField] private string resultSceneName;

    private PeopleMood _peopleMood;
    private RectTransform _timerTransform;

    private bool isFinished = false;

    [SerializeField] AudioClip finshSound;
    AudioSource audioSource;

    void Start()
    {
        _timer = _timeLimit;

        _timerTransform = timerImage.GetComponent<RectTransform>();

        _peopleMood = GetComponent<PeopleMood>();

        finishImage.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isFinished)
        {
            return;
        }

        if (_timer > 0)
        {
            _timer -= Time.deltaTime;

            _timerTransform.localEulerAngles -=
                new Vector3(0, 0, 360 / _timeLimit * Time.deltaTime);
        }
        else
        {
            _timer = 0;

            isFinished = true;

            handController.enabled = false;
            _peopleMood.enabled = false;

            StartCoroutine(FinishSequence());
        }
    }

    private IEnumerator FinishSequence()
    {
        finishImage.SetActive(true);
        audioSource.PlayOneShot(finshSound);

        yield return new WaitForSeconds(resultWaitTime);

        SceneManager.LoadScene(resultSceneName);
    }
}