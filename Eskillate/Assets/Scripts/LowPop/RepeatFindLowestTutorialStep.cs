
using System.Collections.Generic;
using UnityEngine;

namespace LowPop
{
    public class RepeatFindLowestTutorialStep : TutorialStep
    {
        enum TutorialSubStep
        {
            FIRST_POPPED,
            SECOND_POPPED,
            THIRD_POPPED,
            DONE
        }

        private GameObject _directivesGO;
        private bool _loaded = false;
        private TutorialSubStep _state = TutorialSubStep.FIRST_POPPED;
        private GameController _gameController;
        private List<string> _directivesTexts = new List<string>();
        private string _subscriberId;

        private const string SPRITE_PATH = "LowPop/Square";
        private const string MATERIAL_PATH = "Core/GradientAlphaMaterial";

        public override void Load()
        {
            Debug.Log("RepeatFindLowestTutorialStep loaded.");

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
            sr.sprite = Resources.Load<Sprite>(SPRITE_PATH);
            sr.material = Resources.Load<Material>(MATERIAL_PATH);

            sr.material.SetColor("_FirstColor", new Color(50f / 255f, 50f / 255f, 50f / 255f, 1));
            sr.material.SetColor("_SecondColor", new Color(120f / 255f, 120f / 255f, 120f / 255f, 1));
            sr.material.SetFloat("_Opacity", 0.6f);

            var directivesTextGO = new GameObject("directivesTextGO");
            directivesTextGO.transform.parent = directivesBackgroundGO.transform;
            var directivesTextMesh = directivesTextGO.AddComponent<TMPro.TextMeshPro>();
            directivesTextMesh.text = LabelHelper.ResolveLabel(LabelHelper.Label.LowPopTutorialRepeatFindLowestDirectives1);
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

            var gameControllerGO = GameObject.Find("GameController");
            _gameController = gameControllerGO.GetComponent<GameController>();
            WaitForNextLowest();

            _directivesTexts.Add(LabelHelper.ResolveLabel(LabelHelper.Label.LowPopTutorialRepeatFindLowestDirectives1));
            _directivesTexts.Add(LabelHelper.ResolveLabel(LabelHelper.Label.LowPopTutorialRepeatFindLowestDirectives2));
            _directivesTexts.Add(LabelHelper.ResolveLabel(LabelHelper.Label.LowPopTutorialRepeatFindLowestDirectives3));

            _loaded = true;
        }

        private void WaitForNextLowest()
        {
            // Find lowest bubble
            var lowestPoppable = _gameController.GetNextPoppableToPop();
            // SetPreventPopping(false);
            lowestPoppable.SetPoppingPrevented(false);
            // When popped, trigger OnPopped
            _subscriberId = _gameController.SubscribeToPopping(OnPopped);
        }

        public void OnPopped()
        {
            var nextState = _state + 1;
            if(nextState == TutorialSubStep.DONE)
            {
                _directivesGO.SetActive(false);

                // Complete step
                _tutorialManager.CompleteStep();
            }
            else
            {
                // Unsubscribe to the previous Poppable.OnPopped()
                _gameController.Unsubscribe(_subscriberId);
                WaitForNextLowest();
            }
        }

        public override void Update()
        {

        }
    }
}