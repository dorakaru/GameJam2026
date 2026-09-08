using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject leftShougekiha;
    [SerializeField] protected GameObject rightShougekiha;

    public bool SuccessfulHit { get; private set; }
    bool leftEffectOn;
    bool rightEffectOn;
    float leftTimer;
    float rightTimer;

    private enum HandState
    {
        Idle,       
        LeftDown,   
        RightDown   
    }

    private HandState currentState = HandState.Idle;

    private void Start()
    {
        leftTimer = 0;
        rightTimer = 0;
        HideEffect();
    }

    private void OnDisable()
    {
        leftTimer = 0;
        rightTimer = 0;
        SuccessfulHit = false;
    }

    void Update()
    {
        if (Gamepad.current == null)
        {
            return;
        }
        SuccessfulHit = false;
        if (Gamepad.current.rightTrigger.wasPressedThisFrame)
        {
            PressLeft();
        }
        if (Gamepad.current.leftTrigger.wasPressedThisFrame)
        {
            PressRight();
        }

        DisplayLeftHandEffect();
        DisplayRightHandEffect();
    }


    private void PressLeft()
    {

        if (currentState == HandState.Idle)
        {
            animator.SetTrigger("leftHit");
            currentState = HandState.LeftDown;
            SuccessfulHit = true;
            leftEffectOn = true;
            leftTimer = 0;
        }

        else if (currentState == HandState.LeftDown)
        {
            animator.SetTrigger("leftShake");
            SuccessfulHit = false;
        }


        else if (currentState == HandState.RightDown)
        {

            animator.SetTrigger("leftHit");
            currentState = HandState.LeftDown;
            SuccessfulHit = true;
            leftEffectOn = true;
            leftTimer = 0;
        }
    }



    private void PressRight()
    {
        if (currentState == HandState.Idle)
        {
            animator.SetTrigger("rightHit");
            currentState = HandState.RightDown;
            SuccessfulHit = true;
            rightEffectOn = true;
            rightTimer = 0;
        }

        else if (currentState == HandState.RightDown)
        {
            animator.SetTrigger("rightShake");
            SuccessfulHit = false;
        }

        else if (currentState == HandState.LeftDown)
        {
            animator.SetTrigger("rightHit");
            currentState = HandState.RightDown;
            SuccessfulHit = true;
            rightEffectOn = true;
            rightTimer = 0;
        }
    }

    private void DisplayLeftHandEffect()
    {
        if (leftEffectOn)
        { 
            leftTimer += Time.deltaTime;

            if (leftTimer >= 0.1f)
                leftShougekiha.SetActive(true);
            else
                leftShougekiha.SetActive(false);

            if (leftTimer >= 0.2f)
            {
                leftEffectOn = false;
                leftShougekiha.SetActive(false);
                leftTimer = 0;
            }
        }
    }

    private void DisplayRightHandEffect()
    {
        if (rightEffectOn)
        {
            rightTimer += Time.deltaTime;

            if (rightTimer >= 0.1f)
                rightShougekiha.SetActive(true);
            else
                rightShougekiha.SetActive(false);

            if (rightTimer >= 0.2f)
            {
                rightEffectOn = false;
                rightShougekiha.SetActive(false);
                rightTimer = 0;
            }
        }
    }

    public void HideEffect()
    {
        leftShougekiha.SetActive(false);
        rightShougekiha.SetActive(false);
    }
}