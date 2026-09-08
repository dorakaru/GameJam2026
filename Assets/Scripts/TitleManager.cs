using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private string StartButtonGoToScene;
    [SerializeField] private GameObject gripGuide;

    [SerializeField] private float guideReturnTime = 10f;
    private bool guideClosed = false;
    private float guideTimer = 0f;

    [SerializeField] AudioClip firstSound;
    [SerializeField] AudioClip startGameSound;

    AudioSource audioSource;
    bool isMusic;
    bool isStart;

    float timer;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timer = 0;
    }

    void Update()
    {
        if (Gamepad.current == null)
        {
            return;
        }

        if (!guideClosed)
        {
            if (Gamepad.current.rightTrigger.wasPressedThisFrame)
            {
                gripGuide.SetActive(false);
                guideClosed = true;
                guideTimer = 0f;
                audioSource.PlayOneShot(firstSound);
            }

            return;
        }
        guideTimer += Time.deltaTime;
        bool triggerPressed = Gamepad.current.rightTrigger.wasPressedThisFrame || Gamepad.current.leftTrigger.wasPressedThisFrame;

        if(triggerPressed)
        {
            guideTimer = 0f;
        }

        if (!isStart)
        {
            if (Gamepad.current.rightTrigger.wasPressedThisFrame)
            {
                isStart = true;
                
            }

            if (Gamepad.current.leftTrigger.wasPressedThisFrame)
            {
                QuitGame();
            }

            if (guideTimer >= guideReturnTime)
            {
                gripGuide.SetActive(true);
                guideClosed = false;
                guideTimer = 0f;
            }
        }
        else
        {
            if (!isMusic)
            {
                audioSource.PlayOneShot(startGameSound);
                isMusic = true;
            }

            timer += Time.deltaTime;

            if (timer >= 1.0f)
                StartGame();
        }
        


    }

    private void StartGame()
    {
        SceneManager.LoadScene(StartButtonGoToScene);
    }

    private void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}