using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
	private TextMeshProUGUI resultText;
	private GameObject _canvasGameEnd;
	private TextMeshProUGUI finalScoreText;
	private Button quitButton;

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
			currentTime -= Time.deltaTime;
			timerText.text = String.Format("{0:0}",currentTime);
			if (currentTime < 0)
			{
				timerStarted = false;
				timerText.text = 0 + "";
				if (isTeamAPlaying)
					resultText.text = "You found " + scoreTeamA / WORD_SCORE + " words";
				else
				{
					resultText.text = "You found " + scoreTeamB / WORD_SCORE + " words";
					currentRound++;
				}
				_roundCanvasEnd.gameObject.SetActive(true);
				_roundCanvasEnd.SetActive(true);
				_roundCanvasEnd.GetComponent<Canvas>().enabled = true;
				_roundCanvasEnd.GetComponent<Canvas>().gameObject.SetActive(true);
				if (currentRound >= nbRounds)
				{
					nextRoundButton.GetComponentInChildren<TextMeshProUGUI>().text = "End";
					nextRoundButton.onClick.RemoveListener(StartRound);
					nextRoundButton.onClick.AddListener(EndGame);

				}
			}
		}

		if (pencilGrabbed)
			StartLevel();
	}

	private void EndGame()
	{
		_roundCanvasEnd.gameObject.SetActive(false);
		_roundCanvasEnd.SetActive(false);
		_roundCanvasEnd.GetComponent<Canvas>().enabled = false;
		_roundCanvasEnd.GetComponent<Canvas>().gameObject.SetActive(false);
		
		if (scoreTeamA > scoreTeamB)
			finalScoreText.text = "Team RED Won!";
		else if (scoreTeamB > scoreTeamA)
		{
			finalScoreText.text = "Team BLUE Won!";
		}
		else
		{
			finalScoreText.text = "Draw, Nobody Won...";

		}
		_canvasGameEnd.SetActive(true);
	}

	void StartLevel()
	{
		Debug.Log("----------------- START LEVEL");
		_roundCanvasInit.gameObject.SetActive(false);
		
		timerStarted = true;
		currentTime = timeRounds;
		Debug.Log("-----------Current time " + currentTime);
		pencilGrabbed = false;

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
		pencilGrabbed = false;

		_roundCanvasInit.gameObject.SetActive(true);

		_roundCanvasEnd.gameObject.SetActive(false);
		_roundCanvasEnd.SetActive(false);
		_roundCanvasEnd.GetComponent<Canvas>().enabled = false;
		_roundCanvasEnd.GetComponent<Canvas>().gameObject.SetActive(false);
		
		
		_drawingSupport.GetComponent<DrawingSupport>().Clear();
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
		_canvasGameEnd = GameObject.Find("SpectatorsCanvasEndGame");

		
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
		
		GameObject resultTextGO = GameObject.Find("ResultText");
		resultText = resultTextGO.GetComponent<TextMeshProUGUI>();
		
		GameObject finalScoreTextGO = GameObject.Find("FinalScoreText");
		finalScoreText = finalScoreTextGO.GetComponent<TextMeshProUGUI>();
		
		pen = GameObject.Find("Pen");
	
		GameObject quitButtonGO = GameObject.Find("QuitButton");
		quitButton = quitButtonGO.GetComponent<Button>();
		
		quitButton.onClick.AddListener(QuitGame);
		
		_roundCanvasEnd.SetActive(false);
		_canvasGameEnd.SetActive(false);
		
		
	
		StartRound();
		// pen grabbed = start timer

		// show "N round begin : team Red/Blue go!"
		// count score

	}

	private void QuitGame()
	{
		Application.Quit();
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
