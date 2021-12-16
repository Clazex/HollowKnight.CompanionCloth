namespace CompanionCloth;

internal static class Listener {
	internal static void ListenKey() {
		if (Input.GetKeyDown(CompanionCloth.Instance!.GlobalSettings.summonKey)) {
			CompanionCloth.Instance!.LogFine("Summon key pressed");
			ClothControl.OnSummon();
		} else if (Input.GetKeyDown(CompanionCloth.Instance!.GlobalSettings.recallKey)) {
			CompanionCloth.Instance!.LogFine("Recall key pressed");
			ClothControl.OnRecall();
		}
	}
}
