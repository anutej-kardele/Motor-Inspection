using UnityEngine;
using UnityEngine.Localization.Settings;
using TMPro;

public class MenuManage : MonoBehaviour
{
    [Header("This scripts checks for player data i.e. name of the player")]
    [SerializeField] private GameObject initial;
    [SerializeField] private GameObject repeated;
    [SerializeField] private TMP_Dropdown dropdownlanguage;

    public void Awake(){

        if(GameData.repeatedInstance) repeated.SetActive(true);
        else initial.SetActive(true);
    }

    private void Start(){

        dropdownlanguage.onValueChanged.AddListener( delegate {SetLanguage(); });
        dropdownlanguage.value = ReturnCurrentLanguage();
    }

    private void SetLanguage(){
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[dropdownlanguage.value];
        GameData.instance.language = (Language) dropdownlanguage.value;
    }

    private int ReturnCurrentLanguage(){
        if(LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.GetLocale("en")) return 0;
        else if(LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.GetLocale("hi")) return 1;
        return 0;
    }
}
