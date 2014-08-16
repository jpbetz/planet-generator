using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlanetSeed {
	public int seed;

	public PlanetSeed(int seed) {
		this.seed = seed;
	}
}

public class PlanetParameters {

	public PlanetSeed planetSeed;

	public string name;
	
	// changing this will change the dimensions sphere or ovoid that is sampled from perlin space
	// not sure why,  but this perlin noise seams to be less dense on the z axis, so adding a multiplier to even it out
	public Vector3 perlinScaler = new Vector3(1, 1, 1); 
	
	public int PERLIN_OCTAVES = 8; // increase octaves for more detail

	// water
	public float seaLevel;
	public Color32 waterColor;
	public Color WATER_NORMAL = new Color(0.5f, 0.5f, 1f);
	
	// land
	public float terrainHeight;
	public float gradientMultiplier;
	public Color LAND_NORMAL = new Color(0.5f, 0.5f, 1f);

	public Vector3 perlinSpaceSeed;
	public Gradient gradient;

	public Color landIceColor;
	public Color waterIceColor;
	public float icyness;

	public Color starLight;
	public float starIntensity;

	public float planetSize;

	public PlanetParameters() : this(new PlanetSeed((int)System.DateTime.Now.Ticks)) {
	}

	public PlanetParameters(PlanetSeed planetSeed) {
		this.planetSeed = planetSeed;
		Random.seed = planetSeed.seed;
		gradientMultiplier = Random.Range (1.3f, 2.3f);
		
		seaLevel = Random.Range (0.25f, 0.8f);
		terrainHeight = Random.Range (0.04f, 0.08f);

		perlinSpaceSeed = new Vector3(Random.Range(1f, 10f), Random.Range(1f, 10f), Random.Range(1f, 10f));

		icyness = Mathf.Sqrt(Random.Range (0.01f, 15.0f))-2f;

		Color32 shore =     color(Random.Range(60, 120),  Random.Range(120, 180), Random.Range(70, 110));
		Color32 hills =     color(Random.Range(80, 140),  Random.Range(100, 160), Random.Range(60, 100));
		Color32 highHills = color(Random.Range(80, 140),  Random.Range(100, 160), Random.Range(60, 100));
		Color32 mountains = color(Random.Range(210, 250), Random.Range(210, 250), Random.Range(230, 250));

		gradient = new Gradient();
		Gradient.GradientPoint p1 = new Gradient.GradientPoint(shore,     0.0f);
		Gradient.GradientPoint p2 = new Gradient.GradientPoint(hills,     0.5f);
		Gradient.GradientPoint p3 = new Gradient.GradientPoint(highHills, 0.8f);
		Gradient.GradientPoint p4 = new Gradient.GradientPoint(mountains, 1.0f);
		
		gradient.gradientPoints = new Gradient.GradientPoint[] { p1, p2, p3, p4 };

		waterColor    = new Color32((byte)Random.Range(40, 50),    (byte)Random.Range(30, 90),   (byte)Random.Range(90, 150), (byte)80);
		landIceColor  = new Color32((byte)Random.Range(230, 240),  (byte)Random.Range(230, 240), (byte)Random.Range(240, 255), (byte)30);
		waterIceColor = new Color32((byte)Random.Range(230, 245),  (byte)Random.Range(240, 255), (byte)Random.Range(230, 245), (byte)240);

		starLight = new Color32(
			        (byte)(Random.Range(135, 195) - 50*icyness),
		            (byte)(Random.Range(164, 124) - 40*icyness),
		            (byte)(Random.Range(200, 255) - 2*icyness),
		            (byte)255);

		starIntensity = Random.Range (1.5f, 2.0f);

		planetSize = Random.Range (0.75f, 1.25f);

		name = PlanetNameGenerator.GenerateName();
	}

	public Color32 color(int r, int g, int b) {
		return new Color32((byte)r, (byte)g, (byte)b, (byte)0);
	}
}
