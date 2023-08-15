﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using Label = LabelHelper.Label;

namespace Core
{
    public class MiniGame : MonoBehaviour
    {
        public Label NameLabel;
        public Label DescriptionLabel;
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
            Init();
        }

        void Init()
        {
            MiniGameDescriptionObject = GameObject.Find("MiniGameDescription");
            MiniGameNameObject = GameObject.Find("MiniGameName");
            MiniGamePreviewScreen = GameObject.Find("MiniGamePreviewScreen");
            MiniGamePreviewScreenImage = MiniGamePreviewScreen.GetComponent<RawImage>();
            MiniGameVideoPreviewPlayer = GameObject.Find("MiniGameVideoPreviewPlayer");
            _player = MiniGameVideoPreviewPlayer.GetComponent<VideoPlayer>();
        }

        private void OnMouseUp()
        {
            Debug.Log($"Minigame {NameLabel} clicked.");
            // alert mini game selection that this mini game was clicked
            var gameControllerGO = GameObject.Find("GameController");
            var miniGameSelection = gameControllerGO.GetComponent<MiniGameSelection>();
            miniGameSelection.MoveToSelectedSpot(this);
        }

        public void OnSelected()
        {
            Init();

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
            MiniGameNameObject.GetComponent<TMPro.TextMeshProUGUI>().text = LabelHelper.ResolveLabel(NameLabel);
            MiniGameDescriptionObject.GetComponent<TMPro.TextMeshProUGUI>().text = LabelHelper.ResolveLabel(DescriptionLabel);
        }

        public void OnLevelSelectionClicked()
        {
            SceneManager.LoadScene(LevelSelectionSceneName);
        }
    }
}