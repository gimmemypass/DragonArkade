using Components;namespace HECSFramework.Core{	public partial class World	{		partial void FillRegistrators()		{			componentProviderRegistrators = new ComponentProviderRegistrator[]			{				new ComponentProviderRegistrator<AdditionalCanvasTagComponent>(),				new ComponentProviderRegistrator<AnimationCheckOutsHolderComponent>(),				new ComponentProviderRegistrator<AbilitiesHolderComponent>(),				new ComponentProviderRegistrator<AdditionalAbilityIndexComponent>(),				new ComponentProviderRegistrator<ActorProviderComponent>(),				new ComponentProviderRegistrator<IgnoreReferenceContainerTagComponent>(),				new ComponentProviderRegistrator<UntilSuccessStrategyNodeComponent>(),				new ComponentProviderRegistrator<PassiveAbilityTag>(),				new ComponentProviderRegistrator<TestReactComponent>(),				new ComponentProviderRegistrator<ViewDestructionDelayedComponent>(),				new ComponentProviderRegistrator<PredicatesComponent>(),				new ComponentProviderRegistrator<ItemTagComponent>(),				new ComponentProviderRegistrator<InputActionsComponent>(),				new ComponentProviderRegistrator<GameLogicTagComponent>(),				new ComponentProviderRegistrator<WaitStateComponent>(),				new ComponentProviderRegistrator<SlowdownComponent>(),				new ComponentProviderRegistrator<CacheCounterValuesComponent>(),				new ComponentProviderRegistrator<CountersHolderComponent>(),				new ComponentProviderRegistrator<UnityTransformComponent>(),				new ComponentProviderRegistrator<PoolableViewsProviderComponent>(),				new ComponentProviderRegistrator<NetworkEntityTagComponent>(),				new ComponentProviderRegistrator<ViewReferenceComponent>(),				new ComponentProviderRegistrator<UpgradeWindowComponent>(),				new ComponentProviderRegistrator<ArenaFinishComponent>(),				new ComponentProviderRegistrator<VisualFXHolderComponent>(),				new ComponentProviderRegistrator<OverlapSphereCastContext>(),				new ComponentProviderRegistrator<GameStateComponent>(),				new ComponentProviderRegistrator<StickTagComponent>(),				new ComponentProviderRegistrator<RadiusComponent>(),				new ComponentProviderRegistrator<ViewReadyTagComponent>(),				new ComponentProviderRegistrator<SetupAfterViewTagComponent>(),				new ComponentProviderRegistrator<UIGroupTagComponent>(),				new ComponentProviderRegistrator<PoolableTagComponent>(),				new ComponentProviderRegistrator<OnApplicationQuitTagComponent>(),				new ComponentProviderRegistrator<NavMeshAgentComponent>(),				new ComponentProviderRegistrator<InputOverUIComponent>(),				new ComponentProviderRegistrator<InputListenerTagComponent>(),				new ComponentProviderRegistrator<AnimatorStateComponent>(),				new ComponentProviderRegistrator<SoftCurrencyRewardVisualConfigComponent>(),				new ComponentProviderRegistrator<TimeScaleComponent>(),				new ComponentProviderRegistrator<IsDeadTagComponent>(),				new ComponentProviderRegistrator<CachedEntitiesGlobalHolderComponent>(),				new ComponentProviderRegistrator<AbilityPredicateComponent>(),				new ComponentProviderRegistrator<SplashScreenUIComponent>(),				new ComponentProviderRegistrator<OverrideAnimatorComponent>(),				new ComponentProviderRegistrator<StateContextComponent>(),				new ComponentProviderRegistrator<AppVersionComponent>(),				new ComponentProviderRegistrator<ActorContainerID>(),				new ComponentProviderRegistrator<AbilityOwnerComponent>(),				new ComponentProviderRegistrator<StateTimerComponent>(),				new ComponentProviderRegistrator<NeedDefaultDamageTextVisualizerComponent>(),				new ComponentProviderRegistrator<MainCameraComponent>(),				new ComponentProviderRegistrator<LevelModifierHolderComponent>(),				new ComponentProviderRegistrator<ExplodeSpheresAbilityComponent>(),				new ComponentProviderRegistrator<SphereComponent>(),				new ComponentProviderRegistrator<SphereCastContext>(),				new ComponentProviderRegistrator<StateInfoComponent>(),				new ComponentProviderRegistrator<TestInitComponent>(),				new ComponentProviderRegistrator<CheckTwoComponent>(),				new ComponentProviderRegistrator<SoundVolumeComponent>(),				new ComponentProviderRegistrator<SavePositionComponent>(),				new ComponentProviderRegistrator<CardTagComponent>(),				new ComponentProviderRegistrator<PegasusMovementComponent>(),				new ComponentProviderRegistrator<CustomHpBarComponent>(),				new ComponentProviderRegistrator<ChoosingPushDirectionSphereAbilityComponent>(),				new ComponentProviderRegistrator<DirectionComponent>(),				new ComponentProviderRegistrator<CharacterItemsComponent>(),				new ComponentProviderRegistrator<IconComponent>(),				new ComponentProviderRegistrator<HealthBarsManagerComponent>(),				new ComponentProviderRegistrator<UITagComponent>(),				new ComponentProviderRegistrator<AIStrategyComponent>(),				new ComponentProviderRegistrator<SpawnDropItemsComponent>(),				new ComponentProviderRegistrator<ScenesHolderComponent>(),				new ComponentProviderRegistrator<LevelsHolderComponent>(),				new ComponentProviderRegistrator<FactionComponent>(),				new ComponentProviderRegistrator<EnemyTagComponent>(),				new ComponentProviderRegistrator<CharactersHolderComponent>(),				new ComponentProviderRegistrator<ArenaBattleGlobalConfigComponent>(),				new ComponentProviderRegistrator<CameraComponent>(),				new ComponentProviderRegistrator<AutoApplyItemsWhenStayComponent>(),				new ComponentProviderRegistrator<VisualInActionTagComponent>(),				new ComponentProviderRegistrator<SpawnPointComponent>(),				new ComponentProviderRegistrator<ShowHpBarTagComponent>(),				new ComponentProviderRegistrator<NearestDropItemsComponent>(),				new ComponentProviderRegistrator<MainCharacterTagComponent>(),				new ComponentProviderRegistrator<TargetEntityComponent>(),				new ComponentProviderRegistrator<PlayerUpgradeComponent>(),				new ComponentProviderRegistrator<NeedHpBarComponent>(),				new ComponentProviderRegistrator<HealAbilityComponent>(),				new ComponentProviderRegistrator<FinalLevelScreenComponent>(),				new ComponentProviderRegistrator<DamageTextVisualizerComponent>(),				new ComponentProviderRegistrator<CenterTagComponent>(),				new ComponentProviderRegistrator<BlockingAbilityInActionComponent>(),				new ComponentProviderRegistrator<TargetPositionComponent>(),				new ComponentProviderRegistrator<SpheresControllerTagComponent>(),				new ComponentProviderRegistrator<SpeedDependsOnTimeScaleComponent>(),				new ComponentProviderRegistrator<PlayerLevelComponent>(),				new ComponentProviderRegistrator<CardsGlobalHolderComponent>(),				new ComponentProviderRegistrator<ScenarioAnimationComponent>(),				new ComponentProviderRegistrator<AbilityTagComponent>(),				new ComponentProviderRegistrator<ViewReferenceGameObjectComponent>(),				new ComponentProviderRegistrator<UnityRectTransformComponent>(),				new ComponentProviderRegistrator<UIViewReferenceComponent>(),				new ComponentProviderRegistrator<UIPriorityIndexComponent>(),				new ComponentProviderRegistrator<TransformComponent>(),				new ComponentProviderRegistrator<TimeProviderComponent>(),				new ComponentProviderRegistrator<AfterLifeCompleteTagComponent>(),				new ComponentProviderRegistrator<StrategySideExecuteComponent>(),				new ComponentProviderRegistrator<TestSerializationComponent>(),				new ComponentProviderRegistrator<ItemsGlobalHolderComponent>(),				new ComponentProviderRegistrator<RotationComponent>(),				new ComponentProviderRegistrator<PushDirectionComponent>(),				new ComponentProviderRegistrator<OnStayComponent>(),				new ComponentProviderRegistrator<InvincibleComponent>(),				new ComponentProviderRegistrator<CollisionsComponent>(),				new ComponentProviderRegistrator<BelongingComponent>(),				new ComponentProviderRegistrator<MainCanvasTagComponent>(),				new ComponentProviderRegistrator<UIBusyTagComponent>(),				new ComponentProviderRegistrator<StateDataComponent>(),				new ComponentProviderRegistrator<InitOnAddAbilityTagComponent>(),				new ComponentProviderRegistrator<AfterLifeTagComponent>(),				new ComponentProviderRegistrator<DropItemComponent>(),				new ComponentProviderRegistrator<SpeedUpSphereComponent>(),				new ComponentProviderRegistrator<PlayerTagComponent>(),				new ComponentProviderRegistrator<ComicsComponent>(),				new ComponentProviderRegistrator<SoftValueCounterComponent>(),				new ComponentProviderRegistrator<SoftCurrencyCounterMonoComponentProviderComponent>(),				new ComponentProviderRegistrator<RigidbodyProviderComponent>(),				new ComponentProviderRegistrator<CharacterItemsSourceMonoComponentProvider>(),				new ComponentProviderRegistrator<HealthBarPlaceMonoComponentProvider>(),				new ComponentProviderRegistrator<RendererProviderComponent>(),				new ComponentProviderRegistrator<ActionsWithPredicateHolderComponent>(),				new ComponentProviderRegistrator<ActionsHolderComponent>(),				new ComponentProviderRegistrator<EnergyComponent>(),				new ComponentProviderRegistrator<TestComponent>(),				new ComponentProviderRegistrator<TestWorldSingleComponent>(),				new ComponentProviderRegistrator<DamageComponent>(),				new ComponentProviderRegistrator<SpeedComponent>(),				new ComponentProviderRegistrator<HealthComponent>(),				new ComponentProviderRegistrator<CooldownComponent>(),				new ComponentProviderRegistrator<AgilityComponent>(),				new ComponentProviderRegistrator<EnergyRegenerationComponent>(),			};		}	}}