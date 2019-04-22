using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject _optionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        _optionsMenu = GameObject.Find("OptionsMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("MiniGameSelectionMenu");
    }

    public void OnOptionsButtonClicked()
    {
        _optionsMenu.SetActive(true);
    }

    public void OnQuitButtonClicked()
    {

    }
}
