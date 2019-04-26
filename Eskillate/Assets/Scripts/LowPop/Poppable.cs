using UnityEngine;
using System.Collections.Generic;

namespace LowPop
{
    public class Poppable
    {
        public float Value;
        public string Text;

        private const string SPRITE_PATH = "LowPop/Circle1";
        private const string MATERIAL_PATH = "Core/GradientShapeMaterial";

        public void Load(int poppableIndex)
        {
            GameObject foreground = GameObject.Find("2-Foreground");
            GameObject newSprite = new GameObject();
            newSprite.name = $"Poppable{poppableIndex}";
            newSprite.transform.parent = foreground.transform;
            var sr = newSprite.AddComponent<SpriteRenderer>() as SpriteRenderer;
            sr.sprite = Resources.Load<Sprite>(SPRITE_PATH);
            sr.material = Resources.Load<Material>(MATERIAL_PATH);
            newSprite.AddComponent<RectTransform>();

            newSprite.AddComponent<PoppableScript>().Value = this.Value;

            var x = Random.Range(-Screen.width / 2, Screen.width / 2);
            var y = Random.Range(-Screen.height / 2, Screen.height / 2);
            var z = 0;

            newSprite.transform.localPosition = new Vector3(x, y, z);

            newSprite.AddComponent<PolygonCollider2D>();

            GameObject textGO = new GameObject();
            textGO.transform.parent = newSprite.transform;

            textGO.AddComponent<TMPro.TextMeshPro>();
            var textMesh = textGO.GetComponent<TMPro.TextMeshPro>();
            var textMeshRect = textGO.GetComponent<RectTransform>();

            // Set the point size
            textMeshRect.sizeDelta = new Vector2(newSprite.GetComponent<RectTransform>().rect.width, newSprite.GetComponent<RectTransform>().rect.height);
            textMesh.fontSizeMax = 10000;
            textMesh.characterWidthAdjustment = 30;
            textMesh.enableAutoSizing = true;
            textMesh.fontSharedMaterial = Resources.Load<Material>("Fonts & Materials/LiberationSans SDF - Drop Shadow");
            textMesh.fontStyle = TMPro.FontStyles.Bold;
            textMesh.text = this.Text;
            textMesh.alignment = TMPro.TextAlignmentOptions.Center;

            textMesh.transform.localPosition = new Vector3(0, 0, 0);
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