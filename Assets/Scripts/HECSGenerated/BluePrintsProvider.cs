using Components;using System;using Systems;using System.Collections.Generic;namespace HECSFramework.Unity{	public partial class BluePrintsProvider	{		public BluePrintsProvider()		{		Components = new Dictionary<Type, Type>		{			 { typeof(UntilSuccessStrategyNodeComponent), typeof(UntilSuccessStrategyNodeComponentBluePrint) },			 { typeof(StrategySideExecuteComponent), typeof(StrategySideExecuteComponentBluePrint) },			 { typeof(ViewReferenceComponent), typeof(ViewReferenceComponentBluePrint) },			 { typeof(AbilityPredicateComponent), typeof(AbilityPredicateComponentBluePrint) },			 { typeof(StickTagComponent), typeof(StickTagComponentBluePrint) },			 { typeof(RadiusComponent), typeof(RadiusComponentBluePrint) },			 { typeof(TestReactComponent), typeof(TestReactComponentBluePrint) },			 { typeof(IsDeadTagComponent), typeof(IsDeadTagComponentBluePrint) },			 { typeof(AbilityOwnerComponent), typeof(AbilityOwnerComponentBluePrint) },			 { typeof(RotationComponent), typeof(RotationComponentBluePrint) },			 { typeof(PlayerLevelComponent), typeof(PlayerLevelComponentBluePrint) },			 { typeof(NeedHpBarComponent), typeof(NeedHpBarComponentBluePrint) },			 { typeof(FactionComponent), typeof(FactionComponentBluePrint) },			 { typeof(NearestDropItemsComponent), typeof(NearestDropItemsComponentBluePrint) },			 { typeof(EnemyTagComponent), typeof(EnemyTagComponentBluePrint) },			 { typeof(CollisionsComponent), typeof(CollisionsComponentBluePrint) },			 { typeof(AIStrategyComponent), typeof(AIStrategyComponentBluePrint) },			 { typeof(TestSerializationComponent), typeof(TestSerializationComponentBluePrint) },			 { typeof(CountersHolderComponent), typeof(CountersHolderComponentBluePrint) },			 { typeof(SpawnDropItemsComponent), typeof(SpawnDropItemsComponentBluePrint) },			 { typeof(DirectionComponent), typeof(DirectionComponentBluePrint) },			 { typeof(ChoosingPushDirectionSphereAbilityComponent), typeof(ChoosingPushDirectionSphereAbilityComponentBluePrint) },			 { typeof(ItemsGlobalHolderComponent), typeof(ItemsGlobalHolderComponentBluePrint) },			 { typeof(PlayerUpgradeComponent), typeof(PlayerUpgradeComponentBluePrint) },			 { typeof(CardTagComponent), typeof(CardTagComponentBluePrint) },			 { typeof(VisualInActionTagComponent), typeof(VisualInActionTagComponentBluePrint) },			 { typeof(ExplodeSpheresAbilityComponent), typeof(ExplodeSpheresAbilityComponentBluePrint) },			 { typeof(MainCameraComponent), typeof(MainCameraComponentBluePrint) },			 { typeof(IconComponent), typeof(IconComponentBluePrint) },			 { typeof(BelongingComponent), typeof(BelongingComponentBluePrint) },			 { typeof(UITagComponent), typeof(UITagComponentBluePrint) },			 { typeof(OverrideAnimatorComponent), typeof(OverrideAnimatorComponentBluePrint) },			 { typeof(AbilitiesHolderComponent), typeof(AbilitiesHolderComponentBluePrint) },			 { typeof(UnityTransformComponent), typeof(UnityTransformComponentBluePrint) },			 { typeof(UIPriorityIndexComponent), typeof(UIPriorityIndexComponentBluePrint) },			 { typeof(PoolableViewsProviderComponent), typeof(PoolableViewsProviderComponentBluePrint) },			 { typeof(InputActionsComponent), typeof(InputActionsComponentBluePrint) },			 { typeof(IgnoreReferenceContainerTagComponent), typeof(IgnoreReferenceContainerTagComponentBluePrint) },			 { typeof(CharacterItemsComponent), typeof(CharacterItemsComponentBluePrint) },			 { typeof(CheckTwoComponent), typeof(CheckTwoComponentBluePrint) },			 { typeof(UnityRectTransformComponent), typeof(UnityRectTransformComponentBluePrint) },			 { typeof(TransformComponent), typeof(TransformComponentBluePrint) },			 { typeof(PredicatesComponent), typeof(PredicatesComponentBluePrint) },			 { typeof(GameLogicTagComponent), typeof(GameLogicTagComponentBluePrint) },			 { typeof(AutoApplyItemsWhenStayComponent), typeof(AutoApplyItemsWhenStayComponentBluePrint) },			 { typeof(SplashScreenUIComponent), typeof(SplashScreenUIComponentBluePrint) },			 { typeof(StateTimerComponent), typeof(StateTimerComponentBluePrint) },			 { typeof(SpawnPointComponent), typeof(SpawnPointComponentBluePrint) },			 { typeof(HealAbilityComponent), typeof(HealAbilityComponentBluePrint) },			 { typeof(FinalLevelScreenComponent), typeof(FinalLevelScreenComponentBluePrint) },			 { typeof(VisualFXHolderComponent), typeof(VisualFXHolderComponentBluePrint) },			 { typeof(TargetPositionComponent), typeof(TargetPositionComponentBluePrint) },			 { typeof(LevelsHolderComponent), typeof(LevelsHolderComponentBluePrint) },			 { typeof(InvincibleComponent), typeof(InvincibleComponentBluePrint) },			 { typeof(MainCanvasTagComponent), typeof(MainCanvasTagComponentBluePrint) },			 { typeof(UIBusyTagComponent), typeof(UIBusyTagComponentBluePrint) },			 { typeof(AdditionalCanvasTagComponent), typeof(AdditionalCanvasTagComponentBluePrint) },			 { typeof(ScenarioAnimationComponent), typeof(ScenarioAnimationComponentBluePrint) },			 { typeof(StateInfoComponent), typeof(StateInfoComponentBluePrint) },			 { typeof(StateContextComponent), typeof(StateContextComponentBluePrint) },			 { typeof(GameStateComponent), typeof(GameStateComponentBluePrint) },			 { typeof(SoundVolumeComponent), typeof(SoundVolumeComponentBluePrint) },			 { typeof(CachedEntitiesGlobalHolderComponent), typeof(CachedEntitiesGlobalHolderComponentBluePrint) },			 { typeof(SpeedUpSphereComponent), typeof(SpeedUpSphereComponentBluePrint) },			 { typeof(ShowHpBarTagComponent), typeof(ShowHpBarTagComponentBluePrint) },			 { typeof(PlayerTagComponent), typeof(PlayerTagComponentBluePrint) },			 { typeof(CharactersHolderComponent), typeof(CharactersHolderComponentBluePrint) },			 { typeof(CenterTagComponent), typeof(CenterTagComponentBluePrint) },			 { typeof(AnimationCheckOutsHolderComponent), typeof(AnimationCheckOutsHolderComponentBluePrint) },			 { typeof(AbilityTagComponent), typeof(AbilityTagComponentBluePrint) },			 { typeof(ViewReferenceGameObjectComponent), typeof(ViewReferenceGameObjectComponentBluePrint) },			 { typeof(ViewDestructionDelayedComponent), typeof(ViewDestructionDelayedComponentBluePrint) },			 { typeof(UIGroupTagComponent), typeof(UIGroupTagComponentBluePrint) },			 { typeof(PoolableTagComponent), typeof(PoolableTagComponentBluePrint) },			 { typeof(OnApplicationQuitTagComponent), typeof(OnApplicationQuitTagComponentBluePrint) },			 { typeof(NetworkEntityTagComponent), typeof(NetworkEntityTagComponentBluePrint) },			 { typeof(ItemTagComponent), typeof(ItemTagComponentBluePrint) },			 { typeof(AfterLifeTagComponent), typeof(AfterLifeTagComponentBluePrint) },			 { typeof(CacheCounterValuesComponent), typeof(CacheCounterValuesComponentBluePrint) },			 { typeof(ActorContainerID), typeof(ActorContainerIDBluePrint) },			 { typeof(ActorProviderComponent), typeof(ActorProviderComponentBluePrint) },			 { typeof(SavePositionComponent), typeof(SavePositionComponentBluePrint) },			 { typeof(TargetEntityComponent), typeof(TargetEntityComponentBluePrint) },			 { typeof(UpgradeWindowComponent), typeof(UpgradeWindowComponentBluePrint) },			 { typeof(TimeScaleComponent), typeof(TimeScaleComponentBluePrint) },			 { typeof(PushDirectionComponent), typeof(PushDirectionComponentBluePrint) },			 { typeof(OnStayComponent), typeof(OnStayComponentBluePrint) },			 { typeof(CardsGlobalHolderComponent), typeof(CardsGlobalHolderComponentBluePrint) },			 { typeof(ViewReadyTagComponent), typeof(ViewReadyTagComponentBluePrint) },			 { typeof(SetupAfterViewTagComponent), typeof(SetupAfterViewTagComponentBluePrint) },			 { typeof(UIViewReferenceComponent), typeof(UIViewReferenceComponentBluePrint) },			 { typeof(TimeProviderComponent), typeof(TimeProviderComponentBluePrint) },			 { typeof(NavMeshAgentComponent), typeof(NavMeshAgentComponentBluePrint) },			 { typeof(InputOverUIComponent), typeof(InputOverUIComponentBluePrint) },			 { typeof(InputListenerTagComponent), typeof(InputListenerTagComponentBluePrint) },			 { typeof(SoftCurrencyRewardVisualConfigComponent), typeof(SoftCurrencyRewardVisualConfigComponentBluePrint) },			 { typeof(HealthBarsManagerComponent), typeof(HealthBarsManagerComponentBluePrint) },			 { typeof(PegasusMovementComponent), typeof(PegasusMovementComponentBluePrint) },			 { typeof(MainCharacterTagComponent), typeof(MainCharacterTagComponentBluePrint) },			 { typeof(LevelModifierHolderComponent), typeof(LevelModifierHolderComponentBluePrint) },			 { typeof(BlockingAbilityInActionComponent), typeof(BlockingAbilityInActionComponentBluePrint) },			 { typeof(ArenaFinishComponent), typeof(ArenaFinishComponentBluePrint) },			 { typeof(DropItemComponent), typeof(DropItemComponentBluePrint) },			 { typeof(CameraComponent), typeof(CameraComponentBluePrint) },			 { typeof(SlowdownComponent), typeof(SlowdownComponentBluePrint) },			 { typeof(CustomHpBarComponent), typeof(CustomHpBarComponentBluePrint) },			 { typeof(ArenaBattleGlobalConfigComponent), typeof(ArenaBattleGlobalConfigComponentBluePrint) },			 { typeof(SphereCastContext), typeof(SphereCastContextBluePrint) },			 { typeof(OverlapSphereCastContext), typeof(OverlapSphereCastContextBluePrint) },			 { typeof(StateDataComponent), typeof(StateDataComponentBluePrint) },			 { typeof(PassiveAbilityTag), typeof(PassiveAbilityTagBluePrint) },			 { typeof(AdditionalAbilityIndexComponent), typeof(AdditionalAbilityIndexComponentBluePrint) },			 { typeof(WaitStateComponent), typeof(WaitStateComponentBluePrint) },			 { typeof(AnimatorStateComponent), typeof(AnimatorStateComponentBluePrint) },			 { typeof(SphereComponent), typeof(SphereComponentBluePrint) },			 { typeof(SpeedDependsOnTimeScaleComponent), typeof(SpeedDependsOnTimeScaleComponentBluePrint) },			 { typeof(ScenesHolderComponent), typeof(ScenesHolderComponentBluePrint) },			 { typeof(NeedDefaultDamageTextVisualizerComponent), typeof(NeedDefaultDamageTextVisualizerComponentBluePrint) },			 { typeof(ComicsComponent), typeof(ComicsComponentBluePrint) },			 { typeof(DamageTextVisualizerComponent), typeof(DamageTextVisualizerComponentBluePrint) },			 { typeof(InitOnAddAbilityTagComponent), typeof(InitOnAddAbilityTagComponentBluePrint) },			 { typeof(TestInitComponent), typeof(TestInitComponentBluePrint) },			 { typeof(AfterLifeCompleteTagComponent), typeof(AfterLifeCompleteTagComponentBluePrint) },			 { typeof(AppVersionComponent), typeof(AppVersionComponentBluePrint) },			 { typeof(SpheresControllerTagComponent), typeof(SpheresControllerTagComponentBluePrint) },			 { typeof(EnergyRegenerationComponent), typeof(EnergyRegenerationComponentBluePrint) },			 { typeof(SoftCurrencyCounterMonoComponentProviderComponent), typeof(SoftCurrencyCounterMonoComponentProviderComponentBluePrint) },			 { typeof(RigidbodyProviderComponent), typeof(RigidbodyProviderComponentBluePrint) },			 { typeof(CharacterItemsSourceMonoComponentProvider), typeof(CharacterItemsSourceMonoComponentProviderBluePrint) },			 { typeof(HealthBarPlaceMonoComponentProvider), typeof(HealthBarPlaceMonoComponentProviderBluePrint) },			 { typeof(RendererProviderComponent), typeof(RendererProviderComponentBluePrint) },			 { typeof(ActionsWithPredicateHolderComponent), typeof(ActionsWithPredicateHolderComponentBluePrint) },			 { typeof(SoftValueCounterComponent), typeof(SoftValueCounterComponentBluePrint) },			 { typeof(ActionsHolderComponent), typeof(ActionsHolderComponentBluePrint) },			 { typeof(AgilityComponent), typeof(AgilityComponentBluePrint) },			 { typeof(EnergyComponent), typeof(EnergyComponentBluePrint) },			 { typeof(CooldownComponent), typeof(CooldownComponentBluePrint) },			 { typeof(DamageComponent), typeof(DamageComponentBluePrint) },			 { typeof(SpeedComponent), typeof(SpeedComponentBluePrint) },			 { typeof(HealthComponent), typeof(HealthComponentBluePrint) },			 { typeof(TestComponent), typeof(TestComponentBluePrint) },			 { typeof(TestWorldSingleComponent), typeof(TestWorldSingleComponentBluePrint) },		};		Systems = new Dictionary<Type, Type>		{			 { typeof(UpdateActorByTranformSystem), typeof(UpdateActorByTranformSystemBluePrint) },			 { typeof(InputOverUISystem), typeof(InputOverUISystemBluePrint) },			 { typeof(AwaitersUpdateSystem), typeof(AwaitersUpdateSystemBluePrint) },			 { typeof(ArenaBattleUIWindow), typeof(ArenaBattleUIWindowBluePrint) },			 { typeof(MoveMainCharacterSystem), typeof(MoveMainCharacterSystemBluePrint) },			 { typeof(SpeedUpSphereSystem), typeof(SpeedUpSphereSystemBluePrint) },			 { typeof(IndicateDamageSystem), typeof(IndicateDamageSystemBluePrint) },			 { typeof(StressTestReactsSystem), typeof(StressTestReactsSystemBluePrint) },			 { typeof(DamageTextVisualizerManagerSystem), typeof(DamageTextVisualizerManagerSystemBluePrint) },			 { typeof(CollideItemSystem), typeof(CollideItemSystemBluePrint) },			 { typeof(SpawnDropItemsSystem), typeof(SpawnDropItemsSystemBluePrint) },			 { typeof(IterateAttackAnimationSystem), typeof(IterateAttackAnimationSystemBluePrint) },			 { typeof(WaitingCommandsSystems), typeof(WaitingCommandsSystemsBluePrint) },			 { typeof(RemoveComponentWorldSystem), typeof(RemoveComponentWorldSystemBluePrint) },			 { typeof(ApplyAimedItemSystem), typeof(ApplyAimedItemSystemBluePrint) },			 { typeof(UpgradeVisualMainCharacterSystem), typeof(UpgradeVisualMainCharacterSystemBluePrint) },			 { typeof(DrawHP_UISystem), typeof(DrawHP_UISystemBluePrint) },			 { typeof(CardsControllerUISystem), typeof(CardsControllerUISystemBluePrint) },			 { typeof(MainCharLevelingSystem), typeof(MainCharLevelingSystemBluePrint) },			 { typeof(PegasusMovementSystem), typeof(PegasusMovementSystemBluePrint) },			 { typeof(ItemThrowingSystem), typeof(ItemThrowingSystemBluePrint) },			 { typeof(ComicsWindowSystem), typeof(ComicsWindowSystemBluePrint) },			 { typeof(HealthSystem), typeof(HealthSystemBluePrint) },			 { typeof(FlyDragonAnimationSystem), typeof(FlyDragonAnimationSystemBluePrint) },			 { typeof(LoseScreenUISystem), typeof(LoseScreenUISystemBluePrint) },			 { typeof(MultipleCollectItemsSystem), typeof(MultipleCollectItemsSystemBluePrint) },			 { typeof(ItemMovementSystem), typeof(ItemMovementSystemBluePrint) },			 { typeof(HandleCooldownGlobalSystem), typeof(HandleCooldownGlobalSystemBluePrint) },			 { typeof(FireballItemVisualSystem), typeof(FireballItemVisualSystemBluePrint) },			 { typeof(DestroyDeadEntitySystem), typeof(DestroyDeadEntitySystemBluePrint) },			 { typeof(SphereCollisionsHolderSystem), typeof(SphereCollisionsHolderSystemBluePrint) },			 { typeof(StickInputSystem), typeof(StickInputSystemBluePrint) },			 { typeof(CountersHolderSystem), typeof(CountersHolderSystemBluePrint) },			 { typeof(UpdateTranformFromActorSystem), typeof(UpdateTranformFromActorSystemBluePrint) },			 { typeof(StartSystem), typeof(StartSystemBluePrint) },			 { typeof(ActorAfterViewSystem), typeof(ActorAfterViewSystemBluePrint) },			 { typeof(SaveLoadSystem), typeof(SaveLoadSystemBluePrint) },			 { typeof(MainCharacterHealthUISystem), typeof(MainCharacterHealthUISystemBluePrint) },			 { typeof(VFXCreationSystem), typeof(VFXCreationSystemBluePrint) },			 { typeof(MoveEnemyToSpawnPointSystem), typeof(MoveEnemyToSpawnPointSystemBluePrint) },			 { typeof(RotateToTargetSystem), typeof(RotateToTargetSystemBluePrint) },			 { typeof(DestroyAfterDeadEndAnimationEventSystem), typeof(DestroyAfterDeadEndAnimationEventSystemBluePrint) },			 { typeof(MainCharAttackSystem), typeof(MainCharAttackSystemBluePrint) },			 { typeof(EnergyUISystem), typeof(EnergyUISystemBluePrint) },			 { typeof(DropPartsHealthVisualSystem), typeof(DropPartsHealthVisualSystemBluePrint) },			 { typeof(HealthBarsManagerSystem), typeof(HealthBarsManagerSystemBluePrint) },			 { typeof(FinalScreenUISystem), typeof(FinalScreenUISystemBluePrint) },			 { typeof(GlobalRewardsVisualSystem), typeof(GlobalRewardsVisualSystemBluePrint) },			 { typeof(SplashScreenUISystem), typeof(SplashScreenUISystemBluePrint) },			 { typeof(AssetsServiceSystem), typeof(AssetsServiceSystemBluePrint) },			 { typeof(AdditionalCanvasesSystem), typeof(AdditionalCanvasesSystemBluePrint) },			 { typeof(LevelBattleUISystem), typeof(LevelBattleUISystemBluePrint) },			 { typeof(EnergyRegenerationSystem), typeof(EnergyRegenerationSystemBluePrint) },			 { typeof(AnimationDoneCheckOutSystem), typeof(AnimationDoneCheckOutSystemBluePrint) },			 { typeof(StateUpdateSystem), typeof(StateUpdateSystemBluePrint) },			 { typeof(PoolingSystem), typeof(PoolingSystemBluePrint) },			 { typeof(InjectComponentsFromActorSystem), typeof(InjectComponentsFromActorSystemBluePrint) },			 { typeof(AbilitiesSystem), typeof(AbilitiesSystemBluePrint) },			 { typeof(SphereMovementSystem), typeof(SphereMovementSystemBluePrint) },			 { typeof(SphereDamageSystem), typeof(SphereDamageSystemBluePrint) },			 { typeof(InitSpawnPointSystem), typeof(InitSpawnPointSystemBluePrint) },			 { typeof(StickFollowSystem), typeof(StickFollowSystemBluePrint) },			 { typeof(ReleaseActorAssetReferenceSystem), typeof(ReleaseActorAssetReferenceSystemBluePrint) },			 { typeof(HideUISystem), typeof(HideUISystemBluePrint) },			 { typeof(SpawnViewSystem), typeof(SpawnViewSystemBluePrint) },			 { typeof(InputListenSystem), typeof(InputListenSystemBluePrint) },			 { typeof(AINPCSystem), typeof(AINPCSystemBluePrint) },			 { typeof(StartMenuUISystem), typeof(StartMenuUISystemBluePrint) },			 { typeof(FindTargetEntitySystem), typeof(FindTargetEntitySystemBluePrint) },			 { typeof(DisableCollidersWhenDeadSystem), typeof(DisableCollidersWhenDeadSystemBluePrint) },			 { typeof(DamageTextVisualizerSystem), typeof(DamageTextVisualizerSystemBluePrint) },			 { typeof(PoolFxGlobalSystem), typeof(PoolFxGlobalSystemBluePrint) },			 { typeof(SphereLevelingSystem), typeof(SphereLevelingSystemBluePrint) },			 { typeof(RemoveItemFromCharacterSystem), typeof(RemoveItemFromCharacterSystemBluePrint) },			 { typeof(RandomAttackAnimationSystem), typeof(RandomAttackAnimationSystemBluePrint) },			 { typeof(DeathAnimationWhenDeadSystem), typeof(DeathAnimationWhenDeadSystemBluePrint) },			 { typeof(SoftValueUISystem), typeof(SoftValueUISystemBluePrint) },			 { typeof(ArenaBattleIndicatorSystem), typeof(ArenaBattleIndicatorSystemBluePrint) },			 { typeof(SphereCollisionParticleSystem), typeof(SphereCollisionParticleSystemBluePrint) },			 { typeof(SceneManagerSystem), typeof(SceneManagerSystemBluePrint) },			 { typeof(SlowdownSystem), typeof(SlowdownSystemBluePrint) },			 { typeof(StrategiesMainServiceSystem), typeof(StrategiesMainServiceSystemBluePrint) },			 { typeof(SlowdownSpeedSystem), typeof(SlowdownSpeedSystemBluePrint) },			 { typeof(TouchScreenSystem), typeof(TouchScreenSystemBluePrint) },			 { typeof(UISystem), typeof(UISystemBluePrint) },			 { typeof(SoundGlobalSystem), typeof(SoundGlobalSystemBluePrint) },			 { typeof(DestroyEntityWorldSystem), typeof(DestroyEntityWorldSystemBluePrint) },			 { typeof(UpgradeWindowSystem), typeof(UpgradeWindowSystemBluePrint) },			 { typeof(SearchNearestDropItemSystem), typeof(SearchNearestDropItemSystemBluePrint) },			 { typeof(ShakeAnimationWhenDamagedSystem), typeof(ShakeAnimationWhenDamagedSystemBluePrint) },			 { typeof(StopAIWhenDeadSystem), typeof(StopAIWhenDeadSystemBluePrint) },			 { typeof(SendLocalAnimationEventCommandsToAbilitiesSystem), typeof(SendLocalAnimationEventCommandsToAbilitiesSystemBluePrint) },			 { typeof(DropItemSystem), typeof(DropItemSystemBluePrint) },			 { typeof(AnimationSystem), typeof(AnimationSystemBluePrint) },			 { typeof(CompositeAbilitiesSystem), typeof(CompositeAbilitiesSystemBluePrint) },			 { typeof(GamePhasesSystem), typeof(GamePhasesSystemBluePrint) },			 { typeof(HealCardSystem), typeof(HealCardSystemBluePrint) },			 { typeof(ExplodeCardSystem), typeof(ExplodeCardSystemBluePrint) },			 { typeof(AddSphereCardSystem), typeof(AddSphereCardSystemBluePrint) },			 { typeof(CardPushSystem), typeof(CardPushSystemBluePrint) },			 { typeof(ChoosingPushDirCardSystem), typeof(ChoosingPushDirCardSystemBluePrint) },			 { typeof(RotateCharacterItemsSystem), typeof(RotateCharacterItemsSystemBluePrint) },			 { typeof(HealAbilitySystem), typeof(HealAbilitySystemBluePrint) },			 { typeof(FireballRingAbilitySystem), typeof(FireballRingAbilitySystemBluePrint) },			 { typeof(ExplodeSpheresAbilitySystem), typeof(ExplodeSpheresAbilitySystemBluePrint) },			 { typeof(AddSphereAbilitySystem), typeof(AddSphereAbilitySystemBluePrint) },			 { typeof(ChoosingPushDirectionSphereAbilitySystem), typeof(ChoosingPushDirectionSphereAbilitySystemBluePrint) },			 { typeof(SpherePushAbilitySystem), typeof(SpherePushAbilitySystemBluePrint) },			 { typeof(AutoApplyItemsWhenStaySystem), typeof(AutoApplyItemsWhenStaySystemBluePrint) },			 { typeof(DragonDefaultAttackAbilitySystem), typeof(DragonDefaultAttackAbilitySystemBluePrint) },			 { typeof(DefaultAttackAbilitySystem), typeof(DefaultAttackAbilitySystemBluePrint) },			 { typeof(BattleStateSystem), typeof(BattleStateSystemBluePrint) },			 { typeof(PrepareWaveStateSystem), typeof(PrepareWaveStateSystemBluePrint) },			 { typeof(FinalStateSystem), typeof(FinalStateSystemBluePrint) },			 { typeof(PrepareArenaStateSystem), typeof(PrepareArenaStateSystemBluePrint) },			 { typeof(FinishArenaStateSystem), typeof(FinishArenaStateSystemBluePrint) },			 { typeof(StartMenuStateSystem), typeof(StartMenuStateSystemBluePrint) },			 { typeof(PrepareBattleStateSystem), typeof(PrepareBattleStateSystemBluePrint) },			 { typeof(ArenaStateSystem), typeof(ArenaStateSystemBluePrint) },		};		}	}}