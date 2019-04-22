using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private GameObject _mainMenu;
    private GameObject _optionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        var volume = PlayerPrefs.GetFloat("VolumeRatio", 1.0f);
        var sliderGameObject = GameObject.Find("VolumeSlider").GetComponent<Slider>().value = volume;

        _mainMenu = GameObject.Find("MainMenu");
        _optionsMenu = GameObject.Find("OptionsMenu");
        _mainMenu.SetActive(true);
        _optionsMenu.SetActive(false);
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
        _mainMenu.SetActive(false);
        _optionsMenu.SetActive(true);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    public void OnVolumeSliderValueChanged(float newValue)
    {
        PlayerPrefs.SetFloat("VolumeRatio", newValue);
    }

    public void OnBackButtonFromOptionsClicked()
    {
        _optionsMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }
}