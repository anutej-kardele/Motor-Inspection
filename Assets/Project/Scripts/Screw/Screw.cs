using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Screw : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable xRGrabInteractable;

    private void Start(){
        rb = GetComponent<Rigidbody>();
        xRGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "ScrewHolder"){
            Destroy(xRGrabInteractable);
            rb.isKinematic = true;
            transform.position = other.transform.position;
            transform.eulerAngles = other.transform.eulerAngles;
            transform.GetChild(0).GetComponent<Collider>().isTrigger = true;
            Destroy(other.gameObject);
        }
    }
}
