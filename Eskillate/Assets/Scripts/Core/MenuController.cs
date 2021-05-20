using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace core
{
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

            var language = PlayerPrefs.GetInt("Language");
            var languageInt = (LabelHelper.Language) language;
            var isEnglish = false;
            var isFrench = false;
            var isSpanish = false;
            switch(languageInt)
            {
                case LabelHelper.Language.English:
                    isEnglish = true;
                    break;
                case LabelHelper.Language.French:
                    isFrench = true;
                    break;
                case LabelHelper.Language.Spanish:
                    isSpanish = true;
                    break;
            }

            var englishGO = _optionsMenu.transform.Find("Language").transform.Find("English").GetComponent<Toggle>().isOn = isEnglish;
            var frenchGO = _optionsMenu.transform.Find("Language").transform.Find("French").GetComponent<Toggle>().isOn = isFrench;
            var spanishGO = _optionsMenu.transform.Find("Language").transform.Find("Spanish").GetComponent<Toggle>().isOn = isSpanish;

            SetLabelTexts();

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

        public void OnEnglishChanged(bool isSelected)
        {
            if(isSelected)
            {
                LabelHelper.ChangeLanguage(LabelHelper.Language.English);
                SetLabelTexts();
            }
        }

        public void OnSpanishChanged(bool isSelected)
        {
            if (isSelected)
            {
                LabelHelper.ChangeLanguage(LabelHelper.Language.Spanish);
                SetLabelTexts();
            }
        }

        public void OnFrenchChanged(bool isSelected)
        {
            if (isSelected)
            {
                LabelHelper.ChangeLanguage(LabelHelper.Language.French);
                SetLabelTexts();
            }
        }

        private void SetLabelTexts()
        {
            // MainMenu
            var playButtonGO = _mainMenu.transform.Find("PlayButton");
            var playButtonText = playButtonGO.transform.Find("Text");
            playButtonText.GetComponent<TMPro.TextMeshProUGUI>().text = LabelHelper.ResolveLabel(LabelHelper.Label.MainMenuPlayButton);
            var optionButtonGO = _mainMenu.transform.Find("OptionButton");
            var optionButtonText = optionButtonGO.transform.Find("Text");
            optionButtonText.GetComponent<TMPro.TextMeshProUGUI>().text = LabelHelper.ResolveLabel(LabelHelper.Label.MainMenuOptionButton);
            var quitButtonGO = _mainMenu.transform.Find("QuitButton");
            var quitButtonText = quitButtonGO.transform.Find("Text");
            quitButtonText.GetComponent<TMPro.TextMeshProUGUI>().text = LabelHelper.ResolveLabel(LabelHelper.Label.MainMenuQuitButton);

            // OptionsMenu
            var titleGO = _optionsMenu.transform.Find("Title");
            titleGO.GetComponent<TMPro.TextMeshProUGUI>().text = LabelHelper.ResolveLabel(LabelHelper.Label.OptionsMenuTitle);
            var volumeGO = _optionsMenu.transform.Find("VolumeSlider");
            var volumeText = volumeGO.transform.Find("Text");
            volumeText.GetComponent<TMPro.TextMeshProUGUI>().text = LabelHelper.ResolveLabel(LabelHelper.Label.OptionsMenuVolume);
            var backbuttonGO = _optionsMenu.transform.Find("BackButton");
            var backButtonText = backbuttonGO.transform.Find("Text");
            backButtonText.GetComponent<TMPro.TextMeshProUGUI>().text = LabelHelper.ResolveLabel(LabelHelper.Label.OptionsMenuBackButton);
        }
    }
}