using Components;namespace HECSFramework.Core{	public partial class World	{		partial void FillRegistrators()		{			componentProviderRegistrators = new ComponentProviderRegistrator[]			{				new ComponentProviderRegistrator<TestReactComponent>(),				new ComponentProviderRegistrator<SoundVolumeComponent>(),				new ComponentProviderRegistrator<ScenarioAnimationComponent>(),				new ComponentProviderRegistrator<OverlapSphereCastContext>(),				new ComponentProviderRegistrator<StateInfoComponent>(),				new ComponentProviderRegistrator<InitOnAddAbilityTagComponent>(),				new ComponentProviderRegistrator<ViewReferenceGameObjectComponent>(),				new ComponentProviderRegistrator<ViewDestructionDelayedComponent>(),				new ComponentProviderRegistrator<TransformComponent>(),				new ComponentProviderRegistrator<CacheCounterValuesComponent>(),				new ComponentProviderRegistrator<AdditionalAbilityIndexComponent>(),				new ComponentProviderRegistrator<AbilityTagComponent>(),				new ComponentProviderRegistrator<AbilitiesHolderComponent>(),				new ComponentProviderRegistrator<FactionComponent>(),				new ComponentProviderRegistrator<AnimationCheckOutsHolderComponent>(),				new ComponentProviderRegistrator<StateDataComponent>(),				new ComponentProviderRegistrator<PoolableViewsProviderComponent>(),				new ComponentProviderRegistrator<OnApplicationQuitTagComponent>(),				new ComponentProviderRegistrator<NetworkEntityTagComponent>(),				new ComponentProviderRegistrator<InputActionsComponent>(),				new ComponentProviderRegistrator<StrategySideExecuteComponent>(),				new ComponentProviderRegistrator<TestSerializationComponent>(),				new ComponentProviderRegistrator<CountersHolderComponent>(),				new ComponentProviderRegistrator<AnimatorStateComponent>(),				new ComponentProviderRegistrator<StateTimerComponent>(),				new ComponentProviderRegistrator<RotationComponent>(),				new ComponentProviderRegistrator<IconComponent>(),				new ComponentProviderRegistrator<ChoosingPushDirectionSphereAbilityComponent>(),				new ComponentProviderRegistrator<AbilityPredicateComponent>(),				new ComponentProviderRegistrator<AbilityOwnerComponent>(),				new ComponentProviderRegistrator<VisualInActionTagComponent>(),				new ComponentProviderRegistrator<SoftCurrencyRewardVisualConfigComponent>(),				new ComponentProviderRegistrator<SpeedUpSphereComponent>(),				new ComponentProviderRegistrator<MainCameraComponent>(),				new ComponentProviderRegistrator<CollisionsComponent>(),				new ComponentProviderRegistrator<ArenaFinishComponent>(),				new ComponentProviderRegistrator<ArenaBattleGlobalConfigComponent>(),				new ComponentProviderRegistrator<PlayerTagComponent>(),				new ComponentProviderRegistrator<LevelModifierHolderComponent>(),				new ComponentProviderRegistrator<DropItemComponent>(),				new ComponentProviderRegistrator<CenterTagComponent>(),				new ComponentProviderRegistrator<PushDirectionComponent>(),				new ComponentProviderRegistrator<MainCanvasTagComponent>(),				new ComponentProviderRegistrator<SpeedDependsOnTimeScaleComponent>(),				new ComponentProviderRegistrator<SpawnDropItemsComponent>(),				new ComponentProviderRegistrator<CharactersHolderComponent>(),				new ComponentProviderRegistrator<NeedHpBarComponent>(),				new ComponentProviderRegistrator<SetupAfterViewTagComponent>(),				new ComponentProviderRegistrator<SphereCastContext>(),				new ComponentProviderRegistrator<GameStateComponent>(),				new ComponentProviderRegistrator<PassiveAbilityTag>(),				new ComponentProviderRegistrator<UntilSuccessStrategyNodeComponent>(),				new ComponentProviderRegistrator<SphereComponent>(),				new ComponentProviderRegistrator<NearestDropItemsComponent>(),				new ComponentProviderRegistrator<BelongingComponent>(),				new ComponentProviderRegistrator<SlowdownComponent>(),				new ComponentProviderRegistrator<ExplodeSpheresAbilityComponent>(),				new ComponentProviderRegistrator<CardTagComponent>(),				new ComponentProviderRegistrator<SplashScreenUIComponent>(),				new ComponentProviderRegistrator<OnStayComponent>(),				new ComponentProviderRegistrator<HealAbilityComponent>(),				new ComponentProviderRegistrator<CardsGlobalHolderComponent>(),				new ComponentProviderRegistrator<VisualFXHolderComponent>(),				new ComponentProviderRegistrator<PlayerLevelComponent>(),				new ComponentProviderRegistrator<InvincibleComponent>(),				new ComponentProviderRegistrator<CustomHpBarComponent>(),				new ComponentProviderRegistrator<ViewReferenceComponent>(),				new ComponentProviderRegistrator<PoolableTagComponent>(),				new ComponentProviderRegistrator<AutoApplyItemsWhenStayComponent>(),				new ComponentProviderRegistrator<ActorProviderComponent>(),				new ComponentProviderRegistrator<PlayerUpgradeComponent>(),				new ComponentProviderRegistrator<DirectionComponent>(),				new ComponentProviderRegistrator<StickTagComponent>(),				new ComponentProviderRegistrator<RadiusComponent>(),				new ComponentProviderRegistrator<UnityRectTransformComponent>(),				new ComponentProviderRegistrator<UIViewReferenceComponent>(),				new ComponentProviderRegistrator<UIPriorityIndexComponent>(),				new ComponentProviderRegistrator<ItemTagComponent>(),				new ComponentProviderRegistrator<AfterLifeTagComponent>(),				new ComponentProviderRegistrator<SavePositionComponent>(),				new ComponentProviderRegistrator<SpawnPointComponent>(),				new ComponentProviderRegistrator<LevelsHolderComponent>(),				new ComponentProviderRegistrator<ComicsComponent>(),				new ComponentProviderRegistrator<CachedEntitiesGlobalHolderComponent>(),				new ComponentProviderRegistrator<UIBusyTagComponent>(),				new ComponentProviderRegistrator<AdditionalCanvasTagComponent>(),				new ComponentProviderRegistrator<StateContextComponent>(),				new ComponentProviderRegistrator<TargetPositionComponent>(),				new ComponentProviderRegistrator<FinalLevelScreenComponent>(),				new ComponentProviderRegistrator<CameraComponent>(),				new ComponentProviderRegistrator<UITagComponent>(),				new ComponentProviderRegistrator<PredicatesComponent>(),				new ComponentProviderRegistrator<IsDeadTagComponent>(),				new ComponentProviderRegistrator<AppVersionComponent>(),				new ComponentProviderRegistrator<ActorContainerID>(),				new ComponentProviderRegistrator<MainCharacterTagComponent>(),				new ComponentProviderRegistrator<CharacterItemsComponent>(),				new ComponentProviderRegistrator<OverrideAnimatorComponent>(),				new ComponentProviderRegistrator<TestInitComponent>(),				new ComponentProviderRegistrator<CheckTwoComponent>(),				new ComponentProviderRegistrator<NavMeshAgentComponent>(),				new ComponentProviderRegistrator<InputListenerTagComponent>(),				new ComponentProviderRegistrator<IgnoreReferenceContainerTagComponent>(),				new ComponentProviderRegistrator<AfterLifeCompleteTagComponent>(),				new ComponentProviderRegistrator<UpgradeWindowComponent>(),				new ComponentProviderRegistrator<PegasusMovementComponent>(),				new ComponentProviderRegistrator<HealthBarsManagerComponent>(),				new ComponentProviderRegistrator<EnemyTagComponent>(),				new ComponentProviderRegistrator<ViewReadyTagComponent>(),				new ComponentProviderRegistrator<UnityTransformComponent>(),				new ComponentProviderRegistrator<UIGroupTagComponent>(),				new ComponentProviderRegistrator<TimeProviderComponent>(),				new ComponentProviderRegistrator<InputOverUIComponent>(),				new ComponentProviderRegistrator<GameLogicTagComponent>(),				new ComponentProviderRegistrator<WaitStateComponent>(),				new ComponentProviderRegistrator<AIStrategyComponent>(),				new ComponentProviderRegistrator<ItemsGlobalHolderComponent>(),				new ComponentProviderRegistrator<TimeScaleComponent>(),				new ComponentProviderRegistrator<SpheresControllerTagComponent>(),				new ComponentProviderRegistrator<ScenesHolderComponent>(),				new ComponentProviderRegistrator<TargetEntityComponent>(),				new ComponentProviderRegistrator<ShowHpBarTagComponent>(),				new ComponentProviderRegistrator<ActionsHolderComponent>(),				new ComponentProviderRegistrator<EnergyComponent>(),				new ComponentProviderRegistrator<DamageComponent>(),				new ComponentProviderRegistrator<SpeedComponent>(),				new ComponentProviderRegistrator<TestComponent>(),				new ComponentProviderRegistrator<TestWorldSingleComponent>(),				new ComponentProviderRegistrator<HealthComponent>(),				new ComponentProviderRegistrator<CooldownComponent>(),				new ComponentProviderRegistrator<RigidbodyProviderComponent>(),				new ComponentProviderRegistrator<RendererProviderComponent>(),				new ComponentProviderRegistrator<SoftCurrencyCounterMonoComponentProviderComponent>(),				new ComponentProviderRegistrator<EnergyRegenerationComponent>(),				new ComponentProviderRegistrator<SoftValueCounterComponent>(),				new ComponentProviderRegistrator<ActionsWithPredicateHolderComponent>(),			};		}	}}