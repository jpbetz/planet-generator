using UnityEngine;
using System.Collections;
using System;

public enum SaveButtonState {
	Add, Remove, SaveListFull
}

public class SaveButton : MonoBehaviour {

	public static int maxSaves = 13;
	public SolarSystem solarSystem;
	
	public SaveButtonState state = SaveButtonState.Add;

	void Start() {
		if(ApplicationState.singleton.data.savedPlanets.Contains(solarSystem.planetParams.planetSeed)) {
			state = SaveButtonState.Remove;
		} else if (ApplicationState.singleton.data.savedPlanets.Count >= maxSaves) {
			state = SaveButtonState.SaveListFull;
		}
		gameObject.guiText.text = TextForState(state);
	}

	String TextForState(SaveButtonState state) {
		switch(state) {
		case SaveButtonState.Add: return "Save"; 
		case SaveButtonState.Remove: return "Unsave";
		case SaveButtonState.SaveListFull: return "List Full";
		default:
			throw new ArgumentException("unrecognized enum value: " + state);
		}
	}

	void OnMouseDown() {
		ApplicationState appState = ApplicationState.singleton;
		ApplicationData data = appState.data;

		switch(state) {
		case SaveButtonState.Remove:
			data.savedPlanets.Remove(solarSystem.planetParams.planetSeed);
			appState.Save();
			state = SaveButtonState.Add;
			break;
		case SaveButtonState.Add:
			data.savedPlanets.Add(solarSystem.planetParams.planetSeed);
			appState.Save();
			state = SaveButtonState.Remove;
			break;
		case SaveButtonState.SaveListFull:
			break;
		default:
			throw new ArgumentException("unrecognized enum value: " + state);
		}
		gameObject.guiText.text = TextForState(state);
	}
}
