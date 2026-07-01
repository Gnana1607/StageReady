using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PresentationInput : MonoBehaviour
{
    [SerializeField] private InputActionReference nextSlideAction;
    [SerializeField] private InputActionReference previousSlideAction;
    [SerializeField] private SlideManager slideManager;
    public void OnEnable()
    {
        nextSlideAction.action.performed+=NextSlidePressed;
        previousSlideAction.action.performed += PreviousSlidePressed;
    }
    public void OnDisable()
    {
        nextSlideAction.action.performed-=NextSlidePressed;
        previousSlideAction.action.performed -= PreviousSlidePressed;
    }
    public void NextSlidePressed(InputAction.CallbackContext context)
    {
        slideManager.nextSlide();
    }
    private void PreviousSlidePressed(InputAction.CallbackContext context)
    {
        slideManager.previousSlide();
    }
}
