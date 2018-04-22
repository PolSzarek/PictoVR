using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOtherWindow : MonoBehaviour {

	public GameObject pen;

	private SteamVR_TrackedObject trackedObj;
	private TrailRenderer trailRenderer;

	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	// Use this for initialization
	void Start () {
		trailRenderer = pen.GetComponent<TrailRenderer>();

		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.startWidth = 0.2f;
		lineRenderer.endWidth = 0.2f;
		lineRenderer.widthMultiplier = 0.1f;

		InvokeRepeating("DrawLineInOtherWindow", 0.0f, 0.1f);
	}
	
	void DrawLineInOtherWindow() 
	{
		LineRenderer lineRenderer = GetComponent<LineRenderer>();

		Vector3[] trailPositions = new Vector3[trailRenderer.positionCount];

		int nbPositions = trailRenderer.GetPositions(trailPositions);

		Vector3[] linePositions = new Vector3[nbPositions];
		lineRenderer.positionCount = nbPositions;

		for (int i = 0; i < nbPositions; i++)
		{
			linePositions[i] = new Vector3(trailPositions[i].x, trailPositions[i].y + 4, trailPositions[i].z);
		}

		lineRenderer.SetPositions(linePositions);
	}
}

