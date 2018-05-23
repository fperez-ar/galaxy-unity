using System;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryManager : MonoBehaviour {
	
	private IToggleable[] inventories;

	void Awake() {
		inventories = GetComponentsInChildren <IToggleable> () ;
		for (int i = 0; i < inventories.Length; i++) {
			inventories[i].BeforeShow += onBeforeShow;
		} 

	}

	void onBeforeShow() {
		for (int i = 0; i < inventories.Length; i++) {
			if (inventories [i].shown) {
				inventories [i].toggle ();
			}
		}
	}

}
