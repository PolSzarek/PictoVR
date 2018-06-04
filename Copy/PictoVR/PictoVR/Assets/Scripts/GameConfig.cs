using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
	private int nbRound;
	private int timeRound;

	public float NbRound
	{
		get { return nbRound; }
		set { nbRound = (int) value; }
	}

	public float TimeRound
	{
		get { return timeRound; }
		set { timeRound = (int) value; }
	}
}
