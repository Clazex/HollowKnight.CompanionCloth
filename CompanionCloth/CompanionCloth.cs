namespace CompanionCloth;

public sealed class CompanionCloth :
	Mod, ITogglableMod, IGlobalSettings<GlobalSettings> {
	private const string clothScene = "Fungus3_23_boss";
	private const string clothPath = "Battle Scene/Cloth Entry/Cloth Fighter";

	public static CompanionCloth? Instance { get; private set; }

	internal static GameObject? clothFighter = null;


	public override string GetVersion() => Assembly
		.GetExecutingAssembly()
		.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
		.InformationalVersion;

	public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloads) {
		if (Instance != null) {
			return;
		}

		Instance = this;

		if (preloads != null) {
			clothFighter = preloads[clothScene][clothPath];
			LogDebug("Preloads saved");
		}

		ModHooks.HeroUpdateHook += Listener.ListenKey;
		ModHooks.SlashHitHook += EnemyMarker.MarkAttacked;
		On.PlayMakerFSM.OnEnable += ClothFSM.ModifyFSM;
	}

	public void Unload() {
		ModHooks.HeroUpdateHook -= Listener.ListenKey;
		ModHooks.SlashHitHook -= EnemyMarker.MarkAttacked;
		On.PlayMakerFSM.OnEnable -= ClothFSM.ModifyFSM;

		Instance = null;
	}


	public override List<(string, string)> GetPreloadNames() => new() {
		(clothScene, clothPath)
	};


	public GlobalSettings GlobalSettings { get; private set; } = new();
	public void OnLoadGlobal(GlobalSettings s) => GlobalSettings = s;
	public GlobalSettings OnSaveGlobal() => GlobalSettings;
}
