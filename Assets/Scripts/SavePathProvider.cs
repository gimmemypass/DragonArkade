using System;
using System.IO;
using HECSFramework.Unity;
using HECSFramework.Core;
using UnityEngine;

namespace Components
{
    [Serializable]
    [Documentation(Doc.Config, "provide save paths")]
    public static class SavePathProvider
    {
        public static string PlayerSavePath => $"{SavesPath}/playerSave.json";
        public static string GetSavePath(string name) => $"{SavesPath}/{name}.json";
        public static string SavesPath => Path.Combine(ApplicationPath, SavesFolderName);

        private static string SavesFolderName => "Saves";
        private static string ApplicationPath => Application.persistentDataPath;
        
        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            if (!Directory.Exists(SavesPath))
                Directory.CreateDirectory(SavesPath);
        }
    }
}