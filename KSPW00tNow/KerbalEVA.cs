using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace KSPW00tNow
{
	[HarmonyPatch(typeof(KerbalEVA))]
	[HarmonyPatch("HandleMovementInput")]
	class KerbalEVA_HandleMovementInput
	{
		static void Postfix(KerbalEVA __instance, ref Single ___tgtBoundStep, ref Vector3 ___tgtRpos, ref Vector3 ___packTgtRPos, ref Vector3 ___ladderTgtRPos, ref Single ___tgtSpeed, ref Single ___lastTgtSpeed,
							ref Quaternion ___rd_tgtRot, ref Vector3 ___tgtFwd, ref Vector3 ___tgtUp, ref Vector3 ___cmdRot, ref Vector3 ___cmdDir, ref Vector3 ___parachuteInput, ref bool ___manualAxisControl)
		{
			if (!__instance.vessel.isActiveVessel || __instance.vessel.packed) {
				return;
			}

			var mgr = ControlManager.GetInstance();
			mgr.Update();
			KerbalCtrlState newState = mgr.kerbalState;

			bool faceCamera = __instance.CharacterFrameMode;
			Transform transform = __instance.transform;

			LogManager.Log("HandleMovementInput", $"-------------------------");
			LogMovementData(__instance, ___tgtBoundStep, ___tgtRpos, ___packTgtRPos, ___ladderTgtRPos, ___tgtSpeed, ___lastTgtSpeed, ___rd_tgtRot, ___tgtFwd, ___tgtUp, ___cmdRot, ___cmdDir, ___parachuteInput, ___manualAxisControl);

			if (newState.forward != 0 || newState.sideway != 0) {
				float upscale = 1.0f / Math.Max(Math.Abs(newState.forward), Math.Abs(newState.sideway));
				float scale = (float)(1.0 / Math.Sqrt(Math.Pow(newState.forward * upscale, 2.0f) + Math.Pow(newState.sideway * upscale, 2.0f)));

				if (faceCamera)
				{
					___tgtRpos = transform.forward * newState.forward * scale;
					___tgtRpos += transform.right * newState.sideway * scale;
					___tgtFwd = __instance.fFwd;
				} else {
					___tgtRpos = __instance.fFwd * newState.forward * scale;
					___tgtRpos += __instance.fRgt * newState.sideway * scale;
					___tgtFwd = ___tgtRpos.normalized;
				}

				// No scaling necessary, KSP actually ignores the vector length
				___ladderTgtRPos = transform.up * newState.forward;
				___ladderTgtRPos += transform.right * newState.sideway;
				if (__instance.OnALadder) {
					___tgtSpeed = newState.forward * __instance.ladderClimbSpeed;
				}
			}

			if (newState.packX != 0 || newState.packY != 0 || newState.packZ != 0) {
				___packTgtRPos = transform.right * newState.packX;
				___packTgtRPos += transform.up * newState.packY;
				___packTgtRPos += transform.forward * newState.packZ;
			}

			if (newState.forward != 0.0f) {
				___parachuteInput.x = newState.forward;
			}
			if (newState.sideway != 0.0f) {
				___parachuteInput.y = newState.sideway;
			}

			LogManager.Log("HandleMovementInput", $"-----------");
			LogMovementData(__instance, ___tgtBoundStep, ___tgtRpos, ___packTgtRPos, ___ladderTgtRPos, ___tgtSpeed, ___lastTgtSpeed, ___rd_tgtRot, ___tgtFwd, ___tgtUp, ___cmdRot, ___cmdDir, ___parachuteInput, ___manualAxisControl);
		}

		static void LogMovementData(KerbalEVA __instance, float ___tgtBoundStep, Vector3 ___tgtRpos, Vector3 ___packTgtRPos, Vector3 ___ladderTgtRPos, float ___tgtSpeed, float ___lastTgtSpeed, Quaternion ___rd_tgtRot, Vector3 ___tgtFwd, Vector3 ___tgtUp, Vector3 ___cmdRot, Vector3 ___cmdDir, Vector3 ___parachuteInput, bool ___manualAxisControl)
		{
			LogManager.Log("HandleMovementInput", $"CharacterFrameMode {__instance.CharacterFrameMode}");
			LogManager.Log("HandleMovementInput", $"tgtBoundStep {___tgtBoundStep}");
			LogManager.Log("HandleMovementInput", $"tgtRpos {___tgtRpos}");
			LogManager.Log("HandleMovementInput", $"packTgtRPos {___packTgtRPos}");
			LogManager.Log("HandleMovementInput", $"ladderTgtRPos {___ladderTgtRPos}");
			LogManager.Log("HandleMovementInput", $"tgtSpeed {___tgtSpeed}");
			LogManager.Log("HandleMovementInput", $"lastTgtSpeed {___lastTgtSpeed}");
			LogManager.Log("HandleMovementInput", $"rd_tgtRot {___rd_tgtRot}");
			LogManager.Log("HandleMovementInput", $"tgtFwd {___tgtFwd}");
			LogManager.Log("HandleMovementInput", $"tgtUp {___tgtUp}");
			LogManager.Log("HandleMovementInput", $"parachuteInput {___parachuteInput}");
			LogManager.Log("HandleMovementInput", $"manualAxisControl {___manualAxisControl}");
			//			LogManager.Log("HandleMovementInput", $"|cmdRot {___cmdRot}");
			//			LogManager.Log("HandleMovementInput", $"|cmdDir {___cmdDir}");
		}
	}
}
