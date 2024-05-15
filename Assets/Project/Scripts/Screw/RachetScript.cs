using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Device;

public class RachetScript : MonoBehaviour
{
    [SerializeField] private InputActionProperty triggerLeft, triggerRight;
    [SerializeField] private float triggerValueL, triggerValueR;
    private Rigidbody rb;
    private XRGrabInteractable xRGrabInteractable;

    [SerializeField] private Screw screw;
    [SerializeField] public Hand handGrabbed;
    [SerializeField] private float speed = 0.001f;

    [SerializeField] private bool isGrabbed = false;

    private void Start(){
        rb = GetComponent<Rigidbody>();
        xRGrabInteractable = GetComponent<XRGrabInteractable>();
        xRGrabInteractable.selectExited.AddListener(OnRealease);
        xRGrabInteractable.selectEntered.AddListener(OnSelectEntering);
    }

    private void Update(){
        triggerValueL = triggerLeft.action.ReadValue<float>();
        triggerValueR = triggerRight.action.ReadValue<float>();

        // if((triggerValueL > 0)&& screw != null){
        //     // move screw behind till it crosses the max limit and end later 
        // }

        if(screw != null){

            if(triggerValueL > 0 && handGrabbed == Hand.LeftHand){
                // logic 
                OnScrewDown(triggerValueL);
            }
            else if(triggerValueR > 0 && handGrabbed == Hand.RightHand){
                // logic 
                OnScrewDown(triggerValueR);
            }
        }
    }

    private void OnScrewDown(float triggerValue){
        Vector3 rachet = transform.position;
        Vector3 thisScrew = screw.transform.position;

        float difference = speed * triggerValue * Time.deltaTime;
        rachet.z -= difference;
        thisScrew.z -= difference;

        if(screw.screwTightPosition.z > thisScrew.z){
            screw.transform.position = thisScrew;
            transform.position = rachet;
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Screw" && isGrabbed){

            xRGrabInteractable.trackPosition = false;
            xRGrabInteractable.trackRotation = false;

            transform.position = other.transform.parent.position;
            // Vector3 rot = transform.localEulerAngles;
            // rot.x = other.transform.parent.localEulerAngles.x;
            // rot.y = other.transform.parent.localEulerAngles.y;

            // Vector3 rot = other.transform.parent.transform.eulerAngles;
            // Debug.Log($"{other.transform.parent.name} --- {other.transform.parent.transform.eulerAngles}");
            // //rot.y = transform.eulerAngles.y;
            // transform.GetChild(0).eulerAngles = rot;

            // Quaternion quaternionThis = transform.rotation;
            // transform.rotation = other.transform.parent.transform.rotation;
            // Quaternion quaternionThis2 = transform.rotation;
            // quaternionThis2.y = quaternionThis.y;
            // transform.rotation = quaternionThis2;

            transform.rotation = other.transform.parent.transform.rotation;

            // Quaternion quaternionOther = other.transform.parent.transform.rotation;
            // Quaternion quaternionThis = transform.rotation;
            // quaternionOther.y = quaternionThis.y;

            //transform.rotation = quaternionOther;

            // Quaternion quaternionOther = other.transform.parent.transform.rotation;
            // Vector3 temp = quaternionOther.eulerAngles;
            // temp.y = transform.eulerAngles.y;
            // quaternionOther = Quaternion.Euler(temp.x, temp.z, temp.z);
            
            // transform.rotation = quaternionOther;

            screw = other.transform.parent.GetComponent<Screw>();
        }
    }

    private void OnSelectEntering(SelectEnterEventArgs args)
    {
        isGrabbed = true;
        if (args.interactorObject.transform.name == "LeftHand") handGrabbed = Hand.LeftHand;
        else if (args.interactorObject.transform.name == "RightHand") handGrabbed = Hand.RightHand;

    }

    private void OnRealease(SelectExitEventArgs arg0){
        isGrabbed = false;
        xRGrabInteractable.trackPosition = true;
        xRGrabInteractable.trackRotation = true;
        handGrabbed = Hand.None;
    }

}

public enum Hand{
    None, LeftHand, RightHand
}