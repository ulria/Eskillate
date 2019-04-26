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
        private List<Poppable> _elements;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HighScore { get; set; }

        private const int MAX_VALUE = 100;
        private const int MIN_VALUE = 0;

        public Level(int nbElements, bool useExpressions)
        {
            _elements = new List<Poppable>();
            for (var i = 0; i < nbElements; i++)
            {
                var newElement = GenerateElement(useExpressions);
                _elements.Add(newElement);
            }
        }

        private Poppable GenerateNormalElement()
        {
            var value = UnityEngine.Random.Range(MIN_VALUE, MAX_VALUE);
            return new Poppable()
            {
                Text = value.ToString(),
                Value = value
            };
        }

        private Poppable GenerateAdditionElement()
        {
            var value1 = UnityEngine.Random.Range(MIN_VALUE, MAX_VALUE);
            var value2 = UnityEngine.Random.Range(MIN_VALUE, MAX_VALUE);
            return new Poppable()
            {
                Text = value1.ToString() + " + " + value2.ToString(),
                Value = value1 + value2
            };
        }

        private Poppable GenerateSubstractionElement()
        {
            var value1 = UnityEngine.Random.Range(MIN_VALUE, MAX_VALUE);
            var value2 = UnityEngine.Random.Range(MIN_VALUE, MAX_VALUE);
            return new Poppable()
            {
                Text = value1.ToString() + " - " + value2.ToString(),
                Value = value1 - value2
            };
        }

        private Poppable GenerateMultiplicationElement()
        {
            var value1 = UnityEngine.Random.Range(MIN_VALUE, MAX_VALUE);
            var value2 = UnityEngine.Random.Range(MIN_VALUE, MAX_VALUE);
            return new Poppable()
            {
                Text = value1.ToString() + " * " + value2.ToString(),
                Value = value1 * value2
            };
        }

        private Poppable GenerateDivisionElement()
        {
            var value1 = UnityEngine.Random.Range(MIN_VALUE, MAX_VALUE);
            var value2 = UnityEngine.Random.Range(MIN_VALUE, MAX_VALUE);
            return new Poppable()
            {
                Text = value1.ToString() + " / " + value2.ToString(),
                Value = (float)value1 / (float)value2
            };
        }

        private Poppable GenerateNegativeElement()
        {
            var value = UnityEngine.Random.Range(MIN_VALUE, MAX_VALUE);
            return new Poppable()
            {
                Text = "-" + value.ToString(),
                Value = -value
            };
        }

        private Poppable GenerateElement(bool useExpressions)
        {
            var isCorrectElement = false;

            var operationIndex = UnityEngine.Random.Range(0, 5);
            var element = new Poppable();
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

        public List<Poppable> Load()
        {
            for (int i = 0; i < _elements.Count; i++)
            {
                _elements[i].Load(i);
            }

            var poppableComparer = new PoppableComparer();
            _elements.Sort(poppableComparer);

            return _elements;
        }
    }
}