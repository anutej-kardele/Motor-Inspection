using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class AssignMenuBarData : MonoBehaviour
{

    public static AssignMenuBarData instance;

    void Awake(){
        instance = this;
    }

    [SerializeField]
    private Sprite selectedSprite, nonSelectedSprite;

    [SerializeField]
    private TMP_Text[] menuBarText = new TMP_Text[3];
    [SerializeField]
    private Image[] menuBarTextImage = new Image[3];
    [SerializeField]
    private Image[] menuBarHighlightImage = new Image[3];  // changing the color
    [SerializeField]
    private TMP_Text[] menuBarHighlightText = new TMP_Text[3]; // changing the color 

    [HideInInspector]
    public int previosSelectedIndex = 0;

    private int ReturnCurrentLanguage(){
        if(LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.GetLocale("en")) return 0;
        else if(LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.GetLocale("hi-IN")) return 1;
        return 0;
    }

    public void AssignMenuBarText(){
        
        // checking language 
        int languageIndex = ReturnCurrentLanguage();
        
        // assigning data 
        menuBarText[0].text = machineText[languageIndex, (int)MenuScript.instance.navData.machineState];
        menuBarText[1].text = processStageText[languageIndex, (int)MenuScript.instance.navData.processStage];
        menuBarText[2].text = operationStateText[languageIndex,(int)MenuScript.instance.navData.operationState];

        menuBarTextImage[0].sprite = ((int)MenuScript.instance.navData.machineState == 0) ? nonSelectedSprite : selectedSprite;
        menuBarTextImage[1].sprite = ((int)MenuScript.instance.navData.processStage == 0) ? nonSelectedSprite : selectedSprite;
        menuBarTextImage[2].sprite = ((int)MenuScript.instance.navData.operationState == 0) ? nonSelectedSprite : selectedSprite;
    }

    public void AssignMenuBarIndication(int index){
        // un-highlighting previous state
        menuBarHighlightImage[previosSelectedIndex].color = new Color(0.937255f, 0.9411765f, 0.937255f, 1);
        menuBarHighlightText[previosSelectedIndex].color =  Color.black;

        // highlighting current state
        menuBarHighlightImage[index].color = new Color(0.8980393f, 0, 0, 1);
        menuBarHighlightText[index].color =  Color.white;
        
        previosSelectedIndex = index;
    }

    public void ResetData(){
        for(int i = 0; i < menuBarText.Length; i++){
            menuBarHighlightImage[i].color = new Color(0.937255f, 0.9411765f, 0.937255f, 1);
            menuBarHighlightText[i].color =  Color.black;
        }
        menuBarHighlightImage[0].color = new Color(0.8980393f, 0, 0, 1);
        menuBarHighlightText[0].color =  Color.white;
    }

    public string[,] machineText = new string[2,2]{
        {"Not selected","Motor"},
        {"चयनित नहीं", "मोटर"}
    };

    public string[,] processStageText = new string[2,3]{
        {"Not selected","Part Familiarization", "Operation"},
        {"चयनित नहीं", "भाग परिचय", "सं��ालन"}
    };

    public string[,] operationStateText = new string[2,4]{
        {"Not selected", "Assembly", "Disassemble", "Part Familarization"},
        {"चयनित नहीं", "विधानसभा", "इकट्ठा", "भाग परिचय"}
    };
}
