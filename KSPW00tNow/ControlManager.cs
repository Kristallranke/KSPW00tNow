using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KSPW00tNow
{
	enum SasControlMode { Override, Add, Scale };

	class KerbalCtrlState
	{
		public float forward;
		public float sideway;
		public float yaw;

		public float packX;
		public float packY;
		public float packZ;

		public void NeutralizeAll()
		{
			forward = 0.0f;
			sideway = 0.0f;
			yaw = 0.0f;

			packX = 0.0f;
			packY = 0.0f;
			packZ = 0.0f;
		}
	}

	class ControlManager
	{
		bool error;
		public FlightCtrlState flightState;
		public KerbalCtrlState kerbalState;
		public SasControlMode sasControlMode;

		private static ControlManager instance = null;

		public static ControlManager GetInstance()
		{
			if (instance == null) {
				instance = new ControlManager();
			}

			return instance;
		}

		ControlManager()
		{
			error = false;
			sasControlMode = SasControlMode.Override;
			flightState = new FlightCtrlState();
			kerbalState = new KerbalCtrlState();
			WootingAnalogSDKNET.WootingAnalogSDK.Initialise();
		}

		public void Update()
		{
			flightState.NeutralizeAll();
			kerbalState.NeutralizeAll();

			var (keys, readErr) = WootingAnalogSDKNET.WootingAnalogSDK.ReadFullBuffer(20);
			if (readErr == WootingAnalogSDKNET.WootingAnalogResult.Ok) {
				foreach (var analog in keys) {
					Key key = new Key((HidKey)analog.Item1);
					float value = analog.Item2;
					PrepareAxis(ref flightState.pitch, GameSettings.PITCH_DOWN, GameSettings.PITCH_UP, key, value);
					PrepareAxis(ref flightState.roll, GameSettings.ROLL_LEFT, GameSettings.ROLL_RIGHT, key, value);
					PrepareAxis(ref flightState.yaw, GameSettings.YAW_LEFT, GameSettings.YAW_RIGHT, key, value);
					PrepareAxis(ref flightState.wheelSteer, GameSettings.WHEEL_STEER_LEFT, GameSettings.WHEEL_STEER_RIGHT, key, value);
					PrepareAxis(ref flightState.Z, GameSettings.TRANSLATE_BACK, GameSettings.TRANSLATE_FWD, key, value);
					PrepareAxis(ref flightState.Y, GameSettings.TRANSLATE_DOWN, GameSettings.TRANSLATE_UP, key, value);
					PrepareAxis(ref flightState.X, GameSettings.TRANSLATE_LEFT, GameSettings.TRANSLATE_RIGHT, key, value);
					PrepareAxis(ref flightState.mainThrottle, GameSettings.THROTTLE_DOWN, GameSettings.THROTTLE_UP, key, value);
					PrepareAxis(ref flightState.wheelThrottle, GameSettings.WHEEL_THROTTLE_DOWN, GameSettings.WHEEL_THROTTLE_UP, key, value);

					PrepareAxis(ref kerbalState.forward, GameSettings.EVA_back, GameSettings.EVA_forward, key, value);
					PrepareAxis(ref kerbalState.sideway, GameSettings.EVA_left, GameSettings.EVA_right, key, value);
					PrepareAxis(ref kerbalState.yaw, GameSettings.EVA_yaw_left, GameSettings.EVA_yaw_right, key, value);

					PrepareAxis(ref kerbalState.packZ, GameSettings.EVA_Pack_back, GameSettings.EVA_Pack_forward, key, value);
					PrepareAxis(ref kerbalState.packY, GameSettings.EVA_Pack_down, GameSettings.EVA_Pack_up, key, value);
					PrepareAxis(ref kerbalState.packX, GameSettings.EVA_Pack_left, GameSettings.EVA_Pack_right, key, value);
				}
			} else {
				if (!error) {
					error = true;
					LogManager.Error("GlobalControlState", $"Wooting Error code ({readErr})");
				}
			}
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

		private void DumpTypeInfo(System.Type type)
		{
			using (System.IO.FileStream stream = new System.IO.FileStream($"C:\\Users\\Kristallranke\\Desktop\\Kerbal_{ type.Name }.log", System.IO.FileMode.Create)) {
				using (System.IO.StreamWriter writer = new System.IO.StreamWriter(stream)) {
					LogManager.Log(writer, "DumpTypeInfo", $"class {type.Name} " + "{");
					foreach (System.Reflection.MethodInfo m in type.GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)) {
						String parameters = "";
						foreach (var p in m.GetParameters()) {
							parameters += p.ParameterType.Name + " " + p.Name + ", ";
						}
						if (parameters.Length > 2) {
							parameters = parameters.Substring(0, parameters.Length - 2);
						}
						String str = "method " + m.ReturnType.Name + " " + m.Name + "(" + parameters + ");";
						LogManager.Log(writer, "DumpTypeInfo", str);
					}
					foreach (var m in type.GetEvents(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)) {
						String str = $"event {m.EventHandlerType.Name} {m.Name};";
						LogManager.Log(writer, "DumpTypeInfo", str);
					}
					foreach (var f in type.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)) {
						String str = $"{f.FieldType.Name} {f.Name};";
						LogManager.Log(writer, "DumpTypeInfo", str);
					}
					LogManager.Log(writer, "DumpTypeInfo", "}");
					writer.Flush();
					writer.Close();
				}
			}
		}
	}
}
