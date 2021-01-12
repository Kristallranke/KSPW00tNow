using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSPW00tNow
{
	public enum HidKey
	{
		A = 4,
		B = 5,
		C = 6,
		D = 7,
		E = 8,
		F = 9,
		G = 10,
		H = 11,
		I = 12,
		J = 13,
		K = 14,
		L = 15,
		M = 16,
		N = 17,
		O = 18,
		P = 19,
		Q = 20,
		R = 21,
		S = 22,
		T = 23,
		U = 24,
		V = 25,
		W = 26,
		X = 27,
		Y = 28,
		Z = 29,
		Alpha1 = 30,
		Alpha2 = 31,
		Alpha3 = 32,
		Alpha4 = 33,
		Alpha5 = 34,
		Alpha6 = 35,
		Alpha7 = 36,
		Alpha8 = 37,
		Alpha9 = 38,
		Alpha0 = 39,
		Return = 40,
		Escape = 41,
		Backspace = 42,
		Tab = 43,
		Spacebar = 44,
		Minus = 45,
		Equals = 46,
		LBracket = 47,
		RBracket = 48,
		BackSlash = 49,
		Quote = 50,
		Colon = 51,
		Tilde = 53,
		Comma = 54,
		Period = 55,
		Slash = 56,
		CapsLock = 57,
		F1 = 58,
		F2 = 59,
		F3 = 60,
		F4 = 61,
		F5 = 62,
		F6 = 63,
		F7 = 64,
		F8 = 65,
		F9 = 66,
		F10 = 67,
		F11 = 68,
		F12 = 69,
		Print = 70,
		ScrollLock = 71,
		Pause = 72,
		Insert = 73,
		Home = 74,
		PageUp = 75,
		Delete = 76,
		End = 77,
		PageDown = 78,
		RightArrow = 79,
		LeftArrow = 80,
		DownArrow = 81,
		UpArrow = 82,
		NumLock = 83,
		KeypadDivide = 84,
		KeypadMultiply = 85,
		KeypadMinus = 86,
		KeypadPlus = 87,
		KeypadEnter = 88,
		Keypad1 = 89,
		Keypad2 = 90,
		Keypad3 = 91,
		Keypad4 = 92,
		Keypad5 = 93,
		Keypad6 = 94,
		Keypad7 = 95,
		Keypad8 = 96,
		Keypad9 = 97,
		Keypad0 = 98,
		KeypadPeriod = 99,
		F13 = 104,
		F14 = 105,
		F15 = 106,
		Help = 117,
		Menu = 118,
		SysReq = 154,
		Cancel = 155,
		Clear = 156,
		LCtrl = 224,
		LShift = 225,
		LAlt = 226,
		LGui = 227,
		RCtrl = 228,
		RShift = 229,
		RAlt = 230,
		RGui = 231,
		Unused = -1
	}

	public class Key
	{
		public HidKey keyCode { get; }

		static Dictionary<UnityEngine.KeyCode, HidKey> keyMapFromKsp = new Dictionary<UnityEngine.KeyCode, HidKey>()
		{
			{ UnityEngine.KeyCode.Backspace, HidKey.Backspace },
			{ UnityEngine.KeyCode.Tab, HidKey.Tab },
			{ UnityEngine.KeyCode.Clear, HidKey.Clear },
			{ UnityEngine.KeyCode.Return, HidKey.Return },
			{ UnityEngine.KeyCode.Pause, HidKey.Pause },
			{ UnityEngine.KeyCode.Escape, HidKey.Escape },
			{ UnityEngine.KeyCode.Space, HidKey.Spacebar },
			{ UnityEngine.KeyCode.Exclaim, HidKey.Alpha1 },
			{ UnityEngine.KeyCode.DoubleQuote, HidKey.Quote },
			{ UnityEngine.KeyCode.Hash, HidKey.Alpha3 },
			{ UnityEngine.KeyCode.Dollar, HidKey.Alpha4 },
			{ UnityEngine.KeyCode.Percent, HidKey.Alpha5 },
			{ UnityEngine.KeyCode.Ampersand, HidKey.Alpha7 },
			{ UnityEngine.KeyCode.Quote, HidKey.Quote },
			{ UnityEngine.KeyCode.LeftParen, HidKey.Alpha9 },
			{ UnityEngine.KeyCode.RightParen, HidKey.Alpha0 },
			{ UnityEngine.KeyCode.Asterisk, HidKey.Alpha8 },
			{ UnityEngine.KeyCode.Plus, HidKey.Equals },
			{ UnityEngine.KeyCode.Comma, HidKey.Comma },
			{ UnityEngine.KeyCode.Minus, HidKey.Minus },
			{ UnityEngine.KeyCode.Period, HidKey.Period },
			{ UnityEngine.KeyCode.Slash, HidKey.Slash },
			{ UnityEngine.KeyCode.Alpha0, HidKey.Alpha0 },
			{ UnityEngine.KeyCode.Alpha1, HidKey.Alpha1 },
			{ UnityEngine.KeyCode.Alpha2, HidKey.Alpha2 },
			{ UnityEngine.KeyCode.Alpha3, HidKey.Alpha3 },
			{ UnityEngine.KeyCode.Alpha4, HidKey.Alpha4 },
			{ UnityEngine.KeyCode.Alpha5, HidKey.Alpha5 },
			{ UnityEngine.KeyCode.Alpha6, HidKey.Alpha6 },
			{ UnityEngine.KeyCode.Alpha7, HidKey.Alpha7 },
			{ UnityEngine.KeyCode.Alpha8, HidKey.Alpha8 },
			{ UnityEngine.KeyCode.Alpha9, HidKey.Alpha9 },
			{ UnityEngine.KeyCode.Colon, HidKey.Colon },
			{ UnityEngine.KeyCode.Semicolon, HidKey.Colon },
			{ UnityEngine.KeyCode.Less, HidKey.Comma },
			{ UnityEngine.KeyCode.Equals, HidKey.Equals },
			{ UnityEngine.KeyCode.Greater, HidKey.Period },
			{ UnityEngine.KeyCode.Question, HidKey.Slash },
			{ UnityEngine.KeyCode.At, HidKey.Alpha2 },
			{ UnityEngine.KeyCode.LeftBracket, HidKey.LBracket },
			{ UnityEngine.KeyCode.Backslash, HidKey.BackSlash },
			{ UnityEngine.KeyCode.RightBracket, HidKey.RBracket },
			{ UnityEngine.KeyCode.Caret, HidKey.Alpha6 },
			{ UnityEngine.KeyCode.Underscore, HidKey.Minus },
			{ UnityEngine.KeyCode.BackQuote, HidKey.Tilde },
			{ UnityEngine.KeyCode.A, HidKey.A },
			{ UnityEngine.KeyCode.B, HidKey.B },
			{ UnityEngine.KeyCode.C, HidKey.C },
			{ UnityEngine.KeyCode.D, HidKey.D },
			{ UnityEngine.KeyCode.E, HidKey.E },
			{ UnityEngine.KeyCode.F, HidKey.F },
			{ UnityEngine.KeyCode.G, HidKey.G },
			{ UnityEngine.KeyCode.H, HidKey.H },
			{ UnityEngine.KeyCode.I, HidKey.I },
			{ UnityEngine.KeyCode.J, HidKey.J },
			{ UnityEngine.KeyCode.K, HidKey.K },
			{ UnityEngine.KeyCode.L, HidKey.L },
			{ UnityEngine.KeyCode.M, HidKey.M },
			{ UnityEngine.KeyCode.N, HidKey.N },
			{ UnityEngine.KeyCode.O, HidKey.O },
			{ UnityEngine.KeyCode.P, HidKey.P },
			{ UnityEngine.KeyCode.Q, HidKey.Q },
			{ UnityEngine.KeyCode.R, HidKey.R },
			{ UnityEngine.KeyCode.S, HidKey.S },
			{ UnityEngine.KeyCode.T, HidKey.T },
			{ UnityEngine.KeyCode.U, HidKey.U },
			{ UnityEngine.KeyCode.V, HidKey.V },
			{ UnityEngine.KeyCode.W, HidKey.W },
			{ UnityEngine.KeyCode.X, HidKey.X },
			{ UnityEngine.KeyCode.Y, HidKey.Y },
			{ UnityEngine.KeyCode.Z, HidKey.Z },
			{ UnityEngine.KeyCode.LeftCurlyBracket, HidKey.LBracket },
			{ UnityEngine.KeyCode.Pipe, HidKey.BackSlash },
			{ UnityEngine.KeyCode.RightCurlyBracket, HidKey.RBracket },
			{ UnityEngine.KeyCode.Tilde , HidKey.Tilde },
			{ UnityEngine.KeyCode.Delete , HidKey.Delete },
			{ UnityEngine.KeyCode.Keypad0, HidKey.Keypad0 },
			{ UnityEngine.KeyCode.Keypad1, HidKey.Keypad1 },
			{ UnityEngine.KeyCode.Keypad2, HidKey.Keypad2 },
			{ UnityEngine.KeyCode.Keypad3, HidKey.Keypad3 },
			{ UnityEngine.KeyCode.Keypad4, HidKey.Keypad4 },
			{ UnityEngine.KeyCode.Keypad5, HidKey.Keypad5 },
			{ UnityEngine.KeyCode.Keypad6, HidKey.Keypad6 },
			{ UnityEngine.KeyCode.Keypad7, HidKey.Keypad7 },
			{ UnityEngine.KeyCode.Keypad8, HidKey.Keypad8 },
			{ UnityEngine.KeyCode.Keypad9, HidKey.Keypad9 },
			{ UnityEngine.KeyCode.KeypadPeriod, HidKey.KeypadPeriod },
			{ UnityEngine.KeyCode.KeypadDivide, HidKey.KeypadDivide },
			{ UnityEngine.KeyCode.KeypadMultiply, HidKey.KeypadMultiply },
			{ UnityEngine.KeyCode.KeypadMinus, HidKey.KeypadMinus },
			{ UnityEngine.KeyCode.KeypadPlus, HidKey.KeypadPlus },
			{ UnityEngine.KeyCode.KeypadEnter, HidKey.KeypadEnter },
			{ UnityEngine.KeyCode.KeypadEquals, HidKey.Z },
			{ UnityEngine.KeyCode.UpArrow, HidKey.UpArrow },
			{ UnityEngine.KeyCode.DownArrow, HidKey.DownArrow },
			{ UnityEngine.KeyCode.RightArrow, HidKey.RightArrow },
			{ UnityEngine.KeyCode.LeftArrow, HidKey.LeftArrow },
			{ UnityEngine.KeyCode.Insert, HidKey.Insert },
			{ UnityEngine.KeyCode.Home, HidKey.Home },
			{ UnityEngine.KeyCode.End, HidKey.End },
			{ UnityEngine.KeyCode.PageUp, HidKey.PageUp },
			{ UnityEngine.KeyCode.PageDown, HidKey.PageDown },
			{ UnityEngine.KeyCode.F1, HidKey.F1 },
			{ UnityEngine.KeyCode.F2, HidKey.F2 },
			{ UnityEngine.KeyCode.F3, HidKey.F3 },
			{ UnityEngine.KeyCode.F4, HidKey.F4 },
			{ UnityEngine.KeyCode.F5, HidKey.F5 },
			{ UnityEngine.KeyCode.F6, HidKey.F6 },
			{ UnityEngine.KeyCode.F7, HidKey.F7 },
			{ UnityEngine.KeyCode.F8, HidKey.F8 },
			{ UnityEngine.KeyCode.F9, HidKey.F9 },
			{ UnityEngine.KeyCode.F10, HidKey.F10 },
			{ UnityEngine.KeyCode.F11, HidKey.F11 },
			{ UnityEngine.KeyCode.F12, HidKey.F12 },
			{ UnityEngine.KeyCode.F13, HidKey.F13 },
			{ UnityEngine.KeyCode.F14, HidKey.F14 },
			{ UnityEngine.KeyCode.F15, HidKey.F15 },
			{ UnityEngine.KeyCode.Numlock, HidKey.NumLock },
			{ UnityEngine.KeyCode.CapsLock, HidKey.CapsLock },
			{ UnityEngine.KeyCode.ScrollLock, HidKey.ScrollLock },
			{ UnityEngine.KeyCode.RightShift, HidKey.RShift },
			{ UnityEngine.KeyCode.LeftShift, HidKey.LShift },
			{ UnityEngine.KeyCode.RightControl, HidKey.RCtrl },
			{ UnityEngine.KeyCode.LeftControl, HidKey.LCtrl },
			{ UnityEngine.KeyCode.RightAlt, HidKey.RAlt },
			{ UnityEngine.KeyCode.LeftAlt, HidKey.LAlt },
			{ UnityEngine.KeyCode.RightCommand, HidKey.RGui },
			{ UnityEngine.KeyCode.LeftCommand, HidKey.LGui },
			{ UnityEngine.KeyCode.LeftWindows, HidKey.LGui },
			{ UnityEngine.KeyCode.RightWindows, HidKey.RGui },
			{ UnityEngine.KeyCode.AltGr, HidKey.RAlt },
			{ UnityEngine.KeyCode.Help, HidKey.Help },
			{ UnityEngine.KeyCode.Print, HidKey.Print },
			{ UnityEngine.KeyCode.SysReq, HidKey.SysReq },
			{ UnityEngine.KeyCode.Break, HidKey.Cancel },
			{ UnityEngine.KeyCode.Menu, HidKey.Menu }
		};

		public Key(HidKey keyCode)
		{
			this.keyCode = keyCode;
		}

		public Key(UnityEngine.KeyCode keyCode)
		{
			if (keyMapFromKsp.ContainsKey(keyCode)) {
				this.keyCode = keyMapFromKsp[keyCode];
			} else {
				this.keyCode = HidKey.Unused;
			}
		}

		public Key(KeyCodeExtended keyCode)
		{
			if (keyMapFromKsp.ContainsKey(keyCode.code)) {
				this.keyCode = keyMapFromKsp[keyCode.code];
			} else {
				this.keyCode = HidKey.Unused;
			}
		}

		public static bool operator ==(Key lhs, Key rhs)
		{
			return lhs.keyCode == rhs.keyCode;
		}

		public static bool operator !=(Key lhs, Key rhs)
		{
			return !(lhs == rhs);
		}

		public static bool operator ==(Key lhs, KeyCodeExtended rhs)
		{
			HidKey key;
			if (keyMapFromKsp.ContainsKey(rhs.code)) {
				key = keyMapFromKsp[rhs.code];
			} else {
				key = HidKey.Unused;
			}
			return lhs.keyCode == key;
		}

		public static bool operator !=(Key lhs, KeyCodeExtended rhs)
		{
			return !(lhs == rhs);
		}

		bool Valid()
		{
			return this.keyCode != HidKey.Unused;
		}
	}
}
