using UnityEngine;
using System.Collections;

public class HeadsUpDisplay : MonoBehaviour {

	public GUIText planetName;

	public void Draw(PlanetParameters planetParams) {
		planetName.text = planetParams.name;
	}
}
