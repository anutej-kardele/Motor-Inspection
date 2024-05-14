using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class ScrewDriver : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable xRGrabInteractable;

    private void Start(){
        rb = GetComponent<Rigidbody>();
        xRGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Screw"){
            Debug.Log("Screw --------------------------------");

            transform.position = other.transform.position;
            Vector3 rot = transform.eulerAngles;
            rot.x = 0;
            rot.y = 0;
            transform.eulerAngles = rot;

            rb.constraints |= RigidbodyConstraints.FreezePosition;
            rb.constraints |= RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        }
    }
}
