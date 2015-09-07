using UnityEngine;
using System.Collections;

public class StartScreenLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GlobalLogic.Preferences.Clear();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartGameEasy() {
		GlobalLogic.Preferences.Add ("DIFFICULTY", "EASY");
		Application.LoadLevel("gamescene");
	}

	public void StartGameNormal() {
		GlobalLogic.Preferences.Add ("DIFFICULTY", "NORMAL");
		Application.LoadLevel("gamescene");
	}

	public void StartGameHard() {
		GlobalLogic.Preferences.Add ("DIFFICULTY", "HARD");
		Application.LoadLevel("gamescene");
	}

}
