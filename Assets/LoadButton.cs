using UnityEngine;
using System.Collections;

public class LoadButton : MonoBehaviour {

	void OnMouseDown() {
		Application.LoadLevel("SaveList");
	}
}
