using UnityEngine;
using System.Collections;

public class NextButton : MonoBehaviour {

	public SolarSystem solarSystem;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void OnMouseDown(){
		solarSystem.NextSystem();
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.Space)) {
			solarSystem.NextSystem();
		}
	}

}
