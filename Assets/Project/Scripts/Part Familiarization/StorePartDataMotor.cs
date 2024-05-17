using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(Outline))]
public class StorePartDataMotor : MonoBehaviour
{
    public InspectionPartsMotor thisPart;
    public Outline outlineObject;

    void Awake(){
        outlineObject = GetComponent<Outline>();

        // outlineObject.OutlineColor = InspectionManager.instance.hoverColor;
        outlineObject.OutlineColor = Color.green;
        outlineObject.OutlineWidth = 10.0f;
        outlineObject.enabled = false;
    }
}