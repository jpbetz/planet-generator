using UnityEngine;
using System.Collections;

public class SolarLight : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	}

	public void GenerateStar(PlanetParameters planetParams) {
		light.color = planetParams.starLight;
		light.intensity = planetParams.starIntensity;
	}
}
