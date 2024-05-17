using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LookAtAxies : MonoBehaviour
{
    public Transform target;
    [SerializeField] private bool status = true;
    [SerializeField] private XRGrabInteractable xRGrabInteractable;

    void Start(){
        xRGrabInteractable = GetComponent<XRGrabInteractable>();

        xRGrabInteractable.selectEntered.AddListener(ToggleStatus);
        xRGrabInteractable.selectExited.AddListener(ToggleStatus);
    }

    void Update()
    {
        if(status){
            var lookPos = target.position - transform.position;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
        }
    }

    private void ToggleStatus(SelectEnterEventArgs args){
        status = false;
    }

    private void ToggleStatus(SelectExitEventArgs args){
        status = true;
    }
}
