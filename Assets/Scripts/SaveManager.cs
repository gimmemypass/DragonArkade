using System.IO;
using Components;
using UnityEngine;

namespace HECSFramework.Unity
{
    public partial class SaveManager
    {
        public static void ClearSaybooSaves()
        {
            var saveFiles = Directory.GetFiles(SavePathProvider.SavesPath);
            foreach (var saveFile in saveFiles)
            {
                File.Delete(saveFile);
            }
            
            Debug.Log("Удалили сейвы");
        }
    }
}