using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("§ŒÀŽžŠÔ")]
    [SerializeField] float timeLimit;

    [SerializeField] float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
        
    }
}
