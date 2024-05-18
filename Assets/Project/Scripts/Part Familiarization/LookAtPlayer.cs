using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LookAtPlayer : MonoBehaviour
{
    private Transform target;
    private bool status = true;
    private XRGrabInteractable xRGrabInteractable;

    void Start(){
        xRGrabInteractable = GetComponent<XRGrabInteractable>();
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;

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
