using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("êßå¿éûä‘")]
    [SerializeField] private float _timeLimit;

    [Header("")]
    [SerializeField] private float _timer;
    [SerializeField] GameObject timerImage;
    [SerializeField] HandController handController;
    RectTransform _timerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _timer = _timeLimit;
        _timerTransform = timerImage.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            _timerTransform.localEulerAngles -= new Vector3 (0, 0, 360 / _timeLimit * Time.deltaTime);
        }
        else
        {
            _timer = 0;
            handController.enabled = false;
        }
        
    }
}
