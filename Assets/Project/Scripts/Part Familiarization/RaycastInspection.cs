using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class RaycastInspection : MonoBehaviour
{
    [SerializeField] private InputActionProperty trigger_L, trigger_R;
    public HandIndication handIndication;
    public Transform pointTowards;                           // line renderer pointing towards this direction
    public LayerMask layerMask;
    public Material lr_true, lr_false;  
    private GameObject selectedObject;
    private GameObject hoverObject;

    private RaycastHit hit;                                  // raycast used to find objects that could be hovered or selected 
    private float maxDistance = 5;                           // max distance of the raycast/linerendere
    private Vector3 lineDraw;                                // position at which the line is been drawn from the emmiting object
    private LineRenderer lineRenderer;
    private InspectionPartsMotor selectedPart;


    private void Start(){ lineRenderer = GetComponent<LineRenderer>(); }

    private float trigger_L_Value, trigger_R_Value;

    private void Update(){

        trigger_L_Value = trigger_L.action.ReadValue<float>();
        trigger_R_Value = trigger_R.action.ReadValue<float>();

        if(trigger_L_Value > 0 && handIndication == HandIndication.Left) HoverLogic();
        if(trigger_R_Value > 0 && handIndication == HandIndication.Right) HoverLogic();

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance, layerMask)) {
            lineDraw = hit.point;
            DisableHoverObject(hit.collider.gameObject);
        }
        else {
            lineDraw = pointTowards.transform.position;
            DisableHoverObject();
        }
        AssignLineRenderer();
    }

    private void HoverLogic(){
        if(hoverObject != null){
                if(selectedObject != null) ChangeSelectedObjectHoverData(selectedObject, false);
                selectedObject = hoverObject;
                ChangeSelectedObjectHoverData(selectedObject, true);
                // When you call this function it will update the name and discription of the selected object
                
                selectedPart = selectedObject.GetComponent<StorePartDataMotor>().thisPart;
                InspectionManagerMotor.instance.UpdateSelectedObjectInformation((int) selectedPart);
            }
    }

    private void AssignLineRenderer(){                                         /// to create a line renderer
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, lineDraw);
        lineRenderer.material = hit.collider ? lr_true : lr_false;
    }

    private void DisableHoverObject(){                                         /// disable hover object
        if(hoverObject != null && hoverObject != selectedObject) hoverObject.GetComponent<Outline>().enabled = false;
        hoverObject = null;
    }

    private void DisableHoverObject(GameObject hover){                          /// disable hover object & will enable it for the given GameObject
        if(hoverObject == hover) return;
        if(hoverObject != null && hoverObject != selectedObject) hoverObject.GetComponent<Outline>().enabled = false;
        hoverObject = hover;
        if(hoverObject != selectedObject) hoverObject.GetComponent<Outline>().enabled = true;
    }

    private void ChangeSelectedObjectHoverData(GameObject outlinedObject, bool value){
        outlinedObject.GetComponent<Outline>().enabled = value;
        outlinedObject.GetComponent<Outline>().OutlineColor = value ? InspectionManagerMotor.instance.selectedColor : InspectionManagerMotor.instance.hoverColor;
    }
}