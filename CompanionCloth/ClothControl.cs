using HC = HeroController;

namespace CompanionCloth;

internal static class ClothControl {
	private static readonly Vector3 posShift = new(0.0f, 1.2484f, 0.011f);

	private static GameObject? clothInstance = null;

	private static Vector3 HeroPos() =>
		HC.instance.transform.position;

	internal static void OnSummon() {
		if (clothInstance == null) {
			Summon();
		} else if (!clothInstance.activeSelf) {
			Despawn();
			Summon();
		} else {
			Despawn();
		}
	}

	internal static void OnRecall() {
		if (clothInstance is null or { activeSelf: false }) {
			return;
		}

		Recall();
	}


	private static void Summon() {
		clothInstance = UObject.Instantiate(
			CompanionCloth.clothFighter,
			HeroPos() + posShift,
			Quaternion.identity
		) ?? throw new NullReferenceException(
			"Failed to instantiate Cloth Fighter game object"
		);

		clothInstance.name = "Cloth Fighter Companion";

		HC.instance.StartCoroutine(Activate());
	}

	private static void Despawn() {
		UObject.DestroyImmediate(clothInstance);
		clothInstance = null;
	}

	private static void Recall() {
		clothInstance.LocateMyFSM("Control").SetState("Cooldown");
		clothInstance!.GetComponent<tk2dSpriteAnimator>().Play("Idle");
		clothInstance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		clothInstance.transform.position = HeroPos() + posShift;
	}


	private static IEnumerator Activate() {
		yield return null;

		clothInstance!.SetActive(true);
		CompanionCloth.Instance!.LogDebug("Spawned Cloth");

		yield break;
	}
}
