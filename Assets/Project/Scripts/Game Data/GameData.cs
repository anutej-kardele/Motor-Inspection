using System;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;
    public Operation operation;
    public string userName = "";
    public static bool repeatedInstance = false;
    public DateTime dateTime;
    public Language language;

    void Awake(){
        if(instance == null) {
            instance = this;
        }
        else {
            repeatedInstance = true;
            Destroy(this.gameObject);
        };

        DontDestroyOnLoad(this);
        dateTime = DateTime.Now;
    }
}
