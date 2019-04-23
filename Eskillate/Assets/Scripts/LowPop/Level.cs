using Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LowPop
{
    public class Level : ILevel
    {
        public int NbElements;
        public bool UseExpressions;
        private List<Element> _elements;
        private Random _random;

        private const int MAX_VALUE = 100;
        private const int MIN_VALUE = 0;

        public Level(int nbElements, bool useExpressions)
        {
            _random = new Random();

            _elements = new List<Element>();
            for(var i = 0; i < nbElements; i++)
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
                Value = (float) value1 / (float) value2
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
                if(useExpressions)
                {
                    switch(operationIndex)
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

        }
    }
}