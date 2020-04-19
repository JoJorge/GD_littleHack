using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySet {

	public enum KeyName {
		Up, Down, Left, Right
	}; 

	private static Dictionary<KeyName, KeyCode> _KeyMap;
	public static Dictionary<KeyName, KeyCode> KeyMap {
		get {
			if (_KeyMap == null) {
				_KeyMap = new Dictionary<KeyName, KeyCode> ();
				initKeyMapByDefault ();
			}
			return _KeyMap;
		}
	}

	private static void initKeyMapByDefault() {
		KeyMap.Add (KeyName.Up, KeyCode.UpArrow);
		KeyMap.Add (KeyName.Down, KeyCode.DownArrow);
		KeyMap.Add (KeyName.Left, KeyCode.LeftArrow);
		KeyMap.Add (KeyName.Right, KeyCode.RightArrow);
	}
	public static void setKey(KeyName name, KeyCode nowKeyCode) {
		KeyMap [name] = nowKeyCode;
	}
}
