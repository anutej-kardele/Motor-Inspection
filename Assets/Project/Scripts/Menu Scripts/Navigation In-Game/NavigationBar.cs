using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavigationBar : MonoBehaviour
{
    [SerializeField] private Button backButton, homeButton, reloadButton;
    [SerializeField] private NavigationData navData;

    private void Start(){

        backButton.onClick.AddListener(() => ReturnToMenu(true));
        homeButton.onClick.AddListener(() => ReturnToMenu(false));
        reloadButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    private void ReturnToMenu(bool activateBackFunction){
        if(activateBackFunction) navData.retriveData = true;
        SceneManager.LoadScene(0);
    }
}
