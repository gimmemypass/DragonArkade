using Components;using System;using Systems;using System.Collections.Generic;namespace HECSFramework.Unity{	public partial class BluePrintsProvider	{		public BluePrintsProvider()		{		Components = new Dictionary<Type, Type>		{			 { typeof(UITagComponent), typeof(UITagComponentBluePrint) },			 { typeof(SetupAfterViewTagComponent), typeof(SetupAfterViewTagComponentBluePrint) },			 { typeof(PoolableTagComponent), typeof(PoolableTagComponentBluePrint) },			 { typeof(OnApplicationQuitTagComponent), typeof(OnApplicationQuitTagComponentBluePrint) },			 { typeof(NetworkEntityTagComponent), typeof(NetworkEntityTagComponentBluePrint) },			 { typeof(InputActionsComponent), typeof(InputActionsComponentBluePrint) },			 { typeof(SpawnPointComponent), typeof(SpawnPointComponentBluePrint) },			 { typeof(LevelModifierHolderComponent), typeof(LevelModifierHolderComponentBluePrint) },			 { typeof(PassiveAbilityTag), typeof(PassiveAbilityTagBluePrint) },			 { typeof(TestInitComponent), typeof(TestInitComponentBluePrint) },			 { typeof(AbilitiesHolderComponent), typeof(AbilitiesHolderComponentBluePrint) },			 { typeof(StrategySideExecuteComponent), typeof(StrategySideExecuteComponentBluePrint) },			 { typeof(TestSerializationComponent), typeof(TestSerializationComponentBluePrint) },			 { typeof(TargetPositionComponent), typeof(TargetPositionComponentBluePrint) },			 { typeof(OnStayComponent), typeof(OnStayComponentBluePrint) },			 { typeof(CharacterItemsComponent), typeof(CharacterItemsComponentBluePrint) },			 { typeof(NeedDefaultDamageTextVisualizerComponent), typeof(NeedDefaultDamageTextVisualizerComponentBluePrint) },			 { typeof(HealAbilityComponent), typeof(HealAbilityComponentBluePrint) },			 { typeof(UIBusyTagComponent), typeof(UIBusyTagComponentBluePrint) },			 { typeof(AdditionalCanvasTagComponent), typeof(AdditionalCanvasTagComponentBluePrint) },			 { typeof(StateInfoComponent), typeof(StateInfoComponentBluePrint) },			 { typeof(AbilityTagComponent), typeof(AbilityTagComponentBluePrint) },			 { typeof(TestReactComponent), typeof(TestReactComponentBluePrint) },			 { typeof(CheckTwoComponent), typeof(CheckTwoComponentBluePrint) },			 { typeof(SpheresControllerTagComponent), typeof(SpheresControllerTagComponentBluePrint) },			 { typeof(LevelsHolderComponent), typeof(LevelsHolderComponentBluePrint) },			 { typeof(CollisionsComponent), typeof(CollisionsComponentBluePrint) },			 { typeof(CardTagComponent), typeof(CardTagComponentBluePrint) },			 { typeof(MainCanvasTagComponent), typeof(MainCanvasTagComponentBluePrint) },			 { typeof(ViewReadyTagComponent), typeof(ViewReadyTagComponentBluePrint) },			 { typeof(OverlapSphereCastContext), typeof(OverlapSphereCastContextBluePrint) },			 { typeof(ViewReferenceComponent), typeof(ViewReferenceComponentBluePrint) },			 { typeof(UnityTransformComponent), typeof(UnityTransformComponentBluePrint) },			 { typeof(UIPriorityIndexComponent), typeof(UIPriorityIndexComponentBluePrint) },			 { typeof(TransformComponent), typeof(TransformComponentBluePrint) },			 { typeof(PoolableViewsProviderComponent), typeof(PoolableViewsProviderComponentBluePrint) },			 { typeof(NavMeshAgentComponent), typeof(NavMeshAgentComponentBluePrint) },			 { typeof(GameLogicTagComponent), typeof(GameLogicTagComponentBluePrint) },			 { typeof(AdditionalAbilityIndexComponent), typeof(AdditionalAbilityIndexComponentBluePrint) },			 { typeof(WaitStateComponent), typeof(WaitStateComponentBluePrint) },			 { typeof(AIStrategyComponent), typeof(AIStrategyComponentBluePrint) },			 { typeof(AppVersionComponent), typeof(AppVersionComponentBluePrint) },			 { typeof(UpgradeWindowComponent), typeof(UpgradeWindowComponentBluePrint) },			 { typeof(SpeedDependsOnTimeScaleComponent), typeof(SpeedDependsOnTimeScaleComponentBluePrint) },			 { typeof(ShowHpBarTagComponent), typeof(ShowHpBarTagComponentBluePrint) },			 { typeof(PegasusMovementComponent), typeof(PegasusMovementComponentBluePrint) },			 { typeof(FinalLevelScreenComponent), typeof(FinalLevelScreenComponentBluePrint) },			 { typeof(BlockingAbilityInActionComponent), typeof(BlockingAbilityInActionComponentBluePrint) },			 { typeof(ItemsGlobalHolderComponent), typeof(ItemsGlobalHolderComponentBluePrint) },			 { typeof(IconComponent), typeof(IconComponentBluePrint) },			 { typeof(OverrideAnimatorComponent), typeof(OverrideAnimatorComponentBluePrint) },			 { typeof(GameStateComponent), typeof(GameStateComponentBluePrint) },			 { typeof(ComicsComponent), typeof(ComicsComponentBluePrint) },			 { typeof(AutoApplyItemsWhenStayComponent), typeof(AutoApplyItemsWhenStayComponentBluePrint) },			 { typeof(PlayerUpgradeComponent), typeof(PlayerUpgradeComponentBluePrint) },			 { typeof(NearestDropItemsComponent), typeof(NearestDropItemsComponentBluePrint) },			 { typeof(DirectionComponent), typeof(DirectionComponentBluePrint) },			 { typeof(CenterTagComponent), typeof(CenterTagComponentBluePrint) },			 { typeof(ScenarioAnimationComponent), typeof(ScenarioAnimationComponentBluePrint) },			 { typeof(SphereCastContext), typeof(SphereCastContextBluePrint) },			 { typeof(StateDataComponent), typeof(StateDataComponentBluePrint) },			 { typeof(ActorProviderComponent), typeof(ActorProviderComponentBluePrint) },			 { typeof(PredicatesComponent), typeof(PredicatesComponentBluePrint) },			 { typeof(ActorContainerID), typeof(ActorContainerIDBluePrint) },			 { typeof(AbilityOwnerComponent), typeof(AbilityOwnerComponentBluePrint) },			 { typeof(PushDirectionComponent), typeof(PushDirectionComponentBluePrint) },			 { typeof(ArenaBattleGlobalConfigComponent), typeof(ArenaBattleGlobalConfigComponentBluePrint) },			 { typeof(ChoosingPushDirectionSphereAbilityComponent), typeof(ChoosingPushDirectionSphereAbilityComponentBluePrint) },			 { typeof(TimeProviderComponent), typeof(TimeProviderComponentBluePrint) },			 { typeof(ItemTagComponent), typeof(ItemTagComponentBluePrint) },			 { typeof(InputListenerTagComponent), typeof(InputListenerTagComponentBluePrint) },			 { typeof(UntilSuccessStrategyNodeComponent), typeof(UntilSuccessStrategyNodeComponentBluePrint) },			 { typeof(SavePositionComponent), typeof(SavePositionComponentBluePrint) },			 { typeof(SpeedUpSphereComponent), typeof(SpeedUpSphereComponentBluePrint) },			 { typeof(PlayerTagComponent), typeof(PlayerTagComponentBluePrint) },			 { typeof(InvincibleComponent), typeof(InvincibleComponentBluePrint) },			 { typeof(CustomHpBarComponent), typeof(CustomHpBarComponentBluePrint) },			 { typeof(ArenaFinishComponent), typeof(ArenaFinishComponentBluePrint) },			 { typeof(HealthBarsManagerComponent), typeof(HealthBarsManagerComponentBluePrint) },			 { typeof(BelongingComponent), typeof(BelongingComponentBluePrint) },			 { typeof(SoftCurrencyRewardVisualConfigComponent), typeof(SoftCurrencyRewardVisualConfigComponentBluePrint) },			 { typeof(StateTimerComponent), typeof(StateTimerComponentBluePrint) },			 { typeof(ScenesHolderComponent), typeof(ScenesHolderComponentBluePrint) },			 { typeof(MainCameraComponent), typeof(MainCameraComponentBluePrint) },			 { typeof(DropItemComponent), typeof(DropItemComponentBluePrint) },			 { typeof(CameraComponent), typeof(CameraComponentBluePrint) },			 { typeof(TimeScaleComponent), typeof(TimeScaleComponentBluePrint) },			 { typeof(DamageTextVisualizerComponent), typeof(DamageTextVisualizerComponentBluePrint) },			 { typeof(TargetEntityComponent), typeof(TargetEntityComponentBluePrint) },			 { typeof(RotationComponent), typeof(RotationComponentBluePrint) },			 { typeof(FactionComponent), typeof(FactionComponentBluePrint) },			 { typeof(SpawnDropItemsComponent), typeof(SpawnDropItemsComponentBluePrint) },			 { typeof(MainCharacterTagComponent), typeof(MainCharacterTagComponentBluePrint) },			 { typeof(VisualFXHolderComponent), typeof(VisualFXHolderComponentBluePrint) },			 { typeof(SlowdownComponent), typeof(SlowdownComponentBluePrint) },			 { typeof(PlayerLevelComponent), typeof(PlayerLevelComponentBluePrint) },			 { typeof(CardsGlobalHolderComponent), typeof(CardsGlobalHolderComponentBluePrint) },			 { typeof(StickTagComponent), typeof(StickTagComponentBluePrint) },			 { typeof(IsDeadTagComponent), typeof(IsDeadTagComponentBluePrint) },			 { typeof(CachedEntitiesGlobalHolderComponent), typeof(CachedEntitiesGlobalHolderComponentBluePrint) },			 { typeof(ExplodeSpheresAbilityComponent), typeof(ExplodeSpheresAbilityComponentBluePrint) },			 { typeof(StateContextComponent), typeof(StateContextComponentBluePrint) },			 { typeof(InitOnAddAbilityTagComponent), typeof(InitOnAddAbilityTagComponentBluePrint) },			 { typeof(SoundVolumeComponent), typeof(SoundVolumeComponentBluePrint) },			 { typeof(InputOverUIComponent), typeof(InputOverUIComponentBluePrint) },			 { typeof(IgnoreReferenceContainerTagComponent), typeof(IgnoreReferenceContainerTagComponentBluePrint) },			 { typeof(CacheCounterValuesComponent), typeof(CacheCounterValuesComponentBluePrint) },			 { typeof(AbilityPredicateComponent), typeof(AbilityPredicateComponentBluePrint) },			 { typeof(CharactersHolderComponent), typeof(CharactersHolderComponentBluePrint) },			 { typeof(SphereComponent), typeof(SphereComponentBluePrint) },			 { typeof(RadiusComponent), typeof(RadiusComponentBluePrint) },			 { typeof(AnimationCheckOutsHolderComponent), typeof(AnimationCheckOutsHolderComponentBluePrint) },			 { typeof(CountersHolderComponent), typeof(CountersHolderComponentBluePrint) },			 { typeof(ViewReferenceGameObjectComponent), typeof(ViewReferenceGameObjectComponentBluePrint) },			 { typeof(ViewDestructionDelayedComponent), typeof(ViewDestructionDelayedComponentBluePrint) },			 { typeof(UnityRectTransformComponent), typeof(UnityRectTransformComponentBluePrint) },			 { typeof(UIViewReferenceComponent), typeof(UIViewReferenceComponentBluePrint) },			 { typeof(UIGroupTagComponent), typeof(UIGroupTagComponentBluePrint) },			 { typeof(AfterLifeTagComponent), typeof(AfterLifeTagComponentBluePrint) },			 { typeof(AfterLifeCompleteTagComponent), typeof(AfterLifeCompleteTagComponentBluePrint) },			 { typeof(AnimatorStateComponent), typeof(AnimatorStateComponentBluePrint) },			 { typeof(VisualInActionTagComponent), typeof(VisualInActionTagComponentBluePrint) },			 { typeof(SplashScreenUIComponent), typeof(SplashScreenUIComponentBluePrint) },			 { typeof(NeedHpBarComponent), typeof(NeedHpBarComponentBluePrint) },			 { typeof(EnemyTagComponent), typeof(EnemyTagComponentBluePrint) },			 { typeof(SoftValueCounterComponent), typeof(SoftValueCounterComponentBluePrint) },			 { typeof(EnergyRegenerationComponent), typeof(EnergyRegenerationComponentBluePrint) },			 { typeof(ActionsHolderComponent), typeof(ActionsHolderComponentBluePrint) },			 { typeof(TestComponent), typeof(TestComponentBluePrint) },			 { typeof(TestWorldSingleComponent), typeof(TestWorldSingleComponentBluePrint) },			 { typeof(HealthComponent), typeof(HealthComponentBluePrint) },			 { typeof(DamageComponent), typeof(DamageComponentBluePrint) },			 { typeof(SpeedComponent), typeof(SpeedComponentBluePrint) },			 { typeof(AgilityComponent), typeof(AgilityComponentBluePrint) },			 { typeof(CooldownComponent), typeof(CooldownComponentBluePrint) },			 { typeof(EnergyComponent), typeof(EnergyComponentBluePrint) },			 { typeof(RendererProviderComponent), typeof(RendererProviderComponentBluePrint) },			 { typeof(CharacterItemsSourceMonoComponentProvider), typeof(CharacterItemsSourceMonoComponentProviderBluePrint) },			 { typeof(RigidbodyProviderComponent), typeof(RigidbodyProviderComponentBluePrint) },			 { typeof(SoftCurrencyCounterMonoComponentProviderComponent), typeof(SoftCurrencyCounterMonoComponentProviderComponentBluePrint) },			 { typeof(HealthBarPlaceMonoComponentProvider), typeof(HealthBarPlaceMonoComponentProviderBluePrint) },			 { typeof(ActionsWithPredicateHolderComponent), typeof(ActionsWithPredicateHolderComponentBluePrint) },		};		Systems = new Dictionary<Type, Type>		{			 { typeof(PoolingSystem), typeof(PoolingSystemBluePrint) },			 { typeof(ActorAfterViewSystem), typeof(ActorAfterViewSystemBluePrint) },			 { typeof(DestroyEntityWorldSystem), typeof(DestroyEntityWorldSystemBluePrint) },			 { typeof(SearchNearestDropItemSystem), typeof(SearchNearestDropItemSystemBluePrint) },			 { typeof(SphereMovementSystem), typeof(SphereMovementSystemBluePrint) },			 { typeof(ArenaBattleIndicatorSystem), typeof(ArenaBattleIndicatorSystemBluePrint) },			 { typeof(WaitingCommandsSystems), typeof(WaitingCommandsSystemsBluePrint) },			 { typeof(ArenaBattleUIWindow), typeof(ArenaBattleUIWindowBluePrint) },			 { typeof(SceneManagerSystem), typeof(SceneManagerSystemBluePrint) },			 { typeof(ItemThrowingSystem), typeof(ItemThrowingSystemBluePrint) },			 { typeof(GlobalRewardsVisualSystem), typeof(GlobalRewardsVisualSystemBluePrint) },			 { typeof(StopAIWhenDeadSystem), typeof(StopAIWhenDeadSystemBluePrint) },			 { typeof(ShakeAnimationWhenDamagedSystem), typeof(ShakeAnimationWhenDamagedSystemBluePrint) },			 { typeof(ComicsWindowSystem), typeof(ComicsWindowSystemBluePrint) },			 { typeof(AssetsServiceSystem), typeof(AssetsServiceSystemBluePrint) },			 { typeof(UISystem), typeof(UISystemBluePrint) },			 { typeof(StressTestReactsSystem), typeof(StressTestReactsSystemBluePrint) },			 { typeof(AINPCSystem), typeof(AINPCSystemBluePrint) },			 { typeof(UpgradeWindowSystem), typeof(UpgradeWindowSystemBluePrint) },			 { typeof(SendLocalAnimationEventCommandsToAbilitiesSystem), typeof(SendLocalAnimationEventCommandsToAbilitiesSystemBluePrint) },			 { typeof(DamageTextVisualizerManagerSystem), typeof(DamageTextVisualizerManagerSystemBluePrint) },			 { typeof(SoundGlobalSystem), typeof(SoundGlobalSystemBluePrint) },			 { typeof(SpawnViewSystem), typeof(SpawnViewSystemBluePrint) },			 { typeof(AwaitersUpdateSystem), typeof(AwaitersUpdateSystemBluePrint) },			 { typeof(PlayUpgradeParticleSystem), typeof(PlayUpgradeParticleSystemBluePrint) },			 { typeof(HandleCooldownGlobalSystem), typeof(HandleCooldownGlobalSystemBluePrint) },			 { typeof(StickFollowSystem), typeof(StickFollowSystemBluePrint) },			 { typeof(StateUpdateSystem), typeof(StateUpdateSystemBluePrint) },			 { typeof(SpeedUpSphereSystem), typeof(SpeedUpSphereSystemBluePrint) },			 { typeof(SphereLevelingSystem), typeof(SphereLevelingSystemBluePrint) },			 { typeof(RandomAttackAnimationSystem), typeof(RandomAttackAnimationSystemBluePrint) },			 { typeof(MultipleCollectItemsSystem), typeof(MultipleCollectItemsSystemBluePrint) },			 { typeof(HealthBarsManagerSystem), typeof(HealthBarsManagerSystemBluePrint) },			 { typeof(SlowdownSystem), typeof(SlowdownSystemBluePrint) },			 { typeof(DamageTextVisualizerSystem), typeof(DamageTextVisualizerSystemBluePrint) },			 { typeof(SlowdownSpeedSystem), typeof(SlowdownSpeedSystemBluePrint) },			 { typeof(CardsControllerUISystem), typeof(CardsControllerUISystemBluePrint) },			 { typeof(StickInputSystem), typeof(StickInputSystemBluePrint) },			 { typeof(StrategiesMainServiceSystem), typeof(StrategiesMainServiceSystemBluePrint) },			 { typeof(DisableCollidersWhenDeadSystem), typeof(DisableCollidersWhenDeadSystemBluePrint) },			 { typeof(SaveLoadSystem), typeof(SaveLoadSystemBluePrint) },			 { typeof(MainCharLevelingSystem), typeof(MainCharLevelingSystemBluePrint) },			 { typeof(FlyDragonAnimationSystem), typeof(FlyDragonAnimationSystemBluePrint) },			 { typeof(MoveEnemyToSpawnPointSystem), typeof(MoveEnemyToSpawnPointSystemBluePrint) },			 { typeof(EnergyUISystem), typeof(EnergyUISystemBluePrint) },			 { typeof(VFXCreationSystem), typeof(VFXCreationSystemBluePrint) },			 { typeof(UpdateTranformFromActorSystem), typeof(UpdateTranformFromActorSystemBluePrint) },			 { typeof(StartSystem), typeof(StartSystemBluePrint) },			 { typeof(InputOverUISystem), typeof(InputOverUISystemBluePrint) },			 { typeof(InjectComponentsFromActorSystem), typeof(InjectComponentsFromActorSystemBluePrint) },			 { typeof(UpgradeVisualMainCharacterSystem), typeof(UpgradeVisualMainCharacterSystemBluePrint) },			 { typeof(PegasusMovementSystem), typeof(PegasusMovementSystemBluePrint) },			 { typeof(HealthSystem), typeof(HealthSystemBluePrint) },			 { typeof(SpawnDropItemsSystem), typeof(SpawnDropItemsSystemBluePrint) },			 { typeof(ApplyAimedItemSystem), typeof(ApplyAimedItemSystemBluePrint) },			 { typeof(SoftValueUISystem), typeof(SoftValueUISystemBluePrint) },			 { typeof(SplashScreenUISystem), typeof(SplashScreenUISystemBluePrint) },			 { typeof(MoveMainCharacterSystem), typeof(MoveMainCharacterSystemBluePrint) },			 { typeof(InitSpawnPointSystem), typeof(InitSpawnPointSystemBluePrint) },			 { typeof(FireballItemVisualSystem), typeof(FireballItemVisualSystemBluePrint) },			 { typeof(DestroyAfterDeadEndAnimationEventSystem), typeof(DestroyAfterDeadEndAnimationEventSystemBluePrint) },			 { typeof(CollideItemSystem), typeof(CollideItemSystemBluePrint) },			 { typeof(SphereCollisionParticleSystem), typeof(SphereCollisionParticleSystemBluePrint) },			 { typeof(RemoveItemFromCharacterSystem), typeof(RemoveItemFromCharacterSystemBluePrint) },			 { typeof(MainCharAttackSystem), typeof(MainCharAttackSystemBluePrint) },			 { typeof(ItemMovementSystem), typeof(ItemMovementSystemBluePrint) },			 { typeof(FindTargetEntitySystem), typeof(FindTargetEntitySystemBluePrint) },			 { typeof(DrawHP_UISystem), typeof(DrawHP_UISystemBluePrint) },			 { typeof(IndicateDamageSystem), typeof(IndicateDamageSystemBluePrint) },			 { typeof(AbilitiesSystem), typeof(AbilitiesSystemBluePrint) },			 { typeof(FinalScreenUISystem), typeof(FinalScreenUISystemBluePrint) },			 { typeof(DestroyDeadEntitySystem), typeof(DestroyDeadEntitySystemBluePrint) },			 { typeof(SphereDamageSystem), typeof(SphereDamageSystemBluePrint) },			 { typeof(TouchScreenSystem), typeof(TouchScreenSystemBluePrint) },			 { typeof(ReleaseActorAssetReferenceSystem), typeof(ReleaseActorAssetReferenceSystemBluePrint) },			 { typeof(SphereCollisionsHolderSystem), typeof(SphereCollisionsHolderSystemBluePrint) },			 { typeof(LoseScreenUISystem), typeof(LoseScreenUISystemBluePrint) },			 { typeof(DropPartsHealthVisualSystem), typeof(DropPartsHealthVisualSystemBluePrint) },			 { typeof(PoolFxGlobalSystem), typeof(PoolFxGlobalSystemBluePrint) },			 { typeof(RotateToTargetSystem), typeof(RotateToTargetSystemBluePrint) },			 { typeof(EnergyRegenerationSystem), typeof(EnergyRegenerationSystemBluePrint) },			 { typeof(HideUISystem), typeof(HideUISystemBluePrint) },			 { typeof(AdditionalCanvasesSystem), typeof(AdditionalCanvasesSystemBluePrint) },			 { typeof(UpdateActorByTranformSystem), typeof(UpdateActorByTranformSystemBluePrint) },			 { typeof(InputListenSystem), typeof(InputListenSystemBluePrint) },			 { typeof(MainCharacterHealthUISystem), typeof(MainCharacterHealthUISystemBluePrint) },			 { typeof(DropItemSystem), typeof(DropItemSystemBluePrint) },			 { typeof(CountersHolderSystem), typeof(CountersHolderSystemBluePrint) },			 { typeof(StartMenuUISystem), typeof(StartMenuUISystemBluePrint) },			 { typeof(DeathAnimationWhenDeadSystem), typeof(DeathAnimationWhenDeadSystemBluePrint) },			 { typeof(AnimationDoneCheckOutSystem), typeof(AnimationDoneCheckOutSystemBluePrint) },			 { typeof(RemoveComponentWorldSystem), typeof(RemoveComponentWorldSystemBluePrint) },			 { typeof(LevelBattleUISystem), typeof(LevelBattleUISystemBluePrint) },			 { typeof(GamePhasesSystem), typeof(GamePhasesSystemBluePrint) },			 { typeof(AnimationSystem), typeof(AnimationSystemBluePrint) },			 { typeof(HealCardSystem), typeof(HealCardSystemBluePrint) },			 { typeof(ChoosingPushDirCardSystem), typeof(ChoosingPushDirCardSystemBluePrint) },			 { typeof(ExplodeCardSystem), typeof(ExplodeCardSystemBluePrint) },			 { typeof(AddSphereCardSystem), typeof(AddSphereCardSystemBluePrint) },			 { typeof(CardPushSystem), typeof(CardPushSystemBluePrint) },			 { typeof(PrepareBattleStateSystem), typeof(PrepareBattleStateSystemBluePrint) },			 { typeof(BattleStateSystem), typeof(BattleStateSystemBluePrint) },			 { typeof(FinalStateSystem), typeof(FinalStateSystemBluePrint) },			 { typeof(PrepareWaveStateSystem), typeof(PrepareWaveStateSystemBluePrint) },			 { typeof(PrepareArenaStateSystem), typeof(PrepareArenaStateSystemBluePrint) },			 { typeof(StartMenuStateSystem), typeof(StartMenuStateSystemBluePrint) },			 { typeof(FinishArenaStateSystem), typeof(FinishArenaStateSystemBluePrint) },			 { typeof(ArenaStateSystem), typeof(ArenaStateSystemBluePrint) },			 { typeof(CompositeAbilitiesSystem), typeof(CompositeAbilitiesSystemBluePrint) },			 { typeof(DragonDefaultAttackAbilitySystem), typeof(DragonDefaultAttackAbilitySystemBluePrint) },			 { typeof(AutoApplyItemsWhenStaySystem), typeof(AutoApplyItemsWhenStaySystemBluePrint) },			 { typeof(AddSphereAbilitySystem), typeof(AddSphereAbilitySystemBluePrint) },			 { typeof(SpherePushAbilitySystem), typeof(SpherePushAbilitySystemBluePrint) },			 { typeof(ExplodeSpheresAbilitySystem), typeof(ExplodeSpheresAbilitySystemBluePrint) },			 { typeof(ChoosingPushDirectionSphereAbilitySystem), typeof(ChoosingPushDirectionSphereAbilitySystemBluePrint) },			 { typeof(HealAbilitySystem), typeof(HealAbilitySystemBluePrint) },			 { typeof(DefaultAttackAbilitySystem), typeof(DefaultAttackAbilitySystemBluePrint) },			 { typeof(RotateCharacterItemsSystem), typeof(RotateCharacterItemsSystemBluePrint) },			 { typeof(FireballRingAbilitySystem), typeof(FireballRingAbilitySystemBluePrint) },		};		}	}}