﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Label = LabelHelper.Label;
using System.Collections.Generic;
using UnityEngine.Video;
using static Core.ResourcesHelper;

namespace Core
{
    public class MiniGameInfo
    {
        public Label NameLabel;
        public Label DescriptionLabel;
        public string LevelSelectionSceneName;
        public VideoClip MiniGamePreviewClip;
        public string ImagePath;
    }

    public class MiniGameSelection : MonoBehaviour
    {
        private bool isDragging = false;
        private float _elementWidthPlusSpacing = 0.0f;
        private float _scrollRange = 0.0f;
        private float _scrollMin = 0.0f;
        private int _previouslySelectedMiniGameIndex = -1;

        private GameObject ScrollListContent;
        private GameObject ScrollListViewport;
        private List<GameObject> _elements = new List<GameObject>();

        private List<MiniGameInfo> _miniGames = new List<MiniGameInfo>();

        private bool _initDone = false;

        // Start is called before the first frame update
        void Start()
        {
            // Initializin game objects
            ScrollListContent = GameObject.Find("ScrollListContent");
            ScrollListViewport = GameObject.Find("ScrollListViewport");

            // Add the mini games to the scroll list
            var dragAndDropMG = new MiniGameInfo()
            {
                NameLabel = Label.DragAndDropName,
                DescriptionLabel = Label.DragAndDropDescription,
                LevelSelectionSceneName = "DragAndDrop",
                MiniGamePreviewClip = Resources.Load<VideoClip>(Resource.Core.MiniGame.Clips.DragAndDrop),
                ImagePath = Resource.Core.MiniGame.Images.DragAndDrop
            };
            _miniGames.Add(dragAndDropMG);
            var reproduceShapeMG = new MiniGameInfo()
            {
                NameLabel = Label.ReproduceShapeName,
                DescriptionLabel = Label.ReproduceShapeDescription,
                LevelSelectionSceneName = "ReproduceShape",
                MiniGamePreviewClip = Resources.Load<VideoClip>(Resource.Core.MiniGame.Clips.ReproduceShape),
                ImagePath = Resource.Core.MiniGame.Images.ReproduceShape
            };
            //_miniGames.Add(reproduceShapeMG);
            var reproduceSequenceMG = new MiniGameInfo()
            {
                NameLabel = Label.ReproduceSequenceName,
                DescriptionLabel = Label.ReproduceSequenceDescription,
                LevelSelectionSceneName = "ReproduceSequence",
                MiniGamePreviewClip = Resources.Load<VideoClip>(Resource.Core.MiniGame.Clips.ReproduceSequence),
                ImagePath = Resource.Core.MiniGame.Images.ReproduceSequence
            };
            //_miniGames.Add(reproduceSequenceMG);
            var hideAndSeekMG = new MiniGameInfo()
            {
                NameLabel = Label.HideAndSeekName,
                DescriptionLabel = Label.HideAndSeekDescription,
                LevelSelectionSceneName = "HideAndSeek",
                MiniGamePreviewClip = Resources.Load<VideoClip>(Resource.Core.MiniGame.Clips.HideAndSeek),
                ImagePath = Resource.Core.MiniGame.Images.HideAndSeek
            };
            //_miniGames.Add(hideAndSeekMG);
            var lowPopMG = new MiniGameInfo()
            {
                NameLabel = Label.LowPopName,
                DescriptionLabel = Label.LowPopDescription,
                LevelSelectionSceneName = "LowPop",
                MiniGamePreviewClip = Resources.Load<VideoClip>(Resource.Core.MiniGame.Clips.LowPop),
                ImagePath = Resource.Core.MiniGame.Images.LowPop
            };
            _miniGames.Add(lowPopMG);
            var perilousPathMG = new MiniGameInfo()
            {
                NameLabel = Label.PerilousPathName,
                DescriptionLabel = Label.PerilousPathDescription,
                LevelSelectionSceneName = "PerilousPath",
                MiniGamePreviewClip = Resources.Load<VideoClip>(Resource.Core.MiniGame.Clips.PerilousPath),
                ImagePath = Resource.Core.MiniGame.Images.PerilousPath
            };
            //_miniGames.Add(perilousPathMG);
            var trueColorMG = new MiniGameInfo()
            {
                NameLabel = Label.TrueColorName,
                DescriptionLabel = Label.TrueColorDescription,
                LevelSelectionSceneName = "TrueColor",
                MiniGamePreviewClip = Resources.Load<VideoClip>(Resource.Core.MiniGame.Clips.TrueColor),
                ImagePath = Resource.Core.MiniGame.Images.TrueColor
            };
            //_miniGames.Add(trueColorMG);
            var sortMG = new MiniGameInfo()
            {
                NameLabel = Label.SortName,
                DescriptionLabel = Label.SortDescription,
                LevelSelectionSceneName = "Sort",
                MiniGamePreviewClip = Resources.Load<VideoClip>(Resource.Core.MiniGame.Clips.Sort),
                ImagePath = Resource.Core.MiniGame.Images.Sort
            };
            //_miniGames.Add(sortMG);
            foreach(var mg in _miniGames)
            {
                var prefab = Instantiate(Resources.Load(Resource.Core.Prefabs.MiniGameSelection.MiniGame)) as GameObject;
                prefab.transform.SetParent(ScrollListContent.transform, false);
                var mgComponent = prefab.GetComponent<MiniGame>();
                mgComponent.NameLabel = mg.NameLabel;
                mgComponent.DescriptionLabel = mg.DescriptionLabel;
                mgComponent.LevelSelectionSceneName = mg.LevelSelectionSceneName;
                mgComponent.MiniGamePreviewClip = mg.MiniGamePreviewClip;
                if (!string.IsNullOrEmpty(mg.ImagePath))
                {
                    prefab.GetComponent<Image>().sprite = Resources.Load<Sprite>(mg.ImagePath);
                }
                _elements.Add(prefab);
            }

            // Initializing UI components
            var elementWidth = ScrollListContent.transform.GetChild(0).GetComponent<RectTransform>().rect.width;
            var spacing = ScrollListContent.GetComponent<HorizontalLayoutGroup>().spacing;
            _elementWidthPlusSpacing = elementWidth + spacing;

            _scrollRange = (_elements.Count - 1) * _elementWidthPlusSpacing;
            _scrollMin = ScrollListContent.GetComponent<RectTransform>().anchoredPosition.x - _scrollRange;
            _scrollMin /= 2;

            ScrollListContent.GetComponent<HorizontalLayoutGroup>().padding.right = (int)(ScrollListViewport.GetComponent<RectTransform>().rect.width - (elementWidth + spacing));

            var newContentPos = new Vector2(_scrollMin + _scrollRange, 0);
            ScrollListContent.GetComponent<RectTransform>().anchoredPosition = newContentPos;

            // Set the labels
            var backToMainMenuButton = GameObject.Find("BackToMainMenuButton");
            backToMainMenuButton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = LabelHelper.ResolveLabel(Label.BackToMainMenuButton);
            var levelSelectionButton = GameObject.Find("LevelSelectionButton");
            levelSelectionButton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = LabelHelper.ResolveLabel(Label.LevelSelectionButton);

            _initDone = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_initDone)
                return;

            if (!isDragging)
            {
                // Find out which MiniGame is in the Selection spot
                var currentScrollX = ScrollListContent.GetComponent<RectTransform>().anchoredPosition.x;
                var scrollOffset = currentScrollX - _scrollMin;
                var scrollNbElementOffset = Mathf.RoundToInt(scrollOffset / _elementWidthPlusSpacing);
                scrollNbElementOffset = Mathf.Clamp(scrollNbElementOffset, 0, _elements.Count - 1);
                var newOffset = scrollNbElementOffset * _elementWidthPlusSpacing;
                var newPosition = _scrollMin + newOffset;
                // Snap to this MiniGame
                LerpToElement(newPosition);

                // Invert index, because, for some reason index 0 represents the left most element
                var elementIndexReversed = (_elements.Count - 1) - scrollNbElementOffset;
                // Call the OnSelected method of the MiniGame in the selection spot
                if (elementIndexReversed != _previouslySelectedMiniGameIndex)
                {
                    _elements[elementIndexReversed].GetComponent<MiniGame>().OnSelected();
                    _previouslySelectedMiniGameIndex = elementIndexReversed;
                }
            }
        }

        private void LerpToElement(float position)
        {
            //Debug.Log($"Lerping (float) to {position}");
            var newX = Mathf.Lerp(ScrollListContent.GetComponent<RectTransform>().anchoredPosition.x, position, Time.deltaTime * 10f);
            Vector2 newPos = new Vector2(newX, 0);
            ScrollListContent.GetComponent<RectTransform>().anchoredPosition = newPos;
        }

        public void MoveToSelectedSpot(MiniGame selectedMiniGame)
        {
            if (!isDragging)
            {
                // find which miniGame is selected
                for (var index = 0; index < _elements.Count; index++)
                {
                    var miniGame = _elements[index];
                    if (miniGame.GetComponent<MiniGame>().NameLabel == selectedMiniGame.NameLabel)
                    {
                        // move to that mini game
                        var scrollNbElementOffset = _elements.Count - 1 - index;
                        var scrollOffset = scrollNbElementOffset * _elementWidthPlusSpacing;
                        var currentScrollX = scrollOffset + _scrollMin;
                        ScrollListContent.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentScrollX, ScrollListContent.GetComponent<RectTransform>().anchoredPosition.y);
                    }
                }
            }
        }

        public void OnBeginDrag()
        {
            isDragging = true;
        }

        public void OnEndDrag()
        {
            isDragging = false;
        }

        public void OnLevelSelectionClicked()
        {
            _elements[_previouslySelectedMiniGameIndex].GetComponent<MiniGame>().OnLevelSelectionClicked();
        }

        public void OnBackToMainMenuClicked()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}