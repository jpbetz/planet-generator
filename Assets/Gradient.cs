using UnityEngine;
using System.Collections;

public class Gradient {

	public class GradientPoint {
		public GradientPoint(Color32 color, float x) {
			this.color = color;
			this.x = x;
		}

		public Color32 color;
		public float x;
	}

	public GradientPoint[] gradientPoints;

	Color UNASSIGNED = new Color(1f, 1f, 1f, 0f);

	public Color32 ColorAtX(float x, float alpha) {
		for(int i = 0; i < gradientPoints.Length-1; i++) {
			GradientPoint lower = gradientPoints[i];
			GradientPoint upper = gradientPoints[i+1];
			if(x >= lower.x && x <= upper.x) {
				Color result = Color.Lerp(upper.color, lower.color, (upper.x-x)/(upper.x-lower.x));
				result.a = alpha;
				return result;
			}
		}
		return UNASSIGNED;
	}
}
