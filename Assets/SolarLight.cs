using UnityEngine;
using System.Collections;

public class SolarLight : MonoBehaviour {
	public void GenerateStar(PlanetParameters planetParams) {
		light.color = planetParams.starLight;
		light.intensity = planetParams.starIntensity;
	}
}
