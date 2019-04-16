using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MiniGame : MonoBehaviour
{
    public string Name;
    public string Description;
    public string LevelSelectionSceneName;

    public GameObject MiniGameDescriptionObject;
    public GameObject MiniGameNameObject;
    public VideoClip MiniGamePreviewClip;
    
    public void OnSelected()
    {
        DisplayVideoPreview();
        DisplayDescription();
    }

    private void DisplayVideoPreview()
    {

    }

    private void DisplayDescription()
    {
        MiniGameNameObject.GetComponent<TMPro.TextMeshProUGUI>().text = Name;
        MiniGameDescriptionObject.GetComponent<TMPro.TextMeshProUGUI>().text = Description;
    }

    public void OnLevelSelectionClicked()
    {
        SceneManager.LoadScene(LevelSelectionSceneName);
    }
}
