using UnityEngine;
using System.Collections;

public class NextButton : MonoBehaviour {
	public SolarSystem solarSystem;

	void OnMouseDown(){
		solarSystem.NextSystem();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Space)) {
			solarSystem.NextSystem();
		}
	}
}
