using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class Screw : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable xRGrabInteractable;

    public float screwInsert = -0.02414f;

    private void Start(){
        rb = GetComponent<Rigidbody>();
        xRGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "ScrewHolder"){
            Destroy(xRGrabInteractable);
            rb.isKinematic = true;
            transform.position = other.transform.parent.position;
            transform.eulerAngles = other.transform.parent.eulerAngles;
            transform.GetChild(0).GetComponent<Collider>().isTrigger = true;
            Destroy(other.gameObject);
            
            screwSnapPosition = transform.position;
            Vector3 temp = screwSnapPosition;
            temp.z += screwInsert;
            screwTightPosition = temp;
        }
    }

    private Vector3 screwSnapPosition;
    [HideInInspector] public Vector3 screwTightPosition;
}
