using UnityEngine;
using UnityEngine.Localization.Settings;

[CreateAssetMenu]
public class OperationData : ScriptableObject
{
    public Operation currentOperation;
    public Sprite imageDisplay;
    public Sprite taskImage;
    
    public string title{
        get{
            // return GameData.instance.language == Language.English ? titleLanguage[0] : titleLanguage[1];
            return titleLanguage[ReturnCurrentLanguage()];
        }
    }
    public string discription{
        get{
            // return GameData.instance.language == Language.English ? discriptionLanguage[0] : discriptionLanguage[1];
            return discriptionLanguage[ReturnCurrentLanguage()];
        }
    }

    [Header("Index 0 for English & index 1 for Hindi")]
    public string[] titleLanguage;
    public string[] discriptionLanguage;

    private int ReturnCurrentLanguage(){
        if(LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.GetLocale("en")) return 0;
        else if(LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.GetLocale("hi-IN")) return 1;
        return 0;
    }
}
