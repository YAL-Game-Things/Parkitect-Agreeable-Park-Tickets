using System;
using System.Collections.Generic;
using System.Linq;
using Parkitect;
using UnityEngine;
using HarmonyLib;
using System.Windows.Markup;
using System.IO;
using MiniJSON;
using Mono.Security.Authenticode;

namespace AgreeableParkTickets {
    public class AgreeableParkTickets : AbstractMod
    {
        public const string VERSION_NUMBER = "1.0.0";
        public override string getIdentifier() => "cc.yal.AgreeableParkTickets";
        public override string getName() => "Agreeable Park Tickets";
        public override string getDescription() => @"The guests are given a park entrance fee discount if they cannot (or can hardly) afford it";

        public override string getVersionNumber() => VERSION_NUMBER;
        public override bool isMultiplayerModeCompatible() => true;
        public override bool isRequiredByAllPlayersInMultiplayerMode() => true;

        public static AgreeableParkTickets Instance;
        private Harmony _harmony;

        public override void onEnabled() {
            Instance = this;

            Debug.Log("[SPT] Doing a Harmony patch!");
			_harmony = new Harmony(getIdentifier());
			_harmony.PatchAll();
			Debug.Log("[SPT] Patched alright!");
		}

        public override void onDisabled() {
            _harmony?.UnpatchAll(getIdentifier());
		}
    }
}
