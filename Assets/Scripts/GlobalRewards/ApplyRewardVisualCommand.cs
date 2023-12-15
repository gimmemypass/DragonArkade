using HECSFramework.Core;
using UnityEngine;

namespace Commands
{
    [Documentation(Doc.Rewards,Doc.Visual, "Command for applying visual of reward")]
    public struct ApplyRewardVisualCommand : IGlobalCommand
    {
        public int CounterId;
        public int RewardAmount;
        public bool Delayed;
        public int DelayedStateId;
        public bool HideFX;
        public bool ShowCounterUIEffectOnPlayer; 
        public bool ShowCounterUIEffectOnCenterScreen;
        public int CanvasId;
        
        //this is entity whom send this reward, its can be tile or game module
        public Entity Sender;

        public AdditionalContext? AdditionalContext;  
    }

    public struct AdditionalContext
    {
        public Entity ViewToDraw;

        public int? CanvasId;
        public Vector3? From;
        public Vector3? To;
        public Vector3? Size;
    }
}