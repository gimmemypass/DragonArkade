using HECSFramework.Core;
using UnityEditor;

namespace HECSFramework.Unity.Editor
{
    public class SavesEditorManager
    {
        [MenuItem("Say10/Saves/Clear Saves")]
        public static void DeleteSaves()
        {
            SaveManager.ClearSaybooSaves();

            HECSDebug.Log("Saves cleared");
        }
    }

}