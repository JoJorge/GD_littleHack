using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputGameController : InputController {

	#region Singleton
	static private InputGameController instance;
	static public InputGameController Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType (typeof(InputGameController)) as InputGameController;
				if (instance == null) {
					GameObject controller = new GameObject (typeof(InputGameController).ToString());
					instance = controller.AddComponent <InputGameController> ();
				}
			}
			return instance;
		}
	}
	#endregion

    #region Variables
	protected bool controllable = true;
    protected Player player;
    #endregion

    #region Behaviours
	public override void Awake () {
		// for Singleton
		if (Instance != this) {
			Destroy (this);
		}

		// set player and manager
		player = Player.Instance;

		base.Awake ();
	}
		
	public override void Update () {
		base.Update ();
	}
    #endregion

    #region Mutators
	public void setControllable (bool newControllable) {
		controllable = newControllable;
		if (!controllable) {
			stop ();
		}
	}
    public void setInput(bool canInput) {
        setControllable (canInput);
    }
    #endregion

    #region Functions
	protected override void initKeys () {
		setKeyEvent(KeySet.KeyName.Up, TriggerType.Press, new KeyFunction(up));
		setKeyEvent (KeySet.KeyName.Left, TriggerType.Hold, new KeyFunction (left));
		setKeyEvent (KeySet.KeyName.Right, TriggerType.Hold, new KeyFunction (right));
	}
    #endregion

	#region KeyFunctions
	protected void up () {
		if (player.isFreezed () || !controllable) {
			return;
		}

		// jump
		player.StartCoroutine(player.jump());
	}

	protected void left () {
		move (-1);
	}

	protected void right () {
		move (1);
	}
		
	protected void move(int direction) {                // -1: left, 1: right
		if (player.isFreezed () || !controllable) {
			return;
		}
		player.move(direction);
	}

	protected void stop() {
		player.stop ();
	}
		
	#endregion
}
