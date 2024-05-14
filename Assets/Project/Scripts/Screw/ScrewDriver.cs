using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Device;

public class ScrewDriver : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerLeft, triggerRight;
    [SerializeField] private float triggerValueL, triggerValueR;
    private Rigidbody rb;
    private XRGrabInteractable xRGrabInteractable;

    public Hand handGrabbed;

    private void Start(){
        rb = GetComponent<Rigidbody>();
        xRGrabInteractable = GetComponent<XRGrabInteractable>();
        xRGrabInteractable.selectExited.AddListener(OnRealease);
        xRGrabInteractable.selectEntered.AddListener(OnSelectEntering);
    }

    private Screw screw;

    private void Update(){
        triggerValueL = triggerLeft.action.ReadValue<float>();
        triggerValueR = triggerRight.action.ReadValue<float>();

        if((triggerValueL > 0)&& screw != null){
            // move screw behind till it crosses the max limit and end later 
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Screw"){

            xRGrabInteractable.trackPosition = false;
            xRGrabInteractable.trackRotation = false;

            transform.position = other.transform.position;
            Vector3 rot = transform.eulerAngles;
            rot.x = 0;
            rot.y = 0;
            transform.eulerAngles = rot;

            screw = other.transform.parent.GetComponent<Screw>();
        }
    }

    private void OnSelectEntering(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.name == "LeftHand") handGrabbed = Hand.LeftHand;
        else if (args.interactorObject.transform.name == "RightHand") handGrabbed = Hand.RightHand;

    }

    private void OnRealease(SelectExitEventArgs arg0){
        xRGrabInteractable.trackPosition = true;
        xRGrabInteractable.trackRotation = true;
        handGrabbed = Hand.None;
    }
}

public enum Hand{
    None, LeftHand, RightHand
}