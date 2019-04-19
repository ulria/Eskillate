using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MiniGame : MonoBehaviour
{
    public string Name;
    public string Description;
    public string LevelSelectionSceneName;

    private GameObject MiniGameDescriptionObject;
    private GameObject MiniGameNameObject;
    public VideoClip MiniGamePreviewClip;
    private GameObject MiniGameVideoPreviewPlayer;
    private GameObject MiniGamePreviewScreen;
    private RawImage MiniGamePreviewScreenImage;

    private RenderTexture _text;
    private VideoPlayer _player;

    void Start()
    {
        MiniGameDescriptionObject = GameObject.Find("MiniGameDescription");
        MiniGameNameObject = GameObject.Find("MiniGameName");
        MiniGamePreviewScreen = GameObject.Find("MiniGamePreviewScreen");
        MiniGamePreviewScreenImage = MiniGamePreviewScreen.GetComponent<RawImage>();
        MiniGameVideoPreviewPlayer = GameObject.Find("MiniGameVideoPreviewPlayer");
        _player = MiniGameVideoPreviewPlayer.GetComponent<VideoPlayer>();
    }

    public void OnSelected()
    {
        DisplayVideoPreview();
        DisplayDescription();
    }

    private void DisplayVideoPreview()
    {
        if (MiniGamePreviewClip != null)
        {
            _player.clip = MiniGamePreviewClip;
            _text = new RenderTexture((int)_player.clip.width, (int)_player.clip.height, 0);
        }

        _player.targetTexture = _text;
        MiniGamePreviewScreenImage.texture = _text;
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
