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

		void Log(String name, String message = "")
		{
			UnityEngine.Debug.Log($"[KSPW00tNow] {name}: {message}");
		}

		static bool wootInit = false;

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
			//			WootingAnalogSDKNET.WootingAnalogSDK.DeviceEvent += DeviceEventHandler;
		}

		IEnumerator Start()
		{
			Log("Start");
			yield return new UnityEngine.WaitForSeconds(2.5f);
		}
		void OnDestroy()
		{
			FlightInputHandler.OnRawAxisInput -= ControlUpdate;
			//			WootingAnalogSDKNET.WootingAnalogSDK.DeviceEvent -= DeviceEventHandler;
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
//			FlightInputHandler.OnRawAxisInput -= ControlUpdate;
//			WootingAnalogSDKNET.WootingAnalogSDK.DeviceEvent -= DeviceEventHandler;
//			WootingAnalogSDKNET.WootingAnalogSDK.UnInitialise();
			Log("OnDisable");
		}

		float Clamp(float value)
		{
			return Math.Min(1.0f, Math.Max(value, -1.0f));
		}

		void ControlUpdate(FlightCtrlState state)
		{
			float pitch = 0.0f;
			float roll = 0.0f;
			float yaw = 0.0f;
			float throttle = 0.0f;

			float transX = 0.0f;
			float transY = 0.0f;
			float transZ = 0.0f;

			var (keys, readErr) = WootingAnalogSDKNET.WootingAnalogSDK.ReadFullBuffer(20);
			if (readErr == WootingAnalogSDKNET.WootingAnalogResult.Ok) {
				if (keys.Count == 0) {
					lastCtrlState = state;
                } else {
					foreach (var analog in keys)
					{
						HidKey key = (HidKey)analog.Item1;
						if (key == HidKey.W) {
							pitch -= analog.Item2;
						} else if (key == HidKey.S) {
							pitch += analog.Item2;
						} else if (key == HidKey.Q) {
							roll -= analog.Item2;
						} else if (key == HidKey.E) {
							roll += analog.Item2;
						} else if (key == HidKey.A) {
							yaw -= analog.Item2;
						} else if (key == HidKey.D) {
							yaw += analog.Item2;
						} else if (key == HidKey.N) {
							transZ -= analog.Item2;
						} else if (key == HidKey.H) {
							transZ += analog.Item2;
						} else if (key == HidKey.I) {
							transY -= analog.Item2;
						} else if (key == HidKey.K) {
							transY += analog.Item2;
						} else if (key == HidKey.J) {
							transX -= analog.Item2;
						} else if (key == HidKey.L) {
							transX += analog.Item2;
						} else if (key == HidKey.RCtrl) {
							throttle -= analog.Item2;
						} else if(key == HidKey.RShift) {
							throttle += analog.Item2;
						}
						Log("ControlUpdate", $"({analog.Item1},{analog.Item2})");
					}
				}
			} else {
				if (!error) {
					error = true;
					Log("ControlUpdate", $"Wooting Error code ({readErr})");
				}
			}
			if (pitch != 0) {
				state.pitch = pitch;
				Log("ControlUpdate", $"Pitch {pitch})");
			}
			if (roll != 0) {
				state.roll = roll;
				Log("ControlUpdate", $"Pitch {roll})");
			}
			if (yaw != 0) {
				state.yaw = yaw;
				state.wheelSteer = yaw;
				Log("ControlUpdate", $"Pitch {yaw})");
			}
			if (transX != 0) {
				state.X = transX;
				Log("ControlUpdate", $"Pitch {transX})");
			}
			if (transY != 0) {
				state.Y = transY;
				Log("ControlUpdate", $"Pitch {transY})");
			}
			if (transZ != 0) {
				state.Z = transZ;
				Log("ControlUpdate", $"Pitch {transZ})");
			}
			if (throttle != 0) {
				state.mainThrottle = Clamp(lastCtrlState.mainThrottle + throttle/32.0f);
				state.wheelThrottle = Clamp(lastCtrlState.wheelThrottle + throttle/32.0f);
				Log("ControlUpdate", $"Pitch {throttle})");
			}
			lastCtrlState = state;
		}

		void DeviceEventHandler(WootingAnalogSDKNET.DeviceEventType eventType, WootingAnalogSDKNET.DeviceInfo deviceInfo)
		{
			if (eventType == WootingAnalogSDKNET.DeviceEventType.Connected) {
			} else {
			}
		}
	}
}
