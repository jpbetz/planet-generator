// Credit to damien_oconnell from http://forum.unity3d.com/threads/39513-Click-drag-camera-movement
// for using the mouse displacement for calculating the amount of camera movement and panning code.

using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour 
{
	//
	// VARIABLES
	//
	
	public float turnSpeed = 4.0f;		// Speed of camera turning when mouse moves in along an axis
	public float zoomSpeed = 4.0f;		// Speed of the camera going back and forth
	
	private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts
	private bool isRotating;	// Is the camera being rotated?
	private bool isZooming;		// Is the camera zooming?
	private Vector3 origin = new Vector3(0,0,0);

	public float pinchZoomSpeed = 0.1f; 

	//
	// UPDATE
	//
	
	void Update () 
	{
		// Get the left mouse button
		if(Input.GetMouseButtonDown(0))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isRotating = true;
		}
		
		// Get the middle mouse button
		if(Input.GetMouseButtonDown(2))
		{
			Debug.Log ("Mouse zoom");
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isZooming = true;
		}
		
		// Disable movements on button release
		if (!Input.GetMouseButton(0)) isRotating=false;
		if (!Input.GetMouseButton(2)) isZooming=false;
		
		// Rotate camera along Y axis
		if (isRotating)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
			
			//transform.RotateAround(origin, transform.right, -pos.y * turnSpeed);
			transform.RotateAround(origin, Vector3.up, pos.x * turnSpeed);
		}
		
		// Move the camera linearly along Z axis
		if (isZooming)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
			
			//Vector3 move = pos.y * zoomSpeed * transform.forward; 
			//transform.Translate(move, Space.World);

			camera.fieldOfView += pos.y * zoomSpeed;

			camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 11f, 50f);
		}

		// Pinch to zoom.
		// ...If there are two touches on the device...
		if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			
			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			
			// Otherwise change the field of view based on the change in distance between the touches.
			float fieldOfViewChange = deltaMagnitudeDiff * pinchZoomSpeed;

			Debug.Log ("field of view change: " + fieldOfViewChange);
			camera.fieldOfView += Mathf.Clamp (fieldOfViewChange, -1f, 1f);
			
			// Clamp the field of view to make sure it's between our allowed range
			camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 11f, 50f);
		}
	}
}