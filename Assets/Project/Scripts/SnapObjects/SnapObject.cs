using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//[RequireComponent(typeof(XRGrabInteractable), typeof(BoxCollider))]
public class SnapObject : MonoBehaviour
{
    [SerializeField] public List<SnapObject> snapTargets;
    [SerializeField] private SnapObject snapParent;
    [SerializeField] private GameObject snapPosition;
    [SerializeField, Header("If you dont want the parent of these object to be snapParent")] private Transform overrideSnapParent;

    private XRGrabInteractable xRGrabInteractable;
    private Rigidbody rb;

    [SerializeField, Header("Skip Snapping Condition Once all SnapTarget are met directly send Snap to parent ")] private bool skipSnapping = false;
    [SerializeField, Header("Cant grab the object if value is TRUE")] private bool restrictGrabble = false;

    [SerializeField] private int instructionIndexToSend = 0;

    private void Start(){
        xRGrabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        CheckGrabbleStatus();
        if(restrictGrabble) xRGrabInteractable.enabled = false;

        if(snapParent){
            xRGrabInteractable.selectEntered.AddListener(ToggleSnapPositionTrue);
            xRGrabInteractable.selectExited.AddListener(ToggleSnapPositionFalse);
        }
    }

    private void CheckGrabbleStatus(){                                                         // Enable XRGrabInteractable when required 
        if(skipSnapping && snapTargets.Count == 0){
            snapParent.ReduceSnapTarget(this.name);

            if(overrideSnapParent != null) transform.parent = overrideSnapParent;
            else if(snapParent != null) transform.parent = snapParent.transform;

            AssemblyInstructions.instance.SetInstructions(instructionIndexToSend);
            
            return;
        } 
        
        if(!restrictGrabble) xRGrabInteractable.enabled = snapTargets.Count == 0 ? true : false;
    }

    private void ToggleSnapPositionTrue(SelectEnterEventArgs arg0) {  snapPosition.SetActive(true); }
    private void ToggleSnapPositionFalse(SelectExitEventArgs arg0) {  snapPosition.SetActive(false); }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject == snapPosition){
            rb.isKinematic = true;
            Destroy(xRGrabInteractable);
            transform.position = snapPosition.transform.position;
            transform.eulerAngles = snapPosition.transform.eulerAngles;
            Destroy(snapPosition);
            Destroy(GetComponent<Rigidbody>());
            snapParent.ReduceSnapTarget(this.name);
            
            if(overrideSnapParent != null) transform.parent = overrideSnapParent; 
            else transform.parent = snapParent.transform;

            Destroy(GetComponent<Collider>());

            AssemblyInstructions.instance.SetInstructions(instructionIndexToSend);
            Destroy(this);
        }
    }

    private void ReduceSnapTarget(string value){
        for(int i =0; i < snapTargets.Count; i++){
            if(snapTargets[i].name == value){
                snapTargets.Remove(snapTargets[i]);
                CheckGrabbleStatus();
                return;
            }
        }
    }
}
