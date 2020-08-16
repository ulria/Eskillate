using System.Diagnostics;
using UnityEngine;

namespace LowPop
{
    public class PoppableScript : MonoBehaviour
    {
        private const float TIME_BEFORE_FADED = 3000f; // in millisecs

        public float Value;
        private GameObject _gameController;
        private bool _poppingPrevented = false;
        private GameObject _poppingPreventedMsgGO;
        private Stopwatch _stopWatch = new Stopwatch();
        private bool _poppingPreventedMsgCreated = false;

        private const string SPRITE_PATH = "LowPop/Square";
        private const string MATERIAL_PATH = "Core/GradientAlphaMaterial";

        // Start is called before the first frame update
        void Start()
        {
            _gameController = GameObject.Find("GameController");
        }

        // Update is called once per frame
        void Update()
        {
            if(_poppingPreventedMsgGO != null)
            {
                var a = Mathf.Clamp(TIME_BEFORE_FADED - _stopWatch.ElapsedMilliseconds, 0, TIME_BEFORE_FADED) / TIME_BEFORE_FADED;
                _poppingPreventedMsgGO.transform.GetChild(0).GetComponent<SpriteRenderer>().material.SetFloat("_Opacity", a);

                var textOldColor = _poppingPreventedMsgGO.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshPro>().color;
                var textNewColor = new Color(textOldColor.r, textOldColor.g, textOldColor.b, a);
                _poppingPreventedMsgGO.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshPro>().color = textNewColor;
            }
        }

        void OnMouseDown()
        {
            if(_poppingPrevented)
            {
                DisplayPoppingPrevented();
                return;
            }

            var successful = _gameController.GetComponent<GameController>().OnPopped(Value);
            if (successful)
            {
                var renderers = gameObject.GetComponentsInChildren<Renderer>();
                foreach(var renderer in renderers)
                {
                    renderer.enabled = false;
                }
            }
        }

        public void SetPoppingPrevented(bool poppingPrevented)
        {
            _poppingPrevented = poppingPrevented;
        }

        private void DisplayPoppingPrevented()
        {
            if(!_poppingPreventedMsgCreated)
            {
                // Display generic consignes
                var middlegroundGO = GameObject.Find("1-Middleground");
                _poppingPreventedMsgGO = new GameObject("directivesGO");
                _poppingPreventedMsgGO.transform.parent = middlegroundGO.transform;
                var directivesBackgroundGO = new GameObject("directivesBackgroundGO");
                directivesBackgroundGO.transform.parent = _poppingPreventedMsgGO.transform;
                var directivesBackgroundRectTransform = directivesBackgroundGO.AddComponent<RectTransform>();
                directivesBackgroundRectTransform.SetPositionAndRotation(new Vector3(0, -400, 0), new Quaternion());
                directivesBackgroundRectTransform.sizeDelta = new Vector2(155, 155);
                directivesBackgroundRectTransform.localScale = new Vector3(3.25f, 1.625f, 1f);
                var sr = directivesBackgroundGO.AddComponent<SpriteRenderer>();
                sr.sprite = Resources.Load<Sprite>(SPRITE_PATH);
                sr.material = Resources.Load<Material>(MATERIAL_PATH);

                sr.material.SetColor("_FirstColor", new Color(50f / 255f, 50f / 255f, 50f / 255f, 1));
                sr.material.SetColor("_SecondColor", new Color(120f / 255f, 120f / 255f, 120f / 255f, 1));
                sr.material.SetFloat("_Opacity", 0.6f);

                var directivesTextGO = new GameObject("directivesTextGO");
                directivesTextGO.transform.parent = directivesBackgroundGO.transform;
                var directivesTextMesh = directivesTextGO.AddComponent<TMPro.TextMeshPro>();
                directivesTextMesh.text = LabelHelper.ResolveLabel(LabelHelper.Label.LowPopPoppingPrevented);
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

                _poppingPreventedMsgCreated = true;
            }

            _stopWatch.Restart();
        }
    }
}