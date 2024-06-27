using System;
using HECSFramework.Core;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "simple scene manager")]
    public sealed class SceneManagerSystem : BaseSystem
    {
        // private SceneInstance? scene;
        private string loadedScene;

        public override void InitSystem()
        {
        }

        // public async UniTask LoadScene(AssetReference sceneRef)
        // {
        //     if (scene != null)
        //         await Addressables.UnloadSceneAsync(scene.Value);
        //     scene = await sceneRef.LoadSceneAsync(LoadSceneMode.Additive);
        // } 
        public async UniTask LoadScene(string sceneName)
        {
            if (loadedScene != null)
                SceneManager.UnloadSceneAsync(loadedScene);
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).ToUniTask();
            loadedScene = sceneName;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(loadedScene));
        } 
    }
}