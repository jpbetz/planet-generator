using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.IO;

public class ApplicationState : MonoBehaviour {

	// TODO:  the way this singleton is configured is horrid,  need to improve
	public static ApplicationState singleton;
	
	public ApplicationData data;

	public ApplicationState() {
		data = new ApplicationData();
	}

	// Set this game object up before all the other's start
	void Awake () {

		// iOS does not support JIT based serializer, so force use of reflection based serializer
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");

		if(singleton == null) {
			DontDestroyOnLoad(gameObject);
			singleton = this;
		}
		else if (singleton != this) {
			Destroy (gameObject);
		}
	}
	
	void OnEnable() {
		Load();
	}

	// save at every major lifecycle end event
	void OnDisable() {
		Save();
	}

	void OnApplicationQuit() {
		Save();
	}

	void OnApplicationPause() {
		Save();
	}

	string GetAppStateFilePath() {
		return Application.persistentDataPath + "/planetApplicationState.dat";
	}

	public void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream fs = null;
		try {
			fs = File.Open (GetAppStateFilePath(), FileMode.OpenOrCreate);
			bf.Serialize(fs, data);
		} finally {
			if(fs != null) {
				fs.Close();
			}
		}
	}

	public void Load() {
		BinaryFormatter bf = new BinaryFormatter();
		if(File.Exists(GetAppStateFilePath())) {
			FileStream fs = null;
			try {
				fs = File.Open (GetAppStateFilePath(), FileMode.Open);
				data = (ApplicationData)bf.Deserialize(fs);
			} catch (IOException e) {
				Debug.LogWarning("Unable to read load file: " + e.Message);
			} finally {
				if(fs != null) {
					fs.Close();
				}
			}
		}
	}
}



[Serializable]
public class ApplicationData {
	
	public PlanetSeed activePlanet;
	
	public List<PlanetSeed> savedPlanets;

	public ApplicationData() {
		savedPlanets = new List<PlanetSeed>();
	}

}
