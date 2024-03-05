using HarmonyLib;
using Parkitect.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace SubsidizedParkTickets {
	[HarmonyPatch]
	public class APTPatch_Person_onReachedNewBlock {
		static MethodBase TargetMethod() => AccessTools.Method(typeof(Guest), "onReachedNewBlock");

		public static float? previousParkEntranceFee = null;
		public static Guest current = null;

		[HarmonyPrefix]
		public static void Prefix(Guest __instance) {
			//Debug.Log($"[SPT] onReachedNewBlock {__instance.getNameForUI()} start");
			current = __instance;
		}

		[HarmonyPostfix]
		public static void Postfix(Guest __instance) {
			current = null;
			if (previousParkEntranceFee.HasValue) {
				var gameController = GameController.Instance;
				gameController.park.parkInfo.setParkEntranceFee(previousParkEntranceFee.Value);
				previousParkEntranceFee = null;
			}
			//Debug.Log($"[SPT] onReachedNewBlock {__instance.getNameForUI()} end");
		}
	}
	[HarmonyPatch]
	public class APTPatch_Person_hasCooldownOn {
		static MethodBase TargetMethod() => AccessTools.Method(typeof(Person), "hasCooldownOn", new[] { typeof(Person.Cooldown) });

		[HarmonyPrefix]
		public static void Prefix(Person __instance, Person.Cooldown cooldownObject) {
			if (cooldownObject != Person.Cooldown.COOLDOWN_ENTER_PARK) return;
			//Debug.Log($"[SPT] hasCooldownOn {__instance.getNameForUI()} {cooldownObject} {SPTPatch_Person_onReachedNewBlock.current != null}");
			//Debug.Log("[SPT]" + Environment.StackTrace);
			if (APTPatch_Person_onReachedNewBlock.current == null) return;

			var gameController = GameController.Instance;
			var parkInfo = gameController.park.parkInfo;
			var parkEntranceFee = parkInfo.parkEntranceFee;
			var personMoney = __instance.Money;
			var maxFee = Mathf.Min(personMoney * 0.85f, Math.Max(0, personMoney - 20f));
			//Debug.Log($"[SPT] money={personMoney} maxFee={maxFee} fee={parkEntranceFee}");
			if (parkEntranceFee > maxFee) {
				//Debug.Log($"[SPT] Overriding fee from {parkEntranceFee} to {maxFee} for {__instance.getNameForUI()} (who only has ${personMoney})");
				APTPatch_Person_onReachedNewBlock.previousParkEntranceFee = parkEntranceFee;
				__instance.think(new Thought($"I got a ${(parkEntranceFee - maxFee).ToString("#.00")} discount!", Thought.Emotion.INFO));
				parkInfo.setParkEntranceFee(maxFee);
			}
			
		}
	}
}
