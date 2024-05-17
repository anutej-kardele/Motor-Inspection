using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AssemblyInstructions : MonoBehaviour
{

    public static AssemblyInstructions instance;

    public List<int> graph = new List<int>();
    public List<bool> isCompleted = new List<bool>();
    public int[] weight;

    public TMP_Text instructionText;

    private void Start(){
        instance = this;

        for(int i = 0; i < graph.Count; i++) isCompleted.Add(false);
        Giveinstructions();
    }

    private void Giveinstructions(){
        weight = new int[graph.Count];

        for(int i = 0; i < graph.Count; i++){

            int search = graph[i];
            int searchIndex = i;
            int size = 0;

            if(!isCompleted[i]){

                //Debug.Log($"Searching : {i} --- grpah if i : {search} --- graph of search { graph[search] }");
                while(search != searchIndex){
                    size++;
                    searchIndex = search;
                    search = graph[search];
                }
            }
            weight[i] = size;
        }

        int largestWeight = weight[0];
        int largestWeightIndex = 0;

        for(int i = 0; i < weight.Length; i++){
            if(weight[i] > largestWeight) {
                largestWeight = weight[i];
                largestWeightIndex = i;
            }
        }

        // Debug largest i in the UI
        instructionText.text = $"Largest weight is {largestWeight} and it belongs to : {largestWeightIndex}";
    }

    public void SetInstructions(int index){
        isCompleted[index] = true;
        // once a instruction is been done give next instruction
        Giveinstructions();
    }
}
