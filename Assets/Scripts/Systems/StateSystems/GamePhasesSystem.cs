using System;
using Commands;
using HECSFramework.Core;

namespace Systems
{
    [Serializable]
    [Documentation(Doc.GameLogic, "handle game phases")]
    public sealed class GamePhasesSystem : BaseMainGameLogicSystem, ILateStart
    {
        public override void InitSystem()
        {
        }

        public override void GlobalStart()
        {
        }

        public void LateStart()
        {
            ChangeGameState(GameStateIdentifierMap.StartMenu); 
//            ChangeGameState(GameStateIdentifierMap.PrepareArena); 
        }

        protected override void ProcessEndState(EndGameStateCommand endGameStateCommand)
        {
            switch (endGameStateCommand.GameState)
            {
                case GameStateIdentifierMap.PrepareBattle:
                    ChangeGameState(GameStateIdentifierMap.PrepareWave);
                    break;
                case GameStateIdentifierMap.PrepareWave:
                    ChangeGameState(GameStateIdentifierMap.BattleState);
                    break;
                case GameStateIdentifierMap.PrepareArena:
                    ChangeGameState(GameStateIdentifierMap.Arena);
                    break;
                case GameStateIdentifierMap.Arena:
                    ChangeGameState(GameStateIdentifierMap.FinishArena);
                    break;
            }
        }
    }
}