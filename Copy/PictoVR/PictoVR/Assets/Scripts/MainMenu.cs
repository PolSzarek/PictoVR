using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public Canvas MainMenuCanvas;

	public Canvas ConfigureGameCanvas;

	private GameConfig _gameConfig;

	public GameObject GameManager;
	
	// Use this for initialization
	void Start ()
	{
		_gameConfig = GetComponent<GameConfig>();
	}
	
	public void StartConfiguringGame()
	{
		MainMenuCanvas.enabled = false;
		MainMenuCanvas.gameObject.SetActive(false);
		ConfigureGameCanvas.enabled = true;
		ConfigureGameCanvas.gameObject.SetActive(true);
	}
	
	public void QuitGame()
	{
		Application.Quit();
	}
	
	public void LaunchTutorial()
	{
		SceneManager.LoadScene("BasicLevel");
	}

	public void StartGame()
	{
		Debug.Log(_gameConfig.NbRound);
		Debug.Log(_gameConfig.TimeRound);

		int index = Random.Range(1, 2);
		Debug.Log("Loading scene " + index);

		GameManager.GetComponent<PictoGameManager>().nbRounds = (int) _gameConfig.NbRound;
		GameManager.GetComponent<PictoGameManager>().timeRounds = (_gameConfig.TimeRound * 10);
		MoveObjectToNewScene.LoadScene(index, GameManager);
	}
}
