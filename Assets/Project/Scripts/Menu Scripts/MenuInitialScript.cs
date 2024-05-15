using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuInitialScript : MonoBehaviour
{
    [Header("UI elements"), SerializeField] private TMP_InputField userName;
    [SerializeField] private GameObject errorMessage;
    [SerializeField] private Button startButton, enterName;
    [SerializeField] private TMP_Text nameDisplay;

    [Header("UI Panels"), SerializeField]
    private GameObject startPanel;
    [SerializeField]private GameObject nameEnterPanel;

    [Header("Operation selection manager"), SerializeField]
    private GameObject menuManagerStack;

    public void Start() {
        startPanel.SetActive(true);

        startButton.onClick.AddListener(StartButtonFunction);
        enterName.onClick.AddListener(EnterNameFunction);
    }

    public void StartButtonFunction(){
        startPanel.SetActive(false);
        nameEnterPanel.SetActive(true);
    }

    public void EnterNameFunction(){

        if(userName.text!= ""){
            GameData.instance.userName = userName.text;
            nameEnterPanel.SetActive(false);
            errorMessage.SetActive(false);
            menuManagerStack.SetActive(true);
            nameDisplay.text = userName.text;
        }
        else{
            errorMessage.SetActive(true);
        }
    }
}