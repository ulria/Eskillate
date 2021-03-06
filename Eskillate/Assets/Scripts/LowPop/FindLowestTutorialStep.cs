﻿
using UnityEngine;
using static Core.ResourcesHelper;

namespace LowPop
{
    public class FindLowestTutorialStep : TutorialStep
    {
        private GameObject _directivesGO;
        private bool _loaded = false;
        private bool _wasLoaded = false;
        private GameController _gameController;
        private string _subscriberId;

        public override void Load()
        {
            Debug.Log($"{System.DateTime.Now} FindLowestTutorialStep loaded.");
            if (_wasLoaded)
            {
                Reload();
            }
            else
            {
                InternalLoad();
            }
        }

        private void InternalLoad()
        {
            // Display generic consignes
            var middlegroundGO = GameObject.Find("1-Middleground");
            _directivesGO = new GameObject("directivesGO");
            _directivesGO.transform.parent = middlegroundGO.transform;
            var directivesBackgroundGO = new GameObject("directivesBackgroundGO");
            directivesBackgroundGO.transform.parent = _directivesGO.transform;
            var directivesBackgroundRectTransform = directivesBackgroundGO.AddComponent<RectTransform>();
            directivesBackgroundRectTransform.SetPositionAndRotation(new Vector3(-250, 250, 0), new Quaternion());
            directivesBackgroundRectTransform.sizeDelta = new Vector2(155, 155);
            directivesBackgroundRectTransform.localScale = new Vector3(6.5f, 3.25f, 1f);
            var sr = directivesBackgroundGO.AddComponent<SpriteRenderer>();
            sr.sprite = Resources.Load<Sprite>(Resource.LowPop.Square);
            sr.material = Resources.Load<Material>(Resource.Core.GradientAlphaMaterial);

            sr.material.SetColor("_FirstColor", new Color(50f / 255f, 50f / 255f, 50f / 255f, 1));
            sr.material.SetColor("_SecondColor", new Color(120f / 255f, 120f / 255f, 120f / 255f, 1));
            sr.material.SetFloat("_Opacity", 0.6f);

            var directivesTextGO = new GameObject("directivesTextGO");
            directivesTextGO.transform.parent = directivesBackgroundGO.transform;
            var directivesTextMesh = directivesTextGO.AddComponent<TMPro.TextMeshPro>();
            directivesTextMesh.text = LabelHelper.ResolveLabel(LabelHelper.Label.LowPopTutorialFindLowestDirectives);
            var directivesTextGORect = directivesTextGO.GetComponent<RectTransform>();
            directivesTextGORect.sizeDelta = new Vector2(155, 155);
            directivesTextGORect.localScale = new Vector3(1, 1, 1);
            directivesTextMesh.fontSizeMin = 100;
            directivesTextMesh.fontSizeMax = 500;
            directivesTextMesh.color = new Color(225f / 255f, 225f / 255f, 225f / 255f, 1);
            directivesTextMesh.alignment = TMPro.TextAlignmentOptions.Center;
            directivesTextMesh.transform.localPosition = new Vector3(0, 0, -0.1f);
            directivesTextMesh.enableAutoSizing = true;
            // Left, Top, Right, Bottom            
            directivesTextMesh.margin = new Vector4(10, 20, 10, 20);

            // TODO - Allow clicks on the lowest bubble
            // Find lowest bubble
            var gameControllerGO = GameObject.Find("GameController");
            _gameController = gameControllerGO.GetComponent<GameController>();
            var lowestPoppable = _gameController.GetNextPoppableToPop();
            // SetPreventPopping(false);
            lowestPoppable.SetPoppingPrevented(false);
            // When popped, trigger stepCompleted
            _subscriberId = _gameController.SubscribeToPopping(OnPopped);

            _loaded = true;
            _wasLoaded = true;
        }

        private void Reload()
        {
            if (_directivesGO)
            {
                _directivesGO.SetActive(true);
            }

            var lowestPoppable = _gameController.GetNextPoppableToPop();
            // SetPreventPopping(false);
            lowestPoppable.SetPoppingPrevented(false);
            // When popped, trigger stepCompleted
            _subscriberId = _gameController.SubscribeToPopping(OnPopped);
        }

        public void OnPopped()
        {
            _directivesGO.SetActive(false);

            // Complete step
            _tutorialManager.CompleteStep();

            Debug.Log($"{System.DateTime.Now} Completed FindLowestTutorialStep");

            _gameController.Unsubscribe(_subscriberId);
        }

        public override void Update()
        {
            
        }

        public override void Unload()
        {
            Debug.Log($"{System.DateTime.Now} FindLowestTutorialStep unloaded.");
            if (_directivesGO)
            {
                _directivesGO.SetActive(false);
            }
            if (_gameController)
            {
                _gameController.Unsubscribe(_subscriberId);
            }
            _loaded = false;
        }
    }
}