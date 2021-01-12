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
		bool error;

		float[] oldKeystates;
		FlightCtrlState lastCtrlState;

		static bool wootInit = false;

		private void Log(String name, String message = "")
		{
			if (message == "") {
				UnityEngine.Debug.Log($"[KSPW00tNow] {name}");
			} else {
				UnityEngine.Debug.Log($"[KSPW00tNow] {name}: {message}");
			}
		}

		void Awake()
		{
			Log("Awake");
			error = false;

			oldKeystates = new float[256];
			lastCtrlState = null;

			FlightInputHandler.OnRawAxisInput += ControlUpdate;
			if (!wootInit) {
				wootInit = true;
				WootingAnalogSDKNET.WootingAnalogSDK.Initialise();
			}
		}

		IEnumerator Start()
		{
			yield break;
		}

		void OnDestroy()
		{
			FlightInputHandler.OnRawAxisInput -= ControlUpdate;
			//WootingAnalogSDKNET.WootingAnalogSDK.UnInitialise();
			Log("OnDestroy");
		}

		void Update()
		{
		}

		void OnEnable()
		{
			Log("OnEnable");
		}

		void OnDisable()
		{
			Log("OnDisable");
		}

		float Clamp(float value, float min, float max)
		{
			return Math.Min(max, Math.Max(min, value));
		}

		public void ControlUpdate(FlightCtrlState state)
		{
			float pitch = 0.0f;
			float roll = 0.0f;
			float yaw = 0.0f;
			float throttle = 0.0f;

			float transX = 0.0f;
			float transY = 0.0f;
			float transZ = 0.0f;

			float steer = 0.0f;
			float drive = 0.0f;

			var (keys, readErr) = WootingAnalogSDKNET.WootingAnalogSDK.ReadFullBuffer(20);
			if (readErr == WootingAnalogSDKNET.WootingAnalogResult.Ok) {
				if (keys.Count == 0) {
					lastCtrlState = state;
                } else {
					foreach (var analog in keys)
					{
						Key key = new Key((HidKey)analog.Item1);
						float value = analog.Item2;
						PrepareAxis(ref pitch, GameSettings.PITCH_DOWN, GameSettings.PITCH_UP, key, value);
						PrepareAxis(ref roll, GameSettings.ROLL_LEFT, GameSettings.ROLL_RIGHT, key, value);
						PrepareAxis(ref yaw, GameSettings.YAW_LEFT, GameSettings.YAW_RIGHT, key, value);
						PrepareAxis(ref steer, GameSettings.WHEEL_STEER_LEFT, GameSettings.WHEEL_STEER_RIGHT, key, value);
						PrepareAxis(ref transZ, GameSettings.TRANSLATE_BACK, GameSettings.TRANSLATE_FWD, key, value);
						PrepareAxis(ref transY, GameSettings.TRANSLATE_DOWN, GameSettings.PITCH_UP, key, value);
						PrepareAxis(ref transX, GameSettings.TRANSLATE_LEFT, GameSettings.TRANSLATE_RIGHT, key, value);
						PrepareAxis(ref throttle, GameSettings.THROTTLE_DOWN, GameSettings.THROTTLE_UP, key, value);
						PrepareAxis(ref drive, GameSettings.WHEEL_THROTTLE_DOWN, GameSettings.WHEEL_THROTTLE_UP, key, value);
					}
				}
			} else {
				if (!error) {
					error = true;
					Log("ControlUpdate", $"Wooting Error code ({readErr})");
				}
			}

			ApplyAxis(ref state.pitch, pitch, "Pitch");
			ApplyAxis(ref state.roll, roll, "Roll");
			ApplyAxis(ref state.yaw, yaw, "Yaw");
			ApplyAxis(ref state.wheelSteer, steer, "Steer");
			ApplyAxis(ref state.X, transX, "TransX");
			ApplyAxis(ref state.Y, transY, "TransY");
			ApplyAxis(ref state.Z, transZ, "TransZ");
			ApplyThrottleAxis(ref state.mainThrottle, throttle, "Throttle");
			ApplyThrottleAxis(ref state.wheelThrottle, drive, "Drive");

			lastCtrlState = state;
		}

		private bool PrepareAxis(ref float axis, KeyBinding negative, KeyBinding positive, Key key, float value)
		{
			if (key == negative.primary) {
				axis -= value;
			} else if (key == positive.primary) {
				axis += value;
			} else {
				return false;
			}
			return true;
		}

		private void ApplyAxis(ref float output, float value, String text)
		{
			if (value != 0)
			{
				output = value;
				Log("ControlUpdate", $"{text} {value})");
			}
		}

		private void ApplyThrottleAxis(ref float output, float value, String text)
		{
			if (value != 0)
			{
				output = Clamp(output + value/32.0f, -1.0f, 1.0f);
				Log("ControlUpdate", $"{text} {value})");
			}
		}
	}
}
