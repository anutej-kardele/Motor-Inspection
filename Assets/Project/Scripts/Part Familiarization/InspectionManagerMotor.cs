using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InspectionManagerMotor : MonoBehaviour
{
    public static InspectionManagerMotor instance;

    public Color hoverColor, selectedColor;
    public LocalizeStringEvent partName, partDiscription;
    public Texture selection;
    public GameObject prefab;
    public Transform prefabParent;

    public GameObject raycastObject;
    public GameObject endCanvas;
    public Button menuButton;

    private GameObject instantiate;
    private int count;

    private List<bool> selectedObjects;

    private void Awake(){
        if(instance == null) instance = this;
        else Destroy(this);

        InitializeList();
    }

    void Start(){
        menuButton.onClick.AddListener(ReturnToMenu);
        endCanvas.SetActive(false);
        EventManager.taskFinished += EndModule;
    }

    private void InitializeList(){
        count = Enum.GetValues(typeof(InspectionPartsMotor)).Length;
        selectedObjects = new List<bool>();

        for(int i = 0; i < count; i++){
            instantiate = Instantiate(prefab, prefabParent);
            instantiate.name = ((InspectionPartsMotor)i).ToString();
            instantiate.transform.GetChild(1).GetComponent<LocalizeStringEvent>().SetEntry(instantiate.name);
            selectedObjects.Add(false);
        }
    }

    public void UpdateSelectedObjectInformation(int index){
        partName.SetEntry(((InspectionPartsMotor)index).ToString());
        partDiscription.SetEntry(((InspectionPartsMotor)index).ToString());
        prefabParent.GetChild(index).transform.GetChild(0).GetComponent<RawImage>().texture = selection;
        selectedObjects[index] = true;

        CheckModuleCompletion();
    }

    // check if all the parts are inspected 
    private void CheckModuleCompletion(){
        bool taskComplete = true;
        for(int i = 0; i < selectedObjects.Count; i++){
            if(!selectedObjects[i]){
                taskComplete = false;
                break;
            }
        }
        if(taskComplete) Invoke(nameof(CallEnd), 3.0f);           // end the module if all the checks are completed   i.e. if all the parts are inspected
    }

    private void CallEnd(){ EventManager.taskFinished?.Invoke(); }
    
    private void EndModule() { 
        raycastObject.SetActive(false);
        endCanvas.SetActive(true);
    }

    public void ReturnToMenu(){
        SceneManager.LoadScene(0);                    // 0 will always be set as menu scene 
    }
}