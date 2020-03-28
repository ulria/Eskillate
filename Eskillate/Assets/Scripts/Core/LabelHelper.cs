
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

public static class LabelHelper
{
    public enum Label
    {
        BackToMainMenuButton,
        DragAndDropDescription,
        DragAndDropLevel1Description,
        DragAndDropLevel1Name,
        DragAndDropName,
        GenericLevelPlayButton,
        HideAndSeekDescription,
        HideAndSeekName,
        LevelSelectionButton,
        LowPopDescription,
        LowPopDirective,
        LowPopLevel1Description,
        LowPopLevel1Name,
        LowPopLevel2Description,
        LowPopLevel2Name,
        LowPopName,
        LowPopTutorialDescription,
        LowPopTutorialInitialDirectives,
        LowPopTutorialName,
        MainMenuOptionButton,
        MainMenuPlayButton,
        MainMenuQuitButton,
        OptionsMenuBackButton,
        OptionsMenuTitle,
        OptionsMenuVolume,
        PerilousPathDescription,
        PerilousPathName,
        ReproduceSequenceDescription,
        ReproduceSequenceName,
        ReproduceShapeDescription,
        ReproduceShapeName,
        SortDescription,
        SortName,
        TrueColorDescription,
        TrueColorName,
    }

    public enum Language
    { 
        French,
        English,
        Spanish
    }

    private static readonly string _labelFileNameFormat = "Assets\\Resources\\Core\\Labels\\{0}\\{1}.json";
    private static Dictionary<Language, string> _prefixes = new Dictionary<Language, string>()
    {
        { Language.French, "fr" },
        { Language.English, "en" },
        { Language.Spanish, "es" }
    };
    private static Dictionary<Label, string> _labelFileNames = new Dictionary<Label, string>()
    {
        { Label.BackToMainMenuButton, "BackToMainMenuButton" },
        { Label.DragAndDropDescription, "DragAndDropDescription" },
        { Label.DragAndDropLevel1Description, "DragAndDropLevel1Description" },
        { Label.DragAndDropLevel1Name, "DragAndDropLevel1Name" },
        { Label.DragAndDropName, "DragAndDropName" },
        { Label.GenericLevelPlayButton, "GenericLevelPlayButton" },
        { Label.HideAndSeekDescription, "HideAndSeekDescription" },
        { Label.HideAndSeekName, "HideAndSeekName" },
        { Label.LevelSelectionButton, "LevelSelectionButton"},
        { Label.LowPopDescription, "LowPopDescription"},
        { Label.LowPopDirective, "LowPopDirective"},
        { Label.LowPopLevel1Description, "LowPopLevel1Description" },
        { Label.LowPopLevel1Name, "LowPopLevel1Name" },
        { Label.LowPopLevel2Description, "LowPopLevel2Description" },
        { Label.LowPopLevel2Name, "LowPopLevel2Name" },
        { Label.LowPopName, "LowPopName"},
        { Label.LowPopTutorialDescription, "LowPopTutorialDescription"},
        { Label.LowPopTutorialInitialDirectives, "LowPopTutorialInitialDirectives"},
        { Label.LowPopTutorialName, "LowPopTutorialName"},
        { Label.MainMenuOptionButton, "MainMenuOptionButton"},
        { Label.MainMenuPlayButton, "MainMenuPlayButton"},
        { Label.MainMenuQuitButton, "MainMenuQuitButton"},
        { Label.OptionsMenuBackButton, "OptionsMenuBackButton"},
        { Label.OptionsMenuTitle, "OptionsMenuTitle"},
        { Label.OptionsMenuVolume, "OptionsMenuVolume"},
        { Label.PerilousPathDescription, "PerilousPathDescription"},
        { Label.PerilousPathName, "PerilousPathName"},
        { Label.ReproduceSequenceDescription, "ReproduceSequenceDescription"},
        { Label.ReproduceSequenceName, "ReproduceSequenceName"},
        { Label.ReproduceShapeDescription, "ReproduceShapeDescription"},
        { Label.ReproduceShapeName, "ReproduceShapeName"},
        { Label.SortDescription, "SortDescription"},
        { Label.SortName, "SortName"},
        { Label.TrueColorDescription, "TrueColorDescription"},
        { Label.TrueColorName, "TrueColorName"},
    };

    private static string _currentLanguagePrefix = _prefixes[Language.English];

    public static void ChangeLanguage(Language language)
    {
        var intsoa = (int)language;
        PlayerPrefs.SetInt("Language", (int) language);
        _currentLanguagePrefix = _prefixes[language];
    }

    public static string ResolveLabel(Label label)
    {
        var fileName = string.Format(_labelFileNameFormat, _currentLanguagePrefix, label);
        return ReadLabel(fileName);
    }

    private static string ReadLabel(string fileName)
    {
        var json = File.ReadAllText(fileName);
        var label = JsonUtility.FromJson<LabelClass>(json);
        return label.Label;        
    }

    public class LabelClass
    {
        public string Label;
    }
}
