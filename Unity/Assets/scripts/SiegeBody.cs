﻿using UnityEngine;
using System.Collections;

public class SiegeBody : MonoBehaviour {
	public Siege driver;
	public GameObject orcList;
	public GameStateController gameMaster;
	bool advancedScene;

	public bool canAdvanceScene;
	bool done;

	void OnTriggerEnter(Collider collider) {
		if (!advancedScene) {
			if (canAdvanceScene) {
				gameMaster.nextScene();
			}
			advancedScene = true;
		}
		else if (!done) {
			done = true;
			driver.stop();
			transform.parent.animation.Play("dropDoor");
			if (orcList) {
				foreach (Component c in orcList.transform.GetComponentsInChildren(typeof(Orc))) {
					Orc orc = (Orc)c;
					orc.setAttackRun();
				}
			}
			if (canAdvanceScene) {
				foreach (Object n in GameObject.FindObjectsOfType(typeof(Siege))) {
					Component c = ((Siege)n).transform.GetComponentInChildren(typeof(SiegeBody));
					if (c && ((SiegeBody)((SiegeBody)c).gameObject.GetComponentsInChildren(typeof (SiegeBody))[0]).canAdvanceScene) {
					}
					else ((Siege)n).send();
				}
			}
		}
	}
}
