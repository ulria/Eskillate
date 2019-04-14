using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MiniGame : MonoBehaviour
{
    public string Name;
    public string Description;

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
}
