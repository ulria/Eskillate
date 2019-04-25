using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LowPop
{
    public class Level : ILevel
    {
        public int NbElements;
        public bool UseExpressions;
        private List<Element> _elements;
        private System.Random _random;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HighScore { get; set; }

        private const int MAX_VALUE = 100;
        private const int MIN_VALUE = 0;
        private const string SPRITE_PATH = "LowPop/Circle1";
        private const string MATERIAL_PATH = "Core/GradientShapeMaterial";

        public Level(int nbElements, bool useExpressions)
        {
            _random = new System.Random();

            _elements = new List<Element>();
            for (var i = 0; i < nbElements; i++)
            {
                var newElement = GenerateElement(useExpressions);
                _elements.Add(newElement);
            }

            var test = 123;
        }

        private Element GenerateNormalElement()
        {
            var value = _random.Next(MIN_VALUE, MAX_VALUE);
            return new Element()
            {
                Text = value.ToString(),
                Value = value
            };
        }

        private Element GenerateAdditionElement()
        {
            var value1 = _random.Next(MIN_VALUE, MAX_VALUE);
            var value2 = _random.Next(MIN_VALUE, MAX_VALUE);
            return new Element()
            {
                Text = value1.ToString() + " + " + value2.ToString(),
                Value = value1 + value2
            };
        }

        private Element GenerateSubstractionElement()
        {
            var value1 = _random.Next(MIN_VALUE, MAX_VALUE);
            var value2 = _random.Next(MIN_VALUE, MAX_VALUE);
            return new Element()
            {
                Text = value1.ToString() + " - " + value2.ToString(),
                Value = value1 - value2
            };
        }

        private Element GenerateMultiplicationElement()
        {
            var value1 = _random.Next(MIN_VALUE, MAX_VALUE);
            var value2 = _random.Next(MIN_VALUE, MAX_VALUE);
            return new Element()
            {
                Text = value1.ToString() + " * " + value2.ToString(),
                Value = value1 * value2
            };
        }

        private Element GenerateDivisionElement()
        {
            var value1 = _random.Next(MIN_VALUE, MAX_VALUE);
            var value2 = _random.Next(MIN_VALUE, MAX_VALUE);
            return new Element()
            {
                Text = value1.ToString() + " / " + value2.ToString(),
                Value = (float)value1 / (float)value2
            };
        }

        private Element GenerateNegativeElement()
        {
            var value = _random.Next(MIN_VALUE, MAX_VALUE);
            return new Element()
            {
                Text = "-" + value.ToString(),
                Value = -value
            };
        }

        private Element GenerateElement(bool useExpressions)
        {
            var isCorrectElement = false;

            var operationIndex = _random.Next(5);
            var element = new Element();
            while (!isCorrectElement)
            {
                if (useExpressions)
                {
                    switch (operationIndex)
                    {
                        case 0:
                            element = GenerateNormalElement();
                            break;
                        case 1:
                            element = GenerateAdditionElement();
                            break;
                        case 2:
                            element = GenerateSubstractionElement();
                            break;
                        case 3:
                            element = GenerateMultiplicationElement();
                            break;
                        case 4:
                            element = GenerateDivisionElement();
                            break;
                        case 5:
                            element = GenerateNegativeElement();
                            break;
                        default:
                            throw new ArgumentException("Tried to generate an element with an weird operationIndex");
                    }
                }
                else
                {
                    element = GenerateNormalElement();
                }

                if (!_elements.Any(elem => elem.Value == element.Value))
                    isCorrectElement = true;
            }
            return element;
        }

        public void Load()
        {
            for (int i = 0; i < _elements.Count; i++)
            {
                GameObject foreground = GameObject.Find("2-Foreground");
                GameObject newSprite = new GameObject();
                newSprite.name = $"Poppable{i}";
                newSprite.transform.parent = foreground.transform;
                var sr = newSprite.AddComponent<SpriteRenderer>() as SpriteRenderer;
                sr.sprite = Resources.Load<Sprite>(SPRITE_PATH);
                sr.material = Resources.Load<Material>(MATERIAL_PATH);
                newSprite.AddComponent<RectTransform>();

                var x = _random.Next(-Screen.width / 2, Screen.width / 2);
                var y = _random.Next(-Screen.height / 2, Screen.height / 2);
                var z = 0;

                newSprite.transform.localPosition = new Vector3(x, y, z);

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
                textMesh.text = _elements[i].Text;
                textMesh.alignment = TMPro.TextAlignmentOptions.Center;

                textMesh.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }
}