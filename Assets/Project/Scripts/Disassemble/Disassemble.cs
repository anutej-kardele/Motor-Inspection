using System.Collections.Generic;
using UnityEngine.Localization.Components;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Disassemble : MonoBehaviour
{
    private void Start(){
        for(int i = 0; i < graph.Count; i++) isRemoved.Add(false);
        isRemoved[0] = true;
        menuButton.onClick.AddListener(ReturnToMenu);

        for(int i = 0; i < graph.Count; i++) objectToRemove[i].enabled = false;

        endCanvas.SetActive(false);
        NextObjectToRemove();
    }

    // TriggerExit () is called when a DisassembleObject is going outside of the region 

    private void OnTriggerExit(Collider other){
        if(other.gameObject.tag.Equals("DisassembleObject")){

            Debug.Log($"Object name is {other.gameObject.name}");
            if(objectToRemoveIndex == other.GetComponent<DisassembleIndex>().index) {
                isRemoved[objectToRemoveIndex] = true;
                NextObjectToRemove();
            }
        }
    }

    [SerializeField] private List<int> graph = new List<int>();
    [SerializeField] private List<XRGrabInteractable> objectToRemove = new List<XRGrabInteractable>();
    [SerializeField] private List<bool> isRemoved = new List<bool>();
    [SerializeField] private int[] weightsArray;
    [SerializeField] private int objectToRemoveIndex;
    [SerializeField] private LocalizeStringEvent instructionLocalizeString;
    [SerializeField] private GameObject endCanvas;
    [SerializeField] private Button menuButton;

    private void NextObjectToRemove(){
        weightsArray = new int[graph.Count];

        for(int i = 0; i < graph.Count; i++){

            int search = graph[i];
            int searchIndex = i;
            int weight = 0;

            if(!isRemoved[i]){

                //Debug.Log($"Searching : {i} --- grpah if i : {search} --- graph of search { graph[search] }");
                while(search != searchIndex){
                    weight++;
                    searchIndex = search;
                    search = graph[search];
                }
            }
            if(isRemoved[i]) weight = -1;
            weightsArray[i] = weight;
        }

        int smallestWeight = weightsArray[0];
        int smallestWeightIndex = 0;

        for(int i = 0; i < weightsArray.Length; i++){
            if(smallestWeight == -1){
                smallestWeight = weightsArray[i];
                smallestWeightIndex = i;
            }
            if(weightsArray[i] < smallestWeight && smallestWeight > 0) {
                smallestWeight = weightsArray[i];
                smallestWeightIndex = i;
            }
        }

        // 
        if(smallestWeight != -1){
            objectToRemove[smallestWeightIndex].enabled = true;
        }

        // check if the largest weight is 0 to end the module 
        if(smallestWeight == -1) {
            Debug.Log("Disassembly task completed");
            endCanvas.SetActive(true);
        }

        objectToRemoveIndex = smallestWeightIndex;

        Debug.Log($"Smallest Weight index : {smallestWeightIndex}");
        // Debug largest i in the UI
        instructionLocalizeString.SetEntry(smallestWeightIndex.ToString());
    }

    private void ReturnToMenu(){    SceneManager.LoadScene(0); }
}
