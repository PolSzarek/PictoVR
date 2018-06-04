﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class ColorPicker : MonoBehaviour {

	public GameObject colorCube;
	private float hue, saturation, value = 1f;


	// Use this for initialization
	void Start () 
	{
		if (GetComponent<VRTK_ControllerEvents>() == null) {
			Debug.LogError("ColorPicker is required to be attached to a Controller that has the VRTK_ControllerEvents script attached to it");
			return;
		}

		GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged += new ControllerInteractionEventHandler(DoTouchpadAxisChanged);
	}

	private void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e) 
	{
		ChangedHueSaturation (e.touchpadAxis, e.touchpadAngle);
	}

	private void ChangedHueSaturation(Vector2 touchpadAxis, float touchpadAngle) 
	{
		float normalAngle = touchpadAngle - 90;
		if (normalAngle < 0)
			normalAngle = 360 + normalAngle;
		
		//Debug.Log ("ChangeColor: Trackpad axis at: " + touchpadAxis + " (" + normalAngle + " degrees)");

		float rads = normalAngle * Mathf.PI / 180;
		float maxX = Mathf.Cos (rads);
		float maxY = Mathf.Sin (rads);

		float currX = touchpadAxis.x;
		float currY = touchpadAxis.y;

		float percentX = Mathf.Abs (currX / maxX);
		float percentY = Mathf.Abs (currY / maxY);

		this.hue = normalAngle / 360.0f;
		this.saturation = (percentX + percentY) / 2;

		UpdateColor ();
	}

	private void UpdateColor() 
	{
		Color color = Color.HSVToRGB(this.hue, this.saturation, this.value);
		colorCube.GetComponent<Renderer>().material.color = color;
	}
}