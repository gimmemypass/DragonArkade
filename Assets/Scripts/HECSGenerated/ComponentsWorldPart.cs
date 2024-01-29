using Components;namespace HECSFramework.Core{	public partial class World	{		partial void FillRegistrators()		{			componentProviderRegistrators = new ComponentProviderRegistrator[]			{				new ComponentProviderRegistrator<UntilSuccessStrategyNodeComponent>(),				new ComponentProviderRegistrator<StrategySideExecuteComponent>(),				new ComponentProviderRegistrator<ViewReferenceComponent>(),				new ComponentProviderRegistrator<AbilityPredicateComponent>(),				new ComponentProviderRegistrator<StickTagComponent>(),				new ComponentProviderRegistrator<RadiusComponent>(),				new ComponentProviderRegistrator<TestReactComponent>(),				new ComponentProviderRegistrator<IsDeadTagComponent>(),				new ComponentProviderRegistrator<AbilityOwnerComponent>(),				new ComponentProviderRegistrator<RotationComponent>(),				new ComponentProviderRegistrator<PlayerLevelComponent>(),				new ComponentProviderRegistrator<NeedHpBarComponent>(),				new ComponentProviderRegistrator<FactionComponent>(),				new ComponentProviderRegistrator<NearestDropItemsComponent>(),				new ComponentProviderRegistrator<EnemyTagComponent>(),				new ComponentProviderRegistrator<CollisionsComponent>(),				new ComponentProviderRegistrator<AIStrategyComponent>(),				new ComponentProviderRegistrator<TestSerializationComponent>(),				new ComponentProviderRegistrator<CountersHolderComponent>(),				new ComponentProviderRegistrator<SpawnDropItemsComponent>(),				new ComponentProviderRegistrator<DirectionComponent>(),				new ComponentProviderRegistrator<ChoosingPushDirectionSphereAbilityComponent>(),				new ComponentProviderRegistrator<ItemsGlobalHolderComponent>(),				new ComponentProviderRegistrator<PlayerUpgradeComponent>(),				new ComponentProviderRegistrator<CardTagComponent>(),				new ComponentProviderRegistrator<VisualInActionTagComponent>(),				new ComponentProviderRegistrator<ExplodeSpheresAbilityComponent>(),				new ComponentProviderRegistrator<MainCameraComponent>(),				new ComponentProviderRegistrator<IconComponent>(),				new ComponentProviderRegistrator<BelongingComponent>(),				new ComponentProviderRegistrator<UITagComponent>(),				new ComponentProviderRegistrator<OverrideAnimatorComponent>(),				new ComponentProviderRegistrator<AbilitiesHolderComponent>(),				new ComponentProviderRegistrator<UnityTransformComponent>(),				new ComponentProviderRegistrator<UIPriorityIndexComponent>(),				new ComponentProviderRegistrator<PoolableViewsProviderComponent>(),				new ComponentProviderRegistrator<InputActionsComponent>(),				new ComponentProviderRegistrator<IgnoreReferenceContainerTagComponent>(),				new ComponentProviderRegistrator<CharacterItemsComponent>(),				new ComponentProviderRegistrator<CheckTwoComponent>(),				new ComponentProviderRegistrator<UnityRectTransformComponent>(),				new ComponentProviderRegistrator<TransformComponent>(),				new ComponentProviderRegistrator<PredicatesComponent>(),				new ComponentProviderRegistrator<GameLogicTagComponent>(),				new ComponentProviderRegistrator<AutoApplyItemsWhenStayComponent>(),				new ComponentProviderRegistrator<SplashScreenUIComponent>(),				new ComponentProviderRegistrator<StateTimerComponent>(),				new ComponentProviderRegistrator<SpawnPointComponent>(),				new ComponentProviderRegistrator<HealAbilityComponent>(),				new ComponentProviderRegistrator<FinalLevelScreenComponent>(),				new ComponentProviderRegistrator<VisualFXHolderComponent>(),				new ComponentProviderRegistrator<TargetPositionComponent>(),				new ComponentProviderRegistrator<LevelsHolderComponent>(),				new ComponentProviderRegistrator<InvincibleComponent>(),				new ComponentProviderRegistrator<MainCanvasTagComponent>(),				new ComponentProviderRegistrator<UIBusyTagComponent>(),				new ComponentProviderRegistrator<AdditionalCanvasTagComponent>(),				new ComponentProviderRegistrator<ScenarioAnimationComponent>(),				new ComponentProviderRegistrator<StateInfoComponent>(),				new ComponentProviderRegistrator<StateContextComponent>(),				new ComponentProviderRegistrator<GameStateComponent>(),				new ComponentProviderRegistrator<SoundVolumeComponent>(),				new ComponentProviderRegistrator<CachedEntitiesGlobalHolderComponent>(),				new ComponentProviderRegistrator<SpeedUpSphereComponent>(),				new ComponentProviderRegistrator<ShowHpBarTagComponent>(),				new ComponentProviderRegistrator<PlayerTagComponent>(),				new ComponentProviderRegistrator<CharactersHolderComponent>(),				new ComponentProviderRegistrator<CenterTagComponent>(),				new ComponentProviderRegistrator<AnimationCheckOutsHolderComponent>(),				new ComponentProviderRegistrator<AbilityTagComponent>(),				new ComponentProviderRegistrator<ViewReferenceGameObjectComponent>(),				new ComponentProviderRegistrator<ViewDestructionDelayedComponent>(),				new ComponentProviderRegistrator<UIGroupTagComponent>(),				new ComponentProviderRegistrator<PoolableTagComponent>(),				new ComponentProviderRegistrator<OnApplicationQuitTagComponent>(),				new ComponentProviderRegistrator<NetworkEntityTagComponent>(),				new ComponentProviderRegistrator<ItemTagComponent>(),				new ComponentProviderRegistrator<AfterLifeTagComponent>(),				new ComponentProviderRegistrator<CacheCounterValuesComponent>(),				new ComponentProviderRegistrator<ActorContainerID>(),				new ComponentProviderRegistrator<ActorProviderComponent>(),				new ComponentProviderRegistrator<SavePositionComponent>(),				new ComponentProviderRegistrator<TargetEntityComponent>(),				new ComponentProviderRegistrator<UpgradeWindowComponent>(),				new ComponentProviderRegistrator<TimeScaleComponent>(),				new ComponentProviderRegistrator<PushDirectionComponent>(),				new ComponentProviderRegistrator<OnStayComponent>(),				new ComponentProviderRegistrator<CardsGlobalHolderComponent>(),				new ComponentProviderRegistrator<ViewReadyTagComponent>(),				new ComponentProviderRegistrator<SetupAfterViewTagComponent>(),				new ComponentProviderRegistrator<UIViewReferenceComponent>(),				new ComponentProviderRegistrator<TimeProviderComponent>(),				new ComponentProviderRegistrator<NavMeshAgentComponent>(),				new ComponentProviderRegistrator<InputOverUIComponent>(),				new ComponentProviderRegistrator<InputListenerTagComponent>(),				new ComponentProviderRegistrator<SoftCurrencyRewardVisualConfigComponent>(),				new ComponentProviderRegistrator<HealthBarsManagerComponent>(),				new ComponentProviderRegistrator<PegasusMovementComponent>(),				new ComponentProviderRegistrator<MainCharacterTagComponent>(),				new ComponentProviderRegistrator<LevelModifierHolderComponent>(),				new ComponentProviderRegistrator<BlockingAbilityInActionComponent>(),				new ComponentProviderRegistrator<ArenaFinishComponent>(),				new ComponentProviderRegistrator<DropItemComponent>(),				new ComponentProviderRegistrator<CameraComponent>(),				new ComponentProviderRegistrator<SlowdownComponent>(),				new ComponentProviderRegistrator<CustomHpBarComponent>(),				new ComponentProviderRegistrator<ArenaBattleGlobalConfigComponent>(),				new ComponentProviderRegistrator<SphereCastContext>(),				new ComponentProviderRegistrator<OverlapSphereCastContext>(),				new ComponentProviderRegistrator<StateDataComponent>(),				new ComponentProviderRegistrator<PassiveAbilityTag>(),				new ComponentProviderRegistrator<AdditionalAbilityIndexComponent>(),				new ComponentProviderRegistrator<WaitStateComponent>(),				new ComponentProviderRegistrator<AnimatorStateComponent>(),				new ComponentProviderRegistrator<SphereComponent>(),				new ComponentProviderRegistrator<SpeedDependsOnTimeScaleComponent>(),				new ComponentProviderRegistrator<ScenesHolderComponent>(),				new ComponentProviderRegistrator<NeedDefaultDamageTextVisualizerComponent>(),				new ComponentProviderRegistrator<ComicsComponent>(),				new ComponentProviderRegistrator<DamageTextVisualizerComponent>(),				new ComponentProviderRegistrator<InitOnAddAbilityTagComponent>(),				new ComponentProviderRegistrator<TestInitComponent>(),				new ComponentProviderRegistrator<AfterLifeCompleteTagComponent>(),				new ComponentProviderRegistrator<AppVersionComponent>(),				new ComponentProviderRegistrator<SpheresControllerTagComponent>(),				new ComponentProviderRegistrator<EnergyRegenerationComponent>(),				new ComponentProviderRegistrator<SoftCurrencyCounterMonoComponentProviderComponent>(),				new ComponentProviderRegistrator<RigidbodyProviderComponent>(),				new ComponentProviderRegistrator<CharacterItemsSourceMonoComponentProvider>(),				new ComponentProviderRegistrator<HealthBarPlaceMonoComponentProvider>(),				new ComponentProviderRegistrator<RendererProviderComponent>(),				new ComponentProviderRegistrator<ActionsWithPredicateHolderComponent>(),				new ComponentProviderRegistrator<SoftValueCounterComponent>(),				new ComponentProviderRegistrator<ActionsHolderComponent>(),				new ComponentProviderRegistrator<AgilityComponent>(),				new ComponentProviderRegistrator<EnergyComponent>(),				new ComponentProviderRegistrator<CooldownComponent>(),				new ComponentProviderRegistrator<DamageComponent>(),				new ComponentProviderRegistrator<SpeedComponent>(),				new ComponentProviderRegistrator<HealthComponent>(),				new ComponentProviderRegistrator<TestComponent>(),				new ComponentProviderRegistrator<TestWorldSingleComponent>(),			};		}	}}