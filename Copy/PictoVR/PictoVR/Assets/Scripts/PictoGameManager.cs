using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PictoGameManager : MonoBehaviour
{

	public int nbRounds;
	public float timeRounds;

	private int currentRound = 0;
	private bool isTeamAPlaying = true;
	private int scoreTeamA = 0;
	private int scoreTeamB = 0;

	private bool timerStarted = false;
	private float currentTime = 0f;
	private TextMeshProUGUI timerText;

	private Button validateButton;

	private GameObject _spectatorsCanvas;
	private GameObject _roundCanvasInit;
	private TextMeshProUGUI teamText;
	private bool pencilGrabbed;

	private const int WORD_SCORE = 10;

	private GameObject _drawingSupport;
	private GameObject _roundCanvasEnd;

	private Button nextRoundButton;
	private GameObject pen;

	// Use this for initialization
	void Start () 
	{
		Debug.Log("GAME MANAGER START");
	}

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);

		Debug.Log("GAME MANAGER AWAKE");
	}

	// Update is called once per frame
	void Update () 
	{
		if (timerStarted)
		{
			currentTime -= Time.time;
			timerText.text = String.Format("{0:0}",currentTime);
			if (currentTime < 0)
			{
				timerStarted = false;
				timerText.text = 0 + "";
				_roundCanvasEnd.SetActive(true);
			}
		}

		if (pencilGrabbed)
			StartLevel();
	}
	
	void StartLevel()
	{
		_roundCanvasInit.gameObject.SetActive(false);
		
		timerStarted = true;
		currentTime = timeRounds;
		// select random world & show on HTC screen
	}
	
	void StartRound()
	{
		Debug.Log("----------------- START ROUND");
		_roundCanvasInit.gameObject.SetActive(true);

		if (isTeamAPlaying)
			teamText.text = "Team RED Go!";
		else
		{
			teamText.text = "Team BLUE Go!";
		}

		pencilGrabbed = false;

	}

	void StartNewRound()
	{
		Debug.Log("----------------- START NEW ROUND");

		isTeamAPlaying = !isTeamAPlaying;
		
		_roundCanvasEnd.gameObject.SetActive(false);
		_roundCanvasInit.gameObject.SetActive(true);
		_drawingSupport.GetComponent<DrawingSupport>().Clear();
		pencilGrabbed = false;
		pen.GetComponent<PenScript>().ResetPen();

		
		if (isTeamAPlaying)
			teamText.text = "Team RED Go!";
		else
		{
			teamText.text = "Team BLUE Go!";
		}
	}
	
	public void OnNewScene()
	{
		Debug.Log("----------------- GAME MANAGER NEW SCENE");

		// Get timer text
		_spectatorsCanvas = GameObject.Find("SpectatorsCanvas");
		_roundCanvasInit = GameObject.Find("SpectatorsCanvasRoundInit");
		_roundCanvasEnd = GameObject.Find("SpectatorsCanvasRoundEnd");

		
		GameObject timerTextGO = GameObject.Find("TimerText");
		timerText = timerTextGO.GetComponent<TextMeshProUGUI>();
		
		// set onclick du validate button
		GameObject validateButtonGO = GameObject.Find("ValidateButton");
		validateButton = validateButtonGO.GetComponent<Button>();
		
		validateButton.onClick.AddListener(ValidateWord);

		
		GameObject teamTextGO = GameObject.Find("TeamText");
		teamText = teamTextGO.GetComponent<TextMeshProUGUI>();

		
		_drawingSupport = GameObject.FindGameObjectWithTag("DrawingSupport");
		
		
			
		GameObject nextRoundButtonGO = GameObject.Find("NextRoundButton");
		nextRoundButton = nextRoundButtonGO.GetComponent<Button>();
		
		nextRoundButton.onClick.AddListener(StartNewRound);
		
		pen = GameObject.Find("Pen");
			
		_roundCanvasEnd.SetActive(false);

		StartRound();
		// pen grabbed = start timer

		// show "N round begin : team Red/Blue go!"
		// count score

	}
	
	void ValidateWord()
	{
		Debug.Log("Validate WORD");
		if (isTeamAPlaying)
			scoreTeamA += WORD_SCORE;
		else
		{
			scoreTeamB += WORD_SCORE;
		}

		_drawingSupport.GetComponent<DrawingSupport>().Clear();
	}
	
	public void PencilGrabbed()
	{
		pencilGrabbed = true;
	}
}
