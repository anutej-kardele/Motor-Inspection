using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScrewRemoval : MonoBehaviour
{
    public static ScrewRemoval instance;

    private void Awake(){
        instance = this;
    }

    [SerializeField] private Disassemble disassemble;
    [SerializeField] private int screwsToRemove = 12;
    private int screwsRemoved = 0;

    public void RemoveScrew(){

        screwsRemoved += 1;

        if(screwsToRemove == screwsRemoved){
            disassemble.NextObjectToRemove();
            Destroy(this);
        }
    }
}
