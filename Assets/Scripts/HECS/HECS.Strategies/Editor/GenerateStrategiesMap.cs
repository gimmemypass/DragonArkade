using System.IO;
using System.Linq;
using HECSFramework.Core.Generator;
using HECSFramework.Unity.Editor;
using Strategies;
using UnityEditor;
using UnityEngine;

namespace HECSFramework.Unity.HECSFramework
{
    public class GenerateStrategiesMap : UnityEditor.Editor
    {
        private const string MAP_FILE_NAME = "StrategiesMap.cs";

        [MenuItem("HECS Options/Generate Strategies Map")]
        public static void GenerateMap()
        {
            var strategies = AssetDatabase.FindAssets("t:Strategy")
                .Select(x => AssetDatabase.GUIDToAssetPath(x))
                .Select(x => AssetDatabase.LoadAssetAtPath<Strategy>(x)).ToList();

            var tree = new TreeSyntaxNode();
            var body = new TreeSyntaxNode();
            tree.Add(new SimpleSyntax($"public static class StrategiesMap" + CParse.Paragraph));
            tree.Add(new LeftScopeSyntax());
            tree.Add(body);
            tree.Add(new RightScopeSyntax());

            foreach (var strategy in strategies)
            {
                body.Add(new TabSimpleSyntax(1, $"public static int {strategy.name} => {strategy.StrategyIndex.ToString()};"));
                body.Add(new TabSimpleSyntax(1, $"public static string {strategy.name}_string => {CParse.Quote}{strategy.name}{CParse.Quote};"));
            }

            string map = tree.ToString();
            SaveToFile(map);
        }

        private static void SaveToFile(string data)
        {
            var find = Directory.GetFiles(Application.dataPath, MAP_FILE_NAME, SearchOption.AllDirectories);
            var pathToDirectory = InstallHECS.ScriptPath + InstallHECS.HECSGenerated;
            var path = pathToDirectory + MAP_FILE_NAME;
            if (find.Length > 0 && !string.IsNullOrEmpty(find[0]))
                path = find[0];

            try
            {
                if (!Directory.Exists(pathToDirectory))
                    Directory.CreateDirectory(pathToDirectory);
                File.WriteAllText(path, data);
                var sourceFile2 = path.Replace(Application.dataPath, "Assets");
                AssetDatabase.ImportAsset(sourceFile2);
            }
            catch
            {
                Debug.LogError($"Cannot write {pathToDirectory}");
            }
        }
    }
}