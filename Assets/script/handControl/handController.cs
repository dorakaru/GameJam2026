using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public bool SuccessfulHit { get; private set; }

    private enum HandState
    {
        Idle,       
        LeftDown,   
        RightDown   
    }

    private HandState currentState = HandState.Idle;

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
    }


    private void PressLeft()
    {

        if (currentState == HandState.Idle)
        {
            animator.SetTrigger("leftHit");
            currentState = HandState.LeftDown;
            SuccessfulHit = true;
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
        }
    }



    private void PressRight()
    {
        if (currentState == HandState.Idle)
        {
            animator.SetTrigger("rightHit");
            currentState = HandState.RightDown;
            SuccessfulHit = true;
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
        }
    }
}