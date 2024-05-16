using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class MenuUIElement : MonoBehaviour
{
    public static MenuUIElement instance;

    void Awake() {  instance = this; }

    [Header("Panels")] 
    public GameObject nameDisplayText;
    public GameObject nameDisplayIcon;
    public GameObject menuBar;
    public GameObject[] panels;


    [Header("Operation panel")]
    public OperationData[] operationData;
    public TMP_Text operation_title;
    public TMP_Text operation_description;
    public Image operation_image;

    [Header("Change background")]
    public Image backgroundImage;
    public Sprite backgroundSprite;
}
