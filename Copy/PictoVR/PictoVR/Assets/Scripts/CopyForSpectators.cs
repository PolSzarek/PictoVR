using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyForSpectators : MonoBehaviour
{
	private RawImage _image;
	private GameObject _drawingSupport;
	
	// Use this for initialization
	void Start ()
	{
		_image = GetComponent<RawImage>();
		_drawingSupport = GameObject.FindGameObjectWithTag("DrawingSupport");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_drawingSupport)
			_image.texture = _drawingSupport.GetComponent<Renderer>().material.mainTexture;
	}
}
