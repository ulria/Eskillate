using System;
using System.IO;

namespace LabelGenerator
{
    [Serializable]
    class LabelClass
    {
        public string Label;
    }

    class Program
    {
        private const string LABEL_FILE_NAME = "tets";
        private const string FR_TEXT = "test";
        private const string EN_TEXT = "test";
        private const string ES_TEXT = "test";
        private const string PATH_TO_LABEL_FOLDER = "../../Assets/Resources/Core/Labels/";

        private static void WriteFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        static void Main(string[] args)
        {
            var esFileFullPath = PATH_TO_LABEL_FOLDER + "en/" + LABEL_FILE_NAME + ".json";
            if(File.Exists(esFileFullPath))
            {
                Console.WriteLine($"Label {LABEL_FILE_NAME} already exists for espanol.");
            }
            else
            {
                var jsonElem = new LabelClass()
                {
                    Label = ES_TEXT
                };
                var content = UnityEngine.JsonUtility.ToJson(jsonElem);
                WriteFile(esFileFullPath, content);
            }
        }
    }
}
