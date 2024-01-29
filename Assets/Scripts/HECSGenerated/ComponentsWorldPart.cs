using Components;namespace HECSFramework.Core{	public partial class World	{		partial void FillRegistrators()		{			componentProviderRegistrators = new ComponentProviderRegistrator[]			{				new ComponentProviderRegistrator<MainCanvasTagComponent>(),				new ComponentProviderRegistrator<AbilitiesHolderComponent>(),				new ComponentProviderRegistrator<AbilityTagComponent>(),				new ComponentProviderRegistrator<ActorProviderComponent>(),				new ComponentProviderRegistrator<ViewReferenceGameObjectComponent>(),				new ComponentProviderRegistrator<UnityTransformComponent>(),				new ComponentProviderRegistrator<UIGroupTagComponent>(),				new ComponentProviderRegistrator<PoolableViewsProviderComponent>(),				new ComponentProviderRegistrator<InputActionsComponent>(),				new ComponentProviderRegistrator<CacheCounterValuesComponent>(),				new ComponentProviderRegistrator<AIStrategyComponent>(),				new ComponentProviderRegistrator<TestSerializationComponent>(),				new ComponentProviderRegistrator<RadiusComponent>(),				new ComponentProviderRegistrator<SphereCastContext>(),				new ComponentProviderRegistrator<StateDataComponent>(),				new ComponentProviderRegistrator<GameStateComponent>(),				new ComponentProviderRegistrator<TestReactComponent>(),				new ComponentProviderRegistrator<CountersHolderComponent>(),				new ComponentProviderRegistrator<RotationComponent>(),				new ComponentProviderRegistrator<PlayerTagComponent>(),				new ComponentProviderRegistrator<OnStayComponent>(),				new ComponentProviderRegistrator<NeedDefaultDamageTextVisualizerComponent>(),				new ComponentProviderRegistrator<MainCharacterTagComponent>(),				new ComponentProviderRegistrator<MainCameraComponent>(),				new ComponentProviderRegistrator<IconComponent>(),				new ComponentProviderRegistrator<HealAbilityComponent>(),				new ComponentProviderRegistrator<FactionComponent>(),				new ComponentProviderRegistrator<FinalLevelScreenComponent>(),				new ComponentProviderRegistrator<DropItemComponent>(),				new ComponentProviderRegistrator<CustomHpBarComponent>(),				new ComponentProviderRegistrator<DirectionComponent>(),				new ComponentProviderRegistrator<ChoosingPushDirectionSphereAbilityComponent>(),				new ComponentProviderRegistrator<CollisionsComponent>(),				new ComponentProviderRegistrator<PlayerUpgradeComponent>(),				new ComponentProviderRegistrator<UITagComponent>(),				new ComponentProviderRegistrator<AdditionalCanvasTagComponent>(),				new ComponentProviderRegistrator<ViewReadyTagComponent>(),				new ComponentProviderRegistrator<SetupAfterViewTagComponent>(),				new ComponentProviderRegistrator<AbilityPredicateComponent>(),				new ComponentProviderRegistrator<TargetPositionComponent>(),				new ComponentProviderRegistrator<LevelModifierHolderComponent>(),				new ComponentProviderRegistrator<ItemsGlobalHolderComponent>(),				new ComponentProviderRegistrator<VisualFXHolderComponent>(),				new ComponentProviderRegistrator<StateTimerComponent>(),				new ComponentProviderRegistrator<HealthBarsManagerComponent>(),				new ComponentProviderRegistrator<SpeedUpSphereComponent>(),				new ComponentProviderRegistrator<ComicsComponent>(),				new ComponentProviderRegistrator<StickTagComponent>(),				new ComponentProviderRegistrator<UntilSuccessStrategyNodeComponent>(),				new ComponentProviderRegistrator<ViewReferenceComponent>(),				new ComponentProviderRegistrator<TransformComponent>(),				new ComponentProviderRegistrator<AnimatorStateComponent>(),				new ComponentProviderRegistrator<PoolableTagComponent>(),				new ComponentProviderRegistrator<ExplodeSpheresAbilityComponent>(),				new ComponentProviderRegistrator<CharacterItemsComponent>(),				new ComponentProviderRegistrator<CardTagComponent>(),				new ComponentProviderRegistrator<CardsGlobalHolderComponent>(),				new ComponentProviderRegistrator<CameraComponent>(),				new ComponentProviderRegistrator<BelongingComponent>(),				new ComponentProviderRegistrator<ArenaFinishComponent>(),				new ComponentProviderRegistrator<SoftCurrencyRewardVisualConfigComponent>(),				new ComponentProviderRegistrator<PlayerLevelComponent>(),				new ComponentProviderRegistrator<CharactersHolderComponent>(),				new ComponentProviderRegistrator<WaitStateComponent>(),				new ComponentProviderRegistrator<CachedEntitiesGlobalHolderComponent>(),				new ComponentProviderRegistrator<ActorContainerID>(),				new ComponentProviderRegistrator<SphereComponent>(),				new ComponentProviderRegistrator<PushDirectionComponent>(),				new ComponentProviderRegistrator<CenterTagComponent>(),				new ComponentProviderRegistrator<VisualInActionTagComponent>(),				new ComponentProviderRegistrator<TimeScaleComponent>(),				new ComponentProviderRegistrator<TargetEntityComponent>(),				new ComponentProviderRegistrator<SpawnPointComponent>(),				new ComponentProviderRegistrator<ShowHpBarTagComponent>(),				new ComponentProviderRegistrator<SpheresControllerTagComponent>(),				new ComponentProviderRegistrator<SpawnDropItemsComponent>(),				new ComponentProviderRegistrator<LevelsHolderComponent>(),				new ComponentProviderRegistrator<ScenarioAnimationComponent>(),				new ComponentProviderRegistrator<ViewDestructionDelayedComponent>(),				new ComponentProviderRegistrator<UIViewReferenceComponent>(),				new ComponentProviderRegistrator<TimeProviderComponent>(),				new ComponentProviderRegistrator<OnApplicationQuitTagComponent>(),				new ComponentProviderRegistrator<AfterLifeTagComponent>(),				new ComponentProviderRegistrator<AfterLifeCompleteTagComponent>(),				new ComponentProviderRegistrator<StrategySideExecuteComponent>(),				new ComponentProviderRegistrator<PegasusMovementComponent>(),				new ComponentProviderRegistrator<SlowdownComponent>(),				new ComponentProviderRegistrator<DamageTextVisualizerComponent>(),				new ComponentProviderRegistrator<ArenaBattleGlobalConfigComponent>(),				new ComponentProviderRegistrator<UpgradeWindowComponent>(),				new ComponentProviderRegistrator<SpeedDependsOnTimeScaleComponent>(),				new ComponentProviderRegistrator<ScenesHolderComponent>(),				new ComponentProviderRegistrator<UIBusyTagComponent>(),				new ComponentProviderRegistrator<StateInfoComponent>(),				new ComponentProviderRegistrator<InitOnAddAbilityTagComponent>(),				new ComponentProviderRegistrator<AdditionalAbilityIndexComponent>(),				new ComponentProviderRegistrator<SoundVolumeComponent>(),				new ComponentProviderRegistrator<NavMeshAgentComponent>(),				new ComponentProviderRegistrator<ItemTagComponent>(),				new ComponentProviderRegistrator<InputListenerTagComponent>(),				new ComponentProviderRegistrator<IgnoreReferenceContainerTagComponent>(),				new ComponentProviderRegistrator<GameLogicTagComponent>(),				new ComponentProviderRegistrator<PredicatesComponent>(),				new ComponentProviderRegistrator<AppVersionComponent>(),				new ComponentProviderRegistrator<SplashScreenUIComponent>(),				new ComponentProviderRegistrator<NeedHpBarComponent>(),				new ComponentProviderRegistrator<EnemyTagComponent>(),				new ComponentProviderRegistrator<BlockingAbilityInActionComponent>(),				new ComponentProviderRegistrator<AnimationCheckOutsHolderComponent>(),				new ComponentProviderRegistrator<OverlapSphereCastContext>(),				new ComponentProviderRegistrator<AutoApplyItemsWhenStayComponent>(),				new ComponentProviderRegistrator<OverrideAnimatorComponent>(),				new ComponentProviderRegistrator<StateContextComponent>(),				new ComponentProviderRegistrator<PassiveAbilityTag>(),				new ComponentProviderRegistrator<TestInitComponent>(),				new ComponentProviderRegistrator<CheckTwoComponent>(),				new ComponentProviderRegistrator<UnityRectTransformComponent>(),				new ComponentProviderRegistrator<UIPriorityIndexComponent>(),				new ComponentProviderRegistrator<NetworkEntityTagComponent>(),				new ComponentProviderRegistrator<InputOverUIComponent>(),				new ComponentProviderRegistrator<IsDeadTagComponent>(),				new ComponentProviderRegistrator<AbilityOwnerComponent>(),				new ComponentProviderRegistrator<SavePositionComponent>(),				new ComponentProviderRegistrator<NearestDropItemsComponent>(),				new ComponentProviderRegistrator<InvincibleComponent>(),				new ComponentProviderRegistrator<EnergyRegenerationComponent>(),				new ComponentProviderRegistrator<ActionsHolderComponent>(),				new ComponentProviderRegistrator<ActionsWithPredicateHolderComponent>(),				new ComponentProviderRegistrator<HealthComponent>(),				new ComponentProviderRegistrator<EnergyComponent>(),				new ComponentProviderRegistrator<CooldownComponent>(),				new ComponentProviderRegistrator<AgilityComponent>(),				new ComponentProviderRegistrator<SpeedComponent>(),				new ComponentProviderRegistrator<DamageComponent>(),				new ComponentProviderRegistrator<TestComponent>(),				new ComponentProviderRegistrator<TestWorldSingleComponent>(),				new ComponentProviderRegistrator<SoftValueCounterComponent>(),				new ComponentProviderRegistrator<RendererProviderComponent>(),				new ComponentProviderRegistrator<CharacterItemsSourceMonoComponentProvider>(),				new ComponentProviderRegistrator<SoftCurrencyCounterMonoComponentProviderComponent>(),				new ComponentProviderRegistrator<RigidbodyProviderComponent>(),				new ComponentProviderRegistrator<HealthBarPlaceMonoComponentProvider>(),			};		}	}}