using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AssemblyInstructions : MonoBehaviour
{

    public static AssemblyInstructions instance;
    [SerializeField] private List<int> graph = new List<int>();
    private List<bool> isCompleted = new List<bool>();
    private int[] weightsArray;
    [SerializeField] private LocalizeStringEvent instructionLocalizeString;
    [SerializeField] private Button menuButton;
    [SerializeField] private GameObject screwHolder;

    private void Start(){
        instance = this;
        menuButton.onClick.AddListener(ReturnToMenu);
        for(int i = 0; i < graph.Count; i++) isCompleted.Add(false);
        Giveinstructions();
    }

    private void Giveinstructions(){
        weightsArray = new int[graph.Count];

        for(int i = 0; i < graph.Count; i++){

            int search = graph[i];
            int searchIndex = i;
            int weight = 0;

            if(!isCompleted[i]){

                //Debug.Log($"Searching : {i} --- grpah if i : {search} --- graph of search { graph[search] }");
                while(search != searchIndex){
                    weight++;
                    searchIndex = search;
                    search = graph[search];
                }
            }
            weightsArray[i] = weight;
        }

        int largestWeight = weightsArray[0];
        int largestWeightIndex = 0;

        for(int i = 0; i < weightsArray.Length; i++){
            if(weightsArray[i] > largestWeight) {
                largestWeight = weightsArray[i];
                largestWeightIndex = i;
            }
        }

        // check if the largest weight is 0 to end the module 
        // if(largestWeight == 0) endCanvas.SetActive(true);
        if(largestWeight == 0) screwHolder.SetActive(true);

        // Debug largest i in the UI
        instructionLocalizeString.SetEntry(largestWeightIndex.ToString());
    }

    public void SetInstructions(int index){
        isCompleted[index] = true;
        // once a instruction is been done give next instruction
        Giveinstructions();
    }

    private void ReturnToMenu(){    SceneManager.LoadScene(0); }
}
