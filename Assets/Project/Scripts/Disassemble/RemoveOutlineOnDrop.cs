using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RemoveOutlineOnDrop : MonoBehaviour
{
    private XRGrabInteractable xRGrabInteractable;

    private void Start(){
        xRGrabInteractable = GetComponent<XRGrabInteractable>();

        xRGrabInteractable.selectEntered.AddListener(RemoveOutline);
    }

    private void RemoveOutline(SelectEnterEventArgs arg0) {  Destroy(GetComponent<Outline>()); }
}
