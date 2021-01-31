using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSPW00tNow
{
	[HarmonyPatch(typeof(VesselAutopilot.VesselSAS))]
	[HarmonyPatch("CheckPitchInput")]
	class VesselAutopilot_VesselSAS_CheckPitchInput
	{
		static bool Prefix(VesselAutopilot.VesselSAS __instance, Vessel ___vessel, ref bool __result)
		{
			if (___vessel.isActiveAndEnabled && ControlManager.GetInstance().flightState.pitch != 0.0f) {
				__result = true;
				return false;
			}

			return true;
		}
	}

	[HarmonyPatch(typeof(VesselAutopilot.VesselSAS))]
	[HarmonyPatch("CheckYawInput")]
	class VesselAutopilot_VesselSAS_CheckYawInput
	{
		static bool Prefix(VesselAutopilot.VesselSAS __instance, Vessel ___vessel, ref bool __result)
		{
			if (___vessel.isActiveAndEnabled && ControlManager.GetInstance().flightState.yaw != 0.0f) {
				__result = true;
				return false;
			}

			return true;
		}
	}

	[HarmonyPatch(typeof(VesselAutopilot.VesselSAS))]
	[HarmonyPatch("CheckRollInput")]
	class VesselAutopilot_VesselSAS_CheckRollInput
	{
		static bool Prefix(VesselAutopilot.VesselSAS __instance, Vessel ___vessel, ref bool __result)
		{
			if (___vessel.isActiveAndEnabled && ControlManager.GetInstance().flightState.roll != 0.0f) {
				__result = true;
				return false;
			}

			return true;
		}
	}

	[HarmonyPatch(typeof(VesselAutopilot.VesselSAS))]
	[HarmonyPatch("ControlUpdate")]
	class VesselAutopilot_VesselSAS_ControlUpdate
	{
		static void Postfix(FlightCtrlState s, VesselAutopilot.VesselSAS __instance, Vessel ___vessel) {
		}
	}
}
