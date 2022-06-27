namespace CompanionCloth;

public sealed class GlobalSettings {
	[JsonIgnore]
	internal string summonKey = "y";

	[JsonIgnore]
	internal string recallKey = "u";

	public string SummonKey {
		get => summonKey;
		internal set {
			try {
				_ = Input.GetKey(value);
			} catch (ArgumentException) {
				return;
			}

			summonKey = value;
		}
	}

	public string RecallKey {
		get => recallKey;
		internal set {
			try {
				_ = Input.GetKey(value);
			} catch (ArgumentException) {
				return;
			}

			summonKey = value;
		}
	}
}
