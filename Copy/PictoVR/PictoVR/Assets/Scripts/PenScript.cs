using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PenScript : VRTK_InteractableObject 
{
	private RaycastHit touch;
	//private Quaternion lastAngle;
	private bool lastTouch;

	private DrawingSupport drawingSupport;
	private Renderer _renderer;

	private PictoGameManager gameManager;

	// Use this for initialization
	void Start () 
	{
		GameObject gMGameObject = GameObject.Find("GameManager");
		gameManager = gMGameObject.GetComponent<PictoGameManager>();
		
		originalPosition = transform.position;
		originalRotation = transform.rotation;
	}
	
	private VRTK_ControllerReference controllerReference;
	private Vector3 originalPosition;
	private Quaternion originalRotation;

	public override void Grabbed(VRTK_InteractGrab grabbingObject)
	{
		base.Grabbed(grabbingObject);
		controllerReference = VRTK_ControllerReference.GetControllerReference(grabbingObject.controllerEvents.gameObject);
		_renderer = transform.Find ("Tip").GetComponent<Renderer>();
		
		gameManager.PencilGrabbed();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float tipHeight = transform.Find ("Tip").transform.localScale.y;
		Vector3 tip = transform.Find ("Tip").transform.position;

		//Debug.Log (tip);

		// Check for a Raycast from the tip of the pen
		if (Physics.Raycast (tip, transform.up, out touch, tipHeight * 2)) 
		{
			if (!(touch.collider.CompareTag("DrawingSupport")))
			{
				//Debug.Log(touch.collider.tag);
				return;
			}
			//Debug.Log("Touching !!!");
			
			drawingSupport = touch.collider.GetComponent<DrawingSupport>();
			
			drawingSupport.SetColor(_renderer.material.color);
			drawingSupport.SetTouchPosition(touch.textureCoord.x, touch.textureCoord.y);
			drawingSupport.ToggleTouch(true);
			
			// Give haptic feedback when touching the whiteboard
			VRTK_ControllerHaptics.TriggerHapticPulse (controllerReference, 0.05f);
			// If we started touching, get the current angle of the pen
			if (lastTouch == false) 
			{
				lastTouch = true;
				//lastAngle = transform.rotation;
			}
			else
			{
				lastTouch = false;
				drawingSupport.ToggleTouch(false);
			}
		}
		
		
		// Lock the rotation of the pen if "touching"
		/*if (lastTouch) {
			transform.rotation = lastAngle;
		}*/
		
	}

	public void ResetPen()
	{
		ForceStopInteracting();
		transform.position = originalPosition;
		transform.rotation = originalRotation;
	}
}
