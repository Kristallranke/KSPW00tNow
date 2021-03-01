using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KSPW00tNow
{
	enum SasControlMode { Override, Add, Scale };

	enum ResponseMode { Linear, Gamma, GammaLinear };

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
		public ControlTypes lockMask;
		public ResponseMode responseMode;

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
			responseMode = ResponseMode.GammaLinear;
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
					PrepareAxis(ref kerbalState.forward, GameSettings.EVA_back, GameSettings.EVA_forward, key, value, ControlTypes.EVA_INPUT);
					PrepareAxis(ref kerbalState.sideway, GameSettings.EVA_left, GameSettings.EVA_right, key, value, ControlTypes.EVA_INPUT);
					PrepareAxis(ref kerbalState.yaw, GameSettings.EVA_yaw_left, GameSettings.EVA_yaw_right, key, value, ControlTypes.YAW);

					PrepareAxis(ref kerbalState.packZ, GameSettings.EVA_Pack_back, GameSettings.EVA_Pack_forward, key, value, ControlTypes.RCS);
					PrepareAxis(ref kerbalState.packY, GameSettings.EVA_Pack_down, GameSettings.EVA_Pack_up, key, value, ControlTypes.RCS);
					PrepareAxis(ref kerbalState.packX, GameSettings.EVA_Pack_left, GameSettings.EVA_Pack_right, key, value, ControlTypes.RCS);

					value = ApplyResponseCurve(value);
					PrepareAxis(ref flightState.pitch, GameSettings.PITCH_DOWN, GameSettings.PITCH_UP, key, value, ControlTypes.PITCH);
					PrepareAxis(ref flightState.roll, GameSettings.ROLL_LEFT, GameSettings.ROLL_RIGHT, key, value, ControlTypes.ROLL);
					PrepareAxis(ref flightState.yaw, GameSettings.YAW_LEFT, GameSettings.YAW_RIGHT, key, value, ControlTypes.YAW);
					PrepareAxis(ref flightState.wheelSteer, GameSettings.WHEEL_STEER_LEFT, GameSettings.WHEEL_STEER_RIGHT, key, value, ControlTypes.WHEEL_STEER);
					PrepareAxis(ref flightState.Z, GameSettings.TRANSLATE_BACK, GameSettings.TRANSLATE_FWD, key, value, ControlTypes.RCS);
					PrepareAxis(ref flightState.Y, GameSettings.TRANSLATE_DOWN, GameSettings.TRANSLATE_UP, key, value, ControlTypes.RCS);
					PrepareAxis(ref flightState.X, GameSettings.TRANSLATE_LEFT, GameSettings.TRANSLATE_RIGHT, key, value, ControlTypes.RCS);
					PrepareAxis(ref flightState.mainThrottle, GameSettings.THROTTLE_DOWN, GameSettings.THROTTLE_UP, key, value, ControlTypes.THROTTLE);
					PrepareAxis(ref flightState.wheelThrottle, GameSettings.WHEEL_THROTTLE_DOWN, GameSettings.WHEEL_THROTTLE_UP, key, value, ControlTypes.WHEEL_THROTTLE);
				}
			} else {
				if (!error) {
					error = true;
					LogManager.Error("GlobalControlState", $"Wooting Error code ({readErr})");
				}
			}
		}

		private float ApplyResponseCurve(float value)
		{
            if (FlightInputHandler.fetch.precisionMode) {
				if (responseMode == ResponseMode.Linear) {
					return value * 0.5f;
				} else if (responseMode == ResponseMode.Gamma) {
					return (float)Math.Pow(value, 3.0f);
				} else if(responseMode == ResponseMode.GammaLinear) {
					return (float)Math.Pow(value, 2.0f) * 0.5f;
				}
			}
			return value;
		}

		private void PrepareAxis(ref float axis, KeyBinding negative, KeyBinding positive, Key key, float value, ControlTypes lockFlag)
		{
			if (((ulong)lockMask & (ulong)lockFlag) != 0) {
				return;
			} else if (key == negative.primary) {
				axis -= value;
			} else if (key == positive.primary) {
				axis += value;
			}
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
