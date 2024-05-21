using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RemovingScrew : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable xRGrabInteractable;

    private void Start(){
        xRGrabInteractable = GetComponent<XRGrabInteractable>();

        xRGrabInteractable.selectEntered.AddListener(RemoveScrew);
        xRGrabInteractable.selectExited.AddListener(ReleaseKinematicDisable);
    }

    private void RemoveScrew(SelectEnterEventArgs arg0){
        ScrewRemoval.instance.RemoveScrew();
        xRGrabInteractable.selectEntered.RemoveListener(RemoveScrew);
        Debug.Log("Screw removing ++");
    }

    private void ReleaseKinematicDisable(SelectExitEventArgs arg0){
        GetComponent<Rigidbody>().isKinematic = false;
        Destroy(this);
    }
}
