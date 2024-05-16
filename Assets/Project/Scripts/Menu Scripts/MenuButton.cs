using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public static MenuButton instance;

    void Awake() {  instance = this; }

    [Header("Next & back button")]
    public Button nextButton;
    public Button backButton;

    [Header("Operation screen Button")]
    public Button operation_BackButton;
    public Button operation_BeginVR;

    [Header("Machine Button")]
    public Button[] machineButton = new Button[1];

    [Header("Process Button")]
    public Button[] processStageButton = new Button[2];

    [Header("Lathe Button")]
    public Button[] operationButton = new Button[2];
}
