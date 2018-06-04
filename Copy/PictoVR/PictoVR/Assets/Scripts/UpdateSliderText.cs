using TMPro;
using UnityEngine;

public class UpdateSliderText : MonoBehaviour
{
	private TextMeshProUGUI text;
	
	// Use this for initialization
	void Start ()
	{
		text = GetComponent<TextMeshProUGUI>();
	}
	
	public void UpdateTimeValue (float value)
	{
		text.text = "" + value * 10 + "s";
	}
	
	public void UpdateNbValue (float value)
	{
		text.text = "" + value;
	}
}
