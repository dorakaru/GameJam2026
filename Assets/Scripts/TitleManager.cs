using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private string StartButtonGoToScene;
    [SerializeField] private GameObject gripGuide;

    [SerializeField] private float guideReturnTime = 10f;
    private bool guideClosed = false;
    private float guideTimer = 0f;  

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
            }

            return;
        }
        guideTimer += Time.deltaTime;
        bool triggerPressed = Gamepad.current.rightTrigger.wasPressedThisFrame || Gamepad.current.leftTrigger.wasPressedThisFrame;

        if(triggerPressed)
        {
            guideTimer = 0f;
        }

        if (Gamepad.current.rightTrigger.wasPressedThisFrame)
        {
            StartGame();
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