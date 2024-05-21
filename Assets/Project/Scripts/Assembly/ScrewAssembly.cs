using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class ScrewAssembly : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable xRGrabInteractable;
    [SerializeField] private string objectToCollide;

    private void Start(){
        xRGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.name.Contains(objectToCollide)){
            Destroy(xRGrabInteractable);
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<Collider>());

            transform.parent = other.transform.parent;
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;

            ScrewAssemble.instance.ScrewAttach();

            Destroy(other.gameObject);
            Destroy(this);
        }
    }

}
