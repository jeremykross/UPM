using UnityEngine;
using System.Collections;

public class ShellSpawnLogic : MonoBehaviour
{
	public GameObject shell;
	public bool running = true;
	float startTime = 0;

	public int ComboCount {
		get;
		set;
	}

	// Use this for initialization
	void Start ()
	{
		startTime = Time.time;
		StartCoroutine (Spawn ());
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	float howLongToWait ()
	{
		if(GlobalLogic.Preferences["DIFFICULTY"].Equals("INSANE")) {
			return 0;
		}

		if (Time.time - startTime < 10) {
			switch (GlobalLogic.Preferences ["DIFFICULTY"]) {
			case "EASY":
				return 5;
			case "NORMAL": 
				return 2;
			case "HARD": 
				return 1;
			}
		} else {
			switch (GlobalLogic.Preferences ["DIFFICULTY"]) {
			case "EASY":
				return 2;
			case "NORMAL": 
				return 1;
			case "HARD": 
				return .25f;
			}
		}

		return 1;
	}

	IEnumerator Spawn ()
	{
		while (running) {
			Instantiate (shell, Camera.main.ViewportToWorldPoint (new Vector3 (0.5f + .25f * Random.value, 1.2f, 1f)), Quaternion.identity);
			yield return new WaitForSeconds (howLongToWait()); 
		}
	}
}
