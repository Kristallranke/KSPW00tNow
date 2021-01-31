using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSPW00tNow
{
	[KSPAddon(KSPAddon.Startup.Flight, false)]
	public class FlightControl : UnityEngine.MonoBehaviour
	{

		float[] oldKeystates;
		FlightCtrlState lastCtrlState;

		public static bool init = false;

		[System.Diagnostics.Conditional("DEBUG")]
		void EnableLogging()
		{
			LogManager.LevelDebug = true;
		}

		void Awake()
		{
			if (!init) {
				init = true;
				EnableLogging();
				LogManager.Debug("Awake");
				var harmony = new HarmonyLib.Harmony("KSPW00tNow");
				harmony.PatchAll();
			} else {
				LogManager.Debug("Awake");
			}

			oldKeystates = new float[256];
			lastCtrlState = null;

			FlightInputHandler.OnRawAxisInput += ControlUpdate;
		}

		IEnumerator Start()
		{
			yield break;
		}

		void OnDestroy()
		{
			LogManager.Debug("OnDestroy");
			FlightInputHandler.OnRawAxisInput -= ControlUpdate;
		}

		void Update()
		{
		}

		void OnEnable()
		{
			LogManager.Debug("OnEnable");
		}

		void OnDisable()
		{
			LogManager.Debug("OnDisable");
		}

		float Clamp(float value, float min, float max)
		{
			return Math.Min(max, Math.Max(min, value));
		}

		public void ControlUpdate(FlightCtrlState state)
		{
			ControlManager manager = ControlManager.GetInstance();
			manager.Update();
			FlightCtrlState newState = manager.flightState;

			ApplyAxis(ref state.pitch, newState.pitch, "Pitch");
			ApplyAxis(ref state.roll, newState.roll, "Roll");
			ApplyAxis(ref state.yaw, newState.yaw, "Yaw");
			ApplyAxis(ref state.wheelSteer, newState.wheelSteer, "Steer");
			ApplyAxis(ref state.X, newState.X, "TransX");
			ApplyAxis(ref state.Y, newState.Y, "TransY");
			ApplyAxis(ref state.Z, newState.Z, "TransZ");
			ApplyThrottleAxis(ref state.mainThrottle, newState.mainThrottle, "Throttle");
			ApplyThrottleAxis(ref state.wheelThrottle, newState.wheelThrottle, "Drive");

			lastCtrlState = state;
		}

		private void ApplyAxis(ref float output, float value, String text)
		{
			if (value != 0)
			{
				output = value;
				LogManager.Debug("ControlUpdate", $"{text} {value})");
			}
		}

		private void ApplyThrottleAxis(ref float output, float value, String text)
		{
			if (value != 0)
			{
				output = Clamp(output + value/32.0f, -1.0f, 1.0f);
				LogManager.Debug("ControlUpdate", $"{text} {value})");
			}
		}
	}
}
