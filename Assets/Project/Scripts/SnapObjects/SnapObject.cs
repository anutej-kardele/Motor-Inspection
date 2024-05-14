using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapObject : MonoBehaviour
{
    [SerializeField] public List<SnapObject> snapTargets;
    [SerializeField] private SnapObject snapParent;
    [SerializeField] private GameObject snapPosition;

    private XRGrabInteractable xRGrabInteractable;
    private Rigidbody rb;

    private void Start(){
        xRGrabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        CheckGrabbleStatus();

        if(snapParent){
            xRGrabInteractable.selectEntered.AddListener(ToggleSnapPositionTrue);
            xRGrabInteractable.selectExited.AddListener(ToggleSnapPositionFalse);
        }
    }

    private void CheckGrabbleStatus(){                                                              // Enable XRGrabInteractable when required 
        xRGrabInteractable.enabled = snapTargets.Count == 0 ? true : false;
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
            transform.parent = snapParent.transform;
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
