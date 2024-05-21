using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewAssemble : MonoBehaviour
{
    [SerializeField] private int screwToAttach = 12;
    private int screwAttached;
    [SerializeField] private GameObject endCanvas;

    public static ScrewAssemble instance;

    private void Start(){
        instance = this;
        endCanvas.SetActive(false);
    }

    public void ScrewAttach(){
        screwAttached++;
        if(screwAttached == screwToAttach){
            endCanvas.SetActive(true);
        }
    }
}
