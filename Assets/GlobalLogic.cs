using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GlobalLogic : MonoBehaviour {
	public static Dictionary<string, string> Preferences = new Dictionary<string,string>();
	public GameObject scoreObject;
	protected Text scoreText;
	protected int score = 0;

	// Use this for initialization
	void Start () {
		scoreText = scoreObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IncrementScore(int amt) {
		score += amt;	
		scoreText.text = "" + score;
	}
}
