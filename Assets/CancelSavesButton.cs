using UnityEngine;
using System.Collections;

public class CancelSavesButton : MonoBehaviour {

	void OnMouseDown(){
		Application.LoadLevel("Planet");
	}
}
