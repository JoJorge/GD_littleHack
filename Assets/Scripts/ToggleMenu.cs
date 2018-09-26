using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour {

	[SerializeField] private GameObject menu;

	public void Start() {
		menu.SetActive (false);
	}

	public void toggleMenu() {
		menu.SetActive(!menu.activeSelf);
	}
}
