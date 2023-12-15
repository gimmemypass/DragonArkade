using System.IO;

namespace HECSFramework.Unity
{
    public partial class SaveManager
    {
        public static bool TryLoadJson(string path, out string json)
        {
            if (File.Exists(path))
            {
                json = File.ReadAllText(path);
                return true;
            }
            json = default;
            return false;
        }

        public static void SaveJson(string path, string json)
        {
            File.WriteAllText(path, json);
        }
    }
}
