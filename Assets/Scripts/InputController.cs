using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : MonoBehaviour {

	public enum TriggerType{
		Press, Hold, Multi	
	};
	public delegate void KeyFunction ();

    #region Variables 
	[SerializeField] private bool active;
	private Dictionary<TriggerType, HashSet<KeySet.KeyName>> triggeredKeys;
	private Dictionary<KeySet.KeyName, TriggerType> key2type;
	private Dictionary<KeySet.KeyName, KeyFunction> triggerFunction;
    #endregion

	#region Mutators
	public void setKeyEvent(KeySet.KeyName name, TriggerType type, KeyFunction function) {
		// create set if none
		if (!triggeredKeys.ContainsKey(type) || triggeredKeys[type] == null) {
			triggeredKeys[type] = new HashSet<KeySet.KeyName> ();
		}
		// remove old trigger key
		if (key2type.ContainsKey(name) && key2type [name] != type) {
			triggeredKeys [key2type [name]].Remove (name);
		}
		// create new link
		triggeredKeys[type].Add(name);
		key2type [name] = type;
		triggerFunction [name] = function;
	}
	#endregion

    #region Behaviours
	public virtual void Awake() {
		triggeredKeys = new Dictionary<TriggerType, HashSet<KeySet.KeyName>> ();
		key2type = new Dictionary<KeySet.KeyName, TriggerType> ();
		triggerFunction = new Dictionary<KeySet.KeyName, KeyFunction> ();

		// set different key links depends on different controller, e.g. in play or in menu.
		initKeys();
    }

	public virtual void Update() {
		// for singleton of different controllers
		if (!active) {
			return;
		}
	
		foreach(TriggerType type in triggeredKeys.Keys) {
			if (type == TriggerType.Press) {
				foreach(KeySet.KeyName keyName in triggeredKeys[type]) {
					if (Input.GetKeyDown(KeySet.KeyMap[keyName])) {
						triggerFunction [keyName] ();
					}
				}
			}
			else if (type == TriggerType.Hold) {
				foreach(KeySet.KeyName keyName in triggeredKeys[type]) {
					if (Input.GetKey(KeySet.KeyMap[keyName])) {
						triggerFunction [keyName] ();
					}
				}
			} 
			else if (type == TriggerType.Multi) {
				foreach(KeySet.KeyName keyName in triggeredKeys[type]) {
					// handle every keys indivisually
					handleMultiKey (keyName);
				} 
			}
			else {
				Debug.LogError("Unknown Trigger Type!");
			}
		}
    }
    #endregion

	#region Functions
	protected abstract void initKeys();
	protected virtual void handleMultiKey(KeySet.KeyName keyName) {
	}
	private IEnumerator _activate() {
		yield return new WaitForEndOfFrame ();
		active = true;
	}
	public virtual void activate() {
		StartCoroutine (_activate());
	}
	public void deactivate() {
		active = false;
	}
	#endregion
}
