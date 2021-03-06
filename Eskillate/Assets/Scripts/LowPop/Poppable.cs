﻿using UnityEngine;
using System.Collections.Generic;
using static Core.ResourcesHelper;

namespace LowPop
{
    public class Poppable
    {
        public float Value;
        public string Text;

        private GameObject _poppableGameObject;

        public void Load(int poppableIndex)
        {
            GameObject foreground = GameObject.Find("2-Foreground");
            _poppableGameObject = new GameObject();
            _poppableGameObject.name = $"Poppable{poppableIndex}";
            _poppableGameObject.transform.parent = foreground.transform;
            var sr = _poppableGameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
            sr.sprite = Resources.Load<Sprite>(Resource.LowPop.Circle1);
            sr.material = Resources.Load<Material>(Resource.Core.GradientShapeMaterial);
            _poppableGameObject.AddComponent<RectTransform>();

            _poppableGameObject.AddComponent<PoppableScript>().Value = this.Value;

            _poppableGameObject.AddComponent<PolygonCollider2D>();
            _poppableGameObject.tag = "Poppable";

            GameObject textGO = new GameObject();
            textGO.transform.parent = _poppableGameObject.transform;

            var textMesh = textGO.AddComponent<TMPro.TextMeshPro>();
            var textMeshRect = textGO.GetComponent<RectTransform>();

            // Set the point size
            textMeshRect.sizeDelta = new Vector2(_poppableGameObject.GetComponent<RectTransform>().rect.width, _poppableGameObject.GetComponent<RectTransform>().rect.height);
            textMesh.fontSizeMax = 10000;
            textMesh.characterWidthAdjustment = 30;
            textMesh.enableAutoSizing = true;
            textMesh.fontSharedMaterial = Resources.Load<Material>(Resource.FontsAndMaterials.LiberationSansSDFDropShadow);
            textMesh.fontStyle = TMPro.FontStyles.Bold;
            textMesh.text = this.Text;
            textMesh.alignment = TMPro.TextAlignmentOptions.Center;
            textMesh.enableWordWrapping = false;
                                        // Left, Top, Right, Bottom            
            textMesh.margin = new Vector4(10, 0, 10, 0);

            textMesh.transform.localPosition = new Vector3(0, 0, 0);
        }

        public void Unload()
        {
            GameObject.Destroy(_poppableGameObject);
        }

        public void SetPosition(Vector2 position)
        {
            _poppableGameObject.transform.localPosition = new Vector3(position.x, position.y, 0);
        }

        public void SetPoppingPrevented(bool poppingPrevented)
        {
            _poppableGameObject.GetComponent<PoppableScript>().SetPoppingPrevented(poppingPrevented);
        }
    }

    class PoppableComparer : IComparer<Poppable>
    {
        public int Compare(Poppable p1, Poppable p2)
        {
            if(p1 == null || p2 == null)
            {
                return 0;
            }

            return p1.Value.CompareTo(p2.Value);
        }
    }
}