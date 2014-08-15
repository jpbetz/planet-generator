using UnityEngine;
using System.Collections;

public class SaveList : MonoBehaviour {
	
	void Start () {
		ApplicationState state = ApplicationState.singleton;
		ApplicationData data = state.data;

		int i = 0;
		foreach(PlanetSeed planetSeed in data.savedPlanets) {
			GameObject gameObject = new GameObject("GUIText_" + planetSeed.seed.ToString());
			GUIText saveLabel = gameObject.AddComponent(typeof(GUIText)) as GUIText;
			saveLabel.transform.parent = transform;

			saveLabel.transform.localPosition = new Vector3(0f, -0.07f-0.05f*i, 0f);

			saveLabel.text = new PlanetParameters(planetSeed).name;
			saveLabel.fontSize = 40;
			saveLabel.anchor = TextAnchor.MiddleCenter;
			saveLabel.alignment = TextAlignment.Center;
			saveLabel.pixelOffset = new Vector2(0f, 0f);
			SavedPlanetButton savedPlanetButton = saveLabel.gameObject.AddComponent("SavedPlanetButton") as SavedPlanetButton;
			savedPlanetButton.planetSeed = planetSeed;
			i++;
		}
	}
}
