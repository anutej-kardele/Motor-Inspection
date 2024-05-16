using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuScript : MonoBehaviour
{
    void Update(){
        // delete later just for testing purposes
        navData.navigationStates = navData.currentStateStack.ToArray();
    }

    // Stores data regarding selected data & stackes previously selected panel 
    public static MenuScript instance;
    public NavigationData navData;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public void Start()
    {
        if(!GameData.repeatedInstance){
            navData.ResetValues();
        }

        MenuUIElement.instance.backgroundImage.sprite = MenuUIElement.instance.backgroundSprite;
        MenuUIElement.instance.backgroundImage.color = new Color(43, 99, 159, 255);

        MenuUIElement.instance.nameDisplayText.SetActive(true);
        MenuUIElement.instance.nameDisplayIcon.SetActive(true);

        // Assign Buttons 
        MenuButton.instance.nextButton.interactable = false;
        MenuButton.instance.nextButton.onClick.AddListener(NextButton);
        MenuButton.instance.backButton.onClick.AddListener(BackButton);
        MenuButton.instance.backButton.gameObject.SetActive(false);
        MenuButton.instance.operation_BackButton.onClick.AddListener(BackButton);

        // machine selection button 
        for (int i = 1; i < Enum.GetValues(typeof(Machine)).Length; i++)
        {
            int index = i;
            MenuButton.instance.machineButton[index - 1].onClick.AddListener(() => AssignMachineButton((Machine)index));
        }

        // Process state button 
        for (int i = 1; i < Enum.GetValues(typeof(ProcessStage)).Length; i++)
        {
            int index = i;
            MenuButton.instance.processStageButton[index - 1].onClick.AddListener(() => AssignProcessStageButton((ProcessStage)index));
        }

        // Operation button 
        int operationIndex = 0;
        for (int i = 1; i < 3; i++)
        {
            int index = i;
            MenuButton.instance.operationButton[operationIndex].onClick.AddListener(() => AssignOperationButton((Operation)index));
            operationIndex++;
        }

        MenuUIElement.instance.menuBar.SetActive(true);
        navData.currentStateStack = new Stack<NavigationState>();

        if(navData.retriveData){

            Debug.Log("returning value --- retriveData");
            
            // reassigning the stack values using the help of panel stack created 
            for(int i = navData.navigationStates.Length - 1; i >= 0; i--){
                navData.currentStateStack.Push(navData.navigationStates[i]);
            }

            if(navData.currentState == NavigationState.OperationPanel) EnableOperation(navData.operationState);
            MenuUIElement.instance.panels[(int)navData.currentState - 1].SetActive(true);

            ModuleBarIndicator();    // to update highlighted module bar to indicate current panel/state
            AssignMenuBarData.instance.AssignMenuBarText();  // assigning the modular bar text 
            navData.retriveData = false;
            return;
        }

        navData.ResetValues();
        
        Debug.Log("data not retrived ");

        navData.currentState = NavigationState.MachinePanel;
        MenuUIElement.instance.panels[0].SetActive(true);

    }

    private void AssignMachineButton(Machine machine)
    {
        navData.machineState = (Machine)machine;
        MenuButton.instance.nextButton.interactable = true;
    }

    private void AssignProcessStageButton(ProcessStage processStage)
    {
        navData.processStage = (ProcessStage)processStage;
        MenuButton.instance.nextButton.interactable = true;
    }

    private void AssignOperationButton(Operation operation)
    {
        navData.operationState = (Operation)operation;
        MenuButton.instance.nextButton.interactable = true;
    }

    public void BackButton()
    {
        if (navData.currentStateStack.Count == 0)
        {
            Debug.Log("Back button pressed but stack is empty");
            return;
        }

        // write back logic 
        int currentIndex = (int)navData.currentState;
        int previousIndex = (int)navData.currentStateStack.Pop();
        MenuUIElement.instance.panels[currentIndex - 1].SetActive(false);
        MenuUIElement.instance.panels[previousIndex - 1].SetActive(true);

        navData.currentState = (NavigationState)previousIndex;

        // checking for last available screen then if true disabling back button
        MenuButton.instance.backButton.gameObject.SetActive( navData.currentState == NavigationState.MachinePanel ? false : true);

        ModuleBarIndicator();    // to update highlighted module bar to indicate current panel/state
        ModuleBarText((int)(navData.currentState - 1));  // to remove previously selected data in case of back
    }

    private void ModuleBarText(int index)
    {
        if (index == 0) navData.RemoveValue(true, true, true, true);
        if (index == 1) navData.RemoveValue(false, true, true, true);
        if (index == 2) navData.RemoveValue(false, false, true, true);
        else navData.RemoveValue(false, false, false, true);
    }

    private void NextButton()
    {
        if(navData.processStage == ProcessStage.PartFamilarization){
            navData.operationState = Operation.PartFamilarization;
            SetNextPanel(NavigationState.OperationPanel);
        }
        else SetNextPanel();   // settings current value in terms of panels 

        MenuButton.instance.nextButton.interactable = false;
        MenuButton.instance.backButton.gameObject.SetActive(navData.currentState == NavigationState.OperationPanel ? false : true);

        ModuleBarIndicator();   // to update highlighted module bar to indicate current panel/state
    }


    private void SetNextPanel()
    {
        int currentPanelIndex = (int)navData.currentState - 1;  // because Enum has None value at index 0 but Array doesn't have enum value 

        MenuUIElement.instance.panels[currentPanelIndex].SetActive(false);
        MenuUIElement.instance.panels[currentPanelIndex + 1].SetActive(true);

        navData.currentStateStack.Push((NavigationState)(currentPanelIndex + 1));
        navData.currentState = (NavigationState)(currentPanelIndex + 2);

        if(navData.currentState == NavigationState.OperationPanel) EnableOperation(navData.operationState);
    }

    private void SetNextPanel(NavigationState value)
    {
        int currentPanelIndex = (int)navData.currentState - 1;  // because Enum has None value at index 0 but Array doesn't have enum value 

        MenuUIElement.instance.panels[currentPanelIndex].SetActive(false);
        MenuUIElement.instance.panels[(int)(value - 1)].SetActive(true);

        navData.currentStateStack.Push(navData.currentState);
        navData.currentState = value;

        if(navData.currentState == NavigationState.OperationPanel) EnableOperation(navData.operationState);
    }

    private void ModuleBarIndicator()
    {
        // logic to control data on top of panel -- indication current section 
        if (navData.currentState == NavigationState.MachinePanel) AssignMenuBarData.instance.AssignMenuBarIndication(0);
        else if (navData.currentState == NavigationState.ProcessStagePanel) AssignMenuBarData.instance.AssignMenuBarIndication(1);
        else AssignMenuBarData.instance.AssignMenuBarIndication(2);
    }

    // Operation selection 
    public void EnableOperation(Operation value)
    {
        OperationData data = MenuUIElement.instance.operationData[(int)value - 1];

        MenuUIElement.instance.operation_title.text = data.title;
        MenuUIElement.instance.operation_description.text = data.discription;
        MenuUIElement.instance.operation_image.sprite = data.imageDisplay;
        MenuUIElement.instance.operation_image.preserveAspect = true;

        MenuButton.instance.operation_BeginVR.onClick.RemoveAllListeners();
        MenuButton.instance.operation_BeginVR.onClick.AddListener(() => StartOperation(data.currentOperation)); // main Opertions enum dose not have not selected value 
    }

    // Operation selection 
    public void StartOperation(Operation value)
    {
        Debug.Log($"Operations value {value} ");
        // operation selection
        GameData.instance.operation = value;    // index of operation value

        if (value == Operation.Assembly) { SceneManager.LoadScene("MotorAssembly"); } 
        else if (value == Operation.Disassemble) { SceneManager.LoadScene("MotorDisassemble"); } 
        else if (value == Operation.PartFamilarization) { SceneManager.LoadScene("MotorFamiliarization"); }
        else Debug.Log($"No Operation selected to be executed");
    }
}
