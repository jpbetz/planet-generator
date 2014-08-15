using UnityEngine;
using System.Collections;

public class SavedPlanetButton : MonoBehaviour {

	public PlanetSeed planetSeed;

	void OnMouseDown(){
		ApplicationState.singleton.data.activePlanet = planetSeed;
		Application.LoadLevel("Planet");
	}
}
