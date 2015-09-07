using UnityEngine;
using System.Collections;

public class ClassicMode : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMouseUp() {
		Debug.Log("Loading Level");
		Application.LoadLevel("gamescene");
	}
}
