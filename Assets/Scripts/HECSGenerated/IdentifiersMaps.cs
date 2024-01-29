using System.Collections.Generic;public static class AdditionalCanvasIdentifierMap{	public const int HealthBarsCanvas = 707953248;	public const int TopCanvas = 1681681795;}public static class AnimationEventIdentifierMap{	public const int CreateBall = -2079758724;	public const int DeathEnd = 1115665750;	public const int StartAttack = -1235397065;}public static class CounterIdentifierContainerMap{	public const int Agility = 861769995;	public const int Cooldown = 1235009943;	public const int Damage = 491319389;	public const int Energy = 543945576;	public const int EnergyRegen = -1299763567;	public const int Health = 525528583;	public const int SoftValue = 1726309380;	public const int Speed = 284291376;}public static class FactionIdentifierMap{	public const int EnemyFactionIdentifier = -1366459792;	public const int PlayerFactionIdentifier = 2043874456;}public static class FXIdentifierMap{	public const int SoftValue = 1726309380;}public static class GameStateIdentifierMap{	public const int Arena = 282456396;	public const int FinishArena = -1350692264;	public const int PrepareArena = -504282146;	public const int BattleState = -1255925722;	public const int Final = 293269945;	public const int LoadPlayer = -1945733371;	public const int PrepareBattle = 710537165;	public const int PrepareWave = -1262087752;	public const int StartMenu = 1743745450;}public static class InputIdentifierMap{	public const int Stick = 291893696;	public const int Touch = 293007805;}public static class ModifierIdentifierMap{	public const int CounterLevel = -318222159;	public const int ResetToZero = -1196160578;	public const int SpeedUp = 763591493;	public const int TimeScale = 1589273513;}public static class SpawnPointIdentifierMap{	public const int DownLeftSpawnPointIdentifier = -1364756198;	public const int DownRightSpawnPointIdentifier = -50648793;	public const int DownSpawnPointIdentifier = 1582784144;	public const int DropItemSpawnPoint = 597665225;	public const int LeftSpawnPointIdentifier = 1598906990;	public const int Right2SpawnPointIdentifier = 1484359557;	public const int RightSpawnPointIdentifier = 1434715809;	public const int SphereSpawnPointIdentifier = 1579842890;	public const int UpLeftSpawnPointIdentifier = 1615362424;	public const int UpRightSpawnPointIdentifier = -2142322231;	public const int UpSpawnPointIdentifier = -1287301788;}public static class UIGroupIdentifierMap{	public const int ArenaBattleGroup = 965069899;	public const int BattleGroup = -1176628548;	public const int EmptyGroup = -1863869610;	public const int FinalScreenGroup = 905503682;	public const int LoseScreenGroup = -733401422;	public const int StartScreenGroup = 946727171;}public static class UIIdentifierMap{	public const int ArenaBattleWindow_UIIdentifier = 194597572;	public const int BattleWindow_UIIdentifier = 660823757;	public const int BlockUIOneClickWindow_UIIdentifier = 1268740301;	public const int Comics_UIIdentifier = -1638967349;	public const int DamageTextVisualizer_UIIdentifier = -1064658517;	public const int FinalScreen_UIIdentifier = 764734702;	public const int HPBar_UIIdentifier = 359418401;	public const int Joystick_UIIdentifier = -623764641;	public const int LoseScreen_UIIdentifier = 1445918512;	public const int SplashScreen_UIIdentifier = 487089502;	public const int StartScreen_UIIdentifier = 805958191;	public const int Upgrade_UIIdentifier = 813247423;	public const int Dialogue = 1207679710;}public static partial class EntityContainersMap{	static EntityContainersMap()	{		EntityContainersIDtoString = new Dictionary<int, string>		{			{ 1336719493, "DragonDefaultAttackAbility" },			{ -1425178498, "FireballRingAbility" },			{ 560588535, "BaseDefaultAttackAbilityContainer" },			{ -1128263586, "HealAbility" },			{ 951824733, "AddSphereAbility" },			{ -548980526, "ChoosingPushDirectionSphereAbility" },			{ 1207283189, "ExplodeSphereAbility" },			{ -1460799749, "PushSphereAbility" },			{ -142166732, "ArenaFeatureContainer" },			{ -200413309, "DropFireballContainer" },			{ -416818419, "DropItemBaseContainer" },			{ -1553468731, "FireballContainer" },			{ 1482079447, "BreakableBuildingContainer" },			{ -1543640249, "BuildingContainer" },			{ -1309079545, "CameraBrain" },			{ 1056233807, "AddBallCardContainer" },			{ -1590033529, "BaseCardContainer" },			{ -357962829, "BasePushCardContainer" },			{ 651546728, "ChoosingDirToPushCardContainer" },			{ 1567902899, "ExplodeSpheresCardContainer" },			{ -1589509249, "HealCardContainer" },			{ -368447397, "PushDownCardContainer" },			{ -1492487634, "PushUpCardContainer" },			{ -404497827, "PushLeftCardContainer" },			{ 999840029, "PushLeftDownCardContainer" },			{ 1784153924, "PushLeftUpCardContainer" },			{ -1538237565, "PushRightCardContainer" },			{ 1225798097, "PushRightDownCardContainer" },			{ 1203960296, "PushRightUpCardContainer" },			{ -747897427, "CenterContainer" },			{ -1399352187, "EnemyArenaCharacterContainer" },			{ 1968583117, "MainArenaCharacterContainer" },			{ 1889445451, "DefaultSpawnPointContainer" },			{ 1298982807, "ArcherEnemyContainer" },			{ -94059594, "EnemyDefaultContainer" },			{ -1163689974, "CatapultEnemyContainer" },			{ 2138875841, "CommanderEnemyContainer" },			{ 617038772, "MageEnemyContainer" },			{ 1242888374, "MountedMageEnemyContainer" },			{ -49948529, "PegasusEnemyContainer" },			{ -1263303690, "SpearmanEnemyContainer" },			{ 1594580010, "GameLogic" },			{ -972199138, "HealthBarCanvas" },			{ -348625043, "InputManager" },			{ -2064147318, "MainCamera" },			{ -1955105294, "MainCanvas" },			{ -743113290, "PlayerContainer" },			{ -398827109, "SceneManager" },			{ 1681681795, "TopCanvas" },			{ 1699044216, "UIManager" },			{ -265986890, "VFXContainer" },			{ -1601112874, "MainCharContainer" },			{ 597665225, "DropItemSpawnPoint" },			{ -800719749, "SphereContainer" },			{ 1850830982, "ArenaBattleWindowContainer" },			{ -45358591, "BattleWindowContainer" },			{ 1457355451, "BlockUIOneClickWindowContainer" },			{ -776472283, "ComicsContainer" },			{ 550413991, "DamageTextVisualizerContainer" },			{ 1161041570, "FinalScreenContainer" },			{ 2086589855, "HPBarContainer" },			{ -1507985345, "JoystickContainer" },			{ -1348051794, "LoseScreenContainer" },			{ -219092846, "SplashScreenContainer" },			{ 1202265059, "StartScreenContainer" },			{ 857817143, "UpgradeContainer" },		};	}	public const int DragonDefaultAttackAbility = 1336719493;	public const string DragonDefaultAttackAbility_string = "DragonDefaultAttackAbility";	public const int FireballRingAbility = -1425178498;	public const string FireballRingAbility_string = "FireballRingAbility";	public const int BaseDefaultAttackAbilityContainer = 560588535;	public const string BaseDefaultAttackAbilityContainer_string = "BaseDefaultAttackAbilityContainer";	public const int HealAbility = -1128263586;	public const string HealAbility_string = "HealAbility";	public const int AddSphereAbility = 951824733;	public const string AddSphereAbility_string = "AddSphereAbility";	public const int ChoosingPushDirectionSphereAbility = -548980526;	public const string ChoosingPushDirectionSphereAbility_string = "ChoosingPushDirectionSphereAbility";	public const int ExplodeSphereAbility = 1207283189;	public const string ExplodeSphereAbility_string = "ExplodeSphereAbility";	public const int PushSphereAbility = -1460799749;	public const string PushSphereAbility_string = "PushSphereAbility";	public const int ArenaFeatureContainer = -142166732;	public const string ArenaFeatureContainer_string = "ArenaFeatureContainer";	public const int DropFireballContainer = -200413309;	public const string DropFireballContainer_string = "DropFireballContainer";	public const int DropItemBaseContainer = -416818419;	public const string DropItemBaseContainer_string = "DropItemBaseContainer";	public const int FireballContainer = -1553468731;	public const string FireballContainer_string = "FireballContainer";	public const int BreakableBuildingContainer = 1482079447;	public const string BreakableBuildingContainer_string = "BreakableBuildingContainer";	public const int BuildingContainer = -1543640249;	public const string BuildingContainer_string = "BuildingContainer";	public const int CameraBrain = -1309079545;	public const string CameraBrain_string = "CameraBrain";	public const int AddBallCardContainer = 1056233807;	public const string AddBallCardContainer_string = "AddBallCardContainer";	public const int BaseCardContainer = -1590033529;	public const string BaseCardContainer_string = "BaseCardContainer";	public const int BasePushCardContainer = -357962829;	public const string BasePushCardContainer_string = "BasePushCardContainer";	public const int ChoosingDirToPushCardContainer = 651546728;	public const string ChoosingDirToPushCardContainer_string = "ChoosingDirToPushCardContainer";	public const int ExplodeSpheresCardContainer = 1567902899;	public const string ExplodeSpheresCardContainer_string = "ExplodeSpheresCardContainer";	public const int HealCardContainer = -1589509249;	public const string HealCardContainer_string = "HealCardContainer";	public const int PushDownCardContainer = -368447397;	public const string PushDownCardContainer_string = "PushDownCardContainer";	public const int PushUpCardContainer = -1492487634;	public const string PushUpCardContainer_string = "PushUpCardContainer";	public const int PushLeftCardContainer = -404497827;	public const string PushLeftCardContainer_string = "PushLeftCardContainer";	public const int PushLeftDownCardContainer = 999840029;	public const string PushLeftDownCardContainer_string = "PushLeftDownCardContainer";	public const int PushLeftUpCardContainer = 1784153924;	public const string PushLeftUpCardContainer_string = "PushLeftUpCardContainer";	public const int PushRightCardContainer = -1538237565;	public const string PushRightCardContainer_string = "PushRightCardContainer";	public const int PushRightDownCardContainer = 1225798097;	public const string PushRightDownCardContainer_string = "PushRightDownCardContainer";	public const int PushRightUpCardContainer = 1203960296;	public const string PushRightUpCardContainer_string = "PushRightUpCardContainer";	public const int CenterContainer = -747897427;	public const string CenterContainer_string = "CenterContainer";	public const int EnemyArenaCharacterContainer = -1399352187;	public const string EnemyArenaCharacterContainer_string = "EnemyArenaCharacterContainer";	public const int MainArenaCharacterContainer = 1968583117;	public const string MainArenaCharacterContainer_string = "MainArenaCharacterContainer";	public const int DefaultSpawnPointContainer = 1889445451;	public const string DefaultSpawnPointContainer_string = "DefaultSpawnPointContainer";	public const int ArcherEnemyContainer = 1298982807;	public const string ArcherEnemyContainer_string = "ArcherEnemyContainer";	public const int EnemyDefaultContainer = -94059594;	public const string EnemyDefaultContainer_string = "EnemyDefaultContainer";	public const int CatapultEnemyContainer = -1163689974;	public const string CatapultEnemyContainer_string = "CatapultEnemyContainer";	public const int CommanderEnemyContainer = 2138875841;	public const string CommanderEnemyContainer_string = "CommanderEnemyContainer";	public const int MageEnemyContainer = 617038772;	public const string MageEnemyContainer_string = "MageEnemyContainer";	public const int MountedMageEnemyContainer = 1242888374;	public const string MountedMageEnemyContainer_string = "MountedMageEnemyContainer";	public const int PegasusEnemyContainer = -49948529;	public const string PegasusEnemyContainer_string = "PegasusEnemyContainer";	public const int SpearmanEnemyContainer = -1263303690;	public const string SpearmanEnemyContainer_string = "SpearmanEnemyContainer";	public const int GameLogic = 1594580010;	public const string GameLogic_string = "GameLogic";	public const int HealthBarCanvas = -972199138;	public const string HealthBarCanvas_string = "HealthBarCanvas";	public const int InputManager = -348625043;	public const string InputManager_string = "InputManager";	public const int MainCamera = -2064147318;	public const string MainCamera_string = "MainCamera";	public const int MainCanvas = -1955105294;	public const string MainCanvas_string = "MainCanvas";	public const int PlayerContainer = -743113290;	public const string PlayerContainer_string = "PlayerContainer";	public const int SceneManager = -398827109;	public const string SceneManager_string = "SceneManager";	public const int TopCanvas = 1681681795;	public const string TopCanvas_string = "TopCanvas";	public const int UIManager = 1699044216;	public const string UIManager_string = "UIManager";	public const int VFXContainer = -265986890;	public const string VFXContainer_string = "VFXContainer";	public const int MainCharContainer = -1601112874;	public const string MainCharContainer_string = "MainCharContainer";	public const int DropItemSpawnPoint = 597665225;	public const string DropItemSpawnPoint_string = "DropItemSpawnPoint";	public const int SphereContainer = -800719749;	public const string SphereContainer_string = "SphereContainer";	public const int ArenaBattleWindowContainer = 1850830982;	public const string ArenaBattleWindowContainer_string = "ArenaBattleWindowContainer";	public const int BattleWindowContainer = -45358591;	public const string BattleWindowContainer_string = "BattleWindowContainer";	public const int BlockUIOneClickWindowContainer = 1457355451;	public const string BlockUIOneClickWindowContainer_string = "BlockUIOneClickWindowContainer";	public const int ComicsContainer = -776472283;	public const string ComicsContainer_string = "ComicsContainer";	public const int DamageTextVisualizerContainer = 550413991;	public const string DamageTextVisualizerContainer_string = "DamageTextVisualizerContainer";	public const int FinalScreenContainer = 1161041570;	public const string FinalScreenContainer_string = "FinalScreenContainer";	public const int HPBarContainer = 2086589855;	public const string HPBarContainer_string = "HPBarContainer";	public const int JoystickContainer = -1507985345;	public const string JoystickContainer_string = "JoystickContainer";	public const int LoseScreenContainer = -1348051794;	public const string LoseScreenContainer_string = "LoseScreenContainer";	public const int SplashScreenContainer = -219092846;	public const string SplashScreenContainer_string = "SplashScreenContainer";	public const int StartScreenContainer = 1202265059;	public const string StartScreenContainer_string = "StartScreenContainer";	public const int UpgradeContainer = 857817143;	public const string UpgradeContainer_string = "UpgradeContainer";}public static partial class AbilitiesMap{	static AbilitiesMap()	{		AbilitiesToIdentifiersMap = new Dictionary<string, int>		{			{ "DragonDefaultAttackAbility", 1336719493 },			{ "FireballRingAbility", -1425178498 },			{ "BaseDefaultAttackAbilityContainer", 560588535 },			{ "HealAbility", -1128263586 },			{ "AddSphereAbility", 951824733 },			{ "ChoosingPushDirectionSphereAbility", -548980526 },			{ "ExplodeSphereAbility", 1207283189 },			{ "PushSphereAbility", -1460799749 },		};	}	public const int DragonDefaultAttackAbility = 1336719493;	public const string DragonDefaultAttackAbility_string = "DragonDefaultAttackAbility";	public const int FireballRingAbility = -1425178498;	public const string FireballRingAbility_string = "FireballRingAbility";	public const int BaseDefaultAttackAbilityContainer = 560588535;	public const string BaseDefaultAttackAbilityContainer_string = "BaseDefaultAttackAbilityContainer";	public const int HealAbility = -1128263586;	public const string HealAbility_string = "HealAbility";	public const int AddSphereAbility = 951824733;	public const string AddSphereAbility_string = "AddSphereAbility";	public const int ChoosingPushDirectionSphereAbility = -548980526;	public const string ChoosingPushDirectionSphereAbility_string = "ChoosingPushDirectionSphereAbility";	public const int ExplodeSphereAbility = 1207283189;	public const string ExplodeSphereAbility_string = "ExplodeSphereAbility";	public const int PushSphereAbility = -1460799749;	public const string PushSphereAbility_string = "PushSphereAbility";}public static class NetworkEntityContainersMap{}public static class IdentifierToStringMap{	public static readonly Dictionary<int, string> IntToString = new Dictionary<int, string>	{		{ 707953248, "HealthBarsCanvas"},		{ 1681681795, "TopCanvas"},		{ -2079758724, "CreateBall"},		{ 1115665750, "DeathEnd"},		{ -1235397065, "StartAttack"},		{ 861769995, "Agility"},		{ 1235009943, "Cooldown"},		{ 491319389, "Damage"},		{ 543945576, "Energy"},		{ -1299763567, "EnergyRegen"},		{ 525528583, "Health"},		{ 1726309380, "SoftValue"},		{ 284291376, "Speed"},		{ -1366459792, "EnemyFactionIdentifier"},		{ 2043874456, "PlayerFactionIdentifier"},		{ 282456396, "Arena"},		{ -1350692264, "FinishArena"},		{ -504282146, "PrepareArena"},		{ -1255925722, "BattleState"},		{ 293269945, "Final"},		{ -1945733371, "LoadPlayer"},		{ 710537165, "PrepareBattle"},		{ -1262087752, "PrepareWave"},		{ 1743745450, "StartMenu"},		{ 291893696, "Stick"},		{ 293007805, "Touch"},		{ -318222159, "CounterLevel"},		{ -1196160578, "ResetToZero"},		{ 763591493, "SpeedUp"},		{ 1589273513, "TimeScale"},		{ -1364756198, "DownLeftSpawnPointIdentifier"},		{ -50648793, "DownRightSpawnPointIdentifier"},		{ 1582784144, "DownSpawnPointIdentifier"},		{ 597665225, "DropItemSpawnPoint"},		{ 1598906990, "LeftSpawnPointIdentifier"},		{ 1484359557, "Right2SpawnPointIdentifier"},		{ 1434715809, "RightSpawnPointIdentifier"},		{ 1579842890, "SphereSpawnPointIdentifier"},		{ 1615362424, "UpLeftSpawnPointIdentifier"},		{ -2142322231, "UpRightSpawnPointIdentifier"},		{ -1287301788, "UpSpawnPointIdentifier"},		{ 965069899, "ArenaBattleGroup"},		{ -1176628548, "BattleGroup"},		{ -1863869610, "EmptyGroup"},		{ 905503682, "FinalScreenGroup"},		{ -733401422, "LoseScreenGroup"},		{ 946727171, "StartScreenGroup"},		{ 194597572, "ArenaBattleWindow_UIIdentifier"},		{ 660823757, "BattleWindow_UIIdentifier"},		{ 1268740301, "BlockUIOneClickWindow_UIIdentifier"},		{ -1638967349, "Comics_UIIdentifier"},		{ -1064658517, "DamageTextVisualizer_UIIdentifier"},		{ 764734702, "FinalScreen_UIIdentifier"},		{ 359418401, "HPBar_UIIdentifier"},		{ -623764641, "Joystick_UIIdentifier"},		{ 1445918512, "LoseScreen_UIIdentifier"},		{ 487089502, "SplashScreen_UIIdentifier"},		{ 805958191, "StartScreen_UIIdentifier"},		{ 813247423, "Upgrade_UIIdentifier"},		{ 1207679710, "Dialogue"},	};	public const string HealthBarsCanvas = "HealthBarsCanvas";	public const string TopCanvas = "TopCanvas";	public const string CreateBall = "CreateBall";	public const string DeathEnd = "DeathEnd";	public const string StartAttack = "StartAttack";	public const string Agility = "Agility";	public const string Cooldown = "Cooldown";	public const string Damage = "Damage";	public const string Energy = "Energy";	public const string EnergyRegen = "EnergyRegen";	public const string Health = "Health";	public const string SoftValue = "SoftValue";	public const string Speed = "Speed";	public const string EnemyFactionIdentifier = "EnemyFactionIdentifier";	public const string PlayerFactionIdentifier = "PlayerFactionIdentifier";	public const string Arena = "Arena";	public const string FinishArena = "FinishArena";	public const string PrepareArena = "PrepareArena";	public const string BattleState = "BattleState";	public const string Final = "Final";	public const string LoadPlayer = "LoadPlayer";	public const string PrepareBattle = "PrepareBattle";	public const string PrepareWave = "PrepareWave";	public const string StartMenu = "StartMenu";	public const string Stick = "Stick";	public const string Touch = "Touch";	public const string CounterLevel = "CounterLevel";	public const string ResetToZero = "ResetToZero";	public const string SpeedUp = "SpeedUp";	public const string TimeScale = "TimeScale";	public const string DownLeftSpawnPointIdentifier = "DownLeftSpawnPointIdentifier";	public const string DownRightSpawnPointIdentifier = "DownRightSpawnPointIdentifier";	public const string DownSpawnPointIdentifier = "DownSpawnPointIdentifier";	public const string DropItemSpawnPoint = "DropItemSpawnPoint";	public const string LeftSpawnPointIdentifier = "LeftSpawnPointIdentifier";	public const string Right2SpawnPointIdentifier = "Right2SpawnPointIdentifier";	public const string RightSpawnPointIdentifier = "RightSpawnPointIdentifier";	public const string SphereSpawnPointIdentifier = "SphereSpawnPointIdentifier";	public const string UpLeftSpawnPointIdentifier = "UpLeftSpawnPointIdentifier";	public const string UpRightSpawnPointIdentifier = "UpRightSpawnPointIdentifier";	public const string UpSpawnPointIdentifier = "UpSpawnPointIdentifier";	public const string ArenaBattleGroup = "ArenaBattleGroup";	public const string BattleGroup = "BattleGroup";	public const string EmptyGroup = "EmptyGroup";	public const string FinalScreenGroup = "FinalScreenGroup";	public const string LoseScreenGroup = "LoseScreenGroup";	public const string StartScreenGroup = "StartScreenGroup";	public const string ArenaBattleWindow_UIIdentifier = "ArenaBattleWindow_UIIdentifier";	public const string BattleWindow_UIIdentifier = "BattleWindow_UIIdentifier";	public const string BlockUIOneClickWindow_UIIdentifier = "BlockUIOneClickWindow_UIIdentifier";	public const string Comics_UIIdentifier = "Comics_UIIdentifier";	public const string DamageTextVisualizer_UIIdentifier = "DamageTextVisualizer_UIIdentifier";	public const string FinalScreen_UIIdentifier = "FinalScreen_UIIdentifier";	public const string HPBar_UIIdentifier = "HPBar_UIIdentifier";	public const string Joystick_UIIdentifier = "Joystick_UIIdentifier";	public const string LoseScreen_UIIdentifier = "LoseScreen_UIIdentifier";	public const string SplashScreen_UIIdentifier = "SplashScreen_UIIdentifier";	public const string StartScreen_UIIdentifier = "StartScreen_UIIdentifier";	public const string Upgrade_UIIdentifier = "Upgrade_UIIdentifier";	public const string Dialogue = "Dialogue";}