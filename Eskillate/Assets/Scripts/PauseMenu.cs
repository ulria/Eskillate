using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameObject _pauseMenu;
    private GameObject _gameController;

    // Start is called before the first frame update
    void Start()
    {
        _pauseMenu = GameObject.Find("PauseMenuCanvas");
        _gameController = GameObject.Find("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRestartLevelButtonClicked()
    {
        _gameController.GetComponent<GameController>().RestartLevel();
        _pauseMenu.SetActive(false);
    }

    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnResumeButtonClicked()
    {
        _pauseMenu.SetActive(false);
    }
}
