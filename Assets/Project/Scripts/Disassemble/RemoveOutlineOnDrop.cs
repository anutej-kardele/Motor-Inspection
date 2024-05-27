using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class RemoveOutlineOnDrop : MonoBehaviour
{
    private XRGrabInteractable xRGrabInteractable;

    private void Start(){
        xRGrabInteractable = GetComponent<XRGrabInteractable>();

        xRGrabInteractable.selectExited.AddListener(RemoveOutline);
    }

    private void RemoveOutline(SelectExitEventArgs arg0) {  Destroy(GetComponent<Outline>()); }
}
