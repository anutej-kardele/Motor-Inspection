using UnityEngine.InputSystem;
using UnityEngine;

public class AnimateHandInput : MonoBehaviour
{
    [SerializeField]
    private InputActionProperty pinchAnimationAction, gripAnimationAction;
    private Animator handAnimator;

    private float triggerValue, gripValue;

    private void Start(){
        handAnimator = GetComponent<Animator>();
    }

    private void Update(){
        triggerValue = pinchAnimationAction.action.ReadValue<float>();
        gripValue = gripAnimationAction.action.ReadValue<float>();
        
        handAnimator.SetFloat("Pinch", triggerValue);
        handAnimator.SetFloat("Grab", gripValue);
    }
}
