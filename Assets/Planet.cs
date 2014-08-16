using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Planet : MonoBehaviour {

	Mesh mesh;
	Dictionary<CubeSide, Material> materialsBySide;

	int textureWidth;
	int textureHeight;

	float rotationSpeed = 2.5f;

	public void GeneratePlanet (PlanetParameters planetParams) {
		InitMesh();
		GenerateTextures(planetParams);
		ApplyPlanetSizeAndElevationToMesh(planetParams);
	}

	void InitMesh () {
		mesh = GetComponent<MeshFilter>().mesh;
		materialsBySide = LookupMatrials();
		textureWidth = textureHeight = (int)Camera.main.pixelWidth/2; // aim for textures that can fill 1/2 of the screen width
	}
	
	Dictionary<CubeSide, Material> LookupMatrials() {
		Dictionary<CubeSide, Material> materials = new Dictionary<CubeSide, Material>();
		
		// TODO:  is there a better way to programatically access materials?  This is pretty lame.
		foreach(Material material in renderer.materials) {
			if(material.name.StartsWith("BottomMaterial__bottomImage")) {
				materials.Add (CubeSide.Bottom, material);
			} else if(material.name.StartsWith("TopMaterial__topImage")) {
				materials.Add (CubeSide.Top, material);
			} else if(material.name.StartsWith("LeftMaterial__leftImage")) {
				materials.Add (CubeSide.Left, material);
			} else if(material.name.StartsWith("RightMaterial__rightImage")) {
				materials.Add (CubeSide.Right, material);
			} else if(material.name.StartsWith("FrontMaterial__frontImage")) {
				materials.Add (CubeSide.Front, material);
			} else if(material.name.StartsWith("BackMaterial__backImage")) {
				materials.Add (CubeSide.Back, material);
			}
		}
		return materials;
	}

	void GenerateTextures(PlanetParameters planetParams) {
		foreach(KeyValuePair<CubeSide, Material> entry in materialsBySide) {
			GenerateTextures(entry.Key, entry.Value, planetParams);
		}
	}

	void GenerateTextures(CubeSide side, Material material, PlanetParameters planetParams) {
		Texture2D diffuseMap = new Texture2D(textureWidth, textureHeight, TextureFormat.ARGB32, false); // alpha is specular
		diffuseMap.wrapMode = TextureWrapMode.Clamp; // prevent renderer from wrapping textures,  which can cause hairline seams to appear at edges of textures
		Color[] pix = new Color[textureWidth * textureHeight];
		material.mainTexture = diffuseMap;
		
		for (int y = 0; y < diffuseMap.height; y++) {
			for (int x = 0; x < diffuseMap.width; x++) {
				pix[y*textureWidth + x] = GenerateTextureAtPoint(x, y, side, diffuseMap, planetParams);
			}
		}

		diffuseMap.SetPixels(pix);
		diffuseMap.Apply();
	}

	Color GenerateTextureAtPoint(int x, int y, CubeSide side, Texture2D diffuseMap, PlanetParameters planetParams) {
		Vector3 spherePoint = FindSpherePointForTexturePoint(side, (float)x / diffuseMap.width, (float)y / diffuseMap.height);
		spherePoint.Normalize(); 
		float noise = CalcNoiseAtSpherePoint(spherePoint, planetParams);

		float distFromEquator = Mathf.Pow(Mathf.Abs(spherePoint.y), 1.5f);
		
		if(noise <= planetParams.seaLevel) { // water
			return AdjustColorForLongatitude(planetParams.waterColor, planetParams.waterIceColor, distFromEquator, planetParams);
		} else { // land
			float elevation = noise-planetParams.seaLevel;
			float gradientx = elevation*planetParams.gradientMultiplier;
			Color32 gradientColor = planetParams.gradient.ColorAtX(Mathf.Clamp(gradientx, 0f, 1f), Mathf.Clamp (elevation, 0f, 1f));
			return AdjustColorForLongatitude(gradientColor, planetParams.landIceColor, distFromEquator, planetParams);
		}
	}
	
	Color32 AdjustColorForLongatitude(Color color, Color iceColor, float distFromEquator, PlanetParameters planetParams) {
		Color32 polarCapsColor = Color.Lerp (color, iceColor, Mathf.Clamp(planetParams.icyness, 0f, 1f));
		Color32 equatorColor = Color.Lerp (color, iceColor, Mathf.Clamp(planetParams.icyness-1f, 0f, 1f));
		return Color32.Lerp(equatorColor, polarCapsColor, Mathf.Clamp(distFromEquator, 0f, 1f));
	}

	
	void ApplyPlanetSizeAndElevationToMesh(PlanetParameters planetParams) {
		// apply planet size and elevation
		Vector3[] vertices = mesh.vertices;
		for (int i = 0; i < vertices.Length; i++) {
			Vector3 vertex = vertices[i];
			vertex.Normalize();
			float noiseAtVertex = CalcNoiseAtSpherePoint(vertex, planetParams);
			
			float scaleMultiplier = planetParams.planetSize;
			if(noiseAtVertex > planetParams.seaLevel) {
				scaleMultiplier += (noiseAtVertex-planetParams.seaLevel)*planetParams.terrainHeight;
			}
			vertex = Vector3.Scale(vertex, new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier));
			
			vertices[i] = vertex;
		}
		mesh.vertices = vertices;
		mesh.RecalculateBounds();
	}


	enum CubeSide {
		Top, Bottom, Left, Right, Front, Back
	}
	
	Vector3 FindSpherePointForTexturePoint(CubeSide side, float x, float y) { /** x and y should be in the 0..1 range */
		float x2;
		float y2;
		float z2;
		
		// TODO: I believe I could represent the below transforms using matrix coords and some matrix ops
		switch(side) {
		case CubeSide.Top:
			x2 = x;
			y2 = 1;
			z2 = y;
			break;
		case CubeSide.Bottom:
			x2 = x;
			y2 = 0;
			z2 = -(y-1);
			break;
		case CubeSide.Left:
			x2 = 1; 
			y2 = y;
			z2 = x;
			break;
		case CubeSide.Right:
			x2 = 0;
			y2 = y;
			z2 = -(x-1);
			break;
		case CubeSide.Front:
			x2 = -(x-1);
			y2 = y;
			z2 = 1;
			break;
		case CubeSide.Back:
			x2 = x;
			y2 = y;
			z2 = 0;
			break;
		default:
			throw new System.ArgumentException("unrecognized cube face name: " + this.name);
		}
		
		// By centering and normalizing the cube coords,
		// the cube coords are transformed to sphere coords, so that the values sampled from the perlin noise space are from the surface
		// of a sphere.  This results in distorted UV maps,  which is exactly what we want.   They are distorted so that when they are 
		// applied to the sphere they exactly match the values from the sphere surface in the perlin noise they are sampled from.
		
		return new Vector3(x2-0.5f, y2-0.5f, z2-0.5f);
	}
	
	float CalcNoiseAtSpherePoint(Vector3 point, PlanetParameters planetParams) {
		return Noise.Noise.GetOctaveNoise(planetParams.perlinSpaceSeed.x*planetParams.perlinScaler.x + point.x,
		                                  planetParams.perlinSpaceSeed.y*planetParams.perlinScaler.y + point.y,
		                                  planetParams.perlinSpaceSeed.z*planetParams.perlinScaler.z + point.z,
		                                  planetParams.PERLIN_OCTAVES);
	}

	void Update() {
		gameObject.transform.Rotate(Vector3.down * Time.deltaTime * rotationSpeed);
	}
}
