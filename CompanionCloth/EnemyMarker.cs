namespace CompanionCloth;

internal static class EnemyMarker {
	internal static void MarkAttacked(Collider2D otherCollider, GameObject _) =>
		Mark(otherCollider.gameObject);

	private static void Mark(GameObject go) {
		if (go.TryGetComponent<HealthManager>(out HealthManager? hm)) {
			if (hm is {
				isActiveAndEnabled: true,
				isDead: false,
				IsInvincible: false,
				hp: > 0
			}) {
				go.tag = "Boss";
				CompanionCloth.Instance!.LogDebug($"Marked {go.name} as enemy");
			}
		}
	}
}
