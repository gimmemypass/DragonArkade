using Commands;
using Components;
using HECSFramework.Core;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace HECSFramework.Unity.Editor
{
    public class CheatWindow : OdinEditorWindow
    {
        public int Level = 4;
        public int Money = 1000;
        [MenuItem("Say10/CheatWindow")]
        public static void OpenWindow()
        {
            CreateWindow<CheatWindow>();
        }

        [Button]
        public void AddSoft()
        {
            EntityManager.Default.Command(new ApplyRewardVisualCommand()
            {
                CounterId = CounterIdentifierContainerMap.SoftValue,
                RewardAmount = Money,
                Delayed = false,
                Sender = null,
                CanvasId = AdditionalCanvasIdentifierMap.TopCanvas,
                DelayedStateId = 0,
                HideFX = false,
            }); 
        }
        
        [Button]
        public void SetLevel()
        {
            EntityManager.Default.GetSingleComponent<PlayerLevelComponent>().Level = Level;
        }
    }
}