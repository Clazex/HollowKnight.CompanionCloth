namespace CompanionCloth;

internal static class ClothFSM {
	internal static void ModifyFSM(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self) {
		orig(self);

		if (self is {
			FsmName: "Control",
			gameObject.name: "Cloth Fighter Companion"
		}) {
			ModifyFSM(self);

			CompanionCloth.Instance!.LogDebug("Cloth Fighter Companion FSM modified");
		}
	}

	internal static void ModifyFSM(PlayMakerFSM fsm) {
		FsmState readyState = fsm.Fsm.GetState("Ready");
		FsmState idleState = fsm.Fsm.GetState("Idle");
		FsmState cdState = fsm.CreateState("Cooldown");

		FsmEvent cancelEvent = fsm.FsmEvents.First(e => e.Name == "CANCEL");

		fsm.ChangeTransition("Init", "FINISHED", "Ready");
		idleState.AddTransition(cancelEvent, "Cooldown");
		cdState.AddTransition(FsmEvent.Finished, "Idle");

		readyState.InsertAction(0, fsm.GetAction<SetMeshRenderer>("Enter", 0));
		readyState.InsertAction(1, fsm.GetAction<SetGravity2dScale>("Enter", 2));
		readyState.InsertAction(2, fsm.GetAction<SetBoxColliderTrigger>("Collider On", 0));
		readyState.InsertAction(3, fsm.GetAction<Tk2dPlayAnimation>("Idle Wait", 0));

		idleState.RemoveAction(0);
		idleState.InsertAction(3, new GameObjectIsNull() {
			gameObject = fsm.FsmVariables.FindFsmGameObject("Boss"),
			isNull = cancelEvent
		});

		cdState.AddAction(new Wait() {
			time = 0.1f,
			finishEvent = FsmEvent.Finished
		});
	}
}
