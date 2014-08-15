using UnityEngine;
using System.Collections;

public class SolarSystem : MonoBehaviour {

	public Planet planet;
	public SolarLight solarLight;
	public HeadsUpDisplay hud;

	public PlanetParameters planetParams;
	
	void Start() {
		if(ApplicationState.singleton.data.activePlanet != null) {
			Display(ApplicationState.singleton.data.activePlanet);
		} else {
			DisplayRandom();
		}
	}

	public void NextSystem() {
		ApplicationState.singleton.data.activePlanet = null;
		Application.LoadLevel("Planet");
	}
	
	void DisplayRandom() {
		Display(new PlanetParameters());
	}

	void Display(PlanetSeed planetSeed) {
		Display(new PlanetParameters(planetSeed));
	}

	public void Display(PlanetParameters planetParams) {
		this.planetParams = planetParams;
		planet.GeneratePlanet(planetParams);
		solarLight.GenerateStar(planetParams);
		ApplicationState.singleton.data.activePlanet = planetParams.planetSeed;
		hud.Draw(planetParams);
	}
}
