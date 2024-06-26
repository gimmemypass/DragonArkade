using HECSFramework.Core;

namespace Strategies
{
    [DrawInNode]
    public class DebugDecision : InterDecision
    {
        public bool IsOnline = true;
        public bool PrintEntityGUID = false;
        public bool PauseApplication;


        public string DebugMessage = "Debug";
        public override string TitleOfNode { get; } = "Debug Node";

        protected override void Run(Entity entity)
        {
#if UNITY_EDITOR
            if (IsOnline)
            {
                if (PrintEntityGUID)
                    HECSDebug.LogWarning($"{entity.GUID.ToString()} -- {DebugMessage}");
                else
                    HECSDebug.LogWarning($"{DebugMessage}");
            }

            if (PauseApplication)
                UnityEditor.EditorApplication.isPaused = true;
#endif
            Next.Execute(entity);
        }
    }
}