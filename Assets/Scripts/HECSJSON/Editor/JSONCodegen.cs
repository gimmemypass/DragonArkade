#if UNITY_EDITOR
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
#pragma warning disable

[InitializeOnLoad]
public class JSONCodegen : OdinEditorWindow
{
    static JSONCodegen()
    {
        try
        {
            if (PlayerPrefs.HasKey(nameof(ClientScriptDirectory)))
            {
                if (!string.IsNullOrEmpty(EditorPrefs.GetString(nameof(ClientScriptDirectory))))
                    return;
            }

            PlayerPrefs.SetString(nameof(ClientScriptDirectory), Application.dataPath);


            if (PlayerPrefs.HasKey(nameof(JSONCodegenExePath)))
            {
                if (!string.IsNullOrEmpty(EditorPrefs.GetString(nameof(JSONCodegenExePath))))
                    return;
            }

            var find = Directory.GetFiles(Application.dataPath, "JSONCodogen.exe", SearchOption.AllDirectories);

            if (find != null && find.Length > 0 && !string.IsNullOrEmpty(find[0]))
                PlayerPrefs.SetString(nameof(JSONCodegenExePath), find[0]);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Юнити шалит, попробуйте переоткрыть окно или юнити");
        }
    }

    [Sirenix.OdinInspector.FilePath(AbsolutePath = true)]
    [OnInspectorInit("@JSONCodegenExePath")]
    public string JSONCodegenExePath
    {
        get => PlayerPrefs.GetString(nameof(JSONCodegenExePath), "");
        set => PlayerPrefs.SetString(nameof(JSONCodegenExePath), value);
    }

    [FolderPath(AbsolutePath = true)]
    [OnInspectorInit("@ClientScriptDirectory")]
    public string ClientScriptDirectory
    {
        get => PlayerPrefs.GetString(nameof(ClientScriptDirectory), "");
        set => PlayerPrefs.SetString(nameof(ClientScriptDirectory), value);
    }

    [FolderPath(AbsolutePath = true)]
    [OnInspectorInit("@ServerScriptDirectory")]
    public string ServerScriptDirectory
    {
        get => PlayerPrefs.GetString(nameof(ServerScriptDirectory), "");
        set => PlayerPrefs.SetString(nameof(ServerScriptDirectory), value);
    }

    [MenuItem("HECS Options/JSONSerialize Codegen", priority = 0)]
    public static void RoslynCodegenMenu()
        => GetWindow<JSONCodegen>();

    [Button]
    public async void CodegenClient()
        => await Generate($"path:{ClientScriptDirectory} {ClientArguments()}", false);

    [Button]
    public async void CodegenServer()
        => await Generate($"path:{ServerScriptDirectory} server no_blueprints", true);

    [Button]
    public void CodegenAll()
    {
        CodegenServer();
        CodegenClient();
    }

    public async Task CodegenAsync()
    {
        await Generate($"path:{ServerScriptDirectory} server no_blueprints", true);
        await Generate($"path:{ClientScriptDirectory} {ClientArguments()}", false);
    }

    private string ClientArguments()
    {
        string args = string.Empty;
        return args;
    }

    private async Task Generate(string args, bool isServer)
    {
        Debug.Log("Generating Roslyn files...");
        Process myProcess = new Process
        {
            StartInfo =
            {
                FileName = JSONCodegenExePath,
                Arguments = args,
                WorkingDirectory = JSONCodegenExePath
            },
            EnableRaisingEvents = true
        };
        myProcess.Start();

        if (isServer) return;

        Debug.Log("JSON Generation complete");
        EditorApplication.UnlockReloadAssemblies();
    }
}
#endif