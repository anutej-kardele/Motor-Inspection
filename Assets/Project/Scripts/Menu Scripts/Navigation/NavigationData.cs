using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NavigationData : ScriptableObject
{
    public void ResetValues(){

        navigationStates = new NavigationState[0];
        currentStateStack = new Stack<NavigationState>();
        currentState = NavigationState.None;

        machineStateValue = Machine.NotSelected;
        machineState = Machine.NotSelected;

        processStageValue = ProcessStage.NotSelected;
        processStage = ProcessStage.NotSelected;
        
        phaseValue = Phase.NotSelected;
        phase = Phase.NotSelected;
        
        operationStateValue = Operation.NotSelected;
        operationState = Operation.NotSelected;

        //AssignMenuBarData.instance.ResetData();
        //AssignMenuBarData.instance.previosSelectedIndex = 0;
    }
    
    // used to remove values selected while back is performed 
    public void RemoveValue(bool machineSelected, bool processSelected, bool phaseSelected, bool operationSelected){
        if(machineSelected) machineState = Machine.NotSelected;
        if(processSelected) processStage = ProcessStage.NotSelected;
        if(phaseSelected) phase = Phase.NotSelected;
        if(operationSelected) operationState = Operation.NotSelected;
    }

    public bool retriveData = false;
    public NavigationState[] navigationStates;
    public Stack<NavigationState> currentStateStack;
    public NavigationState currentState;

    public Machine machineStateValue;
    public Machine machineState{
        set { 
            machineStateValue = value;
            //AssignMenuBarData.instance.AssignMenuBarText();
        }
        get { return machineStateValue; }
    }

    public ProcessStage processStageValue;
    public ProcessStage processStage{
        set { 
            processStageValue = value;
            //AssignMenuBarData.instance.AssignMenuBarText();
        }
        get { return processStageValue; }
    }

    public Phase phaseValue;
    public Phase phase{
        set { 
            phaseValue = value;
            //AssignMenuBarData.instance.AssignMenuBarText();
        }
        get { return phaseValue; }
    }

    public Operation operationStateValue;
    public Operation operationState{
        set { 
            operationStateValue = value;
            //AssignMenuBarData.instance.AssignMenuBarText();
        }
        get { return operationStateValue; }
    }
}

public enum NavigationState {
    None, MachinePanel, ProcessStagePanel, PhasePanel, LatheOperationPanel, BenchViseOperationPanel, OperationPanel
}

public enum Machine {
    NotSelected, LatheMachine, DrillingMachine, BenchViseMachine
}

public enum ProcessStage {
    NotSelected, PartFamilarization, Operations, Maintanence
}

public enum Phase {
    NotSelected, Training, Assessment
}

public enum Operation{         
    NotSelected, Assembly, Disassembly
}
