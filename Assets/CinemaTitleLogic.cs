using UnityEngine;
using System.Collections;

public class CinemaTitleLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartCinema() {
		Application.LoadLevel("the_encounter");
	}
}
