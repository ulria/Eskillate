using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class LabelHelperUnitTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void LabelHelperUnitTestsSimplePasses()
        {
            // Use the Assert class to test conditions
            foreach(var language in Enum.GetValues(typeof(LabelHelper.Language)).Cast<LabelHelper.Language>())
            {
                LabelHelper.ChangeLanguage(language);
                foreach (var label in Enum.GetValues(typeof(LabelHelper.Label)).Cast<LabelHelper.Label>())
                {
                    //Debug.Log($"Trying to resolve {label} in {language}.");
                    var test = LabelHelper.ResolveLabel(label);
                    Assert.IsTrue(!string.IsNullOrEmpty(test), $"Label {label} is null or empty for language {language}.");
                }
            }
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator LabelHelperUnitTestsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
